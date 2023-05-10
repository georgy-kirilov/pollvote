using Microsoft.EntityFrameworkCore;

namespace DemoServer.Data;

public class StudentsDbContext : DbContext
{
    public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
}
