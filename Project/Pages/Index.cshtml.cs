using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITenantInfo _tenant;

        public IndexModel(ITenantInfo tenant)
        {
            _tenant = tenant;
        }

        public void OnGet()
        {
        }
    }
}
