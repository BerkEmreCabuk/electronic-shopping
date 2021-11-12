﻿using ElectronicShopping.Api.Repositories.Entities.Common;

namespace ElectronicShopping.Api.Repositories.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
