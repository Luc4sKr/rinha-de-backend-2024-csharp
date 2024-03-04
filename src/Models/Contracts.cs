namespace RinhaDeBackend.Models;

public record SaldoResponse(int total, int limite, DateTime? data_extrato);
public record TransacaoResponse(int valor, char tipo, string descricao, DateTime realizada_em);
public record ExtratoResponse(SaldoResponse saldo, IList<TransacaoResponse> transacoes);