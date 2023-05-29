using System;
using System.Collections.Generic;

namespace Thales.Demo.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Role> Roles { get; set;}
        public Guid ParentId { get; set; }

        public Role()
        {
            Roles = new List<Role>();
        }
    }
}
