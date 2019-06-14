namespace acs.Model
{
    public class UserForm
    {
        public string Name;
        public string Email;
        public string Password;
        public string ConfirmPassword;
        

        public UserForm(string name, string email, string password, string confirmPassword)
        {
            Name = name;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public UserForm()
        {
            
        }
    }
}