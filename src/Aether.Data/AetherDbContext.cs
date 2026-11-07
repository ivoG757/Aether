using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Aether.Data.Models;
namespace Aether.Data
{
    public class AetherDbContext(DbContextOptions<AetherDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Realm> Realms { get; set; }
        public DbSet<TextChannel> TextChannels { get; set; }
        public DbSet<VoiceChannel> VoiceChannels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RealmUser> RealmUsers { get; set; }
        public DbSet<Role> RealmRoles { get; set; }
        public DbSet<RealmUserRole> RealmUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(user =>
            {
                user.Property(u => u.IsDeleted).HasDefaultValue(false);
            });

            modelBuilder.Entity<Realm>()
                .HasOne(realm => realm.CreatedByUser)
                .WithMany()
                .HasForeignKey(realm => realm.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealmUser>()
                .HasKey(ru => new { ru.RealmId, ru.UserId });

            modelBuilder.Entity<RealmUserRole>()
                .HasKey(rur => new 
                { 
                    rur.RealmId,
                    rur.UserId,
                    rur.RoleId
                });

            modelBuilder.Entity<RealmUserRole>()
                .HasOne(assignment => assignment.Membership)
                .WithMany(membership => membership.Roles)
                .HasForeignKey(assignment => new
                {
                    assignment.RealmId,
                    assignment.UserId
                });

            modelBuilder.Entity<RealmUserRole>()
                .HasOne(assignment => assignment.Role)
                .WithMany(role => role.RealmUsers)
                .HasForeignKey(assignment => new
                {
                    assignment.RealmId,
                    assignment.RoleId
                })
                //Matching the principal key of the Role entity to the composite foreign key in RealmUserRole to ensure that the realm has this role.
                .HasPrincipalKey(role => new
                {
                    role.RealmId,
                    role.Id
                })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
