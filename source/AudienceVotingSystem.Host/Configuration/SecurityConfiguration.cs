namespace AudienceVotingSystem.Host.Configuration;

/// <summary>
/// Конфигурационные данные для обеспечения безопасности приложения.
/// </summary>
internal sealed class SecurityConfiguration
{
    /// <summary>
    /// Хэш секретной фразы администратора.
    /// </summary>
    /// <remarks>Значение не должно быть пустым и его длина не должна превышать 100 символов.</remarks>
    public string AdministratorSecretHash { get; set; } = string.Empty;
}
