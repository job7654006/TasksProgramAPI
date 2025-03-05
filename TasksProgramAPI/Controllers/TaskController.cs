using Microsoft.AspNetCore.Mvc;
using TasksProgramAPI.Models;
using TasksProgramAPI.Services;

namespace TasksProgramAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController()
        {
            _taskService = new TaskService();
        }

        [HttpGet]
        public ActionResult<List<TaskModel>> GetTasks() => _taskService.GetAllTasks();

        [HttpGet("{id}")]
        public ActionResult<TaskModel> GetTask(int id)
        {
            var task = _taskService.GetTaskById(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            _taskService.AddTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            return _taskService.UpdateTask(id, updatedTask) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            return _taskService.DeleteTask(id) ? NoContent() : NotFound();
        }
    }
}