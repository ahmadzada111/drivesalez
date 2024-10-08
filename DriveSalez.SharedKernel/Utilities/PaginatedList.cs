namespace DriveSalez.SharedKernel.Utilities;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; }
        
    public int TotalPages { get; }
        
    public int TotalCount { get; }
        
    public bool HasPreviousPage => PageIndex > 1;
        
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedList()
    {
        
    }
    
    public PaginatedList(IEnumerable<T> items, int pageIndex, int totalPages, int totalCount)
    {
        AddRange(items);
        PageIndex = pageIndex;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }
    
    public static PaginatedList<T> ToPaginatedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        return new PaginatedList<T>(items, pageIndex, totalPages, totalCount);
    }
}