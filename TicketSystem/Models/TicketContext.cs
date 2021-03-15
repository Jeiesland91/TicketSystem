using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sprint>().HasData(
                new Sprint { SprintId = "1", SprintNumber = "Sprint 1" },
                new Sprint { SprintId = "2", SprintNumber = "Sprint 2" },
                new Sprint { SprintId = "3", SprintNumber = "Sprint 3" },
                new Sprint { SprintId = "4", SprintNumber = "Sprint 4" },
                new Sprint { SprintId = "5", SprintNumber = "Sprint 5" },
                new Sprint { SprintId = "6", SprintNumber = "Sprint 6" },
                new Sprint { SprintId = "7", SprintNumber = "Sprint 7" },
                new Sprint { SprintId = "8", SprintNumber = "Sprint 8" },
                new Sprint { SprintId = "9", SprintNumber = "Sprint 9" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "1", Name = "To Do" },
                new Status { StatusId = "2", Name = "In Progress" },
                new Status { StatusId = "3", Name = "Quality Assurance" },
                new Status { StatusId = "4", Name = "Done" }
            );

            modelBuilder.Entity<Point>().HasData(
                new Point { PointId = 1, Name = "1 Point" },
                new Point { PointId = 2, Name = "2 Points" },
                new Point { PointId = 3, Name = "3 Points" },
                new Point { PointId = 5, Name = "5 Points" },
                new Point { PointId = 8, Name = "8 Points" },
                new Point { PointId = 13, Name = "13 Points" },
                new Point { PointId = 21, Name = "21 Points" }
            );
        }
    }
}
