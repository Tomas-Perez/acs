using System;
using System.Collections.Generic;
using System.Linq;
using acs.Db;
using acs.Exception;
using acs.Model;
using Microsoft.EntityFrameworkCore;

namespace acs.Repository
{
    public class GroupRepositoryInDB: IGroupRepository {
        private readonly GroupContext _groupContext;

        public GroupRepositoryInDB(GroupContext groupContext) {
            _groupContext = groupContext;
        }

        public bool IsEmpty() {
            return _groupContext.Groups.ToList().Count == 0;
        }

        public void Add(Group group) {
            try
            {
                _groupContext.Groups.Add(group);
                _groupContext.SaveChanges();
            }
            catch (DbUpdateException exc)
            {
                throw new ConflictException();
            }
        }

        public Group Get(Guid id) {
            var value = _groupContext.Groups.FirstOrDefault(g => g.Id == id);
            if (value == null) throw new NotFoundException();
            else return value;
        }

        public void Remove(Guid id) {
             var group = Get(id);
            _groupContext.Groups.Remove(group);
            _groupContext.SaveChanges();
        }

        public void Update(Group group) {
            Remove(group.Id);
            Add(group);
        }

        public int AmountOfGroups() {
            return _groupContext.Groups.ToList().Count;
        }
    }
}