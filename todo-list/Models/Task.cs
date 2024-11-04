using System;

namespace todo_list.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // "HIGH", "MID", "LOW"
        public string Status { get; set; } // "done", "undone"
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
