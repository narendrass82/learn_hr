using learn_hr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Contracts
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        ICollection<LeaveRequest> GetLeaveRequestsByEmployee(string employeeId);
    }
}
