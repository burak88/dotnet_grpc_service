using System.Reflection;
using grpcService.Mapping;
using grpcService.Models;
using grpcService.Services;
using grpcService.Services.OrderGrpcServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyMethod()
                                                                             .AllowAnyHeader()
                                                                             .AllowAnyOrigin()));

var dbFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
if (!Directory.Exists(dbFolderPath)) Directory.CreateDirectory(dbFolderPath);

var connectionString = $"Data Source={Path.Combine(dbFolderPath, "grpcService.db")};";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Register Order Service
builder.Services.AddScoped<IOrderService, OrderService>();

//Register AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// builder.Services.AddAutoMapper(typeof(OrderMapping));


// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddGrpcSwagger();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "gRPC Service",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
// Configure the HTTP request pipeline.
app.MapGrpcService<OrderGrpcServices>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
