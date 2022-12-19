using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shoe_Store_Application.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string message;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Email.Equals("abc") && Password.Equals("123"))
            {
                HttpContext.Session.SetString("Email", Email); 
                return RedirectToAction("Index");
            }
            else 
            {
                message = "invalid";
                return Page();
            }
        }


    }
}
