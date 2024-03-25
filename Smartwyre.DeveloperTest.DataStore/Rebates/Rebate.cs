using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartwyre.DeveloperTest.DataStore.Rebates;

[Table("Rebates")]
public class Rebate
{
    [Key]
    public int Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public IncentiveType Incentive { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
}
