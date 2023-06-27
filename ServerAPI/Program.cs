using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerAPI;
using ServerAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigServices(builder);
ConfigurationAuthentication(builder);
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


void ConfigurationAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(c =>
    {
        c.RequireHttpsMetadata = false;
        c.SaveToken = true;
        c.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JWTKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();

void ConfigServices(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(x=> x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira Token JWT",
        Name = "Authorização",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    }));
    builder.Services.AddSwaggerGen(s =>
    s.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }));
    builder.Services.AddTransient<TokenServices>();
    builder.Services.AddTransient<EmailServices>();
    builder.Services.AddControllers();
}


Configuration.JWTKey = app.Configuration.GetValue<string>(key: "JWTKey");
Configuration.ApiKeyName = app.Configuration.GetValue<string>(key: "ApiKeyName");
Configuration.ApiKey = app.Configuration.GetValue<string>(key: "ApiKey");

var Smtp = new Configuration.SmtpConfiguration();
app.Configuration.GetSection(key: "Smtp").Bind(Smtp);
