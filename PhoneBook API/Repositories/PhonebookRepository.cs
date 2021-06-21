using PhoneBook_API.Controllers;
using PhoneBook_API.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook_API.Repositories
{
    public class PhonebookRepository
    {
        private SqlConnection connection;

        public PhonebookRepository()
        {
            string connectionStr = ConfigurationManager.ConnectionStrings["PhoneBookConnStr"].ConnectionString;
            connection = new SqlConnection(connectionStr);
        }

        public void SaveContact(PhoneBookModel phoneBookModel, User authUser)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.PhoneBook_SaveContact";
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = phoneBookModel.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = phoneBookModel.LastName;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = phoneBookModel.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = phoneBookModel.Address;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = phoneBookModel.Mobile;
            cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = authUser.Email;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteContact(int ID, User authUser)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.PhoneBook_DeleteContact";
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = authUser.Email;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void EditContact(PhoneBookModel phoneBook, User authUser)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.PhoneBook_EditContact";

            cmd.Parameters.Add("ID", SqlDbType.Int).Value = phoneBook.ID;
            cmd.Parameters.Add("FirstName", SqlDbType.NVarChar).Value = phoneBook.FirstName;
            cmd.Parameters.Add("LastName", SqlDbType.NVarChar).Value = phoneBook.LastName;
            cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = phoneBook.Email;
            cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = phoneBook.Address;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = phoneBook.Mobile;
            cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = authUser.Email;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public List<PhoneBookModel> GetAllContacts(User authUser)
        {
            var contactList = new List<PhoneBookModel>();

            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.PhoneBook_GetAllContacts";
            cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = authUser.Email;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var contact = new PhoneBookModel();
                contact.ID = int.Parse(reader["ID"].ToString());
                contact.FirstName = reader["FirstName"].ToString();
                contact.LastName = reader["LastName"].ToString();
                contact.Email = reader["Email"].ToString();
                contact.Address = reader["Address"].ToString();
                contact.Mobile = reader["Mobile"].ToString();

                contactList.Add(contact);
            }
            connection.Close();

            return contactList;

        }

    }
}