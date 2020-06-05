using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Api.Models;

namespace Training_Api.Services
{
    public interface ITaskRepository
    {
        public void Create(Models.Task task);
        public void Delete(int id);
        public System.Threading.Tasks.Task<Models.Task> Get(int id);
        public System.Threading.Tasks.Task<List<Models.Task>> GetTasks();
        public void Update(Models.Task task);
        public System.Threading.Tasks.Task<List<Models.User>> GetUsers();
        public System.Threading.Tasks.Task<List<Models.TaskStatus>> GetStatuses();
    }
}
