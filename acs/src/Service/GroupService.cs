using System;
using acs.Exception;
using acs.Model;
using acs.Repository;

namespace acs.Service {
    public class GroupService {
        private readonly GroupRepository _groupRepository;
        private readonly UserRepository _userRepository;

        public GroupService(GroupRepository groupRepository, UserRepository userRepository) {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public void Register(GroupForm groupForm) {
            try {
                _userRepository.Get(groupForm.Owner);
            } catch(DllNotFoundException error) {
                throw new ArgumentException("User does not exist");
            }
            _groupRepository.Add(new Group(groupForm.Name, groupForm.Owner));
        }
    }
}