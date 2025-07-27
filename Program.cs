using BoutiqueApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Smockerie.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueApp", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// 2) JWT configuration (lit directement dans appsettings.json)
var key = builder.Configuration["Jwt:Key"]!;
var issuer = builder.Configuration["Jwt:Issuer"]!;
var audience = builder.Configuration["Jwt:Audience"]!;
var expiresInMinutes = int.Parse(builder.Configuration["Jwt:ExpiresInMinutes"]!);

// 3) Authentification JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// 4) Autorisation
builder.Services.AddAuthorization();

// 5) Controllers, Swagger, EF Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
app.UseSwagger();
app.UseSwaggerUI();
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Entrer **Bearer <token>**"
    });

    // Applique la sécurité globalement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    });
});

builder.Services.AddDbContext<BoutiqueContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33)),
        mysqlOpts => mysqlOpts.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);
builder.Services.AddScoped<Smockerie.Services.IOrderProductService, Smockerie.Services.OrderProductService>();
builder.Services.AddScoped<Smockerie.Services.IOrderService, Smockerie.Services.OrderService>();
builder.Services.AddScoped<Smockerie.Services.IProductService, Smockerie.Services.ProductService>();
builder.Services.AddScoped<Smockerie.Services.IUserService, Smockerie.Services.UserService>();
builder.Services.AddScoped<Smockerie.Services.IUserManagementService, Smockerie.Services.UserManagementService>();
builder.Services.AddScoped<Smockerie.Services.ICategoryService, Smockerie.Services.CategoryService>();



var app = builder.Build();

// 6) Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("VueApp");
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
