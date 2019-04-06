using System;
using System.Collections.Generic;
using acs.Model;
using acs.Repository;

namespace acs.tests.Service.Tests.Helpers
{
    public class VerifiableMockUserRepository: IUserRepository
    {
        public bool AllCalled { get; private set; }
        public bool AddCalled { get; private set; }
        public bool UpdateCalled { get; private set; }
        public bool RemoveCalled { get; private set; }
        public bool GetCalled { get; private set; }
        
        public List<User> All()
        {
            AddCalled = true;
            return new List<User>();
        }

        public void Add(User user)
        {
            AllCalled = true;
        }

        public void Update(User user)
        {
            UpdateCalled = true;
        }

        public void Remove(Guid id)
        {
            RemoveCalled = true;
        }

        public User Get(Guid id)
        {
            GetCalled = true;
            return null;
        }
    }
}