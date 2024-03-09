using RinhaDeBackend;
using RinhaDeBackend.Models;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DB_CONNECTION_STRING") ?? "ERRO de connection string!";

builder.Services.AddNpgsqlDataSource(conn);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// TODO
app.MapPost("/clientes/{id}/transacoes", async (int id, TransacaoRequest transacaoRequest) =>  await Endpoints.Transacoes(id, transacaoRequest, conn));
app.MapGet("/clientes/{id}/extrato", async (int id) => await Endpoints.Extrato(id, conn)).Produces<string>();

app.Run();
