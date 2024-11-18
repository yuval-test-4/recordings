using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recordings.Infrastructure.Models;

namespace Recordings.Infrastructure;

public class RecordingsDbContext : IdentityDbContext<IdentityUser>
{
    public RecordingsDbContext(DbContextOptions<RecordingsDbContext> options)
        : base(options) { }

    public DbSet<VideoDbModel> Videos { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }
}
