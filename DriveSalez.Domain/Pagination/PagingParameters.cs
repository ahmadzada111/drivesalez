namespace DriveSalez.Domain.Pagination
{
    public class PagingParameters
    {
        private const int _maxPageSize = 50;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }

            set
            {
                if (value >= 0) _pageNumber = value;
            }

        }

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                if(value>= 0) _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            }
        }
    }
}
