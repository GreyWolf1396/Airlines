using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.DomainEntities.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public decimal Password { get; set; }

        public virtual User ChiefAdmin { get; set; }

        public virtual Role Role { get; set; }
    }
}
