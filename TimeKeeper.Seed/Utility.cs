using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Seed
{
    public static class Utility
    {
        public static Dictionary<int, int> dayDictionary = new Dictionary<int, int>();  // <stara vrijednost Id-a, nova vrijednost Id-a>
        public static Dictionary<int, int> customerDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> employeeDictionary = new Dictionary<int, int>();
        public static Dictionary<int, int> projectDictionary = new Dictionary<int, int>();
        public static Dictionary<string, int> roleDictionary = new Dictionary<string, int>();
        public static Dictionary<string, int> teamDictionary = new Dictionary<string, int>();

        public static string ReadString(this ExcelWorksheet sht, int row, int col) => sht.Cells[row, col].Value.ToString().Trim();

        public static int ReadInteger(this ExcelWorksheet sht, int row, int col) => int.Parse(sht.ReadString(row, col));

        public static DateTime ReadDate(this ExcelWorksheet sht, int row, int col) => DateTime.Parse(sht.ReadString(row, col));

        public static decimal ReadDecimal(this ExcelWorksheet sht, int row, int col) => decimal.Parse(sht.ReadString(row, col));
    }
}
