using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Treino;
using ToSwim.Application.DTOs.SerieTreino;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class TreinoService : ITreinoService
{
    private readonly ITreinoRepository _treinoRepository;
    private readonly ISerieTreinoRepository _serieTreinoRepository;
    private readonly IFichaBaseRepository _fichaBaseRepository;
    private readonly IMetricasService _metricasService;

    public TreinoService(
        ITreinoRepository treinoRepository,
        ISerieTreinoRepository serieTreinoRepository,
        IFichaBaseRepository fichaBaseRepository,
        IMetricasService metricasService)
    {
        _treinoRepository = treinoRepository;
        _serieTreinoRepository = serieTreinoRepository;
        _fichaBaseRepository = fichaBaseRepository;
        _metricasService = metricasService;
    }

    public async Task<TreinoResponseDto> IniciarTreinoAsync(int codUsuario, IniciarTreinoRequestDto dto)
    {
        // 1. Obter a Ficha Base ativa com suas s?ries associadas
        var ficha = await _fichaBaseRepository.BuscarPorIdAsync(dto.CodFicha, codUsuario);
        if (ficha == null || ficha.Status != 1)
            throw new AppException("Ficha base n?o encontrada ou inativa.", 404);

        if (dto.TamanhoPiscinaM != 25 && dto.TamanhoPiscinaM != 50)
            throw new AppException("O tamanho da piscina deve ser 25 ou 50 metros.", 400);

        // 2. Instanciar o Treino
        var novoTreino = new Treino
        {
            CodUsuario = codUsuario,
            CodFicha = dto.CodFicha,
            TituloTreino = string.IsNullOrWhiteSpace(dto.TituloTreino) ? ficha.TituloFicha : dto.TituloTreino.Trim(),
            Observacao = string.IsNullOrWhiteSpace(dto.Observacao) ? null : dto.Observacao.Trim(),
            TamanhoPiscinaM = dto.TamanhoPiscinaM,
            Status = StatusTreino.Andamento,
            DataTreino = DateTime.UtcNow
        };

        await _treinoRepository.AdicionarAsync(novoTreino);
        await _treinoRepository.SalvarAlteracoesAsync(); // Salva primeiro para gerar o CodTreino

        // 3. Clonar e inserir as s?ries da ficha base na tabela de execu??o (serie_treino)
        foreach (var serieFicha in ficha.Series.OrderBy(s => s.Ordem))
        {
            var novaSerieTreino = new SerieTreino
            {
                CodTreino = novoTreino.CodTreino,
                CodSerieFicha = serieFicha.CodSerieFicha,
                Ordem = serieFicha.Ordem,
                TipoNado = serieFicha.TipoNado,
                QuantidadeRepeticoesPlanejada = serieFicha.QuantidadeRepeticoes,
                DistanciaPlanejadaM = serieFicha.DistanciaM,
                TempoPausaSeg = serieFicha.TempoPausaSeg
            };

            await _serieTreinoRepository.AdicionarAsync(novaSerieTreino);
        }

        await _serieTreinoRepository.SalvarAlteracoesAsync();

        // Recarregar treino completo
        var treinoCompleto = await _treinoRepository.ObterPorIdAsync(novoTreino.CodTreino, codUsuario);
        return MapearParaDto(treinoCompleto!);
    }

    public async Task<TreinoResponseDto> ObterPorIdAsync(int codTreino, int codUsuario)
    {
        var treino = await _treinoRepository.ObterPorIdAsync(codTreino, codUsuario);
        if (treino == null)
            throw new AppException("Treino n?o encontrado.", 404);

        return MapearParaDto(treino);
    }

    public async Task<IEnumerable<TreinoResponseDto>> ObterTodosPorUsuarioAsync(int codUsuario, StatusTreino? status, int pagina, int tamanhoPagina)
    {
        if (pagina <= 0) pagina = 1;
        if (tamanhoPagina <= 0 || tamanhoPagina > 200) tamanhoPagina = 20;

        var treinos = await _treinoRepository.ObterTodosPorUsuarioAsync(codUsuario, status, pagina, tamanhoPagina);
        return treinos.Select(MapearParaDto);
    }

    public async Task<TreinoResponseDto> AtualizarTreinoAsync(int codTreino, int codUsuario, AtualizarTreinoRequestDto dto)
    {
        var treino = await _treinoRepository.ObterPorIdAsync(codTreino, codUsuario);
        if (treino == null)
            throw new AppException("Treino n?o encontrado.", 404);

        if (treino.Status != StatusTreino.Andamento)
            throw new AppException("Apenas treinos em andamento podem ser atualizados.", 400);

        if (dto.TamanhoPiscinaM != 25 && dto.TamanhoPiscinaM != 50)
            throw new AppException("O tamanho da piscina deve ser 25 ou 50 metros.", 400);

        treino.TituloTreino = string.IsNullOrWhiteSpace(dto.TituloTreino) ? treino.TituloTreino : dto.TituloTreino.Trim();
        treino.Observacao = string.IsNullOrWhiteSpace(dto.Observacao) ? null : dto.Observacao.Trim();
        treino.TamanhoPiscinaM = dto.TamanhoPiscinaM;

        _treinoRepository.Atualizar(treino);
        await _treinoRepository.SalvarAlteracoesAsync();

        return MapearParaDto(treino);
    }

    public async Task<TreinoResponseDto> FinalizarTreinoAsync(int codTreino, int codUsuario)
    {
        var treino = await _treinoRepository.ObterPorIdAsync(codTreino, codUsuario);
        if (treino == null)
            throw new AppException("Treino n?o encontrado.", 404);

        if (treino.Status != StatusTreino.Andamento)
            throw new AppException("Este treino j? est? finalizado ou cancelado.", 400);

        // Calcular Consolida??o de Dados em Lote (Dist?ncia Total, Dura??o Total, Pace M?dio)
        int distanciaAcumulada = 0;
        decimal duracaoAcumulada = 0m;

        foreach (var serie in treino.SeriesTreino)
        {
            distanciaAcumulada += serie.DistanciaTotalM ?? 0;
            duracaoAcumulada += serie.TempoTotalSeg ?? 0m;
        }

        treino.DistanciaTotalM = distanciaAcumulada;
        treino.DuracaoTotalSeg = duracaoAcumulada;
        treino.Status = StatusTreino.Concluido;

        if (distanciaAcumulada > 0 && duracaoAcumulada > 0)
        {
            treino.PaceMedioSeg = CalcularPace(distanciaAcumulada, duracaoAcumulada);
        }

        _treinoRepository.Atualizar(treino);
        await _treinoRepository.SalvarAlteracoesAsync();

        await _metricasService.AtualizarMetasAtingidasAsync(codUsuario);

        return MapearParaDto(treino);
    }

    public async Task<TreinoResponseDto> CancelarTreinoAsync(int codTreino, int codUsuario)
    {
        var treino = await _treinoRepository.ObterPorIdAsync(codTreino, codUsuario);
        if (treino == null)
            throw new AppException("Treino n?o encontrado.", 404);

        if (treino.Status != StatusTreino.Andamento)
            throw new AppException("Apenas treinos em andamento podem ser cancelados.", 400);

        treino.Status = StatusTreino.Cancelado;

        _treinoRepository.Atualizar(treino);
        await _treinoRepository.SalvarAlteracoesAsync();

        return MapearParaDto(treino);
    }

    // ==========================================
    // OPERA??ES DE S?RIE DO TREINO
    // ==========================================

    public async Task<SerieTreinoResponseDto> AdicionarSerieAvulsaAsync(int codTreino, int codUsuario, SerieTreinoRequestDto dto)
    {
        var treino = await _treinoRepository.ObterPorIdAsync(codTreino, codUsuario);
        if (treino == null)
            throw new AppException("Treino n?o encontrado ou n?o pertence ao nadador.", 404);

        if (treino.Status != StatusTreino.Andamento)
            throw new AppException("N?o ? permitido adicionar s?ries a um treino j? finalizado ou cancelado.", 400);

        var ordemJaExiste = await _serieTreinoRepository.ExisteOrdemNoTreinoAsync(codTreino, dto.Ordem);
        if (ordemJaExiste)
            throw new AppException($"J? existe uma s?rie na ordem {dto.Ordem} para este treino.", 400);

        var novaSerie = new SerieTreino
        {
            CodTreino = codTreino,
            CodSerieFicha = dto.CodSerieFicha,
            CodMeta = dto.CodMeta,
            Ordem = dto.Ordem,
            TipoNado = dto.TipoNado,
            QuantidadeRepeticoesPlanejada = dto.QuantidadeRepeticoesPlanejada,
            DistanciaPlanejadaM = dto.DistanciaPlanejadaM,
            TempoPausaSeg = dto.TempoPausaSeg,
            TempoTotalSeg = dto.TempoTotalSeg,
            DistanciaTotalM = dto.DistanciaTotalM,
            Observacoes = dto.Observacoes
        };

        if (dto.DistanciaTotalM.HasValue && dto.TempoTotalSeg.HasValue && dto.DistanciaTotalM > 0)
        {
            novaSerie.PaceMedioSeg = CalcularPace(dto.DistanciaTotalM.Value, dto.TempoTotalSeg.Value);
        }

        await _serieTreinoRepository.AdicionarAsync(novaSerie);
        await _serieTreinoRepository.SalvarAlteracoesAsync();

        return MapearParaDto(novaSerie);
    }

    public async Task<IEnumerable<SerieTreinoResponseDto>> ListarSeriesPorTreinoAsync(int codTreino, int codUsuario)
    {
        var series = await _serieTreinoRepository.ObterSeriesPorTreinoAsync(codTreino, codUsuario);
        return series.Select(MapearParaDto);
    }

    public async Task<SerieTreinoResponseDto> ObterSerieExecutadaPorIdAsync(int codSerieTreino, int codUsuario)
    {
        var serie = await _serieTreinoRepository.ObterPorIdAsync(codSerieTreino, codUsuario);
        if (serie == null)
            throw new AppException("S?rie de treino n?o encontrada.", 404);

        return MapearParaDto(serie);
    }

    public async Task<SerieTreinoResponseDto> AtualizarSerieExecutadaAsync(int codSerieTreino, int codUsuario, SerieTreinoRequestDto dto)
    {
        var serie = await _serieTreinoRepository.ObterPorIdAsync(codSerieTreino, codUsuario);
        if (serie == null)
            throw new AppException("S?rie de treino n?o encontrada.", 404);

        if (serie.Treino!.Status != StatusTreino.Andamento)
            throw new AppException("N?o ? permitido editar s?ries de um treino finalizado ou cancelado.", 400);

        if (serie.Ordem != dto.Ordem)
        {
            var ordemJaExiste = await _serieTreinoRepository.ExisteOrdemNoTreinoAsync(serie.CodTreino, dto.Ordem);
            if (ordemJaExiste)
                throw new AppException($"J? existe outra s?rie na ordem {dto.Ordem} para este treino.", 400);
        }

        serie.Ordem = dto.Ordem;
        serie.TipoNado = dto.TipoNado;
        serie.QuantidadeRepeticoesPlanejada = dto.QuantidadeRepeticoesPlanejada;
        serie.DistanciaPlanejadaM = dto.DistanciaPlanejadaM;
        serie.TempoPausaSeg = dto.TempoPausaSeg;
        serie.TempoTotalSeg = dto.TempoTotalSeg;
        serie.DistanciaTotalM = dto.DistanciaTotalM;
        serie.Observacoes = dto.Observacoes;

        if (dto.DistanciaTotalM.HasValue && dto.TempoTotalSeg.HasValue && dto.DistanciaTotalM > 0)
        {
            serie.PaceMedioSeg = CalcularPace(dto.DistanciaTotalM.Value, dto.TempoTotalSeg.Value);
        }
        else
        {
            serie.PaceMedioSeg = null;
        }

        _serieTreinoRepository.Atualizar(serie);
        await _serieTreinoRepository.SalvarAlteracoesAsync();

        return MapearParaDto(serie);
    }

    public async Task RemoverSerieExecutadaAsync(int codSerieTreino, int codUsuario)
    {
        var serie = await _serieTreinoRepository.ObterPorIdAsync(codSerieTreino, codUsuario);
        if (serie == null)
            throw new AppException("S?rie de treino n?o encontrada.", 404);

        if (serie.Treino!.Status != StatusTreino.Andamento)
            throw new AppException("N?o ? permitido excluir s?ries de um treino finalizado ou cancelado.", 400);

        _serieTreinoRepository.Remover(serie);
        await _serieTreinoRepository.SalvarAlteracoesAsync();
    }

    // ==========================================
    // MAPEARES PRIVADOS
    // ==========================================

    private static decimal CalcularPace(int distanciaMetros, decimal tempoSegundos)
    {
        if (distanciaMetros == 0) return 0m;
        return Math.Round(tempoSegundos * 100m / distanciaMetros, 2);
    }

    private static TreinoResponseDto MapearParaDto(Treino t)
    {
        return new TreinoResponseDto
        {
            CodTreino = t.CodTreino,
            CodUsuario = t.CodUsuario,
            CodFicha = t.CodFicha,
            TituloTreino = t.TituloTreino,
            Observacao = t.Observacao,
            DistanciaTotalM = t.DistanciaTotalM,
            DuracaoTotalSeg = t.DuracaoTotalSeg,
            PaceMedioSeg = t.PaceMedioSeg,
            TamanhoPiscinaM = t.TamanhoPiscinaM,
            DataTreino = t.DataTreino,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            SeriesTreino = t.SeriesTreino.Select(MapearParaDto).ToList()
        };
    }

    private static SerieTreinoResponseDto MapearParaDto(SerieTreino s)
    {
        return new SerieTreinoResponseDto
        {
            CodSerieTreino = s.CodSerieTreino,
            CodTreino = s.CodTreino,
            CodSerieFicha = s.CodSerieFicha,
            CodMeta = s.CodMeta,
            Ordem = s.Ordem,
            TipoNado = s.TipoNado,
            QuantidadeRepeticoesPlanejada = s.QuantidadeRepeticoesPlanejada,
            DistanciaPlanejadaM = s.DistanciaPlanejadaM,
            TempoPausaSeg = s.TempoPausaSeg,
            TempoTotalSeg = s.TempoTotalSeg,
            DistanciaTotalM = s.DistanciaTotalM,
            PaceMedioSeg = s.PaceMedioSeg,
            Observacoes = s.Observacoes,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }
}