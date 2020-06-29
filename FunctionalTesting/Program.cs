using System;
using Yahoo.Finance;
using Yahoo.Finance.Printing;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string take_from = "C:\\Users\\tihanewi\\Downloads\\06282020";
            string contenttt = System.IO.File.ReadAllText(take_from);
            Equity[] allequities = JsonConvert.DeserializeObject<Equity[]>(contenttt);


            string content = allequities.PrintToCsvContent();

            string path = "C:\\Users\\tihanewi\\Downloads\\WriteHere.csv";
            System.IO.File.WriteAllText(path, content);
            Console.WriteLine("Done");

            
        }
    }
}
