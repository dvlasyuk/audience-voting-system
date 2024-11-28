using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudienceVotingSystem.DataAccess.Migrations;

/// <inheritdoc />
public partial class SchemaInitialization : Migration
{
    /// <inheritdoc />
    protected override void Up([NotNull] MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ParticipantVotes",
            columns: table => new
            {
                Identifier = table.Column<Guid>(type: "TEXT", nullable: false),
                ParticipantId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                CandidateId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                Note = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ParticipantVotes", x => x.Identifier);
            });
    }

    /// <inheritdoc />
    protected override void Down([NotNull] MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ParticipantVotes");
    }
}
