namespace DriveSalez.SharedKernel.Pagination;

public class PagingParameters
{
    private const int _maxPageSize = 50;
    private int _pageNumber = 1;
    private int _pageSize = 10;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value > 0) ? value : 1;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > 0) ? (value > _maxPageSize ? _maxPageSize : value) : 1; 
    }
}