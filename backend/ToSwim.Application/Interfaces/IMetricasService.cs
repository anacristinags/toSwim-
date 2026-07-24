using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Metricas;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.Interfaces;

public interface IMetricasService
{
    Task<DashboardResumoDto> ObterResumoDashboardAsync(int codUsuario);
    Task<IEnumerable<EvolucaoPaceDto>> ObterEvolucaoPaceAsync(int codUsuario, TipoNado? nado);
    Task<IEnumerable<RecordePessoalDto>> ObterMelhoresTemposAsync(int codUsuario, TipoNado? nado, int? distancia);
    Task<ProgressoMetaDto> ObterProgressoMetaAsync(int codMeta, int codUsuario);
}