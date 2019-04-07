using System;
using System.Collections.Generic;
using acs.Exception;
using acs.Model;

namespace acs.Repository
{
    public class GroupRepositoryInMemory : IGroupRepository
    {
        private readonly Dictionary<Guid, Group> _groups = new Dictionary<Guid, Group>();

        public bool IsEmpty()
        {
            return _groups.Count == 0;
        }

        public void Add(Group group)
        {
            try
            {
                _groups.Add(group.Id, group);
            }
            catch (ArgumentException exc)
            {
                throw new ConflictException();
            }
        }

        public Group Get(Guid id)
        {
            var value = _groups.GetValueOrDefault(id);
            
            if (value == null) throw new NotFoundException();
            return value;
        }

        public void Remove(Guid id)
        {
            var removed = _groups.Remove(id);
            if (!removed) throw new NotFoundException();
        }

        public void Update(Group group)
        {
            Remove(group.Id);
            if (!group.IsEmpty())
            {
                Add(group);
            }
        }

        public int AmountOfGroups()
        {
            return _groups.Count;
        }
    }
}

namespace acs.Repository {
    public interface IGroupRepository {
        bool IsEmpty();
        void Add(Group group);
        Group Get(Guid id);
        void Remove(Guid id);
        void Update(Group group);
        int AmountOfGroups();
    }
}