using Portal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Portal.Pages {

    [Authorize]
    public class StoreModel : PageModel {
        public StoreModel(ProductDbContext ctx) => DbContext = ctx;

        public ProductDbContext DbContext { get; set; }
    }
}
