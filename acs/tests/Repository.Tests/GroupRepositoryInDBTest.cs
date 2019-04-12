using System;
using acs.Db;
using acs.Exception;
using acs.Model;
using acs.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace acs.tests.Repository.Tests
{
    public class GroupRepositoryInDBTest : IDisposable
    {
        private readonly GroupContext _groupContext;
        private readonly UserContext _userContext;
        
         public GroupRepositoryInDBTest(){
            _groupContext = new GroupContext();
            _userContext = new UserContext();
            _groupContext.Database.EnsureCreated();
        }

         public void Dispose()
        {
            _groupContext.Database.ExecuteSqlCommand("set foreign_key_checks = 0");
            _groupContext.Database.ExecuteSqlCommand("drop table if exists Users, Groups, User");
            _groupContext.Database.ExecuteSqlCommand("set foreign_key_checks = 1");
            _groupContext.Dispose();
            _userContext.Dispose();
        }
        
        [Fact]
        public void Test1_GroupRepositoryWhenInitializedShouldBeEmpty()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            
            Assert.True(repository.IsEmpty());
        }

        [Fact]
        public void Test2_GroupRepositoryWhenAddedAGroupShouldNotBeEmpty()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            repository.Add(new Group("group", new User()));
            
            Assert.False(repository.IsEmpty());
        }

        [Fact]
        public void Test3_GroupRepositoryWhenAddedAGroupShouldContainIt()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            repository.Add(group);
            
            Assert.Equal(group, repository.Get(group.Id));
        }

        [Fact]
        public void Test4_GroupRepositoryWhenRemovedItsOnlyGroupShouldBeEmpty()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            repository.Add(group);
            repository.Remove(group.Id);
            
            Assert.True(repository.IsEmpty());
        }
        
        [Fact]
        public void Test5_GroupRepositoryWhenAskedForAGroupThatItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            
            Assert.Throws<NotFoundException>(() => repository.Get(Guid.NewGuid()));
        }

        [Fact]
        public void Test6_GroupRepositoryWhenRemovedAGroupShouldNoLongerContainIt()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            repository.Add(group);
            repository.Remove(group.Id);
            
            Assert.Throws<NotFoundException>(() => repository.Get(group.Id));
        }

        [Fact]
        public void Test7_GroupRepositoryWhenRemovedAGroupItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            
            Assert.Throws<NotFoundException>(() => repository.Remove(Guid.NewGuid()));
        }

        [Fact]
        public void Test8_GroupRepositoryWhenUpdatedAGroupShouldContainTheUpdatedVersion()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            repository.Add(group);
            var updatedGroup = group.UpdateName("better_group");
            repository.Update(updatedGroup);
            
            Assert.Equal(updatedGroup, repository.Get(group.Id));
            
        }
        
        [Fact]
        public void Test9_GroupRepositoryWhenAddedAGroupItAlreadyContainsShouldThrowConflictException()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            repository.Add(group);
            
            Assert.Throws<ConflictException>(() => repository.Add(group));
        }

        [Fact]
        public void Test10_GroupRepositoryWhenUpdatedAGroupItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            
            Assert.Throws<NotFoundException>(() => repository.Update(group));
        }

        [Fact]
        public void Test11_GroupRepositoryWhenUpdatedAGroupShouldHaveTheSameSize()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var group = new Group("group", new User());
            repository.Add(group);
            repository.Update(group.UpdateName("better_group"));
            
            Assert.Equal(1, repository.AmountOfGroups());
        }

        [Fact]
        public void Test12_AGroupWhenItHasNoMembersShouldBeDeletedFromRepository()
        {
            var repository = new GroupRepositoryInDB(_groupContext);
            var owner = new User();
            var group = new Group("group", owner);
            repository.Add(group);
            var newGroup = group.RemoveMember(owner);
            repository.Update(newGroup);
            
            Assert.True(repository.IsEmpty());
        }
    }
}