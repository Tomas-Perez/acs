using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using acs.Exception;

namespace acs.Model
{
    // Immutable
    public class Group
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid Owner { get; }
        public ImmutableList<Guid> Members { get; }
        
        public Group(string name, Guid owner)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Owner = owner;
            this.Members = ImmutableList<Guid>.Empty.Add(owner);
        }

        private Group(Guid id, string name, Guid owner, ImmutableList<Guid> members)
        {
            this.Id = id;
            this.Name = name;
            this.Owner = owner;
            this.Members = members;
        }

        public Group UpdateName(string newName)
        {
            return new Group(Id, newName, Owner, Members);
        }

        public Group UpdateOwner(Guid newOwner)
        {
            if (!Contains(newOwner)) throw new NotFoundException("Member is not in the group.");
            return new Group(Id, Name, newOwner, Members);
        }

        public Group AddMember(Guid newMember)
        {
            if (Contains(newMember)) throw new ConflictException("Member is already in the group.");
            return new Group(Id, Name, Owner, Members.Add(newMember));
        }

        public Group RemoveMember(Guid member)
        {
            if (!Contains(member)) throw new NotFoundException("Member is not in the group.");
            var list = Members.Remove(member);
            if (list.Count == 0)
            {
                return new Group(Id, Name, Guid.Empty, list);
            }
            return member == Owner ? new Group(Id, Name, list[0], list) : new Group(Id, Name, Owner, list);
        }

        public bool IsEmpty()
        {
            return Owner == Guid.Empty;
        }

        public bool Contains(Guid member)
        {
            return Members.Contains(member);
        }
    }
}