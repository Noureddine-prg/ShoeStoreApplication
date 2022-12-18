using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Shoe_Store_Application.Pages.Product
{
    public class IndexModel : PageModel
    {

        public List<ProductInfo> listProduct = new List<ProductInfo>();
        public void OnGet()
        {
            //fill container with product info
            try 
            {                
                //connection string here
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\noure\\OneDrive\\Desktop\\SneakerStoreApplication\\Database\\Database\\ShoeStoreApplication.mdf;Integrated Security=True;Connect Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String query = "SELECT * from products";

                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            //read data from product table and store in product info object 
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.id = reader.GetString(0);
                                productInfo.name = reader.GetString(1);
                                productInfo.price = reader.GetInt32(2);
                                productInfo.description = reader.GetString(3);
                                productInfo.size = reader.GetInt32(4);
                                productInfo.category = reader.GetString(5); 
                                productInfo.image = reader.GetString(6);
                               
                                listProduct.Add(productInfo);
                            }
                        }
                    }
                }   
            } 
            catch (Exception ex) 
            { 
            
            }
        }
    }

    //might have to make adjustments to data types to conform to db standards
    public class ProductInfo
    {
        public String id;
        public String name;
        public String description;
        public String category;
        public String image;
        
        public int size;
        public int price;
    }
}
