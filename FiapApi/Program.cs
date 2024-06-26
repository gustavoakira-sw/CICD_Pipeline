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

// Registra o serviço de filtro da API key
builder.Services.AddScoped<ApiKeyAuthFilter>();

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

app.Run();