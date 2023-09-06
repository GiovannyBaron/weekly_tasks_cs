using weekly_tasks.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using weekly_tasks.Models.Data;
using System.Text.RegularExpressions;

namespace weekly_tasks.Db;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WeeklyTask> WeeklyTask { get; set; }
    public DbSet<TaskAssigned> TaskAssigned { get; set; }
    public DbSet<UserRole> UserRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskAssigned>()
            .HasOne(ta => ta.CreatorUser)
            .WithMany()
            .HasForeignKey(ta => ta.CreatorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskAssigned>()
            .HasOne(ta => ta.UserWhoOwns)
            .WithMany()
            .HasForeignKey(ta => ta.UserWhoOwnsId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}