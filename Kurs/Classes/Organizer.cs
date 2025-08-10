

////using Kurs.Data;

////namespace Kurs.Classes
////{
////    public class Organizer : ApplicationUser
////    {
////        public List<Event> CreatedEvents { get; set; } = new List<Event>();

////        // Метод для створення події
////        public void CreateEvent(Event newEvent)
////        {
////            CreatedEvents.Add(newEvent);
////            Console.WriteLine($"Event '{newEvent.Name}' created by organizer.");
////        }

////        // Метод для оновлення події
////        public void UpdateEvent(Event updatedEvent, List<Event> allEvents)
////        {
////            var eventToUpdate = allEvents.FirstOrDefault(e => e.Id == updatedEvent.Id);
////            if (eventToUpdate != null)
////            {
////                eventToUpdate.Name = updatedEvent.Name;
////                eventToUpdate.Description = updatedEvent.Description;
////                eventToUpdate.Date = updatedEvent.Date;
////                eventToUpdate.Location = updatedEvent.Location;
////                Console.WriteLine($"Event '{updatedEvent.Name}' updated by organizer.");
////            }
////        }

////        // Метод для управління учасниками


////        // Метод для надсилання сповіщення учасникам
////        public void SendNotification(Event eventToNotify, string message)
////        {
////            Console.WriteLine($"Sending notification for event '{eventToNotify.Name}': {message}");
////            // Логіка для надсилання повідомлення
////        }
////    }
////}
//using Kurs.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Kurs.Classes
//{
//    public class Organizer
//    {
//        public string Id { get; set; }
//        public string UserId { get; set; } // Foreign key to ApplicationUser
//        public ApplicationUser User { get; set; }
//        public List<Event> CreatedEvents { get; set; } = new List<Event>();

//        // Метод для створення події
//        public void CreateEvent(Event newEvent)
//        {
//            newEvent.OrganizerId = this.Id; // Прив'язка події до організатора
//            CreatedEvents.Add(newEvent);
//            Console.WriteLine($"Event '{newEvent.Name}' created by organizer.");
//        }

//        // Метод для оновлення події
//        public void UpdateEvent(Event updatedEvent, List<Event> allEvents)
//        {
//            var eventToUpdate = allEvents.FirstOrDefault(e => e.Id == updatedEvent.Id);
//            if (eventToUpdate != null)
//            {
//                eventToUpdate.Name = updatedEvent.Name;
//                eventToUpdate.Description = updatedEvent.Description;
//                eventToUpdate.Date = updatedEvent.Date;
//                eventToUpdate.Location = updatedEvent.Location;
//                Console.WriteLine($"Event '{updatedEvent.Name}' updated by organizer.");
//            }
//        }

//        // Метод для управління учасниками
//        public void ManageParticipants(Event eventToManage, List<Participant> participants)
//        {
//            eventToManage.Participants = participants;  // Оновлення списку учасників для події
//            Console.WriteLine($"Participants updated for event '{eventToManage.Name}'.");
//        }

//        // Метод для надсилання сповіщення учасникам
//        public void SendNotification(Event eventToNotify, string message)
//        {
//            // Логіка для надсилання сповіщення учасникам події
//            Console.WriteLine($"Sending notification for event '{eventToNotify.Name}': {message}");
//            // Тут може бути код для надсилання реальних повідомлень (через email, мобільні сповіщення і т. п.)
//        }
//    }
//}
using Kurs.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Classes
{
    public class Organizer
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public ApplicationUser User { get; set; } 
        public List<Event> CreatedEvents { get; set; } = new List<Event>(); 

        public void CreateEvent(Event newEvent)
        {
            newEvent.OrganizerId = this.Id; 
            CreatedEvents.Add(newEvent);
            Console.WriteLine($"Event '{newEvent.Name}' created by organizer.");
        }

        public void UpdateEvent(Event updatedEvent, List<Event> allEvents)
        {
            var eventToUpdate = allEvents.FirstOrDefault(e => e.Id == updatedEvent.Id);
            if (eventToUpdate != null)
            {
                eventToUpdate.Name = updatedEvent.Name;
                eventToUpdate.Description = updatedEvent.Description;
                eventToUpdate.Date = updatedEvent.Date;
                eventToUpdate.Location = updatedEvent.Location;
                Console.WriteLine($"Event '{updatedEvent.Name}' updated by organizer.");
            }
        }

        public void ManageParticipants(Event eventToManage, List<Participant> participants)
        {
            eventToManage.Participants = participants; 
            Console.WriteLine($"Participants updated for event '{eventToManage.Name}'.");
        }

        public void SendNotification(Event eventToNotify, string message)
        {
            Console.WriteLine($"Sending notification for event '{eventToNotify.Name}': {message}");
        }
    }
}
