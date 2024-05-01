namespace Blblan.Common.Models;

public class PagedResult<T>
{
    public List<T> Items { get; set; }

    public int TotalCount { get; set; }

    public PagedResult()
    {
        Items = new List<T>();
        TotalCount = 0;
    }
}
