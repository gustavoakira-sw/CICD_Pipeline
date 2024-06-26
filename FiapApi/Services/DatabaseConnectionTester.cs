using FiapApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapApi.Services;

public abstract class DatabaseConnectionTester(AppDbContext context, ILogger<DatabaseConnectionTester> logger)
{
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            logger.LogInformation("Testing database connection...");
            await context.Database.OpenConnectionAsync();
            await context.Database.CloseConnectionAsync();
            logger.LogInformation("Database connection successful.");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database connection failed.");
            return false;
        }
    }
}