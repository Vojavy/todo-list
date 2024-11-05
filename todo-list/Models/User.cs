namespace todo_list.Models
{
    public class User
    {
        public int UserId { get; set; } // Первичный ключ
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
