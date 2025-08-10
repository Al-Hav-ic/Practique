using Kurs.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kurs.Data
{
    public interface IEventData
    {
        Task<IEnumerable<Event>> GetAllEventsAsync(); // Asynchronous method for getting all events
        Task<Event> GetEventByIdAsync(int id); // Asynchronous method for getting an event by id
        Task<IEnumerable<Event>> GetEventsByOrganizerIdAsync(int organizerId);
        Task AddEventAsync(Event eventItem); // Asynchronous method for adding an event
        Task UpdateEventAsync(Event eventItem); // Asynchronous method for updating an event
        Task DeleteEventAsync(int id); // Asynchronous method for deleting an event

        
          Task<List<Organizer>> GetAllOrganizersAsync();
        Task<Organizer> GetOrganizerByUserIdAsync(string userId);
        Task AddOrganizerAsync(Organizer organizer);

        Task<Participant> RegisterForEventAsync(int eventId);
    }
}
