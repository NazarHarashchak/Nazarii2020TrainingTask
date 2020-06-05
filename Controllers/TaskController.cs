using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_Api.Services;
using Training_Api.Classes;

namespace Training_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskRepository repository;
        public TaskController(ITaskRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        [Route("gettask/{id}")]
        public async Task<ActionResult> GetTask(int id)
        {
            var task = await repository.Get(id);
            return Ok(task);
        }
        [HttpGet]
        [Route("gettasks")]
        public async Task<ActionResult> GetTasks()
        {
            var tasks = await repository.GetTasks();
            return Ok(tasks);
        }
    }
}