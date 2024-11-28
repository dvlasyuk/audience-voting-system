namespace AudienceVotingSystem.DataAccess.Configuration.Models;

/// <summary>
/// Конфигурация участника.
/// </summary>
public sealed class Participant
{
    /// <summary>
    /// Идентификатор участника.
    /// </summary>
    /// <remarks>Значение не должно быть пустым и его длина не должна превышать 50 символов. Значение
    /// должно быть уникальным для всех сконфигурированных участников.</remarks>
    public string Identifier { get; set; } = string.Empty;

    /// <summary>
    /// Полное имя участника.
    /// </summary>
    /// <remarks>Значение не должно быть пустым и его длина не должна превышать 100 символов.</remarks>
    public string Name { get; set; } = "Неизвестное имя";

    /// <summary>
    /// Идентификатор отряда, к которому относится участник.
    /// </summary>
    /// <remarks>Значение не должно быть пустым и его длина не должна превышать 50 символов. Значение
    /// должно являться идентификатором одного из сконфигурированных отрядов.</remarks>
    public string Brigade { get; set; } = "Неизвестный отряд";
}
