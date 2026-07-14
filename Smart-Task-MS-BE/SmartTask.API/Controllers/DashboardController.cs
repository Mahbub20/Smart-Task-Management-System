using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTask.Application.DTOs.Dashboard;
using SmartTask.Domain.Enums;
using SmartTask.Infrastructure.Persistance;

namespace SmartTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly SmartTaskDbContext _context;

        public DashboardController(SmartTaskDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {

            var userId = GetUserId();

            var projects =
                _context.Projects
                .Where(x => x.UserId == userId);

            var tasks =
                _context.Tasks
                .Where(x => x.Project.UserId == userId);

            var totalProjects =
                await projects.CountAsync();

            var totalTasks =
                await tasks.CountAsync();

            var completedTasks =
                await tasks
                .CountAsync(x =>
                    x.Status == TStatus.Completed);

            var pendingTasks =
                totalTasks - completedTasks;

            var tasksByStatus =
                await tasks
                .GroupBy(x => x.Status)
                .Select(x => new SummaryDto
                {
                    Status = x.Key.ToString(),

                    Count = x.Count()

                })
                .ToListAsync();



            var tasksByPriority =
                await tasks
                .GroupBy(x => x.Priority)
                .Select(x => new SummaryDto
                {

                    Priority = x.Key.ToString(),

                    Count = x.Count()

                })
                .ToListAsync();



            var upcomingTasks =
                await tasks

                .Where(x =>
                    x.DueDate != null &&
                    x.DueDate >= DateTime.UtcNow)

                .OrderBy(x => x.DueDate)

                .Take(5)

                .Select(x => new UpcomingTaskDto
                {

                    Id = x.Id,

                    Title = x.Title,

                    DueDate = x.DueDate

                })

                .ToListAsync();



            var response = new DashboardResponse
            {

                TotalProjects = totalProjects,


                TotalTasks = totalTasks,


                CompletedTasks = completedTasks,


                PendingTasks = pendingTasks,


                TasksByStatus = tasksByStatus,


                TasksByPriority = tasksByPriority,


                UpcomingTasks = upcomingTasks

            };



            return Ok(response);

        }
    }
}