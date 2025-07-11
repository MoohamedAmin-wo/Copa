using System.Security.Claims;
namespace Cupa.Domain.AppExtenssions;
public static class UserExtension
{
    public static string GetUserId(this ClaimsPrincipal claims) =>
         claims.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    public static string GetUserEmail(this ClaimsPrincipal claims) =>
         claims.FindFirst(ClaimTypes.Email)!.Value;
}