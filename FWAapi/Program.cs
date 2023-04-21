using FWAapi.Business;
using FWAapi.DbAccess;
using FWAapi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Business classes
var types = Assembly.GetExecutingAssembly().GetTypes().Where(o => o.BaseType == typeof(Business));
foreach (Type type in types)
{
    builder.Services.AddScoped(type);
}

builder.Services.AddScoped<DbContextFactory>();


builder.Services.AddScoped<IBusinessProvider>(serviceProvider =>
{
    Business BusinessFactory(Type type)
    {
        Business b = (Business)serviceProvider.GetService(type);
        b.SetDbContextFactory(serviceProvider.GetService<DbContextFactory>());
        return b;
    }

    return new BusinessProvider(BusinessFactory);
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
