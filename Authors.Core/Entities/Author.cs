using Authors.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authors.Core.Models
{
    public class Author : BaseEntity
    {
        public int? IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
 
    }
}
