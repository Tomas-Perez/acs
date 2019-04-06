using System;
using System.Collections.Generic;
using acs.Exception;
using acs.Model;
using acs.Repository;
using acs.Service;
using Moq;
using Xunit;

namespace acs.tests.Service.Tests {
    public class GroupServiceTest {

        [Fact]
        public void Test1_OnValidGroupRegisterShouldCallAdd()
        {
            var user = new User("Wawei", "soywawei@yahoo.com.ar", "password");
            var userMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var groupMock = new Mock<GroupRepository>(MockBehavior.Strict);
            
            groupMock.Setup(x => x.Add(It.IsAny<Group>())).Verifiable();

            var service = new GroupService(groupMock.Object, userMock.Object);

            service.Register(new GroupForm("Grupo1", user.Id));

            groupMock.Verify();
        }

        
        [Fact]
        public void Test2_OnUserNotAddedShouldThrowArgumentException()
        {
            var userMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var groupMock = new Mock<GroupRepository>(MockBehavior.Strict);
            var service = new GroupService(groupMock.Object, userMock.Object);
            
            var form = new GroupForm("Grupo1", Guid.NewGuid());

            Assert.Throws<ArgumentException>(() => service.Register(form));
        }
    }
}