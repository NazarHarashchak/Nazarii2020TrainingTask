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
                var sqlQuery = "INSERT INTO Tasks (Title, Description) VALUES(@Title, @TaskDescription)";
                await db.ExecuteAsync(sqlQuery, task);
            }
        }

        public async void Update(Models.Task task)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET Title = @Title, TaskDescription = @TaskDescription WHERE Id = @Id";
                await db.ExecuteAsync(sqlQuery, task);
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
    }
}
