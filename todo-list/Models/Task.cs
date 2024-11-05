namespace todo_list.Models
{
    public class Task
    {
        public int TaskId { get; set; } // Первичный ключ
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // HIGH, MID, LOW
        public string Status { get; set; } // done, undone
        public string CreatedDate { get; set; }
        public int ThemeId { get; set; } // Внешний ключ к теме
        public int UserId { get; set; } // Внешний ключ к пользователю
    }
}
