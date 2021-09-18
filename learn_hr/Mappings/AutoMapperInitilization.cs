using AutoMapper;
using learn_hr.Data;
using learn_hr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Mappings
{
    public class AutoMapperInitilization:Profile
    {
        public AutoMapperInitilization()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();            
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationVM>().ReverseMap();            
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
        }
    }
}
