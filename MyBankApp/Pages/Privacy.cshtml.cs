using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBankApp.Pages
{
    //Nu har du möjlighet att bestämma vilka sidor dina user roles ha tillgång till...
    //  På relevant sida lägg bara till koden:

    //   [Authorize(Roles = "Admin")]
    //eller...
    //   [Authorize(Roles = "Cashier")]

    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
