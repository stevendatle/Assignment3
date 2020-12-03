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
    //DISCLAIMER: 
    //CODE CREATING USING A REFERENCE TO Learning C# For Web Development Pt11 + Pt12
    //Original Developer: Christine Bittle
    //Date Accessed: 11/11/2020
    //LINKS: PT 11: https://www.youtube.com/watch?v=uP2kH8tFXIQ&feature=youtu.be
    //LINKS: PT 12: https://www.youtube.com/watch?v=rluweOu84r0&feature=youtu.be
    //Purpose of using these videos: To practice establishing a connection between a database server and c# web applications.
    //These videos have been used to assist me in completing the requirements of Assignment 3, constructing an MVP. 


    //ASSIGNMENT 4 DISCLAIMER UPDATE:
    // Using the videos linked below, the demonstrations in the video helped me create the code for ADD, DELETE and SEARCH functionality for this assignment.
    // LINKS: PT 13: https://www.youtube.com/watch?v=sOwP2C08WZE&feature=youtu.be
    // LINKS: PT 14: https://www.youtube.com/watch?v=9Ic3EsoX-HA&feature=youtu.be
    // LINKS: PT 15: https://www.youtube.com/watch?v=4jXECEDaAQE&feature=youtu.be
    // Original Developer: Christine Bittle
    // Date Accessed: 12/3/2020


    public class TeacherDataController : ApiController
    {
        //Taking the power of our SchoolDBContext model to allow access into our MySQL Database
        private SchoolDBContext School = new SchoolDBContext();


        //This controller accesses the teachers table of our database.
        // Summary: 
        // Returns list of Teachers in the system


        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens the connection
            Conn.Open();

            //Establishing a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower (@key) or lower(teacherlname) like lower (@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();


            //Gather results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creates an empty list of teachers
            List<Teacher> Teachers = new List<Teacher>{};

            //Loop through each row of the result set
            while (ResultSet.Read())
            {
                //Accessing column information 
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"].ToString();

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
                string EmployeeNumber = (string)ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherHireDate = TeacherHireDate;

            }
            return NewTeacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST: /api/TeacherData/DeleteTeacher/2</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens the connection
            Conn.Open();

            //Establishing a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //Connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens the connection
            Conn.Open();

            //Establishing a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate) values (@TeacherFname,@TeacherLname,@EmployeeNumber, CURRENT_DATE())";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
