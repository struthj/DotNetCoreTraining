using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //ManytoMany
        public List<PersonRole> PersonRole { get; set; }

    }

    public class Role
    {
        public string Id { get; set; }
        //ManytoMany
        public List<PersonRole> PersonRole { get; set; }

    }

    public class PersonRole
    {
        //Id's
        public int PersonId { get; set; }
        public string RoleId { get; set; }

        //ManyToMany
        public Role Role { get; set; }
        public Person Person { get; set; }
    }
}
