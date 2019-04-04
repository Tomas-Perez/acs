using System;
using System.Collections.Generic;
using System.Linq;
using acs.Exception;
using acs.Model;

namespace acs.Repository
{
    public class UserRepository
    {
        private readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>();

        public List<User> All()
        {
            return _users.Values.ToList();
        }

        public void Add(User user)
        {
            try
            {
                _users.Add(user.Id, user);
            }
            catch (ArgumentException exc)
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
            var removed = _users.Remove(id);
            if (!removed) throw new NotFoundException();
        }

        public User Get(Guid id)
        {
            var value = _users.GetValueOrDefault(id);

            if (value == null) throw new NotFoundException();
            else return value;
        }
    }
}