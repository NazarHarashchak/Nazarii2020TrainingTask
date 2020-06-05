using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Api.Services;
using Training_Api.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Training_Api.Classes
{
    public class TaskRepository : ITaskRepository
    {
        string connectionString = null;

        public TaskRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async System.Threading.Tasks.Task<List<Models.Task>> GetTasks()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = string.Format("SELECT Tasks.Id, Title, TaskDescription, DueDate, StatusID, TaskStatus AS ActiveStatus,CreatedUserID,UserFullName AS CreatedUserName,AssigneeUserID, (SELECT UserFullName FROM USERS WHERE Tasks.AssigneeUserID = USERS.Id) AS AssigneeUserName FROM Tasks JOIN TaskStatuses ON TaskStatuses.Id = Tasks.StatusId JOIN USERS ON Tasks.CreatedUserId = USERS.Id");
                return db.Query<Models.Task>(query).ToList();
            }
        }

        public async System.Threading.Tasks.Task<Models.Task> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = string.Format("SELECT Tasks.Id, Title, TaskDescription, DueDate, StatusID, TaskStatus AS ActiveStatus,CreatedUserID,UserFullName AS CreatedUserName,AssigneeUserID, (SELECT UserFullName FROM USERS WHERE Tasks.AssigneeUserID = USERS.Id) AS AssigneeUserName FROM Tasks JOIN TaskStatuses ON TaskStatuses.Id = Tasks.StatusId JOIN USERS ON Tasks.CreatedUserId = USERS.Id WHERE Tasks.Id = {0}", id);
                return db.Query<Models.Task>(query).FirstOrDefault();
            }
        }

        public async void Create(Models.Task task)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                task.DueDate = DateTime.Now;
                string query = string.Format("INSERT INTO Tasks " +
                    "(Title, TaskDescription, CreatedUserID, StatusID, AssigneeUserID, DueDate)" +
                    " VALUES ('{0}','{1}',{2},{3},{4},'{5}')",
                    task.Title, task.TaskDescription, task.CreatedUserId, task.StatusId, task.AssigneeUserId, task.DueDate.ToShortDateString());
                await db.ExecuteAsync(query);
            }
        }

        public async void Update(Models.Task task)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = string.Format("UPDATE Tasks SET " +
                    "Title={0}, TaskDescription={1}, CreatedUserID={2}, StatusID={3}, AssigneeUserID={4}, DueDate={5}" +
                    "WHERE Id = {6}",
                    task.Title, task.TaskDescription, task.CreatedUserId, task.StatusId, task.AssigneeUserId, task.DueDate, task.Id);
                
                await db.ExecuteAsync(query);
            }
        }

        public async void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Tasks WHERE Id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
        public async System.Threading.Tasks.Task<List<Models.User>> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT * FROM USERS");
                return db.Query<User>(query).ToList();
            }
        }
        public async System.Threading.Tasks.Task<List<Models.TaskStatus>> GetStatuses()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = string.Format("SELECT Id, TaskStatus AS Status FROM TaskStatuses");
                return db.Query<Models.TaskStatus>(query).ToList();
            }
        }
    }
}
