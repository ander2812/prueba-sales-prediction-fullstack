using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using SalesDatePrediction.Application.Interfaces.Common;
using SalesDatePrediction.Application.Interfaces.Services;
using SalesDatePrediction.Application.Mappings;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Infrastructure.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

IDependencyRegister dependencyRegister = new DependencyRegister();
dependencyRegister.RegisterDependencies(builder.Configuration, builder.Services);

builder.Services.AddAutoMapper(typeof(CustomerProfile).Assembly);
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IShipperService, ShipperService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IEmployeeService, EmployeeService>()
    .AddScoped<IOrderDetailService, OrderDetailService>()
    .AddScoped<ICustomerOrderService, CustomerOrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:7200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "¡API corriendo correctamente!");

app.Run();

