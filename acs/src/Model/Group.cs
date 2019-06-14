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
        public List<User> Members { get; set; }
        
        public Group(string name, User owner)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Owner = owner;
            this.Members = new List<User>();
            Members.Add(owner);
        }

        private Group(Guid id, string name, User owner, List<User> members)
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
            Members.Add(newMember);
            return new Group(Id, Name, Owner, Members);
        }

        public Group RemoveMember(User member)
        {
            if (!Contains(member)) throw new NotFoundException("Member is not in the group.");
            Members.Remove(member);
            if (Members.Count == 0)
            {
                return new Group(Id, Name, null, Members);
            }
            return member == Owner ? new Group(Id, Name, Members[0], Members) : new Group(Id, Name, Owner, Members);
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