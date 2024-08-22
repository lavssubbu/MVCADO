﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDBADO.DataAccess;
using MVCDBADO.Models;

namespace MVCDBADO.Controllers
{
    public class StudentController : Controller
    {
        StudentDataAccess sda = new StudentDataAccess();
        // GET: StudentController
        public ActionResult Index()
        {
            IEnumerable<Student> res = sda.Display();
            return View(res);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
           Student stud = sda.GetStudentById(id);
            return View(stud);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student s)
        {
            try
            {
                sda.Insert(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
           Student st = sda.GetStudentById(id);
           return View(st);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student s)
        {
            try
            {
                sda.Update(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
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
