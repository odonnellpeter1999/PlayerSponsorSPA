using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Data.Context;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubAdmin> Admins { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }
    public DbSet<Sponsorship> Sponsorships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Club>()
            .HasMany(c => c.Admins)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Club>()
            .HasMany(c => c.Teams)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)
        .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.AwaySponsor)
        .WithMany()
            .HasForeignKey("AwaySponsorId");

        modelBuilder.Entity<Player>()
            .HasOne(p => p.HomeSponsor)
            .WithMany()
            .HasForeignKey("HomeSponsorId");
    }
}