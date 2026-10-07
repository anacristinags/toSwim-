using ToSwim.Application.DTOs.FichaBase;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class FichaBaseService : IFichaBaseService
{
    private readonly IFichaBaseRepository _fichaRepository;
    private readonly ISerieFichaRepository _serieRepository;

    public FichaBaseService(IFichaBaseRepository fichaRepository, ISerieFichaRepository serieRepository)
    {
        _fichaRepository = fichaRepository;
        _serieRepository = serieRepository;
    }

    public async Task<IEnumerable<FichaBaseResponseDto>> ListarPorUsuarioAsync(int codUsuario)
    {
        var fichas = await _fichaRepository.ListarPorUsuarioAsync(codUsuario);
        return fichas.Select(f => MapearParaDto(f));
    }

    public async Task<FichaBaseResponseDto> BuscarPorIdAsync(int codFicha, int codUsuario)
    {
        var ficha = await ObterFichaEValidarDonoAsync(codFicha, codUsuario);
        return MapearParaDto(ficha);
    }

    public async Task<FichaBaseResponseDto> CriarAsync(int codUsuario, FichaBaseRequestDto dto)
    {
        // Regra de Neg�cio: Limite de 5 Fichas Base Ativas
        var quantidadeAtivas = await _fichaRepository.ContarFichasAtivasAsync(codUsuario);
        if (quantidadeAtivas >= 5)
        {
            throw new AppException("Limite de 5 fichas de treino base ativas atingido. Desative ou exclua uma ficha existente.", 400);
        }

        if (dto.TamanhoPiscinaM != 25 && dto.TamanhoPiscinaM != 50)
            throw new AppException("O tamanho da piscina deve ser 25 ou 50 metros.", 400);

        var ficha = new FichaBase
        {
            CodUsuario = codUsuario,
            TituloFicha = dto.TituloFicha.Trim(),
            TipoFicha = dto.TipoFicha,
            TamanhoPiscinaM = dto.TamanhoPiscinaM,
            Status = 1
        };

        var criada = await _fichaRepository.CriarAsync(ficha);
        return MapearParaDto(criada);
    }

    public async Task<FichaBaseResponseDto> AtualizarAsync(int codFicha, int codUsuario, FichaBaseRequestDto dto)
    {
        var ficha = await ObterFichaEValidarDonoAsync(codFicha, codUsuario);

        ficha.TituloFicha = dto.TituloFicha.Trim();
        ficha.TipoFicha = dto.TipoFicha;

        var atualizada = await _fichaRepository.AtualizarAsync(ficha);
        return MapearParaDto(atualizada);
    }

    public async Task AlterarStatusAsync(int codFicha, int codUsuario, short novoStatus)
    {
        if (novoStatus != 0 && novoStatus != 1)
            throw new AppException("Status inválido. Use 0 para inativo ou 1 para ativo.", 400);

        var ficha = await ObterFichaEValidarDonoAsync(codFicha, codUsuario);

        if (novoStatus == 1)
        {
            // Valida o limite antes de reativar uma ficha
            var quantidadeAtivas = await _fichaRepository.ContarFichasAtivasAsync(codUsuario);
            if (quantidadeAtivas >= 5)
                throw new AppException("Não é possível ativar esta ficha. Limite de 5 fichas ativas excedido.", 400);
        }

        ficha.Status = novoStatus;
        await _fichaRepository.AtualizarAsync(ficha);
    }

    public async Task DeletarAsync(int codFicha, int codUsuario)
    {
        var ficha = await ObterFichaEValidarDonoAsync(codFicha, codUsuario);

        var possuiVinculos = await _fichaRepository.TemVinculosQueImpedemExclusaoAsync(codFicha);
        if (possuiVinculos)
        {
            await _fichaRepository.DeletarLogicamenteAsync(ficha);
        }
        else
        {
            await _fichaRepository.ExcluirFisicamenteAsync(ficha);
        }
    }

    public async Task<FichaBaseResponseDto> DuplicarAsync(int codFicha, int codUsuario)
    {
        // Valida o limite de 5 antes de duplicar
        var quantidadeAtivas = await _fichaRepository.ContarFichasAtivasAsync(codUsuario);
        if (quantidadeAtivas >= 5)
        {
            throw new AppException("N�o � poss�vel duplicar. Limite de 5 fichas ativas atingido.", 400);
        }

        var fichaOriginal = await ObterFichaEValidarDonoAsync(codFicha, codUsuario);

        var novaFicha = new FichaBase
        {
            CodUsuario = codUsuario,
            TituloFicha = $"{fichaOriginal.TituloFicha} (Cópia)",
            TipoFicha = fichaOriginal.TipoFicha,
            TamanhoPiscinaM = fichaOriginal.TamanhoPiscinaM,
            Status = 1,
            FichaCopiada = fichaOriginal.CodFicha
        };

        var criada = await _fichaRepository.CriarAsync(novaFicha);

        // Duplica as s�ries da ficha
        foreach (var serie in fichaOriginal.Series)
        {
            var novaSerie = new SerieFicha
            {
                CodFicha = criada.CodFicha,
                Ordem = serie.Ordem,
                TipoNado = serie.TipoNado,
                QuantidadeRepeticoes = serie.QuantidadeRepeticoes,
                DistanciaM = serie.DistanciaM,
                TempoPausaSeg = serie.TempoPausaSeg,
                IsGoalSeries = serie.IsGoalSeries,
                IgnorarNoPace = serie.IgnorarNoPace,
                Observacoes = serie.Observacoes
            };
            await _serieRepository.CriarAsync(novaSerie);
        }

        // Recarrega a ficha criada com as s�ries rec�m-adicionadas
        var fichaCompleta = await _fichaRepository.BuscarPorIdAsync(criada.CodFicha, codUsuario);
        return MapearParaDto(fichaCompleta!);
    }

    private async Task<FichaBase> ObterFichaEValidarDonoAsync(int codFicha, int codUsuario)
    {
        var ficha = await _fichaRepository.BuscarPorIdAsync(codFicha, codUsuario);
        if (ficha is null)
            throw new AppException("Ficha de treino n�o encontrada ou n�o pertence a voc�.", 404);

        return ficha;
    }

    private static FichaBaseResponseDto MapearParaDto(FichaBase f) =>
        new()
        {
            CodFicha = f.CodFicha,
            CodUsuario = f.CodUsuario,
            TituloFicha = f.TituloFicha,
            TipoFicha = f.TipoFicha,
            Status = f.Status,
            TamanhoPiscinaM = f.TamanhoPiscinaM,
            FichaCopiada = f.FichaCopiada,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt,
            Series = f.Series.OrderBy(s => s.Ordem).Select(s => new SerieFichaResponseDto
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
            }).ToList()
        };
}