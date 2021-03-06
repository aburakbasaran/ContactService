using ContactService.Application.Abstract;
using System;
using System.Collections.Generic;

namespace ContactService.ContactModule.Data.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Firm { get; set; }

        public virtual ICollection<UserContactEntity> UserContacts { get; set; }
    }
}