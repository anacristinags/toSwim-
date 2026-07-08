using ToSwim.Application.DTOs.Usuario;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioResponseDto?> BuscarPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

        if (usuario is null) return null;

        return new UsuarioResponseDto
        {
            CodUsuario = usuario.CodUsuario,
            Nome = usuario.Nome,
            Email = usuario.Email,
            StatusConta = (int)usuario.StatusConta,
            CreatedAt = usuario.CreatedAt
        };
    }

    public async Task AtualizarSenhaAsync(int id, AtualizarSenhaRequestDto dto)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

        if (usuario is null)
            throw new AppException("Usuário não encontrado", 404);

        if (!BCrypt.Net.BCrypt.Verify(dto.SenhaAtual, usuario.SenhaHash))
            throw new AppException("Senha atual incorreta", 400);

        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

        await _usuarioRepository.AtualizarAsync(usuario);
    }
}