using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SLAD.DataLib
{
    public class DataAccess
    {
        public async static Task<List<T>> LoadData<T, U>(string sql, U parameters, string cnnStr)
        {
            using (IDbConnection connection = new SQLiteConnection(cnnStr))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }

        public async static Task SaveData<T>(string sql, T parameters, string cnnStr)
        {
            using (IDbConnection connection = new SQLiteConnection(cnnStr))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}