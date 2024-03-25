using Smartwyre.DeveloperTest.DataStore.Rebates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartwyre.DeveloperTest.DataStore.RebateCalculations;

[Table("RebateCalculations")]
public class RebateCalculation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Identifier { get; set; } = string.Empty;

    [Required]
    public string RebateIdentifier { get; set; } = string.Empty;

    [Required]
    public IncentiveType IncentiveType { get; set; }

    [Required]
    public decimal Amount { get; set; }
}
