using Microsoft.AspNetCore.Mvc;
using Todolist.Models;

namespace Todolist.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetAllTasks();
        Task<Models.Task> GetTaskById(int id);
        System.Threading.Tasks.Task AddTask(Models.Task task);
        System.Threading.Tasks.Task  UpdateTask(int id,Models.Dtos.TaskDto task);
        System.Threading.Tasks.Task  DeleteTask(int id);

    }
}
