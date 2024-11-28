using AudienceVotingSystem.DataAccess.Configuration.Models;

namespace AudienceVotingSystem.DataAccess.Configuration;

/// <summary>
/// Конфигурационные данные приложения.
/// </summary>
public sealed class ApplicationConfiguration
{
    /// <summary>
    /// Конфигурация зрительских голосований.
    /// </summary>
    public ICollection<Voting> Votings { get; } = [];

    /// <summary>
    /// Конфигурация отрядов.
    /// </summary>
    public ICollection<Brigade> Brigades { get; } = [];

    /// <summary>
    /// Конфигурация участников.
    /// </summary>
    public ICollection<Participant> Participants { get; } = [];
}
