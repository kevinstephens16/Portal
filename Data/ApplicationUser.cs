using Microsoft.AspNetCore.Identity;

namespace Portal.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }

    }
}
