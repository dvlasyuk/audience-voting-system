namespace AudienceVotingSystem.DataAccess.Configuration.Models;

/// <summary>
/// Конфигурация зрительского голосования.
/// </summary>
public sealed class Voting
{
    /// <summary>
    /// Человеко-читаемое название голосования.
    /// </summary>
    /// <remarks>Значение не должно быть пустым и его длина не должна превышать 100 символов.</remarks>
    public string Name { get; set; } = "Неизвестное голосование";

    /// <summary>
    /// Признак, допустимо ли зрителям отдавать голоса за кандидатов, имеющих прямое отношение к тому
    /// же отряду, что и сами зрители.
    /// </summary>
    public bool FriendlyVoting { get; set; } = true;

    /// <summary>
    /// Количество голосов, которые может отдать каждый зритель.
    /// </summary>
    /// <remarks>Значение должно быть положительным и строго меньше количества сконфигурированных
    /// кандидатов голосования.</remarks>
    public int VotesQuantity { get; set; } = int.MinValue;

    /// <summary>
    /// Кандидаты зрительского голосования.
    /// </summary>
    /// <remarks>Количество сконфигурированных кандидатов не может быть нулевым и не должно превышать
    /// 100 кандидатов.</remarks>
    public ICollection<Candidate> Candidates { get; } = [];
}
