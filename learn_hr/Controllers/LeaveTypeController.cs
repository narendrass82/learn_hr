using AutoMapper;
using learn_hr.Contracts;
using learn_hr.Data;
using learn_hr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr.Controllers
{
    [Authorize]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;
        public LeaveTypeController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: LeaveTypeController
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes);
            return View(model);
        }

        // GET: LeaveTypeController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.isExist(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // GET: LeaveTypeController/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess=_repo.Create(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong..");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong..");
                return View(model);
            }
        }

        // GET: LeaveTypeController/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            if (!_repo.isExist(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // POST: LeaveTypeController/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);                
                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong..");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong..");
                return View(model);
            }
        }

        // GET: LeaveTypeController/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            if (!_repo.isExist(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var isSuccess = _repo.Delete(leaveType);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypeController/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LeaveTypeVM model)
        {
            try
            {
                if (!_repo.isExist(id))
                {
                    return NotFound();
                }
                var leaveType = _repo.FindById(id);
                var isSuccess = _repo.Delete(leaveType);
                if (!isSuccess)
                {
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
