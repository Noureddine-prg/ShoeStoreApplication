using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data.SqlClient;



namespace Shoe_Store_Application.Pages.Users
{
    public class IndexModel : PageModel
    {
        //Create list to store client data 
        public List<UserInfo> listUsers = new List<UserInfo>();

        //on get is executed when we access this page 
        //on page load this accesses database and read data from client table
        public void OnGet()
        {
            //Fill list
            try
            {
                //Connection String here
                //establish connection to database
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\noure\\OneDrive\\Desktop\\SneakerStoreApplication\\Database\\Database\\ShoeStoreApplication.mdf;Integrated Security=True;Connect Timeout=30"; 
                
                using (SqlConnection connection = new SqlConnection(connectionString)) { 
                    
                    connection.Open();
                    String query = "SELECT * FROM users";

                    using (SqlCommand command = new SqlCommand(query,connection)) { 
                    
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //read data from table and store in client info object
                            while (reader.Read()) { 
                                UserInfo userInfo = new UserInfo();
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.password = reader.GetString(3);
                                userInfo.created_at = reader.GetDateTime(4).ToString();

                                listUsers.Add(userInfo);
                            }
                        }
                    }

                };
            }
            catch (Exception ex) { 
                    
                Console.WriteLine("This doesn't work: " + ex.ToString());
            }

        }
    }

    public class UserInfo 
    {
        public String id;
        public string name;
        public string email;
        public string password;
        public string created_at;
    }
}
