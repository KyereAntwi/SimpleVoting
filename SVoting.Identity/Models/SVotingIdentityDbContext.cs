using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SVoting.Identity.Models;

public class SVotingIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public SVotingIdentityDbContext()
    {
        
    }

    public SVotingIdentityDbContext(DbContextOptions<SVotingIdentityDbContext> options) : base(options)
    {
        
    }
}
