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
    private readonly ISerieFichaRepository _serieFichaRepository; // Necessário para validar se a série existe

    public MetaService(IMetaRepository metaRepository, ISerieFichaRepository serieFichaRepository)
    {
        _metaRepository = metaRepository;
        _serieFichaRepository = serieFichaRepository;
    }

    public async Task<MetaResponseDto> ObterPorIdAsync(int codMeta, int codUsuario)
    {
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta não encontrada ou não pertence a este usuário.", 404);

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
            throw new AppException("A série de ficha informada não existe.", 404);

        // 2. Validar se já existe uma meta ativa vinculada a essa série
        var existeMetaAtiva = await _metaRepository.ExisteMetaAtivaParaSerieAsync(codUsuario, dto.CodSerieFicha);
        if (existeMetaAtiva)
            throw new AppException("Já existe uma meta ativa associada a esta série de treino.", 400);

        // 3. Validar valores do alvo
        if (dto.DistanciaAlvoM <= 0)
            throw new AppException("A distância alvo deve ser maior que zero.", 400);
        if (dto.TempoAlvoSeg <= 0)
            throw new AppException("O tempo alvo deve ser maior que zero.", 400);

        // 4. Calcular o Pace Alvo (segundos por 100m)
        int paceCalculado = CalcularPace(dto.DistanciaAlvoM, dto.TempoAlvoSeg);

        // 5. Instanciar a Meta garantindo datas em UTC
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
            throw new AppException("Meta não encontrada.", 404);

        if (meta.Status != StatusMeta.Ativa)
            throw new AppException("Apenas metas ativas podem ser editadas.", 400);

        if (dto.DistanciaAlvoM <= 0 || dto.TempoAlvoSeg <= 0)
            throw new AppException("Distância e tempo alvo devem ser maiores que zero.", 400);

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
            throw new AppException("Meta não encontrada.", 404);

        meta.Status = novoStatus;

        _metaRepository.Atualizar(meta);
        await _metaRepository.SalvarAlteracoesAsync();

        return MapearParaDto(meta);
    }

    public async Task RemoverMetaAsync(int codMeta, int codUsuario)
    {
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta não encontrada.", 404);

        // Regra do banco de dados/migrations: FKs com RESTRICT ou CASCADE. 
        // Permite remoção física direta, mas caso haja vínculos ativos com o histórico que causem restrição de banco, o EF retornará exceção de integridade.
        _metaRepository.Remover(meta);
        await _metaRepository.SalvarAlteracoesAsync();
    }

    // Cálculo matemático do Pace do Nadador baseado em segundos por 100 metros
    private static int CalcularPace(int distanciaMetros, int tempoSegundos)
    {
        if (distanciaMetros == 0) return 0;
        return (int)Math.Round((double)tempoSegundos * 100 / distanciaMetros);
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