using Kurs.Classes;
using Kurs.Data;
using System;
using System.Collections.Generic;

namespace Kurs.Classes
{
    public class Participant 
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public ApplicationUser User { get; set; } 
        public List<Event> RegisteredEvents { get; set; } = new List<Event>();
        public void RegisterForEvent(Event newEvent)
        {
            RegisteredEvents.Add(newEvent);
            Console.WriteLine($"Participant registered for {newEvent.Name}.");
        }
        public void ViewRegisteredEvents()
        {
            Console.WriteLine("Registered Events:");
            foreach (var ev in RegisteredEvents)
            {
                Console.WriteLine($"{ev.Name} on {ev.Date}");
            }
        }
    }
}
