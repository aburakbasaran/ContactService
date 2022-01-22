﻿// <auto-generated />
using System;
using ContactService.ReportModule.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactService.ReportModule.Data.Data.Migrations
{
    [DbContext(typeof(ContactReportDbContext))]
    [Migration("20220122191306_ReportDetailUpdate")]
    partial class ReportDetailUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ContactService.ReportModule.Data.Data.Entities.ReportDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid")
                        .HasColumnName("report_id");

                    b.Property<string>("ReportJson")
                        .HasColumnType("text")
                        .HasColumnName("report_json");

                    b.HasKey("Id")
                        .HasName("pk_report_details");

                    b.HasIndex("ReportId")
                        .IsUnique()
                        .HasDatabaseName("ix_report_details_report_id");

                    b.ToTable("report_details");
                });

            modelBuilder.Entity("ContactService.ReportModule.Data.Data.Entities.ReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date_time");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.HasKey("Id")
                        .HasName("pk_reports");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("ContactService.ReportModule.Data.Data.Entities.ReportDetailEntity", b =>
                {
                    b.HasOne("ContactService.ReportModule.Data.Data.Entities.ReportEntity", "Report")
                        .WithOne("ReportDetail")
                        .HasForeignKey("ContactService.ReportModule.Data.Data.Entities.ReportDetailEntity", "ReportId")
                        .HasConstraintName("fk_report_details_reports_report_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("ContactService.ReportModule.Data.Data.Entities.ReportEntity", b =>
                {
                    b.Navigation("ReportDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
