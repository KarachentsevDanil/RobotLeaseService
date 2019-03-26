namespace RLS.BLL.DTOs.FilterParams
{
    public class BaseFilterParamsDto
    {
        public int PageSize { get; set; } = 25;

        public int PageNumber { get; set; } = 1;
    }
}