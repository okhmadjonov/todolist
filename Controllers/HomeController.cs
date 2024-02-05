using Microsoft.AspNetCore.Mvc;
using Todolist.Models;
using Todolist.Repositories;
using System.Threading.Tasks;
using Todolist.Services;
using Todolist.Models.Dtos;
using AutoMapper;
using System.Linq;

using X.PagedList;



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

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 3;
            var tasks = await _taskRepository.GetAllTasks();

            var pageNumber = page ?? 1;
            var paginatedTasks = tasks.ToPagedList(pageNumber, pageSize);

            return View(paginatedTasks);
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
           
                var task = await _taskRepository.GetTaskById(id);
               
                if (task == null)
                {
                    return NotFound();
                }

                task.IsCompleted = isCompleted;

                await _taskRepository.UpdateTask(id,task); 

                return Ok();
        }
    }
}