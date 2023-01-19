using StarWars.Shared.Kernel.Entity;

namespace StarWars.Domain.Entity
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string name, string username, string password)
        {
            Name = name;
            Username = username;
            Password = password;
        }
    }
}
