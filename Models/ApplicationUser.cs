using Microsoft.AspNetCore.Identity;

namespace Portal.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cell { get; set; }
    }
}
