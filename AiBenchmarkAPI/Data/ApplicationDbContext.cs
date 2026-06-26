using Microsoft.EntityFrameworkCore;
using AiBenchmarkAPI.Models;

namespace AiBenchmarkAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subject> Subjects { get; set; }
}