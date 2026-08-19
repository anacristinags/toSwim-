using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
// 2. CONFIGURAÇÃO DE CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ==========================================
// 3. INJEÇÃO DO DBCONTEXT (EF CORE)
// ==========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' não foi configurada. " +
        "Defina em appsettings.Development.json ou User Secrets.");
}

builder.Services.AddSingleton<AuditableEntityInterceptor>();
builder.Services.AddDbContext<ToSwimDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.AddInterceptors(new AuditableEntityInterceptor());
});

// ==========================================
// 4. CONFIGURAÇÃO DO JWT E AUTENTICAÇÃO
// ==========================================
var jwtSection = builder.Configuration.GetSection("Jwt");

if (!jwtSection.Exists())
{
    throw new InvalidOperationException(
        "A seção 'Jwt' não foi encontrada na configuração da API.");
}

var jwtSettings = new JwtSettings();
jwtSection.Bind(jwtSettings);

if (string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException(
        "A chave JWT não foi configurada. Defina 'Jwt:Key' usando User Secrets ou appsettings.");
}

if (Encoding.UTF8.GetByteCount(jwtSettings.Key) < 32)
{
    throw new InvalidOperationException(
        "A chave JWT deve possuir pelo menos 32 bytes.");
}

if (string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
    string.IsNullOrWhiteSpace(jwtSettings.Audience))
{
    throw new InvalidOperationException(
        "Jwt:Issuer e Jwt:Audience também precisam ser configurados.");
}

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
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddAuthorization();

// ==========================================
// 5. REGISTRO DOS REPOSITÓRIOS E SERVIÇOS
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
// 6. CONTROLLERS E SWAGGER
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

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// ==========================================
// 7. MIDDLEWARES E PIPELINE DE EXECUÇÃO
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

// O Cors deve ser ativado ANTES da autenticação
app.UseCors("AllowVueApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();