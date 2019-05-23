using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using acs.Exception;

namespace acs.Model
{
    // Immutable
    public class Group
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public User Owner { get;  set; }
        public ImmutableList<User> Members { get; set; }
        
        public Group(string name, User owner)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Owner = owner;
            this.Members = ImmutableList<User>.Empty.Add(owner);
        }

        private Group(Guid id, string name, User owner, ImmutableList<User> members)
        {
            this.Id = id;
            this.Name = name;
            this.Owner = owner;
            this.Members = members;
        }

        public Group() {}

        public Group UpdateName(string newName)
        {
            return new Group(Id, newName, Owner, Members);
        }

        public Group UpdateOwner(User newOwner)
        {
            if (!Contains(newOwner)) throw new NotFoundException("Member is not in the group.");
            return new Group(Id, Name, newOwner, Members);
        }

        public Group AddMember(User newMember)
        {
            if (Contains(newMember)) throw new ConflictException("Member is already in the group.");
            return new Group(Id, Name, Owner, Members.Add(newMember));
        }

        public Group RemoveMember(User member)
        {
            if (!Contains(member)) throw new NotFoundException("Member is not in the group.");
            var list = Members.Remove(member);
            if (list.Count == 0)
            {
                return new Group(Id, Name, null, list);
            }
            return member == Owner ? new Group(Id, Name, list[0], list) : new Group(Id, Name, Owner, list);
        }

        public bool IsEmpty()
        {
            return Owner == null;
        }

        public bool Contains(User member)
        {
            return Members.Contains(member);
        }
    }
}