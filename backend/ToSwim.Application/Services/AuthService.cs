using ToSwim.Application.DTOs.Auth;
using ToSwim.Application.Exceptions;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUsuarioRepository usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> RegistrarAsync(RegistroRequestDto dto)
    {
        if (await _usuarioRepository.EmailExisteAsync(dto.Email))
            throw new AppException("Email já cadastrado", 400);

        var usuario = new Usuario
        {
            Nome = dto.Nome.Trim(),
            Email = dto.Email.Trim().ToLower(),
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        var criado = await _usuarioRepository.CriarAsync(usuario);

        return new LoginResponseDto
        {
            Token = _jwtService.GerarToken(criado.CodUsuario, criado.Email, criado.Nome),
            Nome = criado.Nome,
            Email = criado.Email,
            ExpiraEm = _jwtService.ObterExpiracao()
        };
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var usuario = await _usuarioRepository.BuscarPorEmailAsync(dto.Email.Trim().ToLower());

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            throw new AppException("Email ou senha inválidos", 401);

        if ((int)usuario.StatusConta == 0)
            throw new AppException("Conta inativa", 401);

        return new LoginResponseDto
        {
            Token = _jwtService.GerarToken(usuario.CodUsuario, usuario.Email, usuario.Nome),
            Nome = usuario.Nome,
            Email = usuario.Email,
            ExpiraEm = _jwtService.ObterExpiracao()
        };
    }
}