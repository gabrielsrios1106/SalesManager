using Microsoft.EntityFrameworkCore;
using SalesManager.API.Automapper;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;
using SalesManager.API.Services;

var builder = WebApplication.CreateBuilder(args);

string APICorsPolicy_Production = "APICorsPolicy_Production";
string APICorsPolicy_Development = "APICorsPolicy_Development";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SalesManagerContext>(options =>
        options.UseSqlite(
            builder.Configuration.GetConnectionString("SQLiteConnection")
        )
    );

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<IFinancialManagerService, FinancialManagerService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutomapperProfile>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: APICorsPolicy_Development,
        builder =>
        {
            builder.WithOrigins("https://localhost:44312", "https://localhost:7115")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: APICorsPolicy_Production,
        builder =>
        {
            builder.WithOrigins("https://jolly-desert-0b552050f.2.azurestaticapps.net")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(APICorsPolicy_Development);
}
else if (app.Environment.IsProduction())
{
    app.UseCors(APICorsPolicy_Production);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
