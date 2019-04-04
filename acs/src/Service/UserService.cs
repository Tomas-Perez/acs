using System;
using acs.Exception;
using acs.Model;
using acs.Repository;

namespace acs.Service
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public void Register(UserForm form)
        {
            if (form.Password != form.ConfirmPassword) throw new ArgumentException("Passwords do not match");
            if (IsValidEmail(form.Email)) throw new ArgumentException("Invalid email");
            
            var sameEmailIdx = _repository.All().FindIndex(u => u.Email == form.Email);
            if (sameEmailIdx == -1) throw new ConflictException();
            
            _repository.Add(new User(form.Name, form.Email, form.Password));
        }
        
        private bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}