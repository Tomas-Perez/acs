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

        public void Register(GroupForm groupForm)
        {
            try
            {
                _userRepository.Get(groupForm.Owner);
            }
            catch (NotFoundException error)
            {
                throw new ArgumentException("User does not exist");
            }

            _groupRepository.Add(new Group(groupForm.Name, groupForm.Owner));
        }
    }
}