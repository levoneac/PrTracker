﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrTracker.Data;

#nullable disable

namespace PrTracker.Migrations
{
    [DbContext(typeof(LiftContext))]
    partial class LiftContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("PrTracker.Models.Lifts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LiftName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PrimaryMuscleGroupIdId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SecondaryMuscleGroupIdId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryMuscleGroupIdId");

                    b.HasIndex("SecondaryMuscleGroupIdId");

                    b.ToTable("Lifts", (string)null);
                });

            modelBuilder.Entity("PrTracker.Models.MuscleGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PrimaryMuscleGroup")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MuscleGroups", (string)null);
                });

            modelBuilder.Entity("PrTracker.Models.People", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("People", (string)null);
                });

            modelBuilder.Entity("PrTracker.Models.RecordedLifts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DayOfLift")
                        .HasColumnType("TEXT");

                    b.Property<int>("LiftId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LifterIdId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Reps")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("Id");

                    b.HasIndex("LiftId");

                    b.HasIndex("LifterIdId");

                    b.ToTable("RecordedLifts", (string)null);
                });

            modelBuilder.Entity("PrTracker.Models.Lifts", b =>
                {
                    b.HasOne("PrTracker.Models.MuscleGroups", "PrimaryMuscleGroupId")
                        .WithMany()
                        .HasForeignKey("PrimaryMuscleGroupIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrTracker.Models.MuscleGroups", "SecondaryMuscleGroupId")
                        .WithMany()
                        .HasForeignKey("SecondaryMuscleGroupIdId");

                    b.Navigation("PrimaryMuscleGroupId");

                    b.Navigation("SecondaryMuscleGroupId");
                });

            modelBuilder.Entity("PrTracker.Models.RecordedLifts", b =>
                {
                    b.HasOne("PrTracker.Models.Lifts", "Lift")
                        .WithMany("RecordedLifts")
                        .HasForeignKey("LiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrTracker.Models.People", "LifterId")
                        .WithMany("Lifts")
                        .HasForeignKey("LifterIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lift");

                    b.Navigation("LifterId");
                });

            modelBuilder.Entity("PrTracker.Models.Lifts", b =>
                {
                    b.Navigation("RecordedLifts");
                });

            modelBuilder.Entity("PrTracker.Models.People", b =>
                {
                    b.Navigation("Lifts");
                });
#pragma warning restore 612, 618
        }
    }
}
