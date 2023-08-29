using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(DataBaseServis));
            svc.Open();
            Console.WriteLine("BazaPodataka je pokrenut");
            Console.ReadLine();
        }
    }
}
