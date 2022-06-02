using System;

using SQLite;

namespace TDEE_Calculator.Models
{
    public class Tdee_stats
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string LastUpdated { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int ActivtyPW { get; set; }
        public int Tdee { get; set; }
    }
}
