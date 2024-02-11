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
        public async Task<IActionResult> Index(FilterViewModel filter, int? page, string title)
        {
            int pageSize = 3;
            var pageNumber = page ?? 1;
            var tasks = await _taskRepository.GetAllTasks();

            var searchTitle = title?.ToLower();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                tasks = tasks.Where(t => t.Title.ToLower().Contains(searchTitle)).ToList();
            }

           
            bool tasksFound = true;
            ViewData["CurrentFilter"] = title;

            if (filter != null)
            {
                var priority = filter.Priority;
                var completed = filter.IsCompleted;
                var startDate = filter.StartDate;
                var endDate = filter.EndDate;

              
                if (priority.HasValue)
                {
                    tasks = tasks.Where(t => t.Priority == priority.Value).ToList();
                }

               
                if (completed.HasValue)
                {
                    tasks = tasks.Where(t => t.IsCompleted == completed.Value).ToList();
                }

                
                if (startDate.HasValue && endDate.HasValue == false)
                {
                    tasks = tasks.Where(t => t.StartDate == startDate.Value).ToList();
                    Console.WriteLine(tasks);
                }

                if (startDate.HasValue && endDate.HasValue)
                { 
                 tasks = tasks.Where(t => t.StartDate >= startDate.Value && t.StartDate <= endDate.Value).ToList();
                }


                if (tasks.Count() == 0)
                {
                    tasksFound = false;
                }
            }

            var paginatedTasks = tasks.ToPagedList(pageNumber, pageSize);

            
            ViewBag.TasksFound = tasksFound;

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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return RedirectToAction("Index"); 
            }
            return View(task);
        }


    }
}