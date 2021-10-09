using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDB
{
    
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }

        [Column(TypeName ="Varchar(50)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "Varchar(500)")]
        public string Address { get; set; }
        public string ImgPath { get; set; }

        [ForeignKey("Department")]
        public int Deptid { get; set; }
        [ForeignKey("Deptid")]
        public Department Department { get; set; }
    }
}
