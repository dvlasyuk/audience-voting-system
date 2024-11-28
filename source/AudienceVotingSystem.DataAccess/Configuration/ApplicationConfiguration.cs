using AudienceVotingSystem.DataAccess.Configuration.Models;

namespace AudienceVotingSystem.DataAccess.Configuration;

/// <summary>
/// Конфигурационные данные приложения.
/// </summary>
public sealed class ApplicationConfiguration
{
    /// <summary>
    /// Конфигурация зрительского голосования.
    /// </summary>
    public Voting Voting { get; } = new();

    /// <summary>
    /// Конфигурация отрядов.
    /// </summary>
    /// <remarks>Количество сконфигурированных отрядов не может быть нулевым и не должно превышать
    /// 100 отрядов.</remarks>
    public ICollection<Brigade> Brigades { get; } = [];

    /// <summary>
    /// Конфигурация участников.
    /// </summary>
    /// <remarks>Количество сконфигурированных участников не может быть нулевым и не должно превышать
    /// 10000 участников.</remarks>
    public ICollection<Participant> Participants { get; } = [];
}
