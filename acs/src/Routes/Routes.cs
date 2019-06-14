using System;
using acs.Db;
using acs.Model;
using acs.Repository;
using acs.Service;
using Nancy;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using Nancy.Json;
using Nancy.ModelBinding;

namespace acs.Routes
{
    public sealed class Routes: NancyModule
    {
        private UserService userService;
        
        public Routes(UserService userService)
        {   
            this.userService = userService;
            
            Get("/check",
                _ => HttpStatusCode.OK);
            
            /*
            Get("/users/{id}",
                _ => );
                */
            
            Post("/users",
                _ =>
                {
                    // Console.Write(Request.Body);
                    UserForm userForm = this.Bind();
                    userService.Register(userForm);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(userForm);
                    return HttpStatusCode.OK;
                });
        }
    }

   
}