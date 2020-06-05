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
            try
            {
                var task = await repository.Get(id);
                return Ok(task);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        [HttpGet]
        [Route("gettasks")]
        public async Task<ActionResult> GetTasks()
        {
            try
            {
                var tasks = await repository.GetTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        [HttpPost]
        [Route("addnewtask")]
        public async Task<ActionResult> AddTask([FromBody] Models.Task task)
        {
            try
            {
                repository.Create(task);
                return Ok("Succes");
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        [HttpGet]
        [Route("getusers")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = repository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        [HttpGet]
        [Route("getstatuses")]
        public async Task<ActionResult> GetStatuses()
        {
            try
            {
                var statuses = repository.GetStatuses();
                return Ok(statuses);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        [HttpDelete]
        [Route("deletetask/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                repository.Delete(id);
                var tasks = await repository.GetTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
    }
}