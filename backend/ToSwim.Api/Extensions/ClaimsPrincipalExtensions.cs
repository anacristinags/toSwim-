using System.Security.Claims;

namespace ToSwim.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int ObterUsuarioId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("sub")?.Value
            ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return int.TryParse(value, out var id) ? id : 0;
    }
}