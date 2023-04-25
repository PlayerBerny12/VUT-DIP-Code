﻿// <auto-generated />
using System;
using DeepfakeDetectionFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeepfakeDetectionFramework.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeepfakeDetectionFramework.Data.Models.Feedback", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("ID"));

                    b.Property<long?>("RequestID")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("RequestID");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("DeepfakeDetectionFramework.Data.Models.Request", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("ID"));

                    b.Property<string>("Checksum")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("DeepfakeDetectionFramework.Data.Models.Response", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("ID"));

                    b.Property<long>("MethodID")
                        .HasColumnType("bigint");

                    b.Property<long>("RequestID")
                        .HasColumnType("bigint");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("RequestID");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("DeepfakeDetectionFramework.Data.Models.Feedback", b =>
                {
                    b.HasOne("DeepfakeDetectionFramework.Data.Models.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestID");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("DeepfakeDetectionFramework.Data.Models.Response", b =>
                {
                    b.HasOne("DeepfakeDetectionFramework.Data.Models.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });
#pragma warning restore 612, 618
        }
    }
}
