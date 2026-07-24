using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Repeticao;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class RepeticaoSerieTreinoService : IRepeticaoSerieTreinoService
{
    private readonly IRepeticaoSerieTreinoRepository _repeticaoRepository;
    private readonly ISerieTreinoRepository _serieTreinoRepository;

    public RepeticaoSerieTreinoService(
        IRepeticaoSerieTreinoRepository repeticaoRepository,
        ISerieTreinoRepository serieTreinoRepository)
    {
        _repeticaoRepository = repeticaoRepository;
        _serieTreinoRepository = serieTreinoRepository;
    }

    public async Task<RepeticaoResponseDto> RegistrarRepeticaoAsync(int codTreino, int codSerieTreino, int codUsuario, RepeticaoRequestDto dto)
    {
        // 1. Validar se a série de treino existe e pertence ao usuário
        var serie = await _serieTreinoRepository.ObterPorIdAsync(codSerieTreino, codUsuario);
        if (serie == null || serie.CodTreino != codTreino)
            throw new AppException("Série de treino não encontrada para este treino.", 404);

        // 2. Garantir que o treino esteja em andamento para aceitar edições/registros
        if (serie.Treino!.Status != StatusTreino.Andamento)
            throw new AppException("Não é possível registrar repetições para um treino finalizado ou cancelado.", 400);

        // 3. Validar se o número da repetição já foi lançado para esta série
        var numeroJaExiste = await _repeticaoRepository.ExisteNumeroRepeticaoNaSerieAsync(codSerieTreino, dto.NumeroRepeticao);
        if (numeroJaExiste)
            throw new AppException($"A repetição número {dto.NumeroRepeticao} já foi lançada para esta série.", 400);

        if (dto.DistanciaRealM <= 0 || dto.DuracaoSeg <= 0)
            throw new AppException("A distância e duração devem ser maiores que zero.", 400);

        // 4. Instanciar e calcular o Pace (segundos por 100 metros)
        var novaRepeticao = new RepeticaoSerieTreino
        {
            CodSerieTreino = codSerieTreino,
            NumeroRepeticao = dto.NumeroRepeticao,
            DistanciaRealM = dto.DistanciaRealM,
            DuracaoSeg = dto.DuracaoSeg,
            PaceSeg = (int)Math.Round((double)dto.DuracaoSeg * 100 / dto.DistanciaRealM)
        };

        await _repeticaoRepository.AdicionarAsync(novaRepeticao);
        await _repeticaoRepository.SalvarAlteracoesAsync();

        return MapearParaDto(novaRepeticao);
    }

    public async Task<IEnumerable<RepeticaoResponseDto>> ListarRepeticoesDaSerieAsync(int codTreino, int codSerieTreino, int codUsuario)
    {
        var serie = await _serieTreinoRepository.ObterPorIdAsync(codSerieTreino, codUsuario);
        if (serie == null || serie.CodTreino != codTreino)
            throw new AppException("Série de treino não encontrada para este treino.", 404);

        var repeticoes = await _repeticaoRepository.ObterPorSerieTreinoAsync(codSerieTreino, codUsuario);
        return repeticoes.Select(MapearParaDto);
    }

    public async Task<RepeticaoResponseDto> ObterPorIdAsync(int codTreino, int codSerieTreino, int codRepeticao, int codUsuario)
    {
        var repeticao = await _repeticaoRepository.ObterPorIdAsync(codRepeticao, codUsuario);
        if (repeticao == null || repeticao.CodSerieTreino != codSerieTreino || repeticao.SerieTreino!.CodTreino != codTreino)
            throw new AppException("Repetição não encontrada.", 404);

        return MapearParaDto(repeticao);
    }

    public async Task<RepeticaoResponseDto> AtualizarRepeticaoAsync(int codTreino, int codSerieTreino, int codRepeticao, int codUsuario, RepeticaoRequestDto dto)
    {
        var repeticao = await _repeticaoRepository.ObterPorIdAsync(codRepeticao, codUsuario);
        if (repeticao == null || repeticao.CodSerieTreino != codSerieTreino || repeticao.SerieTreino!.CodTreino != codTreino)
            throw new AppException("Repetição não encontrada.", 404);

        if (repeticao.SerieTreino!.Treino!.Status != StatusTreino.Andamento)
            throw new AppException("Não é possível atualizar dados de um treino finalizado ou cancelado.", 400);

        if (repeticao.NumeroRepeticao != dto.NumeroRepeticao)
        {
            var numeroJaExiste = await _repeticaoRepository.ExisteNumeroRepeticaoNaSerieAsync(codSerieTreino, dto.NumeroRepeticao);
            if (numeroJaExiste)
                throw new AppException($"Já existe um registro para a repetição número {dto.NumeroRepeticao}.", 400);
        }

        if (dto.DistanciaRealM <= 0 || dto.DuracaoSeg <= 0)
            throw new AppException("A distância e duração devem ser maiores que zero.", 400);

        repeticao.NumeroRepeticao = dto.NumeroRepeticao;
        repeticao.DistanciaRealM = dto.DistanciaRealM;
        repeticao.DuracaoSeg = dto.DuracaoSeg;
        repeticao.PaceSeg = (int)Math.Round((double)dto.DuracaoSeg * 100 / dto.DistanciaRealM);

        _repeticaoRepository.Atualizar(repeticao);
        await _repeticaoRepository.SalvarAlteracoesAsync();

        return MapearParaDto(repeticao);
    }

    public async Task RemoverRepeticaoAsync(int codTreino, int codSerieTreino, int codRepeticao, int codUsuario)
    {
        var repeticao = await _repeticaoRepository.ObterPorIdAsync(codRepeticao, codUsuario);
        if (repeticao == null || repeticao.CodSerieTreino != codSerieTreino || repeticao.SerieTreino!.CodTreino != codTreino)
            throw new AppException("Repetição não encontrada.", 404);

        if (repeticao.SerieTreino!.Treino!.Status != StatusTreino.Andamento)
            throw new AppException("Não é possível excluir dados de um treino finalizado ou cancelado.", 400);

        _repeticaoRepository.Remover(repeticao);
        await _repeticaoRepository.SalvarAlteracoesAsync();
    }

    private static RepeticaoResponseDto MapearParaDto(RepeticaoSerieTreino r)
    {
        return new RepeticaoResponseDto
        {
            CodRepeticaoSerieTreino = r.CodRepeticaoSerieTreino,
            CodSerieTreino = r.CodSerieTreino,
            NumeroRepeticao = r.NumeroRepeticao,
            DistanciaRealM = r.DistanciaRealM,
            DuracaoSeg = r.DuracaoSeg,
            PaceSeg = r.PaceSeg,
            CreatedAt = r.CreatedAt
        };
    }
}