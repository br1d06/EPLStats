using WOD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace WOD.WebUI.Data;

public partial class PostgresContext : DbContext
{
	public PostgresContext()
	{
		Database.Migrate();
	}

	public PostgresContext(DbContextOptions<PostgresContext> options)
		: base(options)
	{
		Database.Migrate();
	}

	public DbSet<Match> Matches { get; set; }

	public DbSet<Player> Players { get; set; }

	public virtual DbSet<FootballClub> FootballClubs { get; set; }

	public virtual DbSet<News> News { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Match>(entity =>
		{
			entity.HasMany(p => p.MatchParticipants)
			.WithMany(m => m.Matches);
		});

		modelBuilder.Entity<Player>(entity =>
		{
			entity.HasOne(f=>f.FootballClub)
			.WithMany(p=>p.Players)
			.OnDelete(DeleteBehavior.Restrict)
			.HasConstraintName("FK_Player_FootballClub");
		});
	}
}
