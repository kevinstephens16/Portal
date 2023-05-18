using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Models
{

    public class Product
    {
        [Key]    
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)" )]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Category { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        //public DateTime Date { get; set; }
    }
}
