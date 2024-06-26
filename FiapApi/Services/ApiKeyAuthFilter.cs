namespace FiapApi.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FiapApi.Data;
using System.Linq;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly AppDbContext _context;

    public ApiKeyAuthFilter(AppDbContext context)
    {
        _context = context;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var potentialApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var apiKey = potentialApiKey.ToString();
        var user = _context.User.SingleOrDefault(u => u.Key == apiKey);

        if (user == null)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
