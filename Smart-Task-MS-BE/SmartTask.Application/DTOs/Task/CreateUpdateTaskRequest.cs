using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartTask.Domain.Enums;

namespace SmartTask.Application.DTOs.Task
{
    public class CreateUpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TPriority Priority { get; set; }
        public TStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}