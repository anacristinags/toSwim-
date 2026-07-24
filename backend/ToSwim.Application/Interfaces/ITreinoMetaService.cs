using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.TreinoMeta;

namespace ToSwim.Application.Interfaces;

public interface ITreinoMetaService
{
    Task<TreinoMetaResponseDto> VincularTreinoMetaAsync(int codTreino, int codMeta, int? codSerieTreino, int codUsuario);
    Task DesvincularTreinoMetaAsync(int codTreino, int codMeta, int? codSerieTreino, int codUsuario);
    Task<IEnumerable<TreinoMetaResponseDto>> ListarMetasDoTreinoAsync(int codTreino, int codUsuario);
    Task<IEnumerable<TreinoMetaResponseDto>> ListarTreinosDaMetaAsync(int codMeta, int codUsuario);
}