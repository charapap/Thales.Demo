using System;
using Thales.Demo.Models;

namespace Thales.Demo.Stores
{
    public class PersonsStore
    {
        public event Action<Person> PersonAdded;
        public event Action<Person> PersonEdited;
        public event Action<Guid> PersonDeleted;

        public void Add(Person person)
        {
            PersonAdded?.Invoke(person);
        }

        public void Edit(Person person)
        {
            PersonEdited?.Invoke(person);
        }

        public void Delete(Guid id)
        {
            PersonDeleted?.Invoke(id);
        }
    }
}
