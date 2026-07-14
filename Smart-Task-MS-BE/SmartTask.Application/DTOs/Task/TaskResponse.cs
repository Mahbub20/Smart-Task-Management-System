using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartTask.Domain.Enums;

namespace SmartTask.Application.DTOs.Task
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TStatus Status { get; set; }
        public TPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}