using System;
using System.Collections.Generic;
using acs.Exception;
using acs.Model;
using acs.Repository;
using acs.Service;
using Xunit;
using acs.tests.Service.Tests.Helpers;

namespace acs.tests.Service.Tests {
    public class GroupServiceTest {

        [Fact]
        public void Test1_OnValidGroupRegisterShouldCallAdd()
        {
            var user = new User("Wawei", "soywawei@yahoo.com.ar", "password");
            var userMock = new UserRepositoryInMemory();
            userMock.Add(user);
            var groupMock = new VerifiableMockGroupRepository();

            var service = new GroupService(groupMock, userMock);

            service.Register(new GroupForm("Grupo1", user.Id));

            Assert.True(groupMock.AddCalled);
        }

        
        [Fact]
        public void Test2_OnUserNotAddedShouldThrowArgumentException()
        {
            var userMock = new UserRepositoryInMemory();
            var groupMock = new VerifiableMockGroupRepository();
            var service = new GroupService(groupMock, userMock);
            
            var form = new GroupForm("Grupo1", Guid.NewGuid());

            Assert.Throws<ArgumentException>(() => service.Register(form));
        }
    }
}