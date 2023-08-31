using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unesite putanju do direktirojuma sa datotekama koje trebaju da se ucitaju forecast:");
            string path = Console.ReadLine();
            if (path == null)
            {
                Console.WriteLine("Niste uneli dobru putanju");
            }
            Helper.LoadData(path,FileType.PROGNOZIRANO);

            Console.WriteLine("Unesite putanju do direktirojuma sa datotekama koje trebaju da se ucitaju measured:");
            string path2 = Console.ReadLine();
            if (path == null)
            {
                Console.WriteLine("Niste uneli dobru putanju");
            }
            Helper.LoadData(path2, FileType.OSTVARENO);

            Console.WriteLine("Pristupamo proracunu!");
            Helper.Calculate();
            Console.WriteLine("Zavrsen proracun i azurirana je baza podataka");

            Console.ReadLine();

        }
    }
}
