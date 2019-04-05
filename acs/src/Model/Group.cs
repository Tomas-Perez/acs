using System;
using System.Collections.Generic;

namespace acs.Model
{
    // Immutable
    public class Group
    {
        public Guid Id { get; }
        private String Name { get; }
        private Guid Owner { get; }
        private List<Guid> Members { get; }
        
        public Group(String name, Guid owner)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Owner = owner;
            this.Members = new List<Guid>();
            Members.Add(owner);
        }

        private Group(Guid id, String name, Guid owner, List<Guid> members)
        {
            this.Id = id;
            this.Name = name;
            this.Owner = owner;
            this.Members = members;
        }

        public Group UpdateName(String newName)
        {
            return new Group(Id, newName, Owner, new List<Guid>(Members));
        }

        public Group UpdateOwner(Guid newOwner)
        {
            return new Group(Id, Name, newOwner, new List<Guid>(Members));
        }

        public Group AddMember(Guid newMember)
        {
            var list = new List<Guid>(Members) {newMember};
            return new Group(Id, Name, Owner, list);
        }

        public Group RemoveMember(Guid member)
        {
            var list = new List<Guid>(Members);
            list.Remove(member);
            if (list.Count == 0)
            {
                return new Group(Id, Name, Guid.Empty, list); //could be improved
            }
            if (member == Owner)
            {
                return new Group(Id, Name, list[0], list);
            }
            return new Group(Id, Name, Owner, list);
        }

        public bool IsEmpty()
        {
            return Owner == Guid.Empty;
        }
    }
}