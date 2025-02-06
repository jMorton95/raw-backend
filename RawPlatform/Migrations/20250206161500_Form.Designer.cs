﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RawPlatform.Data;

#nullable disable

namespace RawPlatform.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250206161500_Form")]
    partial class Form
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RawPlatform.Data.CommerceToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CommerceTokens");
                });

            modelBuilder.Entity("RawPlatform.Data.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Exception")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<int>("LogLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("RawPlatform.Data.MarketingUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Message")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MarketingUsers");
                });

            modelBuilder.Entity("RawPlatform.Data.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConditionDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("DiscountedPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("EbayId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("EbayPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("EstimatedAlreadySold")
                        .HasColumnType("integer");

                    b.Property<string>("ItemApiUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemWebUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ListingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProductImageBase64")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("RowVersion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
