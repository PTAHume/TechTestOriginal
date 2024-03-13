using System;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        User[] data =
        [
            new User { Id = 1, DateOfBirth = new DateTime(1982, 6, 11, 0, 0, 0, DateTimeKind.Utc), Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true },
            new User { Id = 2, DateOfBirth = new DateTime(1989, 1, 17, 0, 0, 0, DateTimeKind.Utc), Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true },
            new User { Id = 3, DateOfBirth = new DateTime(1978, 5, 7, 0, 0, 0, DateTimeKind.Utc), Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false },
            new User { Id = 4, DateOfBirth = new DateTime(1999, 3, 3, 0, 0, 0, DateTimeKind.Utc), Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true },
            new User { Id = 5, DateOfBirth = new DateTime(1988, 5, 22, 0, 0, 0, DateTimeKind.Utc), Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true },
            new User { Id = 6, DateOfBirth = new DateTime(1982, 6, 11, 0, 0, 0, DateTimeKind.Utc), Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true },
            new User { Id = 7, DateOfBirth = new DateTime(1981, 8, 11, 0, 0, 0, DateTimeKind.Utc), Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false },
            new User { Id = 8, DateOfBirth = new DateTime(1984, 4, 27, 0, 0, 0, DateTimeKind.Utc), Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false },
            new User { Id = 9, DateOfBirth = new DateTime(1983, 9, 11, 0, 0, 0, DateTimeKind.Utc), Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false },
            new User { Id = 10, DateOfBirth = new DateTime(2000, 7, 7, 0, 0, 0, DateTimeKind.Utc), Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true },
            new User { Id = 11, DateOfBirth = new DateTime(1982, 12, 12, 0, 0, 0, DateTimeKind.Utc), Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true },
        ];

        Log[] logData = data
            .Select((user, Index) =>
                    new Log
                    {
                        Action = "Added",
                        Id = Index + 1,
                        Details = JsonSerializer.Serialize(user),
                        TimeStamp = DateTime.UtcNow,
                        UserId = user.Id
                    }
                    ).ToArray();

        _ = modelBuilder.Entity<Log>().HasData(logData);
        _ = modelBuilder.Entity<User>().HasData(data);
    }

    public DbSet<User>? Users { get; set; }

    public DbSet<Log>? Logs { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}
