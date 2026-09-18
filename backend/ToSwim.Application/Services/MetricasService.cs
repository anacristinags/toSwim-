using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Application.DTOs.Metricas;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Data;

namespace ToSwim.Application.Services;

public class MetricasService : IMetricasService
{
    private readonly ToSwimDbContext _context;

    public MetricasService(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResumoDto> ObterResumoDashboardAsync(int codUsuario)
    {
        var treinosConcluidos = await _context.Treinos
            .AsNoTracking()
            .Where(t => t.CodUsuario == codUsuario && t.Status == StatusTreino.Concluido)
            .ToListAsync();

        int totalTreinos = treinosConcluidos.Count;
        int totalMetros = treinosConcluidos.Sum(t => t.DistanciaTotalM);
        decimal totalTempo = treinosConcluidos.Sum(t => t.DuracaoTotalSeg);

        // O pace geral exclui series marcadas para nao participar do calculo do pace medio
        var seriesParaPace = await _context.SeriesTreino
            .AsNoTracking()
            .Where(s => s.Treino!.CodUsuario == codUsuario
                        && s.Treino.Status == StatusTreino.Concluido
                        && !s.IgnorarNoPace
                        && s.DistanciaTotalM.HasValue
                        && s.TempoTotalSeg.HasValue)
            .ToListAsync();

        int metrosParaPace = seriesParaPace.Sum(s => s.DistanciaTotalM ?? 0);
        decimal tempoParaPace = seriesParaPace.Sum(s => s.TempoTotalSeg ?? 0m);
        decimal? paceGeral = (metrosParaPace > 0) ? Math.Round(tempoParaPace * 100m / metrosParaPace, 2) : null;

        int metasAtivas = await _context.Metas.CountAsync(m => m.CodUsuario == codUsuario && m.Status == StatusMeta.Ativa);
        int metasConcluidas = await _context.Metas.CountAsync(m => m.CodUsuario == codUsuario && m.Status == StatusMeta.Concluida);

        return new DashboardResumoDto
        {
            TotalTreinos = totalTreinos,
            TotalMetrosNadados = totalMetros,
            TotalTempoSegundos = totalTempo,
            PaceMedioGeralSeg = paceGeral,
            MetasAtivasContagem = metasAtivas,
            MetasConcluidasContagem = metasConcluidas
        };
    }

    public async Task<IEnumerable<EvolucaoPaceDto>> ObterEvolucaoPaceAsync(int codUsuario, TipoNado? nado)
    {
        var query = _context.SeriesTreino
            .AsNoTracking()
            .Include(s => s.Treino)
            .Where(s => s.Treino!.CodUsuario == codUsuario
                        && s.Treino.Status == StatusTreino.Concluido
                        && !s.IgnorarNoPace
                        && s.DistanciaTotalM.HasValue
                        && s.TempoTotalSeg.HasValue
                        && s.DistanciaTotalM > 0);

        if (nado.HasValue)
        {
            query = query.Where(s => s.TipoNado == nado.Value);
        }

        var series = await query
            .OrderBy(s => s.Treino!.DataTreino)
            .ToListAsync();

        return series.Select(s => new EvolucaoPaceDto
        {
            Data = s.Treino!.DataTreino,
            TituloTreino = s.Treino.TituloTreino,
            TipoNado = s.TipoNado,
            DistanciaM = s.DistanciaTotalM!.Value,
            PaceSeg = s.PaceMedioSeg ?? CalcularPace(s.DistanciaTotalM.Value, s.TempoTotalSeg!.Value)
        });
    }

    public async Task<IEnumerable<RecordePessoalDto>> ObterMelhoresTemposAsync(int codUsuario, TipoNado? nado, int? distancia)
    {
        var query = _context.SeriesTreino
            .AsNoTracking()
            .Include(s => s.Treino)
            .Where(s => s.Treino!.CodUsuario == codUsuario
                        && s.Treino.Status == StatusTreino.Concluido
                        && s.DistanciaTotalM.HasValue
                        && s.TempoTotalSeg.HasValue
                        && s.DistanciaTotalM > 0);

        if (nado.HasValue)
            query = query.Where(s => s.TipoNado == nado.Value);

        if (distancia.HasValue)
            query = query.Where(s => s.DistanciaTotalM == distancia.Value);

        var series = await query.ToListAsync();

        var recordes = series
            .GroupBy(s => new { s.TipoNado, s.DistanciaTotalM })
            .Select(g =>
            {
                var melhorSerie = g.OrderBy(s => s.TempoTotalSeg).First();
                return new RecordePessoalDto
                {
                    TipoNado = g.Key.TipoNado,
                    DistanciaM = g.Key.DistanciaTotalM!.Value,
                    TempoRecordeSeg = melhorSerie.TempoTotalSeg!.Value,
                    PaceRecordeSeg = melhorSerie.PaceMedioSeg ?? CalcularPace(melhorSerie.DistanciaTotalM!.Value, melhorSerie.TempoTotalSeg.Value),
                    DataConquista = melhorSerie.Treino!.DataTreino,
                    TituloTreino = melhorSerie.Treino.TituloTreino
                };
            })
            .OrderBy(r => r.TipoNado)
            .ThenBy(r => r.DistanciaM)
            .ToList();

        return recordes;
    }

    public async Task<ProgressoMetaDto> ObterProgressoMetaAsync(int codMeta, int codUsuario)
    {
        var meta = await _context.Metas
            .AsNoTracking()
            .Include(m => m.SerieFicha!)
                .ThenInclude(sf => sf.Ficha)
            .FirstOrDefaultAsync(m => m.CodMeta == codMeta && m.CodUsuario == codUsuario);

        if (meta == null)
            throw new AppException("Meta nao encontrada.", 404);

        // O progresso so pode considerar treinos feitos na mesma piscina da ficha
        // vinculada a meta (Meta -> SerieFicha -> FichaBase.TamanhoPiscinaM).
        var tamanhoPiscinaMeta = meta.SerieFicha?.Ficha?.TamanhoPiscinaM;

        var seriesHistorico = await _context.SeriesTreino
            .AsNoTracking()
            .Include(s => s.Treino)
            .Where(s => s.Treino != null
                        && s.Treino.CodUsuario == codUsuario
                        && s.Treino.Status == StatusTreino.Concluido
                        && s.TipoNado == meta.TipoNado
                        && s.Treino.TamanhoPiscinaM == tamanhoPiscinaMeta)
            .ToListAsync();

        var idsSeries = seriesHistorico.Select(s => s.CodSerieTreino).ToList();
        var repeticoes = idsSeries.Count == 0
            ? new List<RepeticaoSerieTreino>()
            : await _context.RepeticoesSerieTreino
                .AsNoTracking()
                .Where(r => idsSeries.Contains(r.CodSerieTreino) && r.DuracaoSeg > 0)
                .ToListAsync();

        var repsPorSerie = repeticoes
            .GroupBy(r => r.CodSerieTreino)
            .ToDictionary(g => g.Key, g => g.ToList());

        var historico = new List<EvolucaoPaceDto>();
        decimal? melhorTempo = null;
        decimal? melhorPace = null;

        foreach (var serie in seriesHistorico)
        {
            repsPorSerie.TryGetValue(serie.CodSerieTreino, out var repsDaSerie);
            repsDaSerie ??= new List<RepeticaoSerieTreino>();

            foreach (var tentativa in ExtrairTentativas(meta, serie, repsDaSerie))
            {
                historico.Add(new EvolucaoPaceDto
                {
                    Data = tentativa.Data,
                    TituloTreino = tentativa.TituloTreino,
                    TipoNado = tentativa.TipoNado,
                    DistanciaM = tentativa.DistanciaM,
                    PaceSeg = tentativa.PaceSeg
                });

                if (melhorTempo == null || tentativa.TempoSeg < melhorTempo.Value)
                {
                    melhorTempo = tentativa.TempoSeg;
                    melhorPace = tentativa.PaceSeg;
                }
            }
        }

        double percentual = 0.0;
        if (melhorTempo.HasValue && melhorTempo.Value > 0 && meta.TempoAlvoSeg > 0)
        {
            percentual = (double)(meta.TempoAlvoSeg / melhorTempo.Value) * 100;
            if (percentual > 100.0) percentual = 100.0;
            percentual = Math.Round(percentual, 2);
        }

        var statusAtual = meta.Status;
        if (percentual >= 100.0 && statusAtual == StatusMeta.Ativa)
            statusAtual = await ConcluirMetaSeAtivaAsync(meta.CodMeta);

        return new ProgressoMetaDto
        {
            CodMeta = meta.CodMeta,
            TituloMeta = meta.TituloMeta,
            DistanciaAlvoM = meta.DistanciaAlvoM,
            TempoAlvoSeg = meta.TempoAlvoSeg,
            PaceAlvoSeg = meta.PaceAlvoSeg,
            MelhorTempoRealizadoSeg = melhorTempo,
            MelhorPaceRealizadoSeg = melhorPace,
            PercentualAtingimento = percentual,
            Status = statusAtual,
            HistoricoTentativas = historico.OrderBy(h => h.Data).ToList()
        };
    }

    public async Task AtualizarMetasAtingidasAsync(int codUsuario)
    {
        var metasAtivas = await _context.Metas
            .AsNoTracking()
            .Where(m => m.CodUsuario == codUsuario && m.Status == StatusMeta.Ativa)
            .Select(m => m.CodMeta)
            .ToListAsync();

        foreach (var codMeta in metasAtivas)
            await ObterProgressoMetaAsync(codMeta, codUsuario);
    }

    private async Task<StatusMeta> ConcluirMetaSeAtivaAsync(int codMeta)
    {
        var meta = await _context.Metas.FirstOrDefaultAsync(m => m.CodMeta == codMeta && m.Status == StatusMeta.Ativa);
        if (meta == null)
            return StatusMeta.Concluida;

        meta.Status = StatusMeta.Concluida;
        await _context.SaveChangesAsync();
        return StatusMeta.Concluida;
    }

    private static IEnumerable<TentativaMeta> ExtrairTentativas(
        Meta meta,
        SerieTreino serie,
        List<RepeticaoSerieTreino> repeticoes)
    {
        return meta.ModoAvaliacao switch
        {
            ModoAvaliacaoMeta.Media => ExtrairTentativasMedia(meta, serie, repeticoes),
            ModoAvaliacaoMeta.Total => ExtrairTentativasTotal(meta, serie, repeticoes),
            _ => ExtrairTentativasRepeticao(meta, serie, repeticoes)
        };
    }

    private static IEnumerable<TentativaMeta> ExtrairTentativasRepeticao(
        Meta meta,
        SerieTreino serie,
        List<RepeticaoSerieTreino> repeticoes)
    {
        var tirosAlvo = repeticoes.Where(r => r.DistanciaRealM == meta.DistanciaAlvoM).ToList();
        if (tirosAlvo.Count > 0)
        {
            foreach (var tiro in tirosAlvo)
            {
                var tentativa = CriarTentativa(
                    serie,
                    tiro.DistanciaRealM,
                    tiro.DuracaoSeg,
                    tiro.PaceSeg);
                if (tentativa != null)
                    yield return tentativa;
            }

            yield break;
        }

        var fallback = TentarSerieComoUnicaProva(meta, serie);
        if (fallback != null)
            yield return fallback;
    }

    private static IEnumerable<TentativaMeta> ExtrairTentativasMedia(
        Meta meta,
        SerieTreino serie,
        List<RepeticaoSerieTreino> repeticoes)
    {
        var tirosAlvo = repeticoes.Where(r => r.DistanciaRealM == meta.DistanciaAlvoM).ToList();
        if (tirosAlvo.Count > 0)
        {
            var tempoMedio = tirosAlvo.Average(r => r.DuracaoSeg);
            var tentativa = CriarTentativa(serie, meta.DistanciaAlvoM, tempoMedio, null);
            if (tentativa != null)
                yield return tentativa;

            yield break;
        }

        var fallback = TentarSerieComoUnicaProva(meta, serie);
        if (fallback != null)
            yield return fallback;
    }

    private static IEnumerable<TentativaMeta> ExtrairTentativasTotal(
        Meta meta,
        SerieTreino serie,
        List<RepeticaoSerieTreino> repeticoes)
    {
        if (serie.DistanciaTotalM == meta.DistanciaAlvoM && serie.TempoTotalSeg.GetValueOrDefault() > 0)
        {
            var tentativa = CriarTentativa(
                serie,
                serie.DistanciaTotalM.Value,
                serie.TempoTotalSeg!.Value,
                serie.PaceMedioSeg);
            if (tentativa != null)
                yield return tentativa;

            yield break;
        }

        if (repeticoes.Count == 0)
            yield break;

        var distancia = repeticoes.Sum(r => r.DistanciaRealM);
        var tempo = repeticoes.Sum(r => r.DuracaoSeg);
        if (distancia != meta.DistanciaAlvoM || tempo <= 0)
            yield break;

        var tentativaReps = CriarTentativa(serie, distancia, tempo, null);
        if (tentativaReps != null)
            yield return tentativaReps;
    }

    private static TentativaMeta? TentarSerieComoUnicaProva(Meta meta, SerieTreino serie)
    {
        if (serie.TempoTotalSeg.GetValueOrDefault() <= 0)
            return null;

        var distanciaUnica = serie.DistanciaTotalM == meta.DistanciaAlvoM
            || (serie.DistanciaPlanejadaM == meta.DistanciaAlvoM && serie.QuantidadeRepeticoesPlanejada == 1);

        if (!distanciaUnica)
            return null;

        return CriarTentativa(
            serie,
            meta.DistanciaAlvoM,
            serie.TempoTotalSeg!.Value,
            serie.PaceMedioSeg);
    }

    private static TentativaMeta? CriarTentativa(
        SerieTreino serie,
        int distanciaM,
        decimal tempoSeg,
        decimal? paceSeg)
    {
        if (serie.Treino == null || distanciaM <= 0 || tempoSeg <= 0)
            return null;

        return new TentativaMeta(
            serie.Treino.DataTreino,
            serie.Treino.TituloTreino,
            serie.TipoNado,
            distanciaM,
            tempoSeg,
            paceSeg ?? CalcularPace(distanciaM, tempoSeg));
    }

    private static decimal CalcularPace(int distanciaMetros, decimal tempoSegundos)
    {
        if (distanciaMetros == 0) return 0m;
        return Math.Round(tempoSegundos * 100m / distanciaMetros, 2);
    }

    private sealed record TentativaMeta(
        DateTime Data,
        string TituloTreino,
        TipoNado TipoNado,
        int DistanciaM,
        decimal TempoSeg,
        decimal PaceSeg);
}
