namespace ToSwim.Application.Interfaces;

public interface IJwtService
{
    string GerarToken(int codUsuario, string email, string nome);
    DateTime ObterExpiracao();
}