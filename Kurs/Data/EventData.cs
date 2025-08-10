
using Kurs.Classes;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;  

namespace Kurs.Data
{
    public class EventData : IEventData
    {
        private readonly DataEvent _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NavigationManager _navigationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventData(DataEvent context, UserManager<ApplicationUser> userManager, NavigationManager navigationManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
        }

     
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        // Get an event by ID asynchronously
        //public async Task<Event> GetEventByIdAsync(int id)
        //{
        //    return await _context.Events.FindAsync(id);
        //}
        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _context.Events
                .Include(e => e.Participants)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        // Add a new event asynchronously
        public async Task AddEventAsync(Event eventItem)
        {
            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Organizer>> GetAllOrganizersAsync()
        {
            return await _context.Organizers.Include(o => o.User).ToListAsync();
        }

        public async Task UpdateEventAsync(Event eventItem)
        {
            _context.Events.Update(eventItem);
            await _context.SaveChangesAsync();
        }

        // Delete an event asynchronously
        public async Task DeleteEventAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);
                await _context.SaveChangesAsync();
            }
        }

        // Get an organizer by user ID asynchronously
        public async Task<Organizer> GetOrganizerByUserIdAsync(string userId)
        {
            return await _context.Organizers.FirstOrDefaultAsync(o => o.UserId == userId);
        }

        public async Task AddOrganizerAsync(Organizer organizer)
        {
            _context.Organizers.Add(organizer);
            await _context.SaveChangesAsync();
        }

       

        public async Task<Participant> RegisterForEventAsync(int eventId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            // Перевірка, чи вже є учасник
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (participant == null)
            {
                // Якщо учасник не існує, створюємо нового
                participant = new Participant { UserId = user.Id, User = user };
                _context.Participants.Add(participant);
                Console.WriteLine($"New participant created for user {user.UserName}.");
            }
            else
            {
                Console.WriteLine($"Existing participant found: {participant.User}.");
            }

            // Отримуємо подію за її ID
            var eventItem = await _context.Events.FindAsync(eventId);
            if (eventItem == null)
            {
                // Якщо події не існує, викидаємо помилку або повертаємо повідомлення
                Console.WriteLine($"Event with ID {eventId} not found.");
                throw new Exception("Event not found");
            }
            else
            {
                Console.WriteLine($"Event found: {eventItem.Name} at {eventItem.Location}.");
            }

            // Додаємо подію до списку зареєстрованих подій учасника
            if (!participant.RegisteredEvents.Contains(eventItem))
            {
                participant.RegisteredEvents.Add(eventItem);  // Додаємо подію до зареєстрованих подій учасника
                Console.WriteLine($"Participant {participant.User} registered for event {eventItem.Name}.");
            }
            else
            {
                Console.WriteLine($"Participant {participant.User} is already registered for event {eventItem.Name}.");
            }

            // Додаємо учасника до списку учасників події
            if (!eventItem.Participants.Contains(participant))
            {
                eventItem.Participants.Add(participant);  // Додаємо учасника до події
                Console.WriteLine($"Participant {participant.User} added to the event {eventItem.Name}.");
            }
            else
            {
                Console.WriteLine($"Participant {participant.User} is already in the participant list of event {eventItem.Name}.");
            }

            
            await _context.SaveChangesAsync();
            Console.WriteLine("Changes saved to the database.");

            
            return participant;
        }

        public async Task<IEnumerable<Event>> GetEventsByOrganizerIdAsync(int organizerId)
        {
            return await _context.Events
                .Include(e => e.Organizer)
                .Where(e => e.OrganizerId == organizerId)
                .ToListAsync();
        }

    }
}
