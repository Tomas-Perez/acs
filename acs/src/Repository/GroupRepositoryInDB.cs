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
        private readonly DatabaseContext _databaseContext;

        public GroupRepositoryInDB(DatabaseContext groupContext) {
            _databaseContext = groupContext;
        }

        public bool IsEmpty() {
            return _databaseContext.Groups.ToList().Count == 0;
        }

        public void Add(Group group) {
            try
            {
                _databaseContext.Groups.Add(group);
                _databaseContext.SaveChanges();
            }
            catch (DbUpdateException exc)
            {
                throw new ConflictException();
            }
        }

        public Group Get(Guid id) {
            var value = _databaseContext.Groups.FirstOrDefault(g => g.Id == id);
            if (value == null) throw new NotFoundException();
            else return value;
        }

        public void Remove(Guid id) {
             var group = Get(id);
            _databaseContext.Groups.Remove(group);
            _databaseContext.SaveChanges();
        }

        public void Update(Group group) {
            Remove(group.Id);
            if(!group.Members.IsEmpty) {
                Add(group);
            }
        }

        public int AmountOfGroups() {
            return _databaseContext.Groups.ToList().Count;
        }
    }
} 