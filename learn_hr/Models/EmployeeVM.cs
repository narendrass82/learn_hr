using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Models
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmpNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxId { get; set; }
        public string EmpDeptCode { get; set; }
        public string EmpGradeCode { get; set; }
        public string EmpQuarterNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime DateLastPromotion { get; set; }
    }
}
