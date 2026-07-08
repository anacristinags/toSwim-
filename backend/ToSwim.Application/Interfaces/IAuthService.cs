using ToSwim.Application.DTOs.Auth;

namespace ToSwim.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> RegistrarAsync(RegistroRequestDto dto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
}