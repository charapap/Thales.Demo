using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Thales.Demo.Models;

namespace Thales.Demo.Services
{
    public class DataService
    {
        private const string PersonsFilePath = "persons.json";
        private const string RolesFilePath = "roles.json";

        public List<Person> GetPersons()
        {
            if (File.Exists(PersonsFilePath))
            {
                string json = File.ReadAllText(PersonsFilePath);
                return JsonConvert.DeserializeObject<List<Person>>(json);
            }
            else
            {
                return new List<Person>();
            }
        }

        public void SavePersons(List<Person> dataList)
        {
            string json = JsonConvert.SerializeObject(dataList);
            File.WriteAllText(PersonsFilePath, json);
        }

        public void UpdatePerson(Person person)
        {
            if (File.Exists(PersonsFilePath))
            {
                string json = File.ReadAllText(PersonsFilePath);
                List<Person> data = JsonConvert.DeserializeObject<List<Person>>(json);
                data[data.FindIndex(x => x.Id == person.Id)] = person;
                File.WriteAllText(PersonsFilePath, JsonConvert.SerializeObject(data));
            }
        }

        public List<Role> GetRoles()
        {
            if (File.Exists(RolesFilePath))
            {
                string json = File.ReadAllText(RolesFilePath);
                return JsonConvert.DeserializeObject<List<Role>>(json);
            }
            else
            {
                return new List<Role>() { new Role() { Id = Guid.NewGuid(), Name = "Root role" } };
            }
        }

        public void SaveRoles(List<Role> dataList)
        {
            string json = JsonConvert.SerializeObject(dataList);
            File.WriteAllText(RolesFilePath, json);
        }
    }
}
