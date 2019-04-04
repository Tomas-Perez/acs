namespace acs.Model
{
    public class UserForm
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
        

        public UserForm(string name, string email, string password, string confirmPassword)
        {
            Name = name;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}