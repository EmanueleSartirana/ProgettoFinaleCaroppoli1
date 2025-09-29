using ProgettoFinaleCaroppoli1.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrazione dei servizi
builder.Services.AddScoped<FileStorageService>();
builder.Services.AddScoped<CsvParserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();