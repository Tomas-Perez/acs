using System;
using acs.Db;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;

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
                host.Stop();
            }
//            Console.WriteLine("Hello");
        }
    }
}