using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(Servis));
            svc.Open();
            Console.WriteLine("Server je pokrenut");
            Console.ReadLine();
        }
    }
}
