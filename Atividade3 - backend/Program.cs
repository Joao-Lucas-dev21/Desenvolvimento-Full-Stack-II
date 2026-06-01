using Atividade.Aplicacao.Serviços;
using Atividade.Dominio.Repositorios;
using Atividade.Infra;
using Atividade.Infra.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AtividadePolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}) 
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPedidoRepositorio, PedidoRespositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

builder.Services.AddScoped<IPedidoServico, PedidoServico>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
//Cors
app.UseCors("AtividadePolicy");

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    try
    {
        var useManager = service.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        SeedData(useManager, roleManager).Wait();

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
    }
}

    app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    var emailAdmin = "admin@filmes.com";
    if (await userManager.FindByEmailAsync(emailAdmin) == null)
    {
        var user = new IdentityUser
        {
            UserName = emailAdmin,
            Email = emailAdmin,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, "Admin@123456");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    var emailUser = "user@filmes.com";
    if (await userManager.FindByEmailAsync(emailUser) == null)
    {
        var user = new IdentityUser
        {
            UserName = emailUser,
            Email = emailUser,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, "User@123456");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }

    }



}