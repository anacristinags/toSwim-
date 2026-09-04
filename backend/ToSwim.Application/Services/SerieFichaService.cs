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
            throw new AppException("Ficha nao encontrada.", 404);

        return ficha.Series.OrderBy(s => s.Ordem).Select(MapearParaDto);
    }

    public async Task<SerieFichaResponseDto> AdicionarSerieAsync(int codFicha, int codUsuario, SerieFichaRequestDto dto)
    {
        var ficha = await _fichaRepository.BuscarPorIdAsync(codFicha, codUsuario);
        if (ficha is null)
            throw new AppException("Ficha nao encontrada ou nao pertence a voce.", 404);

        var proximaOrdem = await _serieRepository.ObterProximaOrdemAsync(codFicha);
        var ordem = dto.Ordem ?? proximaOrdem;

        if (ordem > proximaOrdem)
            ordem = proximaOrdem;

        if (ordem < proximaOrdem)
            await _serieRepository.DeslocarOrdensAPartirAsync(codFicha, ordem);

        var serie = new SerieFicha
        {
            CodFicha = codFicha,
            Ordem = ordem,
            TipoNado = (TipoNado)dto.TipoNado,
            QuantidadeRepeticoes = dto.QuantidadeRepeticoes,
            DistanciaM = dto.DistanciaM,
            TempoPausaSeg = dto.TempoPausaSeg,
            IsGoalSeries = dto.IsGoalSeries,
            IgnorarNoPace = dto.IgnorarNoPace,
            Observacoes = dto.Observacoes
        };

        var criada = await _serieRepository.CriarAsync(serie);
        return MapearParaDto(criada);
    }

    public async Task<SerieFichaResponseDto> DuplicarSerieAsync(int codSerieFicha, int codUsuario)
    {
        var serie = await _serieRepository.BuscarPorIdAsync(codSerieFicha);
        if (serie is null || serie.Ficha?.CodUsuario != codUsuario)
            throw new AppException("Serie nao encontrada.", 404);

        var novaOrdem = (short)(serie.Ordem + 1);
        await _serieRepository.DeslocarOrdensAPartirAsync(serie.CodFicha, novaOrdem);

        var copia = new SerieFicha
        {
            CodFicha = serie.CodFicha,
            Ordem = novaOrdem,
            TipoNado = serie.TipoNado,
            QuantidadeRepeticoes = serie.QuantidadeRepeticoes,
            DistanciaM = serie.DistanciaM,
            TempoPausaSeg = serie.TempoPausaSeg,
            IsGoalSeries = serie.IsGoalSeries,
            IgnorarNoPace = serie.IgnorarNoPace,
            Observacoes = serie.Observacoes
        };

        var criada = await _serieRepository.CriarAsync(copia);
        return MapearParaDto(criada);
    }

    public async Task<SerieFichaResponseDto> AtualizarSerieAsync(int codSerieFicha, int codUsuario, SerieFichaRequestDto dto)
    {
        var serie = await _serieRepository.BuscarPorIdAsync(codSerieFicha);
        if (serie is null || serie.Ficha?.CodUsuario != codUsuario)
            throw new AppException("Serie nao encontrada.", 404);

        if (dto.Ordem is short novaOrdem && serie.Ordem != novaOrdem)
        {
            if (await _serieRepository.ExisteOrdemNaFichaAsync(serie.CodFicha, novaOrdem))
                throw new AppException($"Ja existe uma serie na posicao {novaOrdem} nesta ficha.", 400);

            serie.Ordem = novaOrdem;
        }

        serie.TipoNado = (TipoNado)dto.TipoNado;
        serie.QuantidadeRepeticoes = dto.QuantidadeRepeticoes;
        serie.DistanciaM = dto.DistanciaM;
        serie.TempoPausaSeg = dto.TempoPausaSeg;
        serie.IsGoalSeries = dto.IsGoalSeries;
        serie.IgnorarNoPace = dto.IgnorarNoPace;
        serie.Observacoes = dto.Observacoes;

        var atualizada = await _serieRepository.AtualizarAsync(serie);
        return MapearParaDto(atualizada);
    }

    public async Task ExcluirSerieAsync(int codSerieFicha, int codUsuario)
    {
        var serie = await _serieRepository.BuscarPorIdAsync(codSerieFicha);
        if (serie is null || serie.Ficha?.CodUsuario != codUsuario)
            throw new AppException("Serie nao encontrada.", 404);

        int codFicha = serie.CodFicha;
        await _serieRepository.ExcluirAsync(serie);
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
            IgnorarNoPace = s.IgnorarNoPace,
            Observacoes = s.Observacoes,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
}
