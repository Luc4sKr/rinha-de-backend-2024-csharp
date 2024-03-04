using Dapper;
using Npgsql;
using RinhaDeBackend.Models;

namespace RinhaDeBackend
{
    public static class Endpoints
    {
        private static int[] user_ids = new int[] {1, 2, 3, 4, 5};
        
        public static async Task<IResult> Transacoes(int id, TransacaoRequest transacaoRequest, string conn)
        {
            if (!user_ids.Contains(id))
                return Results.NotFound();
            
            using (var connection = new NpgsqlConnection(conn))
            {
                var sqlSaldo = $"SELECT * FROM saldo WHERE user_id = id";
                var saldo = await connection.QueryFirstOrDefaultAsync<SaldoResponse>(sqlSaldo);
                
                if (transacaoRequest.tipo ==  'd')
                {
                    if (saldo.total - transacaoRequest.valor < saldo.limite)
                        return Results.UnprocessableEntity();
                }

                return Results.Ok();
            }
        }

        public static async Task<IResult> Extrato(int id, string conn)
        {
            if (!user_ids.Contains(id))
                return Results.NotFound();
            
            using (var connection = new NpgsqlConnection(conn))
            {
                var sqlSaldo = $"SELECT total, limite, CURRENT_DATE AS data_extrato FROM saldos WHERE user_id = {id}";  
                var saldo = await connection.QueryFirstOrDefaultAsync<SaldoResponse>(sqlSaldo);

                var sqlTransacoes = $"SELECT valor, tipo, descricao, realizada_em FROM transactions WHERE user_id = {id} ORDER BY id DESC LIMIT 10";
                var transacoes = await connection.QueryAsync<UltimasTransacoesResponse>(sqlTransacoes);
                
                return Results.Ok(new ExtratoResponse(saldo, transacoes.ToList()));
            }
        }
    }
}