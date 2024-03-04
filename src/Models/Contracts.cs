namespace RinhaDeBackend.Models;

public record SaldoResponse(int total, int limite, DateTime? data_extrato);
public record UltimasTransacoesResponse(int valor, char tipo, string descricao, DateTime realizada_em);
public record ExtratoResponse(SaldoResponse saldo, IList<UltimasTransacoesResponse> transacoes);

public record TransacaoRequest(int valor, char tipo, string descricao);
public record TransacaoResponse(int limite, int saldo);