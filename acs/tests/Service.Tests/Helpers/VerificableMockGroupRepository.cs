using System;
using System.Collections.Generic;
using acs.Model;
using acs.Repository;

namespace acs.tests.Service.Tests.Helpers
{
    public class VerifiableMockGroupRepository: IGroupRepository
    {
        public bool EmptyCalled { get; private set; }
        public bool AddCalled { get; private set; }
        public bool UpdateCalled { get; private set; }
        public bool RemoveCalled { get; private set; }
        public bool GetCalled { get; private set; }
        public bool AmountOfGroupsCalled{ get; private set; }
        
        public bool IsEmpty() {
            EmptyCalled = true;
            return true;
        }
        public Guid Add(Group group) {
            AddCalled = true;
            return group.Id;
        }
        public Group Get(Guid id) {
            GetCalled = true;
            return null;
        }
        public void Remove(Guid id) {
            RemoveCalled = true;
        }
        public void Update(Group group) {
            UpdateCalled = true;
        }
        public int AmountOfGroups() {
            AmountOfGroupsCalled = true;
            return 0;
        }
    }
}