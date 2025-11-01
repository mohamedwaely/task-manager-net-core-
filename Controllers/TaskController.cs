using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_manager.Data;
using task_manager.Services;

namespace task_manager.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private getTasksService _getTaskService;
        private newTaskService _newTaskService;
        private deleteTaskService _deleteTaskService;
        private updateTaskDesc _updateTaskDesc;
        private updateTaskStatus _updateTaskStatus;
        public TaskController(getTasksService getTaskService, newTaskService newTaskService, deleteTaskService deleteTaskService, updateTaskDesc updateTaskDesc, updateTaskStatus updateTaskStatus) { 
            _getTaskService = getTaskService;
            _newTaskService = newTaskService;
            _deleteTaskService = deleteTaskService;
            _updateTaskDesc = updateTaskDesc;
            _updateTaskStatus = updateTaskStatus;

        }


        [HttpGet]
        public async Task<ActionResult<List<TaskRes>>> getTasks()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid user token");
            }

            var tasks = await _getTaskService.TasksById(userId);
            return Ok(tasks);

        }


        [HttpPost]
        public async Task<ActionResult<TaskRes>> addNewTask(TaskReq req)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid user token");
            }

            var taskDescription = req.description;
            var taskStatus = req.status;
            
            if(string.IsNullOrEmpty(taskDescription) || string.IsNullOrEmpty(taskStatus))
            {
                return BadRequest("All task fields are required");
            }

            var res = await _newTaskService.addTask(userId, taskDescription, taskStatus, req.createdAt);

            return Ok(res);

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> deleteTask([FromRoute] int id)
        {
            Dictionary<string, object> res= new Dictionary<string, object>();
            if(id <= 0)
            {
                res.Add("success", false);
                res.Add("mess", "Task ID in valid");
                return BadRequest(res);
            }


            if (!await _deleteTaskService.deleteTaskById(id))
            {
                res.Add("success", false);
                res.Add("mess", "Task Not Found");
                return NotFound(res);
            }

            else
            {
                res.Add("success", true);
                res.Add("mess", "Task deleted successfully");
                return Ok(res);
            }

            
        }


        [HttpPatch]
        [Route("description")]
        public async Task<ActionResult> updateTaskDesc([FromBody] TaskDescReq taskDescReq)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            if(string.IsNullOrEmpty(taskDescReq.description) || string.IsNullOrWhiteSpace(taskDescReq.description))
            {
                res.Add("success", false);
                res.Add("mess", "Task must has valid description");
                return BadRequest(res);
            }

            var updateRes = await _updateTaskDesc.updateDesc(taskDescReq.taskId, taskDescReq.description);
            if (!updateRes)
            {
                res.Add("success", false);
                res.Add("mess", "Task not found");
                return NotFound(res);
            }


            res.Add("success", true);
            res.Add("mess", "Task Description updated");
            return Ok(res);

        }


        [HttpPatch]
        [Route("status")]
        public async Task<ActionResult> updateTaskStatus([FromBody] taskStatusReq statusReq)
        {
            Dictionary<string, object> res= new Dictionary<string, object>();
            if (statusReq.taskId < 0)
            {
                res.Add("success", false);
                res.Add("mess", "task id must be valid");
                return BadRequest(res);
            }

            if (string.IsNullOrEmpty(statusReq.status) ||  string.IsNullOrWhiteSpace(statusReq.status))
            {
                res.Add("success", false);
                res.Add("mess", "task status must be valid");
                return BadRequest(res);
            }

            var updateRes = await _updateTaskStatus.updateStatus(statusReq.taskId, statusReq.status);

            if (!updateRes)
            {
                res.Add("success", false);
                res.Add("mess", "Task Not found");
                return NotFound(res);
            }

            res.Add("success", false);
            res.Add("mess", "Task updated sucessfully");
            return Ok(res);

        }

    }
}
