using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoe_Store_Application.Pages.Product;
using System.Data.SqlClient;

namespace Shoe_Store_Application.Pages.Checkout
{
    public class CheckoutModel : PageModel
    {
            
        public List<ProductInfo> itemList = new List<ProductInfo>();
        public double totalPrice = 0;
        public double salesTax = 0;
        public int totalItems = 0;

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("_Cart"))) return; 
    
            var cart = HttpContext.Session.Get<List<String>>("_Cart");

            if (cart == null) return;


            foreach (var itemId in cart) 
            { 
                AddProductToCheckout(itemId);
                totalItems++;
            }

            
        }

        public void AddProductToCheckout(String idQuery)         
        {
            //fill container with product info
            try
            {
                //connection string here
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\noure\\OneDrive\\Desktop\\SneakerStoreApplication\\Database\\Database\\ShoeStoreApplication.mdf;Integrated Security=True;Connect Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT * from products WHERE product_id='" + idQuery + "'";

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
                                totalPrice += productInfo.price;
                                salesTax = Math.Floor(totalPrice * .045);

                                totalPrice = Math.Floor(salesTax + totalPrice);

                                itemList.Add(productInfo);
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
}
