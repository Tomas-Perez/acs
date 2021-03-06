using System;
using acs.Exception;
using acs.Model;
using acs.Repository;
using Xunit;

namespace acs.tests.Repository.Tests
{
    public class GroupRepositoryTest
    {

        [Fact]
        public void Test1_GroupRepositoryWhenInitializedShouldBeEmpty()
        {
            var repository = new GroupRepositoryInMemory();
            
            Assert.True(repository.IsEmpty());
        }

        [Fact]
        public void Test2_GroupRepositoryWhenAddedAGroupShouldNotBeEmpty()
        {
            var repository = new GroupRepositoryInMemory();
            repository.Add(new Group("group", new User()));
            
            Assert.False(repository.IsEmpty());
        }

        [Fact]
        public void Test3_GroupRepositoryWhenAddedAGroupShouldContainIt()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            repository.Add(group);
            
            Assert.Equal(group, repository.Get(group.Id));
        }

        [Fact]
        public void Test4_GroupRepositoryWhenRemovedItsOnlyGroupShouldBeEmpty()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            repository.Add(group);
            repository.Remove(group.Id);
            
            Assert.True(repository.IsEmpty());
        }
        
        [Fact]
        public void Test5_GroupRepositoryWhenAskedForAGroupThatItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepositoryInMemory();
            
            Assert.Throws<NotFoundException>(() => repository.Get(Guid.NewGuid()));
        }

        [Fact]
        public void Test6_GroupRepositoryWhenRemovedAGroupShouldNoLongerContainIt()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            repository.Add(group);
            repository.Remove(group.Id);
            
            Assert.Throws<NotFoundException>(() => repository.Get(group.Id));
        }

        [Fact]
        public void Test7_GroupRepositoryWhenRemovedAGroupItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepositoryInMemory();
            
            Assert.Throws<NotFoundException>(() => repository.Remove(Guid.NewGuid()));
        }

        [Fact]
        public void Test8_GroupRepositoryWhenUpdatedAGroupShouldContainTheUpdatedVersion()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            repository.Add(group);
            var updatedGroup = group.UpdateName("better_group");
            repository.Update(updatedGroup);
            
            Assert.Equal(updatedGroup, repository.Get(group.Id));
            
        }
        
        [Fact]
        public void Test9_GroupRepositoryWhenAddedAGroupItAlreadyContainsShouldThrowConflictException()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            repository.Add(group);
            
            Assert.Throws<ConflictException>(() => repository.Add(group));
        }

        [Fact]
        public void Test10_GroupRepositoryWhenUpdatedAGroupItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            
            Assert.Throws<NotFoundException>(() => repository.Update(group));
        }

        [Fact]
        public void Test11_GroupRepositoryWhenUpdatedAGroupShouldHaveTheSameSize()
        {
            var repository = new GroupRepositoryInMemory();
            var group = new Group("group", new User());
            repository.Add(group);
            repository.Update(group.UpdateName("better_group"));
            
            Assert.Equal(1, repository.AmountOfGroups());
        }

        [Fact]
        public void Test12_AGroupWhenItHasNoMembersShouldBeDeletedFromRepository()
        {
            var repository = new GroupRepositoryInMemory();
            var owner = new User();
            var group = new Group("group", owner);
            repository.Add(group);
            var newGroup = group.RemoveMember(owner);
            repository.Update(newGroup);
            
            Assert.True(repository.IsEmpty());
        }
    }
}
