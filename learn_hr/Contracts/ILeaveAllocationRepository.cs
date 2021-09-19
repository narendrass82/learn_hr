using learn_hr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckAllocation(int leaveTypeId,string employeeId);
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeId);
        LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string employeId,int LeaveTypeId);
    }
}
