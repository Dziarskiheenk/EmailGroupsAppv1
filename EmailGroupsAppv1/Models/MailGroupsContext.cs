using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailGroupsAppv1.Models
{
  public class MailGroupsContext : DbContext
  {
    public MailGroupsContext() { }
    public MailGroupsContext(DbContextOptions<MailGroupsContext> options) : base(options) { }

    public virtual DbSet<MailGroup> MailGroups { get; set; }
    public virtual DbSet<MailAddress> MailAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<MailGroup>()
          .HasIndex(u => u.Name)
          .IsUnique();

      builder.Entity<MailGroup>()
          .HasMany(x => x.Addresses)
          .WithOne(x => x.MailGroup)
          .HasForeignKey(x => x.GroupId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
