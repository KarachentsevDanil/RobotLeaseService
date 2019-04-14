namespace RLS.BLL.DTOs.FilterParams
{
    public class BaseFilterParamsDto
    {
        public int Take { get; set; } = 25;

        public int Skip { get; set; } = 0;
    }
}