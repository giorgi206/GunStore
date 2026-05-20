// ── პაკეტები ──────────────────────────────────
using GunShop.Data;
using GunShop.Services;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── DB Context ────────────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ── Services ──────────────────────────────────
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IKnifeService, KnifeService>();
builder.Services.AddScoped<IAmmunitionService, AmmunitionService>();

// ── CORS ── ახალი ─────────────────────────────

builder.Services.AddCors(options =>

{

    options.AddPolicy("AllowFrontend", policy =>

    {

        policy.WithOrigins(

                "http://localhost:3000",   // React CRA

                "http://localhost:5173",   // Vite / React

                "http://localhost:4200"    // Angular

              )

              .AllowAnyHeader()

              .AllowAnyMethod();

    });

});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ── Middleware Pipeline ───────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend"); // ← UseAuthorization()-ის წინ!ახალი


app.UseAuthorization();
app.MapControllers();
app.Run();