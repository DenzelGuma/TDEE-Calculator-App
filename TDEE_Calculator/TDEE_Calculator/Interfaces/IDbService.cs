using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TDEE_Calculator.Models;

namespace TDEE_Calculator.Interfaces
{
    public interface IDbService
    {
        Task<List<Tdee_stats>> GetStatsAsync();

        Task<int> SaveStatsAsync(Tdee_stats tdee_stats);
    }
}
