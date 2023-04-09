using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public int? CustomerID { get; set; }

        [Required(ErrorMessage = "Required Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Required Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Required Contact Title")]
        public string ContactTitle { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Required City")]
        public string City { get; set; }

        public string Region { get; set; }

        [Required(ErrorMessage = "Required Postal-Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Required Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required Phone")]
        public string Phone { get; set; }

        public string Fax { get; set; }
    }
}