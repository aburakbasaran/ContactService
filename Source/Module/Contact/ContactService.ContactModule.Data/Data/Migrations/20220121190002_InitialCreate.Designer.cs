﻿// <auto-generated />
using System;
using ContactService.ContactModule.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactService.ContactModule.Data.Data.Migrations
{
    [DbContext(typeof(ContactDbContext))]
    [Migration("20220121190002_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ContactService.ContactModule.Data.Data.Entities.UserContactEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint")
                        .HasColumnName("type");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_user_contacts");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_contacts_user_id");

                    b.ToTable("user_contacts");
                });

            modelBuilder.Entity("ContactService.ContactModule.Data.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Firm")
                        .HasColumnType("text")
                        .HasColumnName("firm");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("SurName")
                        .HasColumnType("text")
                        .HasColumnName("sur_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ContactService.ContactModule.Data.Data.Entities.UserContactEntity", b =>
                {
                    b.HasOne("ContactService.ContactModule.Data.Data.Entities.UserEntity", "User")
                        .WithMany("UserContacts")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_contacts_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ContactService.ContactModule.Data.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("UserContacts");
                });
#pragma warning restore 612, 618
        }
    }
}
