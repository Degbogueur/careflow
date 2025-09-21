using System.Security.Claims;

namespace CareFlow.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserFullName(this ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);
        return principal.FindFirstValue("FullName");
    }
}
