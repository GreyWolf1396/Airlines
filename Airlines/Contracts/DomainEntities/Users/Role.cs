using System.ComponentModel.DataAnnotations;

namespace Contracts.DomainEntities.Users
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int AccessLevel { get; set; }
    }
}
