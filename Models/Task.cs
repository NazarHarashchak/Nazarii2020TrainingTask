using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Api.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public int AssigneeUserId { get; set; }
        public string AssigneeUserName { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
        public string ActiveStatus { get; set; }
    }
}
