using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;

namespace TDEE_Calculator.Services
{
    public class DbService : IDbService, IServiceOverridable
    {
        readonly SQLiteAsyncConnection _database;

        public DbService()
        {
            _database = new SQLiteAsyncConnection(Constants.DbPath);
            _database.CreateTableAsync<Tdee_stats>().Wait();
        }

        public Task<List<Tdee_stats>> GetStatsAsync()
        {
            return _database.Table<Tdee_stats>().ToListAsync();
        }

        public Task<int> SaveStatsAsync(Tdee_stats tdee_stats)
        {
            return _database.InsertAsync(tdee_stats);
        }
    }
}
