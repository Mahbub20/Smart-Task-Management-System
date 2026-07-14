using System;
using System.Collections.Generic;
using System.Linq;
using SmartTask.Domain.Enums;

namespace SmartTask.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;


        public string? Description { get; set; }


        public TStatus Status { get; set; }

        public TPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}