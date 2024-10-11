using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI_ShopTech_PV321.Core.Helpers;
using WebAPI_ShopTech_PV321.Core.Interfaces;
using WebAPI_ShopTech_PV321.Core.Sevices;
using WebAPI_ShopTech_PV321.Infrastructure;
using WebAPI_ShopTech_PV321.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("ShopTechDbConnection");
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
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
