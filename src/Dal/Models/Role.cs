using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Role
    {
        public string Id { get; set; }
        //ManytoMany
        public List<PersonRole> PersonRole { get; set; }

    }
}