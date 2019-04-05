using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            var repository = new GroupRepository();
            
            Assert.True(repository.IsEmpty());
        }

        [Fact]
        public void Test2_GroupRepositoryWhenAddedAGroupShouldNotBeEmpty()
        {
            var repository = new GroupRepository();
            repository.Add(new Group("group", Guid.NewGuid()));
            
            Assert.False(repository.IsEmpty());
        }

        [Fact]
        public void Test3_GroupRepositoryWhenAddedAGroupShouldContainIt()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            repository.Add(group);
            
            Assert.Equal(group, repository.Get(group.Id));
        }

        [Fact]
        public void Test4_GroupRepositoryWhenRemovedItsOnlyGroupShouldBeEmpty()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            repository.Add(group);
            repository.Remove(group.Id);
            
            Assert.True(repository.IsEmpty());
        }
        
        [Fact]
        public void Test5_GroupRepositoryWhenAskedForAGroupThatItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepository();
            
            Assert.Throws<NotFoundException>(() => repository.Get(Guid.NewGuid()));
        }

        [Fact]
        public void Test6_GroupRepositoryWhenRemovedAGroupShouldNoLongerContainIt()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            repository.Add(group);
            repository.Remove(group.Id);
            
            Assert.Throws<NotFoundException>(() => repository.Get(group.Id));
        }

        [Fact]
        public void Test7_GroupRepositoryWhenRemovedAGroupItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepository();
            
            Assert.Throws<NotFoundException>(() => repository.Remove(Guid.NewGuid()));
        }

        [Fact]
        public void Test8_GroupRepositoryWhenUpdatedAGroupShouldContainTheUpdatedVersion()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            repository.Add(group);
            var updatedGroup = group.UpdateName("better_group");
            repository.Update(updatedGroup);
            
            Assert.Equal(updatedGroup, repository.Get(group.Id));
            
        }
        
        [Fact]
        public void Test9_GroupRepositoryWhenAddedAGroupItAlreadyContainsShouldThrowConflictException()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            repository.Add(group);
            
            Assert.Throws<ConflictException>(() => repository.Add(group));
        }

        [Fact]
        public void Test10_GroupRepositoryWhenUpdatedAGroupItDoesNotContainShouldThrowNotFoundException()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            
            Assert.Throws<NotFoundException>(() => repository.Update(group));
        }

        [Fact]
        public void Test11_GroupRepositoryWhenUpdatedAGroupShouldHaveTheSameSize()
        {
            var repository = new GroupRepository();
            var group = new Group("group", Guid.NewGuid());
            repository.Add(group);
            repository.Update(group.UpdateName("better_group"));
            
            Assert.Equal(1, repository.AmountOfGroups());
        }

        [Fact]
        public void Test12_AGroupWhenItHasNoMembersShouldBeDeletedFromRepository()
        {
            var repository = new GroupRepository();
            var owner = Guid.NewGuid();
            var group = new Group("group", owner);
            repository.Add(group);
            var newGroup = group.RemoveMember(owner);
            repository.Update(newGroup);
            
            Assert.True(repository.IsEmpty());
        }
    }
}