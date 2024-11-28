﻿// <auto-generated />
using System;
using AudienceVotingSystem.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AudienceVotingSystem.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241128021410_SchemaInitialization")]
    partial class SchemaInitialization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("AudienceVotingSystem.DataAccess.Database.Entities.ParticipantVote", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CandidateId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Identifier");

                    b.ToTable("ParticipantVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
