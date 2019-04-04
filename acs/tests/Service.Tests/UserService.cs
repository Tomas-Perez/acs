using System;
using System.Collections.Generic;
using acs.Exception;
using acs.Model;
using acs.Repository;
using acs.Service;
using Moq;
using Xunit;

namespace acs.tests.Service.Tests

{
    public class UserServiceTest
    {
        [Fact]
        public void Test1_OnValidUserRegisterShouldCallAdd()
        {
            var mock = new Mock<UserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.Add(It.IsAny<User>())).Verifiable();

            var service = new UserService(mock.Object);

            service.Register(new UserForm("Mark", "mark@marko.com", "1234", "1234"));

            mock.Verify();
        }

        [Fact]
        public void Test2_OnInvalidEmailShouldThrowArgumentException()
        {
            var mock = new Mock<UserRepository>(MockBehavior.Strict);
            var service = new UserService(mock.Object);

            var form = new UserForm("Mark", "mark", "1234", "1234");

            Assert.Throws<ArgumentException>(() => service.Register(form));
        }

        [Fact]
        public void Test3_OnMismatchedPasswordsShouldThrowArgumentException()
        {
            var mock = new Mock<UserRepository>(MockBehavior.Strict);
            var service = new UserService(mock.Object);

            var form = new UserForm("Mark", "mark@marko.com", "12345", "1234");

            Assert.Throws<ArgumentException>(() => service.Register(form));
        }

        [Fact]
        public void Test4_OnAlreadyUsedEmailShouldThrowConflictException()
        {
            const string email = "mark@marko.com";
            var mock = new Mock<UserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.All()).Returns(
                new List<User>(new User[] {new User("Mark", email, "1234")})
            );
            var service = new UserService(mock.Object);

            var form = new UserForm("Mark", email, "12345", "1234");

            Assert.Throws<ConflictException>(() => service.Register(form));

            mock.Verify();
        }
    }
}