using PhoneBook_API.Controllers;
using PhoneBook_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using WebApiOwin.Controllers;

namespace WebApiOwin.Repositories
{
    public class UsersRepository
    {
        MyDB mydb = new MyDB();

        public UserRegistrationModel GetAuthUser(string userName, string password)
        {
            var authUser = mydb.GetAllContacts().Where(x => x.Email == userName && x.Password == password).Select(x => new UserRegistrationModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                //აქ რო არ გადავცემთ password-ს მაგიტომ ბრუნდება null უკან
                Email = x.Email
            }).FirstOrDefault(); //სავარაუდოდ ეს აბრუნებინებს null-ს პაროლს

            return authUser;
        }
    }

    public class MyDB
    {

        private SqlConnection connection;

        public MyDB()
        {
            string connectionStr = ConfigurationManager.ConnectionStrings["PhoneBookConnStr"].ConnectionString;
            connection = new SqlConnection(connectionStr);
        }

        public List<UserRegistrationModel> GetAllContacts()
        {
            List<UserRegistrationModel> contactList = new List<UserRegistrationModel>();

            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.getUsers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var contact = new UserRegistrationModel();
                contact.FirstName = reader["FirstName"].ToString();
                contact.LastName = reader["LastName"].ToString();
                contact.Email = reader["Email"].ToString();
                contact.Password = reader["Password"].ToString();

                contactList.Add(contact);
            }
            connection.Close();

            return contactList;

        }
    }
}