using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shoe_Store_Application.Pages
{
    public class IndexModel : PageModel
    {
        public readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string Email;
        public void OnGet()
        {
            Email = HttpContext.Session.GetString("email");
        }
    }
}