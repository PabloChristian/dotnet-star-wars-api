using StarWars.Shared.Kernel.Entity;

namespace StarWars.Domain.Entity
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
