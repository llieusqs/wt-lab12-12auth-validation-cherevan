using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_cherevan.Data;

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// БЛОК 1. Подключение к БД (Локально и на Render)
// =========================================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseUrl = builder.Configuration["DATABASE_URL"]; // Читаем переменную Render

// Если мы на Render, переделываем URL в строку подключения
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    int dbPort = uri.Port > 0 ? uri.Port : 5432; 

    connectionString =
        $"Host={uri.Host};" +
        $"Port={dbPort};" +
        $"Database={uri.AbsolutePath.TrimStart('/')};" +
        $"Username={Uri.UnescapeDataString(userInfo[0])};" +
        $"Password={Uri.UnescapeDataString(userInfo[1])};" +
        "SSL Mode=Disable"; // На Render (внутри) SSL обычно не нужен
}

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

// Настройка порта для Docker/Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddSession();
builder.Services.AddRazorPages();

var app = builder.Build();

// =========================================================
// БЛОК 2. Автоматическое применение миграций
// =========================================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Создаст таблицу RazorTasks, если её нет
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();