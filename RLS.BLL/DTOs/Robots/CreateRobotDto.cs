﻿namespace RLS.BLL.DTOs.Robots
{
    public class CreateRobotDto
    {
        public double DailyCosts { get; set; }

        public string UserId { get; set; }

        public int ModelId { get; set; }

        public string Photo { get; set; }

        public string Icon { get; set; }
    }
}