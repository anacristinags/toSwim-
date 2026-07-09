using ToSwim.Application.DTOs.FichaBase;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class SerieFichaService : ISerieFichaService
{
    private readonly ISerieFichaRepository _serieRepository;
    private readonly IFichaBaseRepository _fichaRepository;

    public SerieFichaService(ISerieFichaRepository serieRepository, IFichaBaseRepository fichaRepository)
    {
        _serieRepository = serieRepository;
        _fichaRepository = fichaRepository;
    }

    public async Task<IEnumerable<SerieFichaResponseDto>> ListarSeriesPorFichaAsync(int codFicha, int codUsuario)
    {
        var ficha = await _fichaRepository.BuscarPorIdAsync(codFicha, codUsuario);
        if (ficha is null)
            throw new AppException("Ficha não encontrada.", 404);

        return ficha.Series.Select(s => MapearParaDto(s));
    }

    public async Task<SerieFichaResponseDto> AdicionarSerieAsync(int codFicha, int codUsuario, SerieFichaRequestDto dto)
    {
        var ficha = await _fichaRepository.BuscarPorIdAsync(codFicha, codUsuario);
        if (ficha is null)
            throw new AppException("Ficha não encontrada ou não pertence a você.", 404);

        if (await _serieRepository.ExisteOrdemNaFichaAsync(codFicha, dto.Ordem))
            throw new AppException($"Já existe uma série na posição {dto.Ordem} nesta ficha.", 400);

        var serie = new SerieFicha
        {
            CodFicha = codFicha,
            Ordem = dto.Ordem,
            TipoNado = (TipoNado)dto.TipoNado,
            QuantidadeRepeticoes = dto.QuantidadeRepeticoes,
            DistanciaM = dto.DistanciaM,
            TempoPausaSeg = dto.TempoPausaSeg,
            IsGoalSeries = dto.IsGoalSeries,
            Observacoes = dto.Observacoes
        };

        var criada = await _serieRepository.CriarAsync(serie);
        return MapearParaDto(criada);
    }

    public async Task<SerieFichaResponseDto> AtualizarSerieAsync(int codSerieFicha, int codUsuario, SerieFichaRequestDto dto)
    {
        var serie = await _serieRepository.BuscarPorIdAsync(codSerieFicha);
        if (serie is null || serie.Ficha?.CodUsuario != codUsuario)
            throw new AppException("Série não encontrada.", 404);

        // Se mudou de ordem, valida se a nova posição está livre
        if (serie.Ordem != dto.Ordem && await _serieRepository.ExisteOrdemNaFichaAsync(serie.CodFicha, dto.Ordem))
            throw new AppException($"Já existe uma série na posição {dto.Ordem} nesta ficha.", 400);

        serie.Ordem = dto.Ordem;
        serie.TipoNado = (TipoNado)dto.TipoNado;
        serie.QuantidadeRepeticoes = dto.QuantidadeRepeticoes;
        serie.DistanciaM = dto.DistanciaM;
        serie.TempoPausaSeg = dto.TempoPausaSeg;
        serie.IsGoalSeries = dto.IsGoalSeries;
        serie.Observacoes = dto.Observacoes;

        var atualizada = await _serieRepository.AtualizarAsync(serie);
        return MapearParaDto(atualizada);
    }

    public async Task ExcluirSerieAsync(int codSerieFicha, int codUsuario)
    {
        var serie = await _serieRepository.BuscarPorIdAsync(codSerieFicha);
        if (serie is null || serie.Ficha?.CodUsuario != codUsuario)
            throw new AppException("Série não encontrada.", 404);

        int codFicha = serie.CodFicha;
        await _serieRepository.ExcluirAsync(serie);

        // Após excluir, reorganiza as ordens restantes para evitar "buracos" (ex: série 1, 3, 4 vira 1, 2, 3)
        await _serieRepository.ReordenarSeriesFichaAsync(codFicha);
    }

    private static SerieFichaResponseDto MapearParaDto(SerieFicha s) =>
        new()
        {
            CodSerieFicha = s.CodSerieFicha,
            CodFicha = s.CodFicha,
            Ordem = s.Ordem,
            TipoNado = (short)s.TipoNado,
            QuantidadeRepeticoes = s.QuantidadeRepeticoes,
            DistanciaM = s.DistanciaM,
            TempoPausaSeg = s.TempoPausaSeg,
            IsGoalSeries = s.IsGoalSeries,
            Observacoes = s.Observacoes,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
}