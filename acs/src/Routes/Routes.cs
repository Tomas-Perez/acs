using System;
using acs.Db;
using acs.Exception;
using acs.Model;
using acs.Repository;
using acs.Service;
using Nancy;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using Nancy.Json;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.TinyIoc;

namespace acs.Routes
{
    public class Bootstrap : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register(new DatabaseContext());

            base.ConfigureApplicationContainer(container);
        }
    } 
    
    public sealed class Routes: NancyModule
    {
        private DatabaseContext _databaseContext;
        public Routes()
        {
            _databaseContext = new DatabaseContext();
            _databaseContext.Database.EnsureCreated();
            var userRepositoryInDb = new UserRepositoryInDb(_databaseContext);
            var groupRepositoryInDb = new GroupRepositoryInDB(_databaseContext);
            var userService = new UserService(userRepositoryInDb);
            var groupService = new GroupService(groupRepositoryInDb, userRepositoryInDb);
            
            Get("/check",
                _ => HttpStatusCode.OK);
            
            Post("/users", _ =>
            {
                try
                {
                    var userForm = this.Bind<UserForm>();
                    var response = (Response) userService.Register(userForm).ToString();
                    response.StatusCode = HttpStatusCode.Created;
                    return response;
                }
                catch (ConflictException e)
                {
                    return HttpStatusCode.Conflict;
                }
            });
            
            Post("/groups", _ =>
            {
                try
                {
                    var group = this.Bind<GroupFormId>();
                    var user = userService.Find(Guid.Parse(group.Owner));
                    var groupForm = new GroupForm(@group.Name, user);
                    
                    var response = (Response) groupService.Register(groupForm).ToString();
                    response.StatusCode = HttpStatusCode.Created;
                    return response;
                }
                catch (ArgumentException err)
                {
                    return HttpStatusCode.NotFound;
                }
               
            });
            
            Get("/groups/{id}", parameters =>
            {
                try
                {
                    var group = groupService.Find(parameters.id);
                    var response = (Response) new JavaScriptSerializer().Serialize(group);
                    response.StatusCode = HttpStatusCode.Found;
                    return response;
                }
                catch (ArgumentException err)
                {
                    return HttpStatusCode.NotFound;
                }
            });
            
            Get("/users/{id}", parameters =>
            {
                try
                {
                    var user = userService.Find(parameters.id);
                    var response = (Response) new JavaScriptSerializer().Serialize(user);
                    response.StatusCode = HttpStatusCode.Found;
                    return response;
                }
                catch (ArgumentException err)
                {
                    return HttpStatusCode.NotFound;
                }
            });
            

            Put("/groups/{groupId}/add/{userId}", parameters =>
            {
                try
                {
                    groupService.AddMember(parameters.groupId, parameters.userId);
                    return HttpStatusCode.OK;
                }
                catch (ArgumentException e)
                {
                    return HttpStatusCode.NotFound;
                }
                catch (ConflictException e)
                {
                    return HttpStatusCode.Conflict;
                }
            });

            Delete("/groups/{id}", parameter =>
            {
                try
                {
                    groupService.Remove(parameter.id);
                    return HttpStatusCode.OK;
                }
                catch (ArgumentException err)
                {
                    return HttpStatusCode.NotFound;
                }
            });
            
            Delete("/users/{id}", parameters =>
            {
                try
                {
                    userService.Remove(parameters.id);
                    return HttpStatusCode.OK;
                }
                catch (ArgumentException err)
                {
                    return HttpStatusCode.NotFound;
                }
            });
        }
    }

    class GroupFormId
    {
        public string Name;
        public string Owner;
    }
}