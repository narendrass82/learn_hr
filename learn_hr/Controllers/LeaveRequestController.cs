using AutoMapper;
using learn_hr.Contracts;
using learn_hr.Data;
using learn_hr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveTypeRepository _leavetyperepo;
        private readonly ILeaveRequestRepository _leaverequestrepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public LeaveRequestController(ILeaveTypeRepository leavetyperepo, ILeaveRequestRepository leaverequestrepo, ILeaveAllocationRepository leaveallocationrepo, IMapper mapper, UserManager<Employee> userManager)
        {
            _leavetyperepo = leavetyperepo;
            _leaverequestrepo = leaverequestrepo;
            _leaveallocationrepo = leaveallocationrepo;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize(Roles ="Administrator")]
        // GET: LeaveRequestController
        public ActionResult Index()
        {
            var leaveRequest = _leaverequestrepo.FindAll();
            var leaveRequestModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequest);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests=leaveRequestModel.Count,
                ApprovedRequests=leaveRequestModel.Where(q=>q.Approved==true).Count(),
                PendingRequests = leaveRequestModel.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestModel.Count(q => q.Approved == false),
                LeaveRequests=leaveRequestModel
            };
            return View(model);
        }

        public ActionResult MyLeave()
        {
            try
            {
                var employee = _userManager.GetUserAsync(User).Result;
                var leaveallocation = _leaveallocationrepo.GetLeaveAllocationsByEmployee(employee.Id);
                var leaverequest = _leaverequestrepo.GetLeaveRequestsByEmployee(employee.Id);
                var employeAllocationModel = _mapper.Map<List<LeaveAllocationVM>>(leaveallocation);
                var employeRequestModel = _mapper.Map<List<LeaveRequestVM>>(leaverequest);
                var model = new EmployeeLeaveRequestViewVM
                {
                    LeaveAllocations=employeAllocationModel,
                    LeaveRequests=employeRequestModel
                };
                return View(model);
            }
            catch 
            {
                return View();
            }
            
        }
        public ActionResult CancelRequest(int id)
        {
            var leaverequest = _leaverequestrepo.FindById(id);
            leaverequest.Cancelled = true;
            _leaverequestrepo.Update(leaverequest);
            return RedirectToAction("MyLeave");
        }
        // GET: LeaveRequestController/Details/5
        public ActionResult Details(int id)
        {
            var leaverequest = _leaverequestrepo.FindById(id);
            var model = _mapper.Map<LeaveRequestVM>(leaverequest);
            return View(model);
        }

        public ActionResult ApproveRequest(int id)
        {
            try
            {
                var leaverequest = _leaverequestrepo.FindById(id);
                var leaveallocation = _leaveallocationrepo.GetLeaveAllocationsByEmployeeAndType(leaverequest.RequestingEmployee.Id,leaverequest.LeaveType.Id);
                var daysRequested = (int)(leaverequest.EndDate - leaverequest.StartDate).TotalDays;                
                var model = _mapper.Map<LeaveRequestVM>(leaverequest);
                leaverequest.Approved = true;
                leaverequest.ApprovedById = _userManager.GetUserAsync(User).Result.Id;
                leaverequest.DateActioned = DateTime.Now;
                var isSuccess = _leaverequestrepo.Update(leaverequest);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                leaveallocation.NumberOfDays=leaveallocation.NumberOfDays - daysRequested;
                _leaveallocationrepo.Update(leaveallocation);
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return RedirectToAction(nameof(Index));
            }            
        }
        public ActionResult RejectRequest(int id)
        {
            try
            {
                var leaverequest = _leaverequestrepo.FindById(id);
                var model = _mapper.Map<LeaveRequestVM>(leaverequest);
                leaverequest.Approved = false;
                leaverequest.ApprovedById = _userManager.GetUserAsync(User).Result.Id;
                leaverequest.DateActioned = DateTime.Now;
                var isSuccess = _leaverequestrepo.Update(leaverequest);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: LeaveRequestController/Create
        public ActionResult Create()
        {
            var leaveTypes = _leavetyperepo.FindAll();
            var leaveTypeItems = leaveTypes.Select
                (q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                }
                );
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestVM model)
        {
            try
            {
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var leaveTypes = _leavetyperepo.FindAll();
                var leaveTypeItems = leaveTypes.Select
                    (q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    }
                    );
                model.LeaveTypes = leaveTypeItems;
                if (!ModelState.IsValid)
                {
                    
                    return View(model);
                }
                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "Start date can't be further in the future than the end date");
                    return View(model);
                }
                var employee = _userManager.GetUserAsync(User).Result;
                var allocations = _leaveallocationrepo.GetLeaveAllocationsByEmployeeAndType(employee.Id,model.LeaveTypeId);
                if (allocations == null)
                {
                    ModelState.AddModelError("", "You don't have allocated leave.");
                    return View(model);
                }
                var daysRequested =(int) (endDate - startDate).TotalDays;
                if (daysRequested > allocations.NumberOfDays)
                {
                    ModelState.AddModelError("", "You don't have sufficient days for this request.");
                    return View(model);
                }
                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId=employee.Id,
                    StartDate= startDate,
                    EndDate=endDate,
                    Approved=null,
                    DateRequested=DateTime.Now,
                    DateActioned=DateTime.Now,
                    LeaveTypeId=model.LeaveTypeId,
                    RequestedComments=model.RequestedComments

                };
                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                var isSuccess = _leaverequestrepo.Create(leaveRequest);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong.");
                    return View(model);
                }

                return RedirectToAction(nameof(Index),"Home");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
