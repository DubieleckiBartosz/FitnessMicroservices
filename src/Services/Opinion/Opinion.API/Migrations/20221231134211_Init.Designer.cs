﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Opinion.API.Infrastructure.Database;

#nullable disable

namespace Opinion.API.Migrations
{
    [DbContext(typeof(OpinionContext))]
    [Migration("20221231134211_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Opinion.API.Domain.Opinion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OpinionFor")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Opinions", (string)null);
                });

            modelBuilder.Entity("Opinion.API.Domain.Reaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("OpinionId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ReactionFor")
                        .HasColumnType("uuid");

                    b.Property<int>("ReactionType")
                        .HasColumnType("integer");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OpinionId");

                    b.ToTable("Reactions", (string)null);
                });

            modelBuilder.Entity("Opinion.API.Domain.Reaction", b =>
                {
                    b.HasOne("Opinion.API.Domain.Opinion", null)
                        .WithMany("Reactions")
                        .HasForeignKey("OpinionId");
                });

            modelBuilder.Entity("Opinion.API.Domain.Opinion", b =>
                {
                    b.Navigation("Reactions");
                });
#pragma warning restore 612, 618
        }
    }
}