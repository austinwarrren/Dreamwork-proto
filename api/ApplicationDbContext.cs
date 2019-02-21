using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using dreamwork_proto.Models;

namespace dreamwork_proto
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        { }
        public DbSet<Run> RunData { get; set; }
        public DbSet<Workout> WorkoutData { get; set; }
    }
}