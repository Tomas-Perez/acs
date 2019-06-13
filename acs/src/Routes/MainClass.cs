using System;
using Nancy.Hosting.Self;

namespace acs.Routes
{
    public static class MainClass
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1234");
                Console.ReadLine();
            }
//            Console.WriteLine("Hello");
        }
    }
}