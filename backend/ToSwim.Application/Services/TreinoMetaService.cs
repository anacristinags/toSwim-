using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.TreinoMeta;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class TreinoMetaService : ITreinoMetaService
{
    private readonly ITreinoMetaRepository _treinoMetaRepository;
    private readonly ITreinoRepository _treinoRepository;
    private readonly IMetaRepository _metaRepository;
    private readonly ISerieTreinoRepository _serieTreinoRepository;

    public TreinoMetaService(
        ITreinoMetaRepository treinoMetaRepository,
        ITreinoRepository treinoRepository,
        IMetaRepository metaRepository,
        ISerieTreinoRepository serieTreinoRepository)
    {
        _treinoMetaRepository = treinoMetaRepository;
        _treinoRepository = treinoRepository;
        _metaRepository = metaRepository;
        _serieTreinoRepository = serieTreinoRepository;
    }

    public async Task<TreinoMetaResponseDto> VincularTreinoMetaAsync(int codTreino, int codMeta, int? codSerieTreino, int codUsuario)
    {
        // 1. Validar propriedade do Treino
        var treino = await _treinoRepository.ObterPorIdAsync(codTreino, codUsuario);
        if (treino == null)
            throw new AppException("Treino não encontrado.", 404);

        // 2. Validar propriedade da Meta
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Meta de tempo não encontrada.", 404);

        // 3. Validar se a série (caso fornecida) pertence ao treino
        if (codSerieTreino.HasValue)
        {
            var serie = await _serieTreinoRepository.ObterPorIdAsync(codSerieTreino.Value, codUsuario);
            if (serie == null || serie.CodTreino != codTreino)
                throw new AppException("A série especificada não pertence ao treino informado.", 400);
        }

        // 4. Verificar se o vínculo já existe
        var jaExiste = await _treinoMetaRepository.ExisteVinculoAsync(codTreino, codMeta, codSerieTreino);
        if (jaExiste)
            throw new AppException("Este treino já está vinculado a esta meta.", 400);

        // 5. Salvar o vínculo
        var vinculo = new TreinoMeta
        {
            CodTreino = codTreino,
            CodMeta = codMeta,
            CodSerieTreino = codSerieTreino
        };

        await _treinoMetaRepository.AdicionarAsync(vinculo);
        await _treinoMetaRepository.SalvarAlteracoesAsync();

        return new TreinoMetaResponseDto
        {
            CodTreinoMeta = vinculo.CodTreinoMeta,
            CodTreino = vinculo.CodTreino,
            CodMeta = vinculo.CodMeta,
            CodSerieTreino = vinculo.CodSerieTreino,
            CreatedAt = vinculo.CreatedAt,
            TituloMeta = meta.TituloMeta,
            TituloTreino = treino.TituloTreino
        };
    }

    public async Task DesvincularTreinoMetaAsync(int codTreino, int codMeta, int? codSerieTreino, int codUsuario)
    {
        var vinculo = await _treinoMetaRepository.ObterVinculoAsync(codTreino, codMeta, codSerieTreino);
        if (vinculo == null)
            throw new AppException("Vínculo não encontrado.", 404);

        // Garantir que pertence ao usuário logado
        var meta = await _metaRepository.ObterPorIdAsync(codMeta, codUsuario);
        if (meta == null)
            throw new AppException("Permissão negada ou dados inválidos.", 403);

        _treinoMetaRepository.Remover(vinculo);
        await _treinoMetaRepository.SalvarAlteracoesAsync();
    }

    public async Task<IEnumerable<TreinoMetaResponseDto>> ListarMetasDoTreinoAsync(int codTreino, int codUsuario)
    {
        var vinculos = await _treinoMetaRepository.ListarMetasPorTreinoAsync(codTreino, codUsuario);
        return vinculos.Select(v => new TreinoMetaResponseDto
        {
            CodTreinoMeta = v.CodTreinoMeta,
            CodTreino = v.CodTreino,
            CodMeta = v.CodMeta,
            CodSerieTreino = v.CodSerieTreino,
            CreatedAt = v.CreatedAt,
            TituloMeta = v.Meta?.TituloMeta ?? string.Empty,
            TituloTreino = v.Treino?.TituloTreino ?? string.Empty
        });
    }

    public async Task<IEnumerable<TreinoMetaResponseDto>> ListarTreinosDaMetaAsync(int codMeta, int codUsuario)
    {
        var vinculos = await _treinoMetaRepository.ListarTreinosPorMetaAsync(codMeta, codUsuario);
        return vinculos.Select(v => new TreinoMetaResponseDto
        {
            CodTreinoMeta = v.CodTreinoMeta,
            CodTreino = v.CodTreino,
            CodMeta = v.CodMeta,
            CodSerieTreino = v.CodSerieTreino,
            CreatedAt = v.CreatedAt,
            TituloMeta = v.Meta?.TituloMeta ?? string.Empty,
            TituloTreino = v.Treino?.TituloTreino ?? string.Empty
        });
    }
}