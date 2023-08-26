using Microsoft.EntityFrameworkCore;
using SVoting.Domain.Entities;

namespace SVoting.Persistence.Data;

public class SVotingDbContext : DbContext
{
    public SVotingDbContext(DbContextOptions<SVotingDbContext> options) : base(options)
    {
    }

    public DbSet<Code> Codes { get; set; }
    public DbSet<Nominee> Nominees { get; set; }
    public DbSet<NomineeCategory> NomineeCategories { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<PollCategory> PollCategories { get; set; }
    public DbSet<PollingCategory> PollingCategories { get; set; }
    public DbSet<PollingSpace> PollingSpaces { get; set; }
    public DbSet<Vote> Votes { get; set; }
}
