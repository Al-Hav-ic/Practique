using Kurs.Data;

namespace Kurs.Classes
{
    public class Review
    {
        public int Id { get; set; } // ID відгуку
        public int EventId { get; set; } // Foreign key до Event
        public Event Event { get; set; } // Навігаційне поле до Event
        public string UserId { get; set; } // ID користувача, який залишив відгук
        public ApplicationUser User { get; set; } // Навігаційне поле до користувача
        public string Content { get; set; } // Текст відгуку
        public DateTime CreatedAt { get; set; } // Дата створення відгуку
    }
}
