using Newtonsoft.Json;
using VipFitness_Management_API.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;

namespace VipFitness_Management_API.Entities
{
   
    public class UserEntity
    {
        string conn = Environment.GetEnvironmentVariable("ConnectionString");


        //public void UseMyConn()   BU ALAN USERSECRET'S DEPLOY öğrenildiğinde aktif edilecek...
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //        .AddUserSecrets<Program>() // User Secrets konfigürasyonunu ekleyin
        //        .Build();

        //    GetConnString myConn = new GetConnString(configuration);
        //   conn = myConn.SendConnStr(); // Bağlantı dizesini kullan
        //}

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return string.Equals(hashedInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        public int UserCheck(string username, string password) // Login kontrolü buradan dönecek.
        {
            //UseMyConn();
            SqlConnection sqlConnection = new SqlConnection(conn);
            sqlConnection.ConnectionString = conn;
            string hashedPassword = HashPassword(password);
            var query = "SELECT COUNT(*) FROM tblUsers WHERE username = @Username AND password = @Password";
            var parameters = new { Username = username, Password = hashedPassword };
            var get = sqlConnection.ExecuteScalar<int>(query, parameters);
            return get;
        }


        public List<User> GetUsers()
        {
            //UseMyConn();
            SqlConnection sqlConnection = new SqlConnection(conn);
            sqlConnection.ConnectionString = conn;
            var query = "Select * from tblUsers";
            var users = sqlConnection.Query<User>(query);
            return users.ToList();
        }

        public List<User> FindUser(int id) 
        {
            //UseMyConn();
            SqlConnection sqlConnection = new SqlConnection(conn);
            sqlConnection.ConnectionString = conn;
            var query = "Select * from tblUsers where id="+id;
            var user = sqlConnection.Query<User>(query);    

            return user.ToList();
        }


        // CRUD İŞLEMLERİ -
        public string AddUser(User user)
        {
            //UseMyConn();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = conn;
            string hashedPassword = HashPassword(user.password);
            var query = $"INSERT INTO tblUsers(username,password,UType,isdeleted) VALUES('{user.username}','{hashedPassword}',{user.UType},{user.isdeleted})";
            sqlConnection.Query<User>(query);
            return "Başarıyla Eklenmiştir.";
        }
        public string UpdateUser(int id,User user)
        {
            //UseMyConn();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = conn;
            string hashedPassword = HashPassword(user.password);
            var query = $"Update tblUsers Set username='{user.username}',password='{hashedPassword}',UType={user.UType},isdeleted={user.isdeleted} WHERE id={id}";
            sqlConnection.ExecuteScalar<UserEntity>(query);
            return "Başarıyla Değiştirilmiştir.";
        }
        public string DeleteUser(int id)
        {
           // UseMyConn();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = conn;
            var query = "Update tblUsers SET isdeleted=1 where id=" + id;
            sqlConnection.ExecuteScalar<UserEntity>(query);
            return "Başarıyla Silinmiştir.";
        }
    }
}
