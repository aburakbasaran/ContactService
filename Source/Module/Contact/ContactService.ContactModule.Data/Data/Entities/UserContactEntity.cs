using System;
using ContactService.Application.Abstract;

namespace ContactService.ContactModule.Data.Data.Entities
{
    public class UserContactEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public byte Type { get; set; }
        public string Value { get; set; }

        public virtual UserEntity User { get; set; }
    }
}