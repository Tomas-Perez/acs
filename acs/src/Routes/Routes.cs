using System;
using Nancy;
using Nancy.Hosting.Self;

namespace acs.Routes
{
    public sealed class Routes: NancyModule
    {
        
        public Routes()
        {
            Get("/check",
                _ => HttpStatusCode.OK);
            
        }
    }

   
}