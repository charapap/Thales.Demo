using System;

namespace Thales.Demo.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public string Notes { get; set; }
    }
}
