using System;
using System.Collections.Generic;
using acs.Model;
using acs.Repository;

namespace acs.tests.Service.Tests.Helpers
{
    public class PredefinedAllUserRepository: IUserRepository
    {
        private readonly List<User> _onAll;
        
        public PredefinedAllUserRepository(List<User> onAll)
        {
            this._onAll = onAll;
        }
        
        public List<User> All()
        {
            return _onAll;
        }
    
        public Guid Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}