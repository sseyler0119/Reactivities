using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; } // this will be our table name
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // create primary key from AppUserId combined with ActivityId
            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new{aa.AppUserId, aa.ActivityId})); 
            // configure many-to-many relationship
            builder.Entity<ActivityAttendee>().HasOne(u => u.AppUser).WithMany(a => a.Activities).HasForeignKey(aa => aa.AppUserId);
            // now build the other side of the relationship
            builder.Entity<ActivityAttendee>().HasOne(u => u.Activity).WithMany(a => a.Attendees).HasForeignKey(aa => aa.ActivityId);

            // build relationship with Comments and Activity tables
            builder.Entity<Comment>().HasOne(a => a.Activity)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);
            
            // build relationship with UserFollowing and AppUser
            builder.Entity<UserFollowing>(b => 
            {
                b.HasKey(k => new {k.ObserverId, k.TargetId});
                b.HasOne(o => o.Observer).WithMany(f => f.Followings)
                    .HasForeignKey(o=> o.ObserverId).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(o => o.Target).WithMany(f => f.Followers)
                .HasForeignKey(o => o.TargetId).OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}