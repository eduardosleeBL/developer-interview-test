using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartwyre.DeveloperTest.DataStore.Products;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Uom { get; set; } = string.Empty;
    public SupportedIncentiveType SupportedIncentives { get; set; }
}
