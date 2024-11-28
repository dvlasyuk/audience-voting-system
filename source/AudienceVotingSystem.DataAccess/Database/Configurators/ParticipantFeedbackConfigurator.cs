using System.Diagnostics.CodeAnalysis;

using AudienceVotingSystem.DataAccess.Database.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudienceVotingSystem.DataAccess.Database.Configurators;

/// <summary>
/// Конфигуратор отзыва участника на мероприятие в рамках конкурса мероприятий.
/// </summary>
internal sealed class ParticipantFeedbackConfigurator : IEntityTypeConfiguration<ParticipantFeedback>
{
    /// <summary>
    /// Конфигурирует модель сущности базы данных.
    /// </summary>
    /// <param name="builder">Конструктор для конфигурирования модели.</param>
    public void Configure([NotNull] EntityTypeBuilder<ParticipantFeedback> builder)
    {
        builder.HasKey(entity => entity.Identifier);
        builder
            .HasMany(entity => entity.Grades)
            .WithOne(entity => entity.Feedback)
            .HasForeignKey(entity => entity.FeedbackId);

        builder.Property(entity => entity.EventId).HasMaxLength(50);
        builder.Property(entity => entity.ParticipantId).HasMaxLength(50);
        builder.Property(entity => entity.Note).HasMaxLength(1000);
    }
}
