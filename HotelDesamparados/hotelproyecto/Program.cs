using hotelproyecto.Data;
using hotelproyecto.Service;
using hotelproyecto.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registro de dependencias de ADO.NET
builder.Services.AddScoped<ConexionDB>();
builder.Services.AddScoped<RolData>();
builder.Services.AddScoped<UsuarioData>();
builder.Services.AddScoped<TokenRecuperacionData>();
builder.Services.AddScoped<UbicacionProductoData>();
builder.Services.AddScoped<ProductoData>();
builder.Services.AddScoped<EmpleadoData>();
builder.Services.AddScoped<HabitacionData>();
builder.Services.AddScoped<LimpiezaHabitacionData>();
builder.Services.AddScoped<ReservaData>();
builder.Services.AddScoped<ContabilidadData>();
builder.Services.AddScoped<ReporteriaData>();


// Registro de servicios 
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenRecuperacionService>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<UbicacionProductoService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<HabitacionService>();
builder.Services.AddScoped<LimpiezaHabitacionService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<ContabilidadService>();
builder.Services.AddScoped<ReporteriaService>();


// Tiempo de sesión y autenticación
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Middleware para manejar sesiones y autenticación
app.UseSession();         // Sesión primero
app.UseAuthentication();  // Luego autenticación
app.UseAuthorization();   // Después autorización

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
app.Run();
