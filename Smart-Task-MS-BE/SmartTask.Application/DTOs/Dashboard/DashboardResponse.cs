using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTask.Application.DTOs.Dashboard
{
    public class DashboardResponse
    {
        public int TotalProjects { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public List<SummaryDto> TasksByStatus { get; set; }
            = new();
        public List<SummaryDto> TasksByPriority { get; set; }
            = new();
        public List<UpcomingTaskDto> UpcomingTasks { get; set; }
            = new();
    }
}