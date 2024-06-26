using FiapApi.Data;
using FiapApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração do Entity Framework e MySQL Server
builder.Services.AddDbContext<AppDbContext>(options => { options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")); });

// Registra o serviço de teste da conexão com DB
// builder.Services.AddTransient<DatabaseConnectionTester>();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Test the database connection
// using (var scope = app.Services.CreateScope())
// {
//     var tester = scope.ServiceProvider.GetRequiredService<DatabaseConnectionTester>();
//     var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//     
//     var connectionSuccessful = await tester.TestConnectionAsync();
//     
//     if (!connectionSuccessful)
//     {
//         logger.LogError("Unable to establish a database connection.");
//         return; // Optionally exit the application or handle the error accordingly
//     }
//     else
//     {
//         logger.LogInformation("Database connection established successfully.");
//     }
// }

app.Run();