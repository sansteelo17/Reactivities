using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>(a => a.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

        builder.Entity<ActivityAttendee>()
               .HasOne(a => a.AppUser)
               .WithMany(aa => aa.Activities)
               .HasForeignKey(x => x.AppUserId);

        builder.Entity<ActivityAttendee>()
               .HasOne(a => a.Activity)
               .WithMany(aa => aa.Attendees)
               .HasForeignKey(x => x.ActivityId);
    }
}