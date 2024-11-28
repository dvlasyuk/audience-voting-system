using System.Diagnostics.CodeAnalysis;

using AudienceVotingSystem.DataAccess.Database.Configurators;
using AudienceVotingSystem.DataAccess.Database.Entities;

using Microsoft.EntityFrameworkCore;

namespace AudienceVotingSystem.DataAccess.Database;

/// <summary>
/// Контекст для доступа к базе данных.
/// </summary>
public sealed class DatabaseContext : DbContext
{
    /// <summary>
    /// Создаёт новый экземпляр <see cref="DatabaseContext"/>.
    /// </summary>
    /// <param name="options">Опции для конфигурирования контекста.</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    { }

    /// <summary>
    /// Голоса участников в рамках зрительских голосований.
    /// </summary>
    public DbSet<ParticipantVote> ParticipantVotes => Set<ParticipantVote>();

    /// <summary>
    /// Конфигурирует модель базы данных.
    /// </summary>
    /// <param name="modelBuilder">Конструктор для конфигурирования модели.</param>
    protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ParticipantVoteConfigurator());
    }
}
