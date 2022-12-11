using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Shoe_Store_Application.Pages.Users
{
    public class RegisterUserModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.password = Request.Form["password"];

            //Check for Empty Fields
            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 || userInfo.password.Length == 0) 
            {
                errorMessage = "All fields are required to make a new account. Please check again";
                return;
            }

            //Save new clients into database
            try 
            {
                //Connection string here
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\noure\\OneDrive\\Desktop\\SSA\\Database\\Users\\RegisteredUsers.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO users " + "(name, email, pass) Values " + "(@name, @email, @pass);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("email", userInfo.email);
                        command.Parameters.AddWithValue("pass", userInfo.password); 
                        
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            //clear all fields  
            userInfo.name = ""; userInfo.email = ""; userInfo.password = "";

            //push success message
            successMessage = "Account Registered!";
        }
       
    }
}
