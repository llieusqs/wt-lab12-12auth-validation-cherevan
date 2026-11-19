using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_cherevan.Models; // 

namespace wt_lab12_12auth_validation_cherevan.Data; //

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    // ДОБАВЛЯЕМ ТАБЛИЦУ ПОЛЬЗОВАТЕЛЕЙ:
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TaskItem>().ToTable("RazorTasks");
    }
}