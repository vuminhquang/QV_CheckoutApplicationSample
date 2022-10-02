using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RepositoryHelper.Abstraction;

namespace PosApplication.Domain.Entities;

[Table("article")]
public class Article : BaseEntity<long>
{
    [JsonPropertyName("article")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [ForeignKey(nameof(Basket))]
    public long BasketId { get; set; }
    
    [JsonIgnore] public virtual Basket? Basket { get; set; }
}
