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
        private readonly DatabaseContext _databaseContext;
        
        public UserRepositoryInDb(DatabaseContext userContext)
        {
            this._databaseContext = userContext;
        }
        
        public List<User> All()
        {
            return _databaseContext.Users.ToList();
        }

        public Guid Add(User user)
        {
            try
            {
                var entityEntry = _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();
                return entityEntry.Entity.Id;
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
            _databaseContext.Users.Remove(user);
            _databaseContext.SaveChanges();
        }

        public User Get(Guid id)
        {
            var value = _databaseContext.Users.FirstOrDefault(u => u.Id == id);
            if (value == null) throw new NotFoundException();
            else return value;
        }
    }
}