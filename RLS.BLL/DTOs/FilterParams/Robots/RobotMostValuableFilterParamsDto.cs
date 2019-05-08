namespace RLS.BLL.DTOs.FilterParams.Robots
{
    public class RobotMostValuableFilterParamsDto
    {
        public string UserId { get; set; }

        public int TypeId { get; set; }

        public int Take { get; set; }

        public int CurrentRobotId { get; set; }
    }
}