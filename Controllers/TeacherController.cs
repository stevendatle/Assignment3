using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    //DISCLAIMER: This controller was created using reference material from Learning C# For Web Development Pt 11 + Pt 12
    // LINKS: https://www.youtube.com/watch?v=uP2kH8tFXIQ&feature=youtu.be
    // LINKS: https://www.youtube.com/watch?v=rluweOu84r0&feature=youtu.be
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET : /Teachers/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);
            
            return View(newTeacher);
        }

    }
}