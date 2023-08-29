using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Unesite putanju do direktirojuma sa datotekama koje trebaju da se ucitaju:");
                string path = Console.ReadLine();
                if (path == null)
                {
                    Console.WriteLine("Niste uneli dobru putanju");
                }
                Helper.LoadData(path);
            }
        }
    }
}
