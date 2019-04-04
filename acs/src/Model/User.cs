using System;

namespace acs.Model
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get;  }
        public string Password { get; }

        public User(string name, string email, string password)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }
        
        public User(Guid id, string name, string email, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        protected bool Equals(User other)
        {
            return Id.Equals(other.Id) && string.Equals(Name, other.Name) && string.Equals(Email, other.Email) && string.Equals(Password, other.Password);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}