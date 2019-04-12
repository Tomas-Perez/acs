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
            var owner = new User();
            var group = new Group("group", owner);
            var newOwner = new User();
            group.UpdateName("hello").AddMember(newOwner).UpdateOwner(newOwner);
            
            Assert.Equal("group", group.Name);
            Assert.Equal(owner, group.Owner);
            Assert.Single(group.Members);
        }

        [Fact]
        public void Test2_GroupWhenUpdatedShouldReturnANewGroupWithUpdatedValues()
        {
            var group = new Group("group", new User());
            var newOwner = new User();
            var newGroup = group.UpdateName("hello").AddMember(newOwner).UpdateOwner(newOwner);
            
            Assert.Equal("hello", newGroup.Name);
            Assert.Equal(newOwner, newGroup.Owner);
        }
        
        [Fact]
        public void Test3_GroupWhenAddedAMemberShouldReturnANewGroupWithThatMember()
        {
            var member = new User();
            var group = new Group("group", new User()).AddMember(member);
            
            Assert.True(group.Contains(member));
        }
        
        [Fact]
        public void Test4_GroupWhenRemovedAMemberShouldNoLongerHaveThatMember()
        {
            var member = new User();
            var group = new Group("group", new User()).AddMember(member).RemoveMember(member);
            
            Assert.False(group.Contains(member));
        }
        
        [Fact]
        public void Test5_GroupWhenRemovedTheOwnerShouldReplaceItWithOneOfTheMembers()
        {
            var owner = new User();
            var member = new User();
            var group = new Group("group", owner).AddMember(member).RemoveMember(owner);
            
            Assert.Equal(member, group.Owner);
        }
        
        [Fact]
        public void Test6_GroupWhenRemovedTheOnlyMemberShouldBeEmpty()
        {
            var owner = new User();
            var group = new Group("group", owner).RemoveMember(owner);
            
            Assert.True(group.IsEmpty());
        }
        
        [Fact]
        public void Test7_GroupWhenCreatedShouldHaveTheOwnerAsAMember()
        {
            var owner =new User();
            var group = new Group("group", owner);
            
            Assert.True(group.Contains(owner));
            Assert.Single(group.Members);
        }
        
        [Fact]
        public void Test8_GroupWhenAddedAMemberThatIsAlreadyAMemberShouldThrowConflictException()
        {
            var owner = new User();
            var group = new Group("group", owner);
            
            Assert.Throws<ConflictException>(() => group.AddMember(owner));
        }
        
        [Fact]
        public void Test9_GroupWhenRemovedAMemberThatIsNotAMemberShouldThrowNotFoundException()
        {
            var group = new Group("group", new User());
            
            Assert.Throws<NotFoundException>(() => group.RemoveMember(new User()));
        }
        
        [Fact]
        public void Test10_GroupWhenUpdatedAnOwnerWhoIsNotAMemberShouldThrowNotFoundException()
        {
            var group = new Group("group", new User());
            
            Assert.Throws<NotFoundException>(() => group.UpdateOwner(new User()));
        }
        
    }
}