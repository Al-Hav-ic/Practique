
using Kurs.Classes;
using Microsoft.EntityFrameworkCore;

namespace Kurs.Data
{
    public class DataEvent : DbContext
    {
        public DataEvent(DbContextOptions<DataEvent> options) : base(options)
        {
        }

        // DbSets для подій, учасників, організаторів
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Review> Reviews { get; set; } // DbSet для відгуків
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Participant>()
      .HasOne(p => p.User)  // Учасник має один користувач
      .WithMany()  // Один користувач може мати кілька подій
      .HasForeignKey(p => p.UserId)  // Зв'язок за UserId
      .OnDelete(DeleteBehavior.Cascade);  // Видалення учасника також призведе до видалення події, якщо так налаштовано
            builder.Entity<Event>()
    .HasOne(e => e.Organizer) // Подія має одного організатора
    .WithMany(o => o.CreatedEvents) // Організатор може мати кілька подій
    .HasForeignKey(e => e.OrganizerId) // Зв'язок за OrganizerId
    .OnDelete(DeleteBehavior.SetNull); // Якщо організатор видалений, зв'язок буде null

            builder.Entity<Organizer>(entity =>
            {
                entity.HasKey(o => o.Id);  // Встановлюємо первинний ключ для організатора
                entity.Property(o => o.UserId).IsRequired();  // Вказуємо, що UserId є обов'язковим
                entity.HasMany(o => o.CreatedEvents)  // Один організатор може мати кілька подій
                      .WithOne(e => e.Organizer)  // Кожна подія має одного організатора
                      .HasForeignKey(e => e.OrganizerId);  // Зв'язок через OrganizerId
            });
            builder.Entity<Review>()
               .HasOne(r => r.Event)
               .WithMany(e => e.Reviews)
               .HasForeignKey(r => r.EventId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
