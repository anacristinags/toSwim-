using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Meta;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.Interfaces;

public interface IMetaService
{
    Task<MetaResponseDto> ObterPorIdAsync(int codMeta, int codUsuario);
    Task<IEnumerable<MetaResponseDto>> ObterTodasPorUsuarioAsync(int codUsuario, StatusMeta? status);
    Task<MetaResponseDto> CriarMetaAsync(int codUsuario, MetaRequestDto dto);
    Task<MetaResponseDto> AtualizarMetaAsync(int codMeta, int codUsuario, MetaRequestDto dto);
    Task<MetaResponseDto> AtualizarStatusMetaAsync(int codMeta, int codUsuario, StatusMeta novoStatus);
    Task RemoverMetaAsync(int codMeta, int codUsuario);
}