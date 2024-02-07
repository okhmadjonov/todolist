using System.Collections.Generic;
using System.Threading.Tasks;
using Todolist.Models;
using Todolist.Data;
using Microsoft.EntityFrameworkCore;
using Todolist.Repositories;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace Todolist.Services
{
    public class TaskService : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasks()
        {
            var tasks = await  _context.Tasks
                .OrderByDescending(t => t.Priority)
                .ThenBy(t=> t.Id)
                .ToListAsync();

            return tasks;
        }

        public  async Task<Models.Task> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        }

        public async System.Threading.Tasks.Task AddTask(Models.Task task)
        {
   
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateTask(int id, Models.Task task)
        {
            var foundTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (foundTask != null)
            {
                foundTask.Title = task.Title;
                foundTask.Description = task.Description;
                foundTask.Priority = task.Priority;
                foundTask.StartDate = task.StartDate;
                foundTask.EndDate = task.EndDate;
                foundTask.IsCompleted = task.IsCompleted;
                await _context.SaveChangesAsync();
            }
        }


        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
