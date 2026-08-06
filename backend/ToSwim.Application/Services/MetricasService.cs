using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Application.DTOs.Metricas;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
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
        decimal? paceGeral = (totalMetros > 0) ? Math.Round(totalTempo * 100m / totalMetros, 2) : null;

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

        // Agrupar por nado e dist˜ncia para extrair os recordes (menor tempo)
        var recordes = series
            .GroupBy(s => new { s.TipoNado, s.DistanciaTotalM })
            .Select(g => {
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
            .FirstOrDefaultAsync(m => m.CodMeta == codMeta && m.CodUsuario == codUsuario);

        if (meta == null)
            throw new AppException("Meta n˜o encontrada.", 404);

        // Obter todas as tentativas vinculadas a esta meta
        var vinculos = await _context.TreinosMetas
            .AsNoTracking()
            .Include(tm => tm.Treino)
            .Include(tm => tm.SerieTreino)
            .Where(tm => tm.CodMeta == codMeta)
            .ToListAsync();

        var historico = new List<EvolucaoPaceDto>();
        decimal? melhorTempo = null;
        decimal? melhorPace = null;

        foreach (var v in vinculos)
        {
            if (v.SerieTreino != null && v.SerieTreino.TempoTotalSeg.HasValue && v.SerieTreino.DistanciaTotalM > 0)
            {
                decimal pace = v.SerieTreino.PaceMedioSeg ?? CalcularPace(v.SerieTreino.DistanciaTotalM.Value, v.SerieTreino.TempoTotalSeg.Value);

                historico.Add(new EvolucaoPaceDto
                {
                    Data = v.Treino!.DataTreino,
                    TituloTreino = v.Treino.TituloTreino,
                    TipoNado = v.SerieTreino.TipoNado,
                    DistanciaM = v.SerieTreino.DistanciaTotalM.Value,
                    PaceSeg = pace
                });

                if (v.SerieTreino.DistanciaTotalM.Value == meta.DistanciaAlvoM)
                {
                    if (melhorTempo == null || v.SerieTreino.TempoTotalSeg.Value < melhorTempo.Value)
                    {
                        melhorTempo = v.SerieTreino.TempoTotalSeg.Value;
                        melhorPace = pace;
                    }
                }
            }
        }

        // Calcular percentual de aproxima˜˜o do pace
        double percentual = 0.0;
        if (melhorPace.HasValue)
        {
            // Ex: Se o pace alvo ˜ 90s (1min30s) e meu melhor ˜ 100s:
            // Formula: (PaceAlvo / MelhorPace) * 100. Se bateu a meta, fica >= 100%
            percentual = (double)meta.PaceAlvoSeg / (double)melhorPace.Value * 100;
            if (percentual > 100.0) percentual = 100.0;
            percentual = Math.Round(percentual, 2);
        }

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
            HistoricoTentativas = historico.OrderBy(h => h.Data).ToList()
        };
    }

    private static decimal CalcularPace(int distanciaMetros, decimal tempoSegundos)
    {
        if (distanciaMetros == 0) return 0m;
        return Math.Round(tempoSegundos * 100m / distanciaMetros, 2);
    }
}