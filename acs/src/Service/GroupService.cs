using System;
using acs.Model;
using acs.Repository;
using acs.Exception;


namespace acs.Service
{
    public class GroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public Guid Register(GroupForm groupForm)
        {
            try
            {
                _userRepository.Get(groupForm.Owner.Id);
            }
            catch (NotFoundException error)
            {
                throw new ArgumentException("User does not exist");
            }

            return _groupRepository.Add(new Group(groupForm.Name, groupForm.Owner));
        }

        public Group Find(Guid id)
        {
            try
            {
                return _groupRepository.Get(id);
            }
            catch (NotFoundException e)
            {
                throw new ArgumentException("Group does not exist");
            }
        }

        public Group AddMember(Guid groupId, Guid userId)
        {
            try
            {
                var group = _groupRepository.Get(groupId);
                var newGroup = @group.AddMember(_userRepository.Get(userId));
                _groupRepository.Update(newGroup);
                return group;
            }
            catch (NotFoundException e)
            {
                throw new ArgumentException("Group or user does not exist");

            }
        }

        public void Remove(Guid groupId)
        {
            try
            {
                _groupRepository.Remove(groupId);
            }
            catch (NotFoundException error)
            {
                throw new ArgumentException("Group does not exist");
            }
           
        }
    }
}