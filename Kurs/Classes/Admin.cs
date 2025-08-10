
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Kurs.Data;
using Microsoft.EntityFrameworkCore;
using Kurs.Classes;

namespace Kurs.Classes
{
    public class Admin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventData _context;
        private readonly ApplicationDbContext _dbContext;

        public Admin(UserManager<ApplicationUser> userManager, IEventData context, ApplicationDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Метод для додавання нового користувача
        public async Task<IdentityResult> AddUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        // Метод для видалення користувача
        public async Task RemoveUser(string userId, List<ApplicationUser> allUsers)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            if (allUsers == null)
            {
                throw new ArgumentNullException(nameof(allUsers));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Видалення повідомлень користувача
                var messagesToDelete = _dbContext.Messages.Where(m => m.ReceiverId == user.Id || m.SenderId == user.Id);
                _dbContext.Messages.RemoveRange(messagesToDelete);

                // Видалення користувача
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    allUsers.Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Failed to delete user with ID {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        // Метод для отримання всіх подій
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.GetAllEventsAsync();  // Використовуємо асинхронний метод з IEventData
        }

        // Метод для отримання події за ідентифікатором
        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.GetEventByIdAsync(id);  // Використовуємо асинхронний метод з IEventData
        }

        // Метод для додавання нової події
        public async Task AddEventAsync(Event eventItem)
        {
            await _context.AddEventAsync(eventItem);  // Використовуємо асинхронний метод з IEventData
        }

        // Метод для оновлення події
        public async Task UpdateEventAsync(Event eventItem)
        {
            await _context.UpdateEventAsync(eventItem);  // Використовуємо асинхронний метод з IEventData
        }

        // Метод для видалення події
        public async Task DeleteEventAsync(int id)
        {
            await _context.DeleteEventAsync(id);  // Використовуємо асинхронний метод з IEventData
        }

        // Метод для отримання всіх користувачів
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
