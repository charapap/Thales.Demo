using System;
using Thales.Demo.Models;

namespace Thales.Demo.Stores
{
    public class RolesStore
    {
        public event Action<Role> RoleAdded;
        public event Action<Role> RoleEdited;
        public event Action<Role> RoleDeleted;

        public void Add(Role role)
        {
            RoleAdded?.Invoke(role);
        }

        public void Edit(Role role)
        {
            RoleEdited?.Invoke(role);
        }

        public void Delete(Role role)
        {
            RoleDeleted?.Invoke(role);
        }
    }
}
