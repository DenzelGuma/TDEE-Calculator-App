using System;
using System.IO;

namespace TDEE_Calculator
{
    public static class Constants
    {
        public static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "stats.db3");
    }
}
