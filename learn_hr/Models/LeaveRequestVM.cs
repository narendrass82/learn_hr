using learn_hr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Models
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }        
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }        
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
