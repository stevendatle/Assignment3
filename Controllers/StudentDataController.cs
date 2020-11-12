﻿using Assignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        //Accessing Database
        private SchoolDBContext School = new SchoolDBContext();

        //Purpose of this controller is to return student data

        public IEnumerable<Student> ListStudents()
        {
            //Connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens the connection
            Conn.Open();

            //Establishing a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Select * from students";

            //Gather results of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creates an empty list of students
            List<Student> Students = new List<Student> { };

            //Loop through each row of the result set
            while (ResultSet.Read())
            {
                //Accessing column information 
                int StudentId = (int)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;

          
                Students.Add(NewStudent);
            }


            Conn.Close();
          
            return Students;
        }
    }


    }

