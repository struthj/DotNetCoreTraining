using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class PersonRole
    {
        //Id's
        public int PersonId { get; set; }
        public int RoleId { get; set; }

        //ManyToMany
        public Role Role { get; set; }
        public Person Person { get; set; }
    }
}
