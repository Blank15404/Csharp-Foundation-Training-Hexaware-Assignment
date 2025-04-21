using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CARS_CaseStudy.util
{
    public static class DBPropertyUtil
    {
        public static string GetPropertyString(string propertyFileName)
        {
            return @"Server=DESKTOP-F473ICG\SQLEXPRESS;Database=CrimeAnalysisDB;Integrated Security=True;";
        }
    }
}