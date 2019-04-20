using System;
using acs.Db;
using acs.Exception;
using acs.Model;
using acs.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace acs.tests.Repository.Tests
{
    public class UserRepositoryInDbTest : IDisposable
    {
        private readonly DatabaseContext _databaseContext;        
        
        public UserRepositoryInDbTest()
        {
            _databaseContext = new DatabaseContext();
            _databaseContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _databaseContext.Database.ExecuteSqlCommand("set foreign_key_checks = 0; drop table if exists Groups, Users; set foreign_key_checks = 1");

            _databaseContext.Dispose();
        }

        [Fact]
        public void Test1_InitializedShouldBeEmpty()
        {
            var repository = new UserRepositoryInDb(_databaseContext);
            
            Assert.Empty(repository.All());
        }

        [Fact]
        public void Test2_UserAddedAllShouldNotBeEmpty()
        {
            var repository = new UserRepositoryInDb(_databaseContext);
            
            repository.Add(new User("Mark", "mark@marko.com", "1234"));
            Assert.NotEmpty(repository.All());
        }

        [Fact]
        public void Test3_LoneUserIsRemovedAllShouldBeEmpty()
        {
            var user = new User("Mark", "mark@marko.com", "1234");
            var repository = new UserRepositoryInDb(_databaseContext);
            
            repository.Add(user);
            repository.Remove(user.Id);
            Assert.Empty(repository.All());
        }

        [Fact]
        public void Test4_TryingToRemoveMissingUserShouldThrowNotFoundException()
        {
            var repository = new UserRepositoryInDb(_databaseContext);
            Assert.Throws<NotFoundException>(() => repository.Remove(Guid.NewGuid()));
        }

        [Fact]
        public void Test5_AddingAnExistingUserShouldThrowConflictException()
        {
            var user = new User("Mark", "mark@marko.com", "1234");
            var repository = new UserRepositoryInDb(_databaseContext);
            
            repository.Add(user);
            Assert.Throws<ConflictException>(() => repository.Add(user));
        }

        [Fact]
        public void Test6_UpdatingAnExistingUserShouldMaintainAllSize()
        {
            var user = new User("Mark", "mark@marko.com", "1234");
            var updated = new User(user.Id, "Mark", "mark@marko.com.ar", "1234");
            var repository = new UserRepositoryInDb(_databaseContext);
            
            repository.Add(user);
            repository.Update(updated);
            Assert.Single(repository.All());
        }

        [Fact]
        public void Test6_UpdatingAMissingUserShouldThrowNotFoundException()
        {
            var user = new User("Mark", "mark@marko.com", "1234");
            var repository = new UserRepositoryInDb(_databaseContext);
            
            Assert.Throws<NotFoundException>(() => repository.Update(user));
        }

        [Fact]
        public void Test7_GetByExistingUserIdShouldReturnUser()
        {
            var user = new User("Mark", "mark@marko.com", "1234");
            var repository = new UserRepositoryInDb(_databaseContext);

            repository.Add(user);
            var savedUser = repository.Get(user.Id);
            
            Assert.Equal(user, savedUser);
        }

        [Fact]
        public void Test8_GetByMissingUserIdShouldThrowNotFoundException()
        {
            var repository = new UserRepositoryInDb(_databaseContext);

            Assert.Throws<NotFoundException>(() => repository.Get(Guid.NewGuid()));
        }
    }
}