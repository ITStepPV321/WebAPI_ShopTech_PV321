using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI_ShopTech_PV321.Core.Helpers;
using WebAPI_ShopTech_PV321.Core.Interfaces;
using WebAPI_ShopTech_PV321.Core.Sevices;
using WebAPI_ShopTech_PV321.Infrastructure;
using WebAPI_ShopTech_PV321.Infrastructure.Data;
using WEBAPI_ShopTech_PV321.Core.Interfaces;
using WEBAPI_ShopTech_PV321.Core.Sevices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("ShopTechDbConnectionAzure");
//builder.Services.AddDbContext<ShopTechAPI_PV321>(options => {
//    options.UseSqlServer(connection);
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});


builder.Services.AddDbContextCustom(connection);
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options=>options.SignIn.RequireConfirmedAccount=true).
//    AddEntityFrameworkStores<ShopTechAPI_PV321>();
builder.Services.AddIdentityCustom();
builder.Services.AddAutoMapper(typeof(MapperProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        //option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    }
    );
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
//add jwt 
//builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
       ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    }

    ));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
