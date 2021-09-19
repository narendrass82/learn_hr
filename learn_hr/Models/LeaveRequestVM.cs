using learn_hr.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Models
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }        
        public EmployeeVM RequestingEmployee { get; set; }
        [Display(Name = "Employee Name")]
        public string RequestingEmployeeId { get; set; }
        [Display(Name ="Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }        
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        [Display(Name = "Date Requested")]        
        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; }
        public string RequestedComments { get; set; }
        [Display(Name = "Date Actioned")]
        [DataType(DataType.Date)]
        public DateTime DateActioned { get; set; }
        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
        [Display(Name = "Approved By")]
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class AdminLeaveRequestViewVM
    {
        [Display(Name ="Total Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }
        public List<LeaveRequestVM> LeaveRequests { get; set; }

    }
    public class CreateLeaveRequestVM
    {
        
        [Display(Name = "Start Date")]
        [Required]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        public string EndDate { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        public string RequestedComments { get; set; }
    }

    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
        public List<LeaveRequestVM> LeaveRequests { get; set; }

    }
}
