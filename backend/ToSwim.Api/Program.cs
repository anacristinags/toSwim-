using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;
using ToSwim.Api.Middleware;
using ToSwim.Application.Interfaces;
using ToSwim.Application.Services;
using ToSwim.Application.Settings;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Data.Interceptors;
using ToSwim.Infrastructure.Repositories;
using ToSwim.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CONFIGURAÇÃO DE TIMEZONE E POSTGRES
// ==========================================
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var timeZoneId = builder.Configuration["TimeZone"] ?? "America/Sao_Paulo";
Environment.SetEnvironmentVariable("TZ", timeZoneId);

// ==========================================
// 2. INJEÇÃO DO DBCONTEXT (EF CORE)
// ==========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<AuditableEntityInterceptor>();
builder.Services.AddDbContext<ToSwimDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.AddInterceptors(new AuditableEntityInterceptor());
});

// ==========================================
// 3. CONFIGURAÇÃO DO JWT E AUTENTICAÇÃO
// ==========================================
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddAuthorization();

// ==========================================
// 4. REGISTRO DOS REPOSITÓRIOS E SERVIÇOS
// ==========================================
// Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IConfigPiscinaRepository, ConfigPiscinaRepository>();
builder.Services.AddScoped<IFichaBaseRepository, FichaBaseRepository>();
builder.Services.AddScoped<ISerieFichaRepository, SerieFichaRepository>();
builder.Services.AddScoped<IMetaRepository, MetaRepository>();
builder.Services.AddScoped<ITreinoRepository, TreinoRepository>(); 
builder.Services.AddScoped<ISerieTreinoRepository, SerieTreinoRepository>();
builder.Services.AddScoped<IRepeticaoSerieTreinoRepository, RepeticaoSerieTreinoRepository>();
builder.Services.AddScoped<ITreinoMetaRepository, TreinoMetaRepository>();

// Serviços
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IConfigPiscinaService, ConfigPiscinaService>();
builder.Services.AddScoped<IFichaBaseService, FichaBaseService>();
builder.Services.AddScoped<ISerieFichaService, SerieFichaService>();
builder.Services.AddScoped<IMetaService, MetaService>();
builder.Services.AddScoped<ITreinoService, TreinoService>();
builder.Services.AddScoped<IRepeticaoSerieTreinoService, RepeticaoSerieTreinoService>();
builder.Services.AddScoped<ITreinoMetaService, TreinoMetaService>();
builder.Services.AddScoped<IMetricasService, MetricasService>();

// ==========================================
// 5. CONTROLLERS E SWAGGER (SIMPLIFICADO)
// ==========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Digite: Bearer {seu token JWT}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ==========================================
// 6. MIDDLEWARES E PIPELINE DE EXECUÇÃO
// ==========================================
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "toSwim API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();