using System.Collections.Generic;
using Newtonsoft.Json;

namespace RLS.WebApi.Models.Charts
{
    public class BarChartModel
    {
        public IEnumerable<string> Titles { get; set; }

        public IEnumerable<int> RobotRentsCount { get; set; }

        public IEnumerable<int> RobotsCount { get; set; }
    }
}