using PhoneBook_API.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook_API.Repositories
{
    public class RegistrationRepo
    {
        private SqlConnection connection;

        public RegistrationRepo()
        {
            string connectionStr = ConfigurationManager.ConnectionStrings["PhoneBookConnStr"].ConnectionString;
            connection = new SqlConnection(connectionStr);
        }


        public void RegisterUser(UserRegistrationModel registrationModel)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.RegisterUser";
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = registrationModel.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = registrationModel.LastName;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = registrationModel.Email;
            cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = registrationModel.Password;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public int CheckIfUserExists(UserRegistrationModel registrationModel)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.CheckUsersPasswordAndEmail";
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = registrationModel.Email;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = registrationModel.Password;


            SqlParameter outputParameter = new SqlParameter();
            outputParameter.ParameterName = "@checkAccount";
            outputParameter.DbType = (DbType)SqlDbType.Int;
            outputParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParameter);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();


            var accountCheck = int.Parse(outputParameter.Value.ToString());
            connection.Close();
            return accountCheck;
        }

        public int CheckIfEmailExists(UserRegistrationModel registrationModel)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.CheckEmail";
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = registrationModel.Email;

            SqlParameter outputParameter = new SqlParameter();
            outputParameter.ParameterName = "@EmailCount";
            outputParameter.DbType = (DbType)SqlDbType.Int;
            outputParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParameter);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();


            var emailChecker = int.Parse(outputParameter.Value.ToString());
            connection.Close();
            return emailChecker;
        }

    }
}