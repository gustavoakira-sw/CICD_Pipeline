using FiapApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapApi.Services;

public abstract class DatabaseConnectionTester(AppDbContext context, ILogger<DatabaseConnectionTester> logger)
{
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            logger.LogInformation("Testando conexão com banco de dados...");
            await context.Database.OpenConnectionAsync();
            await context.Database.CloseConnectionAsync();
            logger.LogInformation("Conexão concluída com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha na conexão.");
            return false;
        }
    }
}