using acs.Db;
using acs.Model;

namespace acs
{
    public class Program
    {
        static void Main(string[] args)
        {
     
            using (var ctx = new UserContext())
            {
                ctx.Database.EnsureCreated();
                
                var stud = new User(name: "Me", email:"me@me.com", password:"1234");
                ctx.Users.Add(stud);
                ctx.SaveChanges();                
            }
        }
    }
}