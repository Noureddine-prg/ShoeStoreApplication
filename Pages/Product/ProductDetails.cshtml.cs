using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Shoe_Store_Application.Pages.Product
{
    public class ProductDetailsModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public void OnGet()
        {
            String idQuery = HttpContext.Request.Query["id"].ToString();
            String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\noure\\OneDrive\\Desktop\\SneakerStoreApplication\\Database\\Database\\ShoeStoreApplication.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            { 
                connection.Open();
                String query = "SELECT * from products WHERE product_id='" + idQuery + "'";

                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        while (reader.Read()) 
                        {
                            productInfo.id = reader.GetString(0);
                            productInfo.name = reader.GetString(1);
                            productInfo.price = reader.GetInt32(2);
                            productInfo.description = reader.GetString(3);
                            productInfo.size = reader.GetInt32(4);
                            productInfo.category = reader.GetString(5);
                            productInfo.image = reader.GetString(6);

                        }
                    }    
                }
            } 
        }
    }
}
