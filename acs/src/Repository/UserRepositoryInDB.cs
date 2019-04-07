using System;
using System.Collections.Generic;
using System.Linq;
using acs.Db;
using acs.Exception;
using acs.Model;
using Microsoft.EntityFrameworkCore;

namespace acs.Repository
{
    public class UserRepositoryInDb : IUserRepository
    {
        private readonly UserContext _userContext;
        
        public UserRepositoryInDb(UserContext userContext)
        {
            this._userContext = userContext;
        }
        
        public List<User> All()
        {
            return _userContext.Users.ToList();
        }

        public void Add(User user)
        {
            try
            {
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException exc)
            {
                throw new ConflictException();
            }
        }

        public void Update(User user)
        {
            Remove(user.Id);
            Add(user);
        }

        public void Remove(Guid id)
        {
            var user = Get(id);
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }

        public User Get(Guid id)
        {
            var value = _userContext.Users.FirstOrDefault(u => u.Id == id);
            if (value == null) throw new NotFoundException();
            else return value;
        }
    }
}