using System;
using acs.Exception;
using acs.Model;
using Xunit;

namespace acs.tests.Model.Tests
{
    public class GroupTest
    {
        [Fact]
        public void Test1_GroupShouldBeImmutable()
        {
            var owner = Guid.NewGuid();
            var group = new Group("group", owner);
            var newOwner = Guid.NewGuid();
            group.UpdateName("hello").AddMember(newOwner).UpdateOwner(newOwner);
            
            Assert.Equal("group", group.Name);
            Assert.Equal(owner, group.Owner);
            Assert.Single(group.Members);
        }

        [Fact]
        public void Test2_GroupWhenUpdatedShouldReturnANewGroupWithUpdatedValues()
        {
            var group = new Group("group", Guid.NewGuid());
            var newOwner = Guid.NewGuid();
            var newGroup = group.UpdateName("hello").AddMember(newOwner).UpdateOwner(newOwner);
            
            Assert.Equal("hello", newGroup.Name);
            Assert.Equal(newOwner, newGroup.Owner);
        }
        
        [Fact]
        public void Test3_GroupWhenAddedAMemberShouldReturnANewGroupWithThatMember()
        {
            var member = Guid.NewGuid();
            var group = new Group("group", Guid.NewGuid()).AddMember(member);
            
            Assert.True(group.Contains(member));
        }
        
        [Fact]
        public void Test4_GroupWhenRemovedAMemberShouldNoLongerHaveThatMember()
        {
            var member = Guid.NewGuid();
            var group = new Group("group", Guid.NewGuid()).AddMember(member).RemoveMember(member);
            
            Assert.False(group.Contains(member));
        }
        
        [Fact]
        public void Test5_GroupWhenRemovedTheOwnerShouldReplaceItWithOneOfTheMembers()
        {
            var owner = Guid.NewGuid();
            var member = Guid.NewGuid();
            var group = new Group("group", owner).AddMember(member).RemoveMember(owner);
            
            Assert.Equal(member, group.Owner);
        }
        
        [Fact]
        public void Test6_GroupWhenRemovedTheOnlyMemberShouldBeEmpty()
        {
            var owner = Guid.NewGuid();
            var group = new Group("group", owner).RemoveMember(owner);
            
            Assert.True(group.IsEmpty());
        }
        
        [Fact]
        public void Test7_GroupWhenCreatedShouldHaveTheOwnerAsAMember()
        {
            var owner = Guid.NewGuid();
            var group = new Group("group", owner);
            
            Assert.True(group.Contains(owner));
            Assert.Single(group.Members);
        }
        
        [Fact]
        public void Test8_GroupWhenAddedAMemberThatIsAlreadyAMemberShouldThrowConflictException()
        {
            var owner = Guid.NewGuid();
            var group = new Group("group", owner);
            
            Assert.Throws<ConflictException>(() => group.AddMember(owner));
        }
        
        [Fact]
        public void Test9_GroupWhenRemovedAMemberThatIsNotAMemberShouldThrowNotFoundException()
        {
            var group = new Group("group", Guid.NewGuid());
            
            Assert.Throws<NotFoundException>(() => group.RemoveMember(Guid.NewGuid()));
        }
        
        [Fact]
        public void Test10_GroupWhenUpdatedAnOwnerWhoIsNotAMemberShouldThrowNotFoundException()
        {
            var group = new Group("group", Guid.NewGuid());
            
            Assert.Throws<NotFoundException>(() => group.UpdateOwner(Guid.NewGuid()));
        }
        
    }
}