using System.Text.Json.Serialization;

namespace PosApplication.Domain.Dtos;

public record Status(
    [property: JsonPropertyName("status")] string Name
);