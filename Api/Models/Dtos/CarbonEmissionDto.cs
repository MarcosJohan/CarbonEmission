using System.Text.Json.Serialization;
using Api.Models.Entities;

namespace Api.Models.Dtos;

public class CarbonEmissionDto
{
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("CompanyId")]
    public int CompanyId { get; set; }
    
    [JsonPropertyName("Description")]
    public string Description { get; set; }
    
    [JsonPropertyName("Quatity")]
    public float Quantity { get; set; }

    [JsonPropertyName("Type")] 
    public string Type { get; set; }

    public CarbonEmission ToEntity()
    {
        var entity = new CarbonEmission
        {
            CompanyId = this.CompanyId,
            Description = this.Description,
            Quantity = this.Quantity,
            Type = this.Type,
            Date = this.Date
        };

        return entity;
    }
}