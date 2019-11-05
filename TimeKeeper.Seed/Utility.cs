using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Seed
{
    public static class Utility
    {
        public static Dictionary<string, int> dicTeam = new Dictionary<string, int>();
        public static Dictionary<string, int> dicRole = new Dictionary<string, int>();
        public static Dictionary<int, int> dicCust = new Dictionary<int, int>();
        public static Dictionary<int, int> dicProj = new Dictionary<int, int>();
        public static Dictionary<int, int> dicProjStatus = new Dictionary<int, int>();
        public static Dictionary<int, int> dicProjPricing = new Dictionary<int, int>();
        public static Dictionary<int, int> dicEmpl = new Dictionary<int, int>();
        public static Dictionary<string, int> dicEmpPosition = new Dictionary<string, int>();
        public static Dictionary<int, int> dicDays = new Dictionary<int, int>();

        public static string ReadString(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                return sht.Cells[row, col].Value.ToString().Trim();
            }
            catch
            {
                return "";
            }
        }

        public static int ReadInteger(this ExcelWorksheet sht, int row, int col) => int.Parse(sht.ReadString(row, col));

        public static DateTime ReadDate(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                var data = sht.Cells[row, col].Value;
                if (data == null) return DateTime.MinValue;
                return DateTime.FromOADate(double.Parse(data.ToString()));
                //return DateTime.Parse(sht.ReadString(row, col));
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }
        }

        public static bool ReadBool(this ExcelWorksheet sht, int row, int col) => sht.ReadString(row, col) == "-1";

        public static decimal ReadDecimal(this ExcelWorksheet sht, int row, int col) => decimal.Parse(sht.ReadString(row, col));
    }
}
