namespace DriveSalez.SharedKernel.Utilities;

public class PagingParameters
{
    private const int _maxPageSize = 50;
    private int _pageIndex = 1;
    private int _pageSize = 10;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value > 0) ? value : 1;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > 0) ? (value > _maxPageSize ? _maxPageSize : value) : 1; 
    }
}