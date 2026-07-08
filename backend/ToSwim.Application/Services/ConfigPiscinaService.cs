using ToSwim.Application.DTOs.ConfigPiscina;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class ConfigPiscinaService : IConfigPiscinaService
{
    private readonly IConfigPiscinaRepository _repository;

    public ConfigPiscinaService(IConfigPiscinaRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConfigPiscinaResponseDto?> BuscarPorUsuarioAsync(int codUsuario)
    {
        var config = await _repository.BuscarPorUsuarioAsync(codUsuario);

        if (config is null) return null;

        return MapearParaDto(config);
    }

    public async Task<ConfigPiscinaResponseDto> CriarAsync(int codUsuario, ConfigPiscinaRequestDto dto)
    {
        if (await _repository.ExistePorUsuarioAsync(codUsuario))
            throw new AppException("Usuário já possui configuração de piscina", 400);

        ValidarCampos(dto);

        var config = new ConfigPiscina
        {
            CodUsuario = codUsuario,
            TamanhoM = dto.TamanhoM,
            FormaContagem = (FormaContagem)dto.FormaContagem,
            Status = 1
        };

        var criado = await _repository.CriarAsync(config);
        return MapearParaDto(criado);
    }

    public async Task<ConfigPiscinaResponseDto> AtualizarAsync(int codUsuario, ConfigPiscinaRequestDto dto)
    {
        var config = await _repository.BuscarPorUsuarioAsync(codUsuario);

        if (config is null)
            throw new AppException("Configuração de piscina não encontrada", 404);

        ValidarCampos(dto);

        config.TamanhoM = dto.TamanhoM;
        config.FormaContagem = (FormaContagem)dto.FormaContagem;

        var atualizado = await _repository.AtualizarAsync(config);
        return MapearParaDto(atualizado);
    }

    private static void ValidarCampos(ConfigPiscinaRequestDto dto)
    {
        if (dto.TamanhoM != 25 && dto.TamanhoM != 50)
            throw new AppException("Tamanho de piscina inválido. Use 25 ou 50", 400);

        if (dto.FormaContagem != 0 && dto.FormaContagem != 1)
            throw new AppException("Forma de contagem inválida. Use 0 (distância) ou 1 (voltas)", 400);
    }

    private static ConfigPiscinaResponseDto MapearParaDto(ConfigPiscina config) =>
        new()
        {
            CodPiscina = config.CodPiscina,
            CodUsuario = config.CodUsuario,
            TamanhoM = config.TamanhoM,
            FormaContagem = (short)config.FormaContagem,
            Status = config.Status,
            CreatedAt = config.CreatedAt,
            UpdatedAt = config.UpdatedAt
        };
}