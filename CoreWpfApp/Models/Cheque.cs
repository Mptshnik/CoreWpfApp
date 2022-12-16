using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWpfApp.Models
{
    public class Cheque
    {

        [Key]
        public int ID { get; set; }
        public string Date { get; set; }
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
