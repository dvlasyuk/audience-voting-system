using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Options;

namespace AudienceVotingSystem.Host.Configuration;

/// <summary>
/// Валидатор конфигурационных данные для обеспечения безопасности приложения.
/// </summary>
internal sealed class SecurityConfigurationValidator : IValidateOptions<SecurityConfiguration>
{
    /// <summary>
    /// Валидирует заданный экземпляр конфигурационных данных.
    /// </summary>
    /// <param name="name">Название экземпляра данных для валидации.</param>
    /// <param name="options">Экземпляр данных для валидации.</param>
    /// <returns>Результат валидации.</returns>
    public ValidateOptionsResult Validate(string? name, [NotNull] SecurityConfiguration options)
    {
        var failureMessages = new List<string>();

        if (string.IsNullOrEmpty(options.AdministratorSecretHash))
        {
            failureMessages.Add("Для секретной фразы администратора задан пустой хэш");
        }
        else if (options.AdministratorSecretHash.Length > 100)
        {
            failureMessages.Add("Для секретной фразы администратора задан хэш, превышающий 100 символов");
        }

        return failureMessages.Count > 0
            ? ValidateOptionsResult.Fail(failureMessages)
            : ValidateOptionsResult.Success;
    }
}
