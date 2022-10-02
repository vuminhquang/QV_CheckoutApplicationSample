using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RepositoryHelper.Abstraction;

namespace PosApplication.Domain.Entities;

[Table("basket")]
public class Basket : BaseEntity<long>
{
    [JsonPropertyName("articles")]
    public List<Article> Articles { get; set; }

    [JsonPropertyName("totalNet")]
    public decimal TotalNet { get; set; }

    [JsonPropertyName("totalGross")]
    public decimal TotalGross { get; set; }

    [JsonPropertyName("customer")]
    public string Customer { get; set; }

    [JsonPropertyName("paysVAT")]
    public bool PaysVAT { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}