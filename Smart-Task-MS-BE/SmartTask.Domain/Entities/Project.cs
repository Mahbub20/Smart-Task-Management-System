using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTask.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; } = string.Empty;


        public ICollection<TaskItem> Tasks { get; set; }
            = new List<TaskItem>();
    }
}