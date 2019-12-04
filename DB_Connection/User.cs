namespace KITWTF1
{
    public class User
    {
        public User()
        {

        }
        public User(string Username, string Password, string Email, string Phonenumber, string Name)
        {
            this.Username = Username;
            this.Password = Password;
            this.Email = Email;
            this.Phonenumber = Phonenumber;
            this.Name = Name;
        }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Name { get; set; }
    }
}