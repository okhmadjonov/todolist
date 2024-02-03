using Microsoft.AspNetCore.Mvc;
using Todolist.Models;
using Todolist.Repositories;
using System.Threading.Tasks;
using Todolist.Services;
using Todolist.Models.Dtos;
using AutoMapper;

namespace Todolist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public HomeController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(Models.Task taskDto)
        {
            if (ModelState.IsValid)
            {
                await _taskRepository.AddTask(taskDto);
                return RedirectToAction("Index");
            }
            else
            {
                return View(taskDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Models.Task taskDto)
        {
           

            if (ModelState.IsValid)
            {
                await _taskRepository.UpdateTask(id, taskDto);
                return RedirectToAction("Index");
            }
            return View(taskDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskRepository.DeleteTask(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> MarkCompleted(int id, bool isCompleted)
        {
            try
            {
                var task = await _taskRepository.GetTaskById(id);
                //var taskDto = _mapper.Map<TaskDto>(task);
                if (task == null)
                {
                    return NotFound();
                }

                task.IsCompleted = isCompleted;

                await _taskRepository.UpdateTask(id,task); 

                return Ok();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while marking task as completed.");
            }
        }


    }
}