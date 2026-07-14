using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTask.Application.DTOs.Task;
using SmartTask.Domain.Entities;
using SmartTask.Domain.Enums;
using SmartTask.Infrastructure.Persistance;

namespace SmartTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly SmartTaskDbContext _context;

        public TasksController(SmartTaskDbContext context)
        {
            _context = context;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;
        }

        [HttpPost("projects/{projectId}/tasks")]
        public async Task<IActionResult> Create(int projectId, CreateUpdateTaskRequest request)
        {

            var userId = GetUserId();


            var project =
                await _context.Projects
                .FirstOrDefaultAsync(x =>
                    x.Id == projectId &&
                    x.UserId == userId);



            if (project == null)
            {
                return NotFound("Project not found");
            }



            var task = new TaskItem
            {

                Title = request.Title,

                Description = request.Description,

                Priority = request.Priority,

                Status = request.Status,

                DueDate = request.DueDate,

                ProjectId = projectId

            };


            _context.Tasks.Add(task);


            await _context.SaveChangesAsync();



            return Ok(new TaskResponse
            {
                Id = task.Id,

                Title = task.Title,

                Description = task.Description,

                Status = task.Status,

                Priority = task.Priority,

                DueDate = task.DueDate
            });

        }

        [HttpGet("projects/{projectId}/tasks")]
        public async Task<IActionResult> GetTasks(int projectId)
        {

            var userId = GetUserId();



            var tasks =
                await _context.Tasks

                .Where(x =>
                    x.ProjectId == projectId &&
                    x.Project.UserId == userId)

                .Select(x => new TaskResponse
                {

                    Id = x.Id,

                    Title = x.Title,

                    Description = x.Description,

                    Status = x.Status,

                    Priority = x.Priority,

                    DueDate = x.DueDate

                })

                .ToListAsync();



            return Ok(tasks);

        }

        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> Update(int id, CreateUpdateTaskRequest request)
        {

            var userId = GetUserId();


            var task =
                await _context.Tasks

                .Include(x => x.Project)

                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.Project.UserId == userId);



            if (task == null)
            {
                return NotFound();
            }



            task.Title = request.Title;

            task.Description = request.Description;

            task.Status = request.Status;

            task.Priority = request.Priority;

            task.DueDate = request.DueDate;



            await _context.SaveChangesAsync();



            return Ok(
                new
                {
                    message = "Task updated"
                });

        }

        [HttpDelete("tasks/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var userId = GetUserId();
            var task =
                await _context.Tasks

                .Include(x => x.Project)

                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.Project.UserId == userId);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message = "Task deleted"
                });

        }
    }
}