using System.Text.Json.Serialization;

namespace PosApplication.Infrastructure.WebHelpers.Pagination;

public class PageResponse
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("isFirst")]
    public bool IsFirst { get; set; }

    [JsonPropertyName("isLast")]
    public bool IsLast { get; set; }

    [JsonPropertyName("hasNext")]
    public bool HasNext { get; set; }

    [JsonPropertyName("hasPrevious")]
    public bool HasPrevious { get; set; }
}