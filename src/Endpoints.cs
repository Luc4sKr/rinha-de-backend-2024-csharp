using Dapper;
using Npgsql;
using RinhaDeBackend.Models;
using Transaction = System.Transactions.Transaction;

namespace RinhaDeBackend
{
    public static class Endpoints
    {
        private static int[] user_ids = new int[] {1, 2, 3, 4, 5};
        
        public static async Task<Transaction?> Transacoes(int id, string conn)
        {
            using (var connection = new NpgsqlConnection(conn))
            {
                var sql = $"SELECT * FROM transaction WHERE user_id = id";     
                return await connection.QueryFirstOrDefaultAsync<Transaction>(sql);
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
                var transacoes = await connection.QueryAsync<TransacaoResponse>(sqlTransacoes);
                
                return Results.Ok(new ExtratoResponse(saldo, transacoes.ToList()));
            }
        }
    }
}