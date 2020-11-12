using Assignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment3.Controllers
{
    //DISCLAIMER: CODE CREATING USING A REFERENCE TO Learning C# For Web Development Pt11 + Pt12
    //LINKS: PT 11: https://www.youtube.com/watch?v=uP2kH8tFXIQ&feature=youtu.be
    //LINKS: PT 12: https://www.youtube.com/watch?v=rluweOu84r0&feature=youtu.be

    public class TeacherDataController : ApiController
    {
        //Taking the power of our SchoolDBContext model to allow access into our MySQL Database
        private SchoolDBContext School = new SchoolDBContext();


        //This controller accesses the teachers table of our database.
        // Summary: 
        // Returns list of Teachers in the system


        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens the connection
            Conn.Open();

            //Establishing a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Select * from teachers";

            //Gather results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creates an empty list of teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row of the result set
            while (ResultSet.Read())
            {
                //Accessing column information 
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;

                //Adding the teacher name to the list
                Teachers.Add(NewTeacher);
            }

            //Closing the connection between WebServver and MySql Database
            Conn.Close();

            //Returning the final list of teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            //Connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens the connection
            Conn.Open();

            //Establishing a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            //Gather results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Accessing column information 
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;

            }
            return NewTeacher;
        }

    }
}
