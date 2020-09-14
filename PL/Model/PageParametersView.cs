namespace IsraelitProTestTask.BLL.DTO
{
    public class PageParametersView
    {
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }
    }
}
