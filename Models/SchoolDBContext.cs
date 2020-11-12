using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Assignment3.Models
{
    //SQL CONNECTION ESTABLISHED USING MATERIAL IN: Learning C# For Web Development Pt11
    // LINK IS FOUND HERE: "https://www.youtube.com/watch?v=uP2kH8tFXIQ&feature=youtu.be" 
    // PORT Has been changed to match my specific port on my PHPServer
    public class SchoolDBContext
    {
        private static string User { get { return "root"; } }

        private static string Password { get { return "root";  } }

        private static string Database { get { return "schooldb";  } }

        private static string Server { get { return "localhost";  } }

        private static string Port { get { return "3306";  } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        public MySqlConnection AccessDatabase()
        {
            //Instantiating the MySqlConnection Class to create an object
            //Object is a specific connection to our school database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}