using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Models
{
    public class LeaveTypeVM
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Deafult Number Of Days")]
        [Range(1,30,ErrorMessage ="Please Enter A Valid Number.")]
        public int DefaultDays { get; set; }
        [Display(Name="Date Created")]
        public DateTime DateCreated { get; set; }
    }
    
}
