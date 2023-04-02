using Microsoft.AspNetCore.Authorization;

namespace Portal.Pages.Identity.Admin {

    //[AllowAnonymous]
    [Authorize(Roles = "Admin")]
    public class AdminPageModel : UserPageModel {

        // no methods or properties required
    }
}
