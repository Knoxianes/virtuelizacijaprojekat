using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        public static  string odgovor = "";
        static void Main(string[] args)
        {
            
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\tKoje fajlove zelite da uneste:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t  1. Fajlove sa prognoziranim vrednostima");
                Console.WriteLine("\t  2. Fajlove sa ostvarenim vrednostima");
                Console.WriteLine("\t  0. Izlaz");
                if (odgovor != "")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\tServer: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(odgovor);
                    Console.ForegroundColor = ConsoleColor.White;
                    
                }
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        ForecastChosen();
                        break;
                    case '2':
                        Console.Clear();
                        MeasuredChosen();
                        break;
                    case '0':
                        return;
                    default:
                        odgovor = "Niste pritisnuli dobar broj probajte opet";
                        Console.Clear();
                        break;
                }
            }
            while (true);

        }
         
        private static void MeasuredChosen()
        {
            string path;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tUnesite absolutnu putanju do direktorijuma u kojem se nalaze fajlovi sa izmerenim vrednostima: ");
                Console.ForegroundColor = ConsoleColor.White;
                path = Console.ReadLine();
                if (path == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tNiste uneli dobru putanju\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
               
            } while (path == null);
            bool error = Helper.LoadData(path, FileType.OSTVARENO);
            if (!error)
            {
                odgovor = "Doslo je do greske, fajl nije lepo imenovan ili direktorijum je prazan!\n\t\t Proverite da li ste dobru putanju do direktorijuma uneli!";
                Console.Clear();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tServer pocinje proracunavanje !");
            Helper.Calculate();
            odgovor = "Uspesno izracunato";
            Console.Clear();

        }

        private static void ForecastChosen()
        {
            string path;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tUnesite absolutnu putanju do direktorijuma u kojem se nalaze fajlovi sa prognoziranim vrednostima: ");
                Console.ForegroundColor = ConsoleColor.White;
                path = Console.ReadLine();
                if (path == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tNiste uneli dobru putanju!\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (path == null);
            bool error = Helper.LoadData(path, FileType.PROGNOZIRANO);
            if (!error)
            {
                odgovor = " Doslo je do greske, fajl nije lepo imenovan ili direktorijum je prazan!\n\t\t Proverite da li ste dobru putanju do direktorijuma uneli!";
                Console.Clear();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tServer pocinje proracunavanje !");
            Helper.Calculate();
            odgovor = "Uspesno izracunato";
            Console.Clear();
        }
    }
}
