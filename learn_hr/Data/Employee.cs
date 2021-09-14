using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Data
{
    public class Employee: IdentityUser
    {
        public string EmpNo { get; set; }
        public string FirstName { get; set; }
        public string TaxId { get; set; }
        public string EmpDeptCode { get; set; }
        public string EmpGradeCode { get; set; }
        public string EmpQuarterNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime DateLastPromotion { get; set; }
        


    }
}
