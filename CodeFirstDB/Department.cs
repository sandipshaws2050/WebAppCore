using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDB
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Deptid { get; set; }
        [Column(TypeName = "Varchar(50)")]
        [Required]
        public string Name { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
