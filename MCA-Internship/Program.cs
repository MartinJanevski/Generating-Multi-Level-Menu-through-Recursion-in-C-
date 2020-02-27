using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MCA_Internship
{
    class Program
    {
        public static List<MenuItem> Menu;
        static void Main(string[] args)
        {
            ReadCSVFile();
            PrintMenu();
        }
        public static void ReadCSVFile()
        {
            Menu = new List<MenuItem>();
            using (var reader = new StreamReader("C:\\Users\\Martin\\Desktop\\MCA\\navigationMenu.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new MenuItem
                    {
                        ID = csv.GetField<string>("ID"),
                        MenuName = csv.GetField<string>("MenuName"),
                        ParentID = csv.GetField<string>("ParentID"),
                        isHidden = csv.GetField<bool>("isHidden")
                    };
                    Menu.Add(record);
                }
            }
        }
        public static void PrintMenu()
        {
            var firstLevelMenuItems = Menu.Where(x => x.ParentID == "NULL").OrderBy(x => x.MenuName).ToList();
            firstLevelMenuItems.ForEach(x => PrintMenuItem(x, 1));
            void PrintMenuItem(MenuItem menuItem, int n)
            {
                if (menuItem.isHidden) return;
                Console.WriteLine(MenuItemPrefix(n) + menuItem.MenuName);
                var children = Menu.Where(x => x.ParentID == menuItem.ID).OrderBy(x => x.MenuName);
                if (children.Count() > 0)
                {
                    foreach (var child in children)
                        PrintMenuItem(child, n + 1);
                }
            }
        }
        

        public static string MenuItemPrefix(int n)
        {
            var result = "";
            var numberOfDots = (n - 1) * 3 + 1;
            for (int i = 0; i < numberOfDots; i++)
            {
                result += ".";
            }
            return result;
        }

    }

}