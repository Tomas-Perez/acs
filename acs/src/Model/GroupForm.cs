using System;

namespace acs.Model
{
    public class GroupForm
    {
        public string Name { get; }
        public User Owner{ get; }

        public GroupForm(string name, User owner)
        {
            Name = name;
            Owner = owner;
        }
    }
}