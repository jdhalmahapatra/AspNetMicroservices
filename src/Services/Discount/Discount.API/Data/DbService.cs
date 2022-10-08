using System;
using System.Data;
using Dapper;
using Npgsql;

namespace Discount.API.Data
{
    public class DbService : IDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _db;

        public DbService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _db = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
        }

        //Dapper function - QueryAsync and ExecuteAsync
        //QueryAsync - Used for SELECT based statement internally
        //ExecuteAsync - USed of DML(UPDATE, INSERT, DELETE) based statement internally

        public async Task<int> EditData(string command, object parms)
        {
            int result;

            result = await _db.ExecuteAsync(command, parms);

            return result;
        }

        public async Task<List<T>> GetAll<T>(string command, object parms)
        {
            List<T> result = new List<T>();

            result = (await _db.QueryAsync<T>(command, parms)).ToList();

            return result;
        }

        public async Task<T> GetAsync<T>(string command, object parms)
        {
            T? result;

            result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();

            return result;
        }
    }
}

