using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            //if(studentVM.Student.Major.MajorId > 0)
            //{
            //    studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            //}
            //if (ModelState["Student.Major.MajorName"].Errors.Count == 1)
            //{
            //    ModelState["Student.Major.MajorName"].Errors.Clear();
            //}
            //if (ModelState["Student.Courses"].Errors.Count == 1)
            //{
            //    ModelState["Student.Courses"].Errors.Clear();
            //}
            if (!ModelState.IsValid)
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View(studentVM);
            }

            studentVM.Student.Courses = new List<Course>();
            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            StudentRepository.Add(studentVM.Student);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int studentId)
        {
            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(studentId);
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            if (viewModel.Student.Courses != null)
            {
                viewModel.SelectedCourseIds = viewModel.Student.Courses.Select(c => c.CourseId).ToList();
            }
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentVM studentVM)
        {
            if (!ModelState.IsValid)
            {
                studentVM.Student = StudentRepository.Get(studentVM.Student.StudentId);
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                if (studentVM.Student.Courses != null)
                {
                    studentVM.SelectedCourseIds = studentVM.Student.Courses.Select(c => c.CourseId).ToList();
                }
                studentVM.SetStateItems(StateRepository.GetAll());
                return View(studentVM);
            }

            studentVM.Student.Courses = new List<Course>();
            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            StudentRepository.Edit(studentVM.Student);


            studentVM.Student.Address.State = StateRepository.Get(studentVM.Student.Address.State.StateAbbreviation);
            studentVM.Student.Address.AddressId = studentVM.Student.StudentId;
            StudentRepository.SaveAddress(studentVM.Student.StudentId, studentVM.Student.Address);
            
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int studentId)
        {

            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(studentId);
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            if (viewModel.Student.Courses != null)
            {
                viewModel.SelectedCourseIds = viewModel.Student.Courses.Select(c => c.CourseId).ToList();
            }
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);

        }


        [HttpPost]
        public ActionResult Delete(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult EditAddress(int studentId)
        {
            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(studentId);
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditAddress(StudentVM studentVM)
        {
            if (!ModelState.IsValid)
            {
                studentVM.Student = StudentRepository.Get(studentVM.Student.StudentId);
                studentVM.SetStateItems(StateRepository.GetAll());
                return View(studentVM);
            }

            studentVM.Student.Address.State = StateRepository.Get(studentVM.Student.Address.State.StateAbbreviation);
            studentVM.Student.Address.AddressId = studentVM.Student.StudentId;
            StudentRepository.SaveAddress(studentVM.Student.StudentId, studentVM.Student.Address);


            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(studentVM.Student.StudentId);
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            if (viewModel.Student.Courses != null)
            {
                viewModel.SelectedCourseIds = viewModel.Student.Courses.Select(c => c.CourseId).ToList();
            }
            return RedirectToAction("List");
        }
    }
}