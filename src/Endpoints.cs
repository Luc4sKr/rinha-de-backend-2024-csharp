using System.Transactions;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using RinhaDeBackend.Models;

namespace RinhaDeBackend
{
    public static class Endpoints
    {
        private static readonly int[] user_ids = new int[] { 1, 2, 3, 4, 5 };

        public static async Task<IResult> Transacoes(int id, TransacaoRequest transacaoRequest, string conn)
        {
            if (!user_ids.Contains(id))
                return Results.NotFound();

            await using (var connection = new NpgsqlConnection(conn))
            {
                await connection.OpenAsync();
                await using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlSaldo =
                            $"SELECT total, limite, CURRENT_DATE AS data_extrato FROM saldos WHERE user_id = {id}";
                        var saldo = await connection.QueryFirstOrDefaultAsync<SaldoResponse>(sqlSaldo);

                        if (!ValidarTransacao(transacaoRequest, saldo))
                            return Results.UnprocessableEntity();

                        var sqlInsert =
                            $@"INSERT INTO transactions (user_id, valor, tipo, descricao, realizada_em) 
                                    VALUES (@UserId, @Valor, @Tipo, @Descricao, @RealizadaEm)";
                        var parametros = new { UserId = id, Valor = transacaoRequest.valor, Tipo = transacaoRequest.tipo, Descricao = transacaoRequest.descricao, RealizadaEm = DateTime.Now };
                        await connection.ExecuteAsync(sqlInsert, parametros, transaction);
                        
                        // Calcular novo saldo
                        var novoSaldo = CalcularNovoSaldo(saldo.total, transacaoRequest.valor, transacaoRequest.tipo);

                        // Atualizar o saldo do cliente na tabela saldos
                        var sqlUpdateSaldo = "UPDATE saldos SET total = @NovoSaldo WHERE user_id = @UserId";
                        await connection.ExecuteAsync(sqlUpdateSaldo, new { novoSaldo, UserId = id }, transaction);

                        transaction.Commit();
                        
                        return Results.Ok(new { Limite = saldo.limite, Saldo = novoSaldo });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Results.NotFound(ex.Message);
                    }
                }
            }
        }

        public static async Task<IResult> Extrato(int id, string conn)
        {
            if (!user_ids.Contains(id))
                return Results.NotFound();

            await using (var connection = new NpgsqlConnection(conn))
            {
                try
                {
                    var sqlSaldo =
                        $"SELECT total, limite, CURRENT_DATE AS data_extrato FROM saldos WHERE user_id = {id}";
                    var saldo = await connection.QueryFirstOrDefaultAsync<SaldoResponse>(sqlSaldo);

                    var sqlTransacoes =
                        $"SELECT valor, tipo, descricao, realizada_em FROM transactions WHERE user_id = {id} ORDER BY id DESC LIMIT 10";
                    var transacoes = await connection.QueryAsync<UltimasTransacoesResponse>(sqlTransacoes);
                    return Results.Ok(new ExtratoResponse(saldo, transacoes.ToList()));
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            }
        }

        #region methods
        private static bool ValidarTransacao(TransacaoRequest request, SaldoResponse saldo)
        {
            if (request.tipo == 'd' && saldo.total - request.valor < -saldo.limite)
                return false;

            return true;
        }
        
        private static decimal CalcularNovoSaldo(decimal saldoAtual, decimal valorTransacao, char tipoTransacao)
        {
            if (tipoTransacao == 'c')
                return saldoAtual + valorTransacao;
            if (tipoTransacao == 'd')
                return saldoAtual - valorTransacao;

            return saldoAtual;
        }
        #endregion
    }
}