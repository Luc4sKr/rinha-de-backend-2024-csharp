using RinhaDeBackend;
using RinhaDeBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsqlDataSource(
    Environment.GetEnvironmentVariable(
        "Host=localhost;Username=postgres;Password=admin;Database=rinha;Minimum Pool Size=10;Maximum Pool Size=10;Multiplexing=true;Application Name=RinhaDeBackendDotnet") ??
        "ERRO de connection string!");

string _conn = "Host=localhost;Username=postgres;Password=admin;Database=rinha;Minimum Pool Size=10;Maximum Pool Size=10;Multiplexing=true;Application Name=RinhaDeBackendDotnet";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// TODO
app.MapPost("/clientes/{id}/transacoes", async (int id, TransacaoRequest transacaoRequest) =>  await Endpoints.Transacoes(id, transacaoRequest, _conn));
app.MapGet("/clientes/{id}/extrato", async (int id) => await Endpoints.Extrato(id, _conn)).Produces<string>();

app.Run();
