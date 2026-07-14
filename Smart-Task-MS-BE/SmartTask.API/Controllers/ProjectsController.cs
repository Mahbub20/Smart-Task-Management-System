using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTask.Application.DTOs.Project;
using SmartTask.Domain.Entities;
using SmartTask.Infrastructure.Persistance;

namespace SmartTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly SmartTaskDbContext _context;
        public ProjectsController(SmartTaskDbContext context)
        {
            _context = context;
        }

        private String GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // Create Project
        [HttpPost]
        public async Task<IActionResult> Create(
    CreateUpdateProjectRequest request)
        {

            var userId = GetUserId();


            var project = new Project
            {

                Name = request.Name,

                Description = request.Description,

                CreatedDate = DateTime.UtcNow,

                UserId = userId

            };


            _context.Projects.Add(project);


            await _context.SaveChangesAsync();



            return Ok(new ProjectResponse
            {

                Id = project.Id,

                Name = project.Name,

                Description = project.Description,

                CreatedDate = project.CreatedDate

            });

        }

        //Get all user projects
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var userId = GetUserId();


            var projects =
                await _context.Projects

                .Where(x => x.UserId == userId)

                .Select(x => new ProjectResponse
                {

                    Id = x.Id,

                    Name = x.Name,

                    Description = x.Description,

                    CreatedDate = x.CreatedDate

                })

                .ToListAsync();



            return Ok(projects);

        }

        //Get project details by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
int id)
        {

            var userId = GetUserId();


            var project =
                await _context.Projects

                .Where(x =>
                    x.Id == id &&
                    x.UserId == userId)

                .Select(x => new ProjectResponse
                {

                    Id = x.Id,

                    Name = x.Name,

                    Description = x.Description,

                    CreatedDate = x.CreatedDate

                })

                .FirstOrDefaultAsync();



            if (project == null)
            {
                return NotFound();
            }


            return Ok(project);

        }

        //Update project
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
int id,
CreateUpdateProjectRequest request)
        {

            var userId = GetUserId();


            var project =
                await _context.Projects
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.UserId == userId);



            if (project == null)
            {
                return NotFound();
            }



            project.Name = request.Name;

            project.Description = request.Description;



            await _context.SaveChangesAsync();



            return Ok(
                new
                {
                    message = "Project updated successfully"
                });

        }

        //Delete project
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
int id)
        {

            var userId = GetUserId();


            var project =
                await _context.Projects
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.UserId == userId);



            if (project == null)
            {
                return NotFound();
            }



            _context.Projects.Remove(project);


            await _context.SaveChangesAsync();



            return Ok(
                new
                {
                    message = "Project deleted successfully"
                });

        }
    }
}