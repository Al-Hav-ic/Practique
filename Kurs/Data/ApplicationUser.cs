using Microsoft.AspNetCore.Identity;

namespace Kurs.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Додаткові властивості для ApplicationUser
        public string? FullName { get; set; } // Повне ім'я
        public string? Role { get; set; } // Роль користувача

    }
}
