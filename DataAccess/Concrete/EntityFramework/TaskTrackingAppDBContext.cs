using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Task = Entities.Concrete.Task;


namespace DataAccess.Concrete.EntityFramework
{
    public class TaskTrackingAppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TaskTrackingAppDB;Trusted_Connection=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        public DbSet<Message> Messages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany()
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Task>()
               .HasOne(t => t.AssignedUser)
               .WithMany()
              .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
             .HasMany<Message>()
             .WithOne(m => m.User)
              .HasForeignKey("UserId");
    
            modelBuilder.Entity<User>()
                .HasMany<ChatRoomUser>()
                .WithOne(cru => cru.User)
                .HasForeignKey("UserId");
        }

    }
}
