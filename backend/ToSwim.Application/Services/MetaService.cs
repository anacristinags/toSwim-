using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Meta;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class MetaService : IMetaService
{
    private readonly IMetaRepository _metaRepository;
    private readonly ISerieFichaRepository _serieFichaRepository;
    private readonly IFichaBaseRepository _fichaBaseRepository; 

    public MetaService(
        IMetaRepository metaRepository,
        ISerieFichaRepository serieFichaRepository,
        IFichaBaseRepository fichaBaseRepository)
    {
        _metaRepository = metaRepository;
        _serieFichaRepository = serieFichaRepository;
        _fichaBaseRepository = fichaBaseRepository;
    }

    public async Task<MetaResponseDto> ObterPorIdAsync(int codMeta, int codUsuario)
    {
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta nùo encontrada ou nùo pertence a este usuùrio.", 404);

        return MapearParaDto(meta);
    }

    public async Task<IEnumerable<MetaResponseDto>> ObterTodasPorUsuarioAsync(int codUsuario, StatusMeta? status)
    {
        var metas = await _metaRepository.ObterTodasPorUsuarioAsync(codUsuario, status);
        return metas.Select(MapearParaDto);
    }

    public async Task<MetaResponseDto> CriarMetaAsync(int codUsuario, MetaRequestDto dto)
    {
        // 1. Validar se a serie_ficha existe
        var serie = await _serieFichaRepository.BuscarPorIdAsync(dto.CodSerieFicha);
        if (serie == null)
            throw new AppException("A sùrie de ficha informada nùo existe.", 404);

        var fichaDona = await _fichaBaseRepository.BuscarPorIdAsync(serie.CodFicha, codUsuario);
        if (fichaDona == null)
            throw new AppException("A sùrie de ficha informada nùo pertence ao usuùrio autenticado.", 403);

        var existeMetaAtiva = await _metaRepository.ExisteMetaAtivaParaSerieAsync(codUsuario, dto.CodSerieFicha);
        if (existeMetaAtiva)
            throw new AppException("Jù existe uma meta ativa associada a esta sùrie de treino.", 400);

        if (dto.DistanciaAlvoM <= 0)
            throw new AppException("A distùncia alvo deve ser maior que zero.", 400);
        if (dto.TempoAlvoSeg <= 0)
            throw new AppException("O tempo alvo deve ser maior que zero.", 400);

        decimal paceCalculado = CalcularPace(dto.DistanciaAlvoM, dto.TempoAlvoSeg);

        var novaMeta = new Meta
        {
            CodUsuario = codUsuario,
            CodSerieFicha = dto.CodSerieFicha,
            TituloMeta = dto.TituloMeta,
            TipoNado = dto.TipoNado,
            DistanciaAlvoM = dto.DistanciaAlvoM,
            TempoAlvoSeg = dto.TempoAlvoSeg,
            PaceAlvoSeg = paceCalculado,
            ModoAvaliacao = dto.ModoAvaliacao,
            Status = StatusMeta.Ativa,
            DataInicio = dto.DataInicio.ToUniversalTime(),
            DataFimPrevista = dto.DataFimPrevista?.ToUniversalTime(),
            Observacoes = dto.Observacoes
        };

        await _metaRepository.AdicionarAsync(novaMeta);
        await _metaRepository.SalvarAlteracoesAsync();

        return MapearParaDto(novaMeta);
    }

    public async Task<MetaResponseDto> AtualizarMetaAsync(int codMeta, int codUsuario, MetaRequestDto dto)
    {
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta nùo encontrada.", 404);

        if (meta.Status != StatusMeta.Ativa)
            throw new AppException("Apenas metas ativas podem ser editadas.", 400);

        if (dto.DistanciaAlvoM <= 0 || dto.TempoAlvoSeg <= 0)
            throw new AppException("Distùncia e tempo alvo devem ser maiores que zero.", 400);

        // Atualizar dados e recalcular o Pace Alvo
        meta.TituloMeta = dto.TituloMeta;
        meta.TipoNado = dto.TipoNado;
        meta.DistanciaAlvoM = dto.DistanciaAlvoM;
        meta.TempoAlvoSeg = dto.TempoAlvoSeg;
        meta.PaceAlvoSeg = CalcularPace(dto.DistanciaAlvoM, dto.TempoAlvoSeg);
        meta.ModoAvaliacao = dto.ModoAvaliacao;
        meta.DataInicio = dto.DataInicio.ToUniversalTime();
        meta.DataFimPrevista = dto.DataFimPrevista?.ToUniversalTime();
        meta.Observacoes = dto.Observacoes;

        _metaRepository.Atualizar(meta);
        await _metaRepository.SalvarAlteracoesAsync();

        return MapearParaDto(meta);
    }

    public async Task<MetaResponseDto> AtualizarStatusMetaAsync(int codMeta, int codUsuario, StatusMeta novoStatus)
    {
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta nùo encontrada.", 404);

        meta.Status = novoStatus;

        _metaRepository.Atualizar(meta);
        await _metaRepository.SalvarAlteracoesAsync();

        return MapearParaDto(meta);
    }

    public async Task RemoverMetaAsync(int codMeta, int codUsuario)
    {
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta nùo encontrada.", 404);

        _metaRepository.Remover(meta);
        await _metaRepository.SalvarAlteracoesAsync();
    }

    private static decimal CalcularPace(int distanciaMetros, decimal tempoSegundos)
    {
        if (distanciaMetros == 0) return 0m;
        return Math.Round(tempoSegundos * 100m / distanciaMetros, 2);
    }

    private static MetaResponseDto MapearParaDto(Meta meta)
    {
        return new MetaResponseDto
        {
            CodMeta = meta.CodMeta,
            CodUsuario = meta.CodUsuario,
            CodSerieFicha = meta.CodSerieFicha,
            TituloMeta = meta.TituloMeta,
            TipoNado = meta.TipoNado,
            DistanciaAlvoM = meta.DistanciaAlvoM,
            TempoAlvoSeg = meta.TempoAlvoSeg,
            PaceAlvoSeg = meta.PaceAlvoSeg,
            ModoAvaliacao = meta.ModoAvaliacao,
            Status = meta.Status,
            DataInicio = meta.DataInicio,
            DataFimPrevista = meta.DataFimPrevista,
            Observacoes = meta.Observacoes,
            CreatedAt = meta.CreatedAt,
            UpdatedAt = meta.UpdatedAt
        };
    }
}