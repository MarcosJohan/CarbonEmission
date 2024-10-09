using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities;

public class CarbonEmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int CompanyId { get; set; }  
    
    public string Description { get; set; }
    
    public float Quantity { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Type { get; set; }

    public Boolean Deleted { get; set; } = false;
}