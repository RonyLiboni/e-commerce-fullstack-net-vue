namespace Rony.Store.Domain.Parameters.Pagination;
public class PagedResult<T>
{
    public List<T> Results { get; set; } = [];
    public int Count { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PagedResult(List<T> results, PageParameters parameters)
    {
        Results = results;
        PageNumber = parameters.PageNumber;
        PageSize = parameters.PageSize;
        Count = parameters.Count;
    }
}
