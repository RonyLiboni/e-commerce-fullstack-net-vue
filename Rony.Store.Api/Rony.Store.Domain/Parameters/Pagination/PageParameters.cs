using System.Text.Json.Serialization;

namespace Rony.Store.Domain.Parameters.Pagination;
public abstract class PageParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    [JsonIgnore]
    public int Skip => PageSize * (PageNumber - 1);
    [JsonIgnore]
    public int Count {  get; set; }
}
