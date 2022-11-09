﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Training.API.Database;

#nullable disable

namespace Training.API.Migrations
{
    [DbContext(typeof(TrainingContext))]
    [Migration("20221109220941_Init-1")]
    partial class Init1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Training.API.Trainings.ReadModels.TrainingDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Availability")
                        .HasColumnType("integer");

                    b.Property<int?>("BreakBetweenExercisesInMinutes")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DurationTrainingInMinutes")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TrainerUniqueCode")
                        .HasColumnType("uuid");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Trainings", (string)null);
                });

            modelBuilder.Entity("Training.API.Trainings.ReadModels.TrainingUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TrainingDetailsTrainingUser", b =>
                {
                    b.Property<Guid>("TrainingUsersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TrainingsId")
                        .HasColumnType("uuid");

                    b.HasKey("TrainingUsersId", "TrainingsId");

                    b.HasIndex("TrainingsId");

                    b.ToTable("TrainingDetailsTrainingUser");
                });

            modelBuilder.Entity("Training.API.Trainings.ReadModels.TrainingDetails", b =>
                {
                    b.OwnsMany("Training.API.Trainings.ReadModels.TrainingExercise", "TrainingExercises", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<int>("BreakBetweenSetsInMinutes")
                                .HasColumnType("integer");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("boolean");

                            b1.Property<int>("NumberRepetitions")
                                .HasColumnType("integer");

                            b1.Property<Guid>("TrainingDetailsId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("TrainingDetailsId");

                            b1.ToTable("TrainingExercises", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TrainingDetailsId");
                        });

                    b.Navigation("TrainingExercises");
                });

            modelBuilder.Entity("TrainingDetailsTrainingUser", b =>
                {
                    b.HasOne("Training.API.Trainings.ReadModels.TrainingUser", null)
                        .WithMany()
                        .HasForeignKey("TrainingUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Training.API.Trainings.ReadModels.TrainingDetails", null)
                        .WithMany()
                        .HasForeignKey("TrainingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
