using Microsoft.AspNetCore.Mvc;

namespace Portal.Pages.Identity {

    public class SignInCodesWarningModel : UserPageModel {

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; } = "/";
    }
}
