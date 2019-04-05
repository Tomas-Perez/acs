using System;

namespace acs.Model
{
    public class GroupForm
    {
        public string Name { get; }
        public Guid Owner{ get; }

        public GroupForm(string name, Guid owner)
        {
            name = Name;
            Owner = owner;
        }
    }
}