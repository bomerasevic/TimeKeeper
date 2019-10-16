﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Customers
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Customers: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Customer c = new Customer
                {
                    Name = rawData.ReadString(row, 2),
                    Image = rawData.ReadString(row,3),
                    Contact = rawData.ReadString(row,4),
                    Email = rawData.ReadString(row,5),
                    Phone = rawData.ReadString(row,6),
                    //Status = (CustomerStatus)rawData.ReadInteger(row,10) ERROR OVDJE
                };
                c.Address.Road = rawData.ReadString(row, 7);
                c.Address.ZipCode = rawData.ReadString(row, 8);
                c.Address.City = rawData.ReadString(row, 9);
                c.Address.Country = rawData.ReadString(row, 9);

                unit.Customers.Insert(c);
                unit.Save();
                Utility.dicCust.Add(oldId, c.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
