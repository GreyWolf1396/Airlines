using System;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Enums;

namespace Contracts.DomainEntities.Users
{
    public class UserRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public RequestStatus Status { get; set; }

        public virtual User Creator { get; set; }

        public virtual User AssignedTo { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastModified { get; set; }
    }
}

