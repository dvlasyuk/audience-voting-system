using System.Diagnostics.CodeAnalysis;

using AudienceVotingSystem.DataAccess.Configuration.Models;

using Microsoft.Extensions.Options;

namespace AudienceVotingSystem.DataAccess.Configuration;

/// <summary>
/// Валидатор конфигурационных данных приложения.
/// </summary>
internal sealed class ApplicationConfigurationValidator : IValidateOptions<ApplicationConfiguration>
{
    /// <summary>
    /// Валидирует заданный экземпляр конфигурационных данных.
    /// </summary>
    /// <param name="name">Название экземпляра данных для валидации.</param>
    /// <param name="options">Экземпляр данных для валидации.</param>
    /// <returns>Результат валидации.</returns>
    public ValidateOptionsResult Validate(string? name, [NotNull] ApplicationConfiguration options)
    {
        var failureMessages = new List<string>();

        failureMessages.AddRange(ValidateIdentifiersUniqueness(options));
        failureMessages.AddRange(ValidateVoting(options.Voting, options.Brigades));
        failureMessages.AddRange(options.Brigades.SelectMany(item => ValidateBrigade(item, options.Participants)));
        failureMessages.AddRange(options.Participants.SelectMany(item => ValidateParticipant(item, options.Brigades)));

        if (options.Brigades.Count == 0)
        {
            failureMessages.Add($"Не задано ни одного отряда");
        }
        if (options.Brigades.Count > 100)
        {
            failureMessages.Add($"Задано более 100 отрядов");
        }

        if (options.Participants.Count == 0)
        {
            failureMessages.Add($"Не задано ни одного участника");
        }
        if (options.Participants.Count > 10000)
        {
            failureMessages.Add($"Задано более 10000 участников");
        }

        return failureMessages.Count > 0
            ? ValidateOptionsResult.Fail(failureMessages)
            : ValidateOptionsResult.Success;
    }

    private static List<string> ValidateIdentifiersUniqueness(ApplicationConfiguration options)
    {
        var failureMessages = new List<string>();

        failureMessages.AddRange(ValidateIdentifierUniqueness(
            name: "Идентификатор кандидата зрительского голосования",
            values: options.Voting.Candidates
                .Select(item => item.Identifier)));

        failureMessages.AddRange(ValidateIdentifierUniqueness(
            name: "Идентификатор отряда",
            values: options.Brigades
                .Select(team => team.Identifier)));

        failureMessages.AddRange(ValidateIdentifierUniqueness(
            name: "Идентификатор участника",
            values: options.Participants
                .Select(participant => participant.Identifier)));

        return failureMessages;
    }

    private static List<string> ValidateVoting(Voting voting, ICollection<Brigade> brigades)
    {
        var failureMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(voting.Name))
        {
            failureMessages.Add($"Для зрительского голосования задано пустое название");
        }
        else if (voting.Name.Length > 100)
        {
            failureMessages.Add($"Для зрительского голосования задано название, превышающее 100 символов");
        }

        if (voting.VotesQuantity < 0)
        {
            failureMessages.Add($"Для зрительского голосования задано отрицательное количество голосов");
        }
        if (voting.VotesQuantity >= voting.Candidates.Count)
        {
            failureMessages.Add($"Для зрительского голосования задано количество голосов, большее или равное количеству кандидатов");
        }

        if (voting.Candidates.Count == 0)
        {
            failureMessages.Add($"Для зрительского голосования не задано ни одного кандидата");
        }
        if (voting.Candidates.Count > 100)
        {
            failureMessages.Add($"Для зрительского голосования задано более 100 кандидатов");
        }

        failureMessages.AddRange(voting.Candidates.SelectMany(item => ValidateCandidate(
            candidate: item,
            brigades: brigades,
            friendlyVoting: voting.FriendlyVoting)));

        return failureMessages;
    }

    private static List<string> ValidateCandidate(
        Candidate candidate,
        ICollection<Brigade> brigades,
        bool friendlyVoting)
    {
        var failureMessages = new List<string>();

        if (!IsValidIdentifier(candidate.Identifier))
        {
            failureMessages.Add("Для одного из кандидатов голосований задан пустой или слишком длинный идентификатор");
            return failureMessages;
        }

        if (string.IsNullOrWhiteSpace(candidate.Name))
        {
            failureMessages.Add($"Для кандидата голосования {candidate.Identifier} задано пустое название/имя");
        }
        else if (candidate.Name.Length > 100)
        {
            failureMessages.Add($"Для кандидата голосования {candidate.Identifier} задано название/имя, превышающее 100 символов");
        }

        if (!friendlyVoting)
        {
            if (candidate.Brigades.Count == 0)
            {
                failureMessages.Add($"Для кандидата голосования {candidate.Identifier} не задано ни одного связанного отряда");
            }
            if (candidate.Brigades.Count > 10)
            {
                failureMessages.Add($"Для кандидата голосования {candidate.Identifier} задано более 10 связанных отрядов");
            }

            foreach (var brigade in candidate.Brigades)
            {
                if (!IsValidIdentifier(brigade))
                {
                    failureMessages.Add($"Для кандидата голосования {candidate.Identifier} задан пустой или слишком длинный идентификатор связанного отряда");
                }
                if (!brigades.Any(item => item.Identifier == brigade))
                {
                    failureMessages.Add($"Для кандидата голосования {candidate.Identifier} задан несуществующий идентификатор связанного отряда");
                }
            }

            failureMessages.AddRange(ValidateIdentifierUniqueness(
                name: $"Идентификатор связанного отряда для кандидата голосования {candidate.Identifier}",
                values: candidate.Brigades));
        }

        return failureMessages;
    }

    private static List<string> ValidateBrigade(Brigade brigade, ICollection<Participant> participants)
    {
        var failureMessages = new List<string>();

        if (!IsValidIdentifier(brigade.Identifier))
        {
            failureMessages.Add("Для одного из отрядов задан пустой или слишком длинный идентификатор");
            return failureMessages;
        }

        if (string.IsNullOrWhiteSpace(brigade.Name))
        {
            failureMessages.Add($"Для отряда {brigade.Identifier} задано пустое название");
        }
        else if (brigade.Name.Length > 100)
        {
            failureMessages.Add($"Для отряда {brigade.Identifier} задано название, превышающее 100 символов");
        }

        var members = participants
            .Where(participant => participant.Brigades
                .Contains(brigade.Identifier))
            .ToList();

        if (members.Count > 100)
        {
            failureMessages.Add($"Для отряда {brigade.Identifier} задано более 100 участников");
        }

        return failureMessages;
    }

    private static List<string> ValidateParticipant(Participant participant, ICollection<Brigade> brigades)
    {
        var failureMessages = new List<string>();

        if (!IsValidIdentifier(participant.Identifier))
        {
            failureMessages.Add("Для одного из участников задан пустой или слишком длинный идентификатор");
            return failureMessages;
        }

        if (string.IsNullOrWhiteSpace(participant.Name))
        {
            failureMessages.Add($"Для участника {participant.Identifier} задано пустое имя");
        }
        else if (participant.Name.Length > 100)
        {
            failureMessages.Add($"Для участника {participant.Identifier} задано имя, превышающее 100 символов");
        }

        if (participant.Brigades.Count == 0)
        {
            failureMessages.Add($"Для участника {participant.Identifier} не задано ни одного отряда");
        }
        if (participant.Brigades.Count > 10)
        {
            failureMessages.Add($"Для участника {participant.Identifier} задано более 10 отрядов");
        }

        foreach (var brigade in participant.Brigades)
        {
            if (!IsValidIdentifier(brigade))
            {
                failureMessages.Add($"Для участника {participant.Identifier} задан пустой или слишком длинный идентификатор отряда");
            }
            if (!brigades.Any(item => item.Identifier == brigade))
            {
                failureMessages.Add($"Для участника {participant.Identifier} задан несуществующий идентификатор отряда");
            }
        }

        failureMessages.AddRange(ValidateIdentifierUniqueness(
            name: $"Идентификатор отряда для участника {participant.Identifier}",
            values: participant.Brigades));

        return failureMessages;
    }

    private static bool IsValidIdentifier(string identifier) =>
        !string.IsNullOrWhiteSpace(identifier) && identifier.Length <= 50;

    private static List<string> ValidateIdentifierUniqueness(string name, IEnumerable<string> values)
    {
        return values
            .Where(IsValidIdentifier)
            .GroupBy(identifier => identifier)
            .Where(group => group.Count() > 1)
            .Select(group => $"{name} {group.Key} повторяется {group.Count()} раз")
            .ToList();
    }
}
