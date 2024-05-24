using AspnetMvcOracleCrud3.Interfaces;
using AspnetMvcOracleCrud3.Models;
using AspnetMvcOracleCrud3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcOracleCrud3.Controllers
{
    public class StudentController : Controller
    {
        IStudentService studentService=new StudentService();
        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<Student>   students=studentService.GetAllStudents();
            return View(students);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                // TODO: Add insert logic here
                studentService.AddStudent(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            Student student = studentService.GetStudentById(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                // TODO: Add update logic here
                studentService.EditStudent(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            Student student = studentService.GetStudentById(id);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            try
            {
                // TODO: Add delete logic here
                studentService.DeleteStudent(student);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
