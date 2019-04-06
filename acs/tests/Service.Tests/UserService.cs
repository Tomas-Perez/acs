using System;
using System.Collections.Generic;
using acs.Exception;
using acs.Model;
using acs.Repository;
using acs.Service;
using acs.tests.Service.Tests.Helpers;
using Moq;
using Xunit;

namespace acs.tests.Service.Tests

{
    public class UserServiceTest
    {
        [Fact]
        public void Test1_OnValidUserRegisterShouldCallAdd()
        {
            var mock = new VerifiableMockUserRepository();
            var service = new UserService(mock);
            service.Register(new UserForm("Mark", "mark@marko.com", "1234", "1234"));

            Assert.True(mock.AddCalled);
        }

        [Fact]
        public void Test2_OnInvalidEmailShouldThrowArgumentException()
        {
            var mock = new VerifiableMockUserRepository();
            var service = new UserService(mock);

            var form = new UserForm("Mark", "mark", "1234", "1234");

            Assert.Throws<ArgumentException>(() => service.Register(form));
            Assert.False(mock.AddCalled);
        }

        [Fact]
        public void Test3_OnMismatchedPasswordsShouldThrowArgumentException()
        {
            var mock = new VerifiableMockUserRepository();
            var service = new UserService(mock);

            var form = new UserForm("Mark", "mark@marko.com", "12345", "1234");

            Assert.Throws<ArgumentException>(() => service.Register(form));
            Assert.False(mock.AddCalled);
        }

        [Fact]
        public void Test4_OnAlreadyUsedEmailShouldThrowConflictException()
        {
            const string email = "mark@marko.com";
            var mock = new PredefinedAllUserRepository(new List<User>(new User[] {new User("Mark", email, "1234")}));
          
            var service = new UserService(mock);

            var form = new UserForm("Mark", email, "1234", "1234");

            Assert.Throws<ConflictException>(() => service.Register(form));
        }
    }
}