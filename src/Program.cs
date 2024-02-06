using RinhaDeBackend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsqlDataSource(
    Environment.GetEnvironmentVariable(
        "DB_CONNECTION_STRING") ??
        "ERRO de connection string!");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/get", () => Endpoints.Get()).Produces<string>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
