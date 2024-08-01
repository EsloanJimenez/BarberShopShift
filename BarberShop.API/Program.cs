using BarberShop.Infrastructure.Context;
using BarberShop.IOC.Dependencies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//CONFIGURAR CORS
builder.Services.AddCors(op =>
{
    op.AddPolicy("AllowVueApp",
        policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Add services to the container.

#region "Region del contexto"
var pr = builder.Configuration.GetConnectionString("BarberShop");

builder.Services.AddDbContext<BarberShopContext>(op => op.UseMySQL(pr));
#endregion

#region "Registro de dependencias"
builder.Services.AddBarberDependency();
builder.Services.AddCustomerDependency();
builder.Services.AddShiftDependency();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowVueApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
