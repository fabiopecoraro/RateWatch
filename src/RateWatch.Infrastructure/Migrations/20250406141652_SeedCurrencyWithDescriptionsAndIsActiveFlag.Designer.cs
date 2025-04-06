﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RateWatch.Infrastructure.Data;

#nullable disable

namespace RateWatch.Infrastructure.Migrations
{
    [DbContext(typeof(RateWatchDbContext))]
    [Migration("20250406141652_SeedCurrencyWithDescriptionsAndIsActiveFlag")]
    partial class SeedCurrencyWithDescriptionsAndIsActiveFlag
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RateWatch.Domain.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "EUR",
                            Description = "Euro",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 2,
                            Code = "USD",
                            Description = "US Dollar",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 3,
                            Code = "JPY",
                            Description = "Japanese Yen",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 4,
                            Code = "BGN",
                            Description = "Bulgarian Lev",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 5,
                            Code = "CZK",
                            Description = "Czech Koruna",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 6,
                            Code = "DKK",
                            Description = "Danish Krone",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 7,
                            Code = "GBP",
                            Description = "Pound Sterling",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 8,
                            Code = "HUF",
                            Description = "Hungarian Forint",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 9,
                            Code = "PLN",
                            Description = "Polish Zloty",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 10,
                            Code = "RON",
                            Description = "Romanian Leu",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 11,
                            Code = "SEK",
                            Description = "Swedish Krona",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 12,
                            Code = "CHF",
                            Description = "Swiss Franc",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 13,
                            Code = "ISK",
                            Description = "Icelandic Krona",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 14,
                            Code = "NOK",
                            Description = "Norwegian Krone",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 15,
                            Code = "TRY",
                            Description = "Turkish Lira",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 16,
                            Code = "AUD",
                            Description = "Australian Dollar",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 17,
                            Code = "BRL",
                            Description = "Brazilian Real",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 18,
                            Code = "CAD",
                            Description = "Canadian Dollar",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 19,
                            Code = "CNY",
                            Description = "Chinese Yuan",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 20,
                            Code = "HKD",
                            Description = "Hong Kong Dollar",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 21,
                            Code = "IDR",
                            Description = "Indonesian Rupiah",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 22,
                            Code = "ILS",
                            Description = "Israeli Shekel",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 23,
                            Code = "INR",
                            Description = "Indian Rupee",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 24,
                            Code = "KRW",
                            Description = "South Korean Won",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 25,
                            Code = "MXN",
                            Description = "Mexican Peso",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 26,
                            Code = "MYR",
                            Description = "Malaysian Ringgit",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 27,
                            Code = "NZD",
                            Description = "New Zealand Dollar",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 28,
                            Code = "PHP",
                            Description = "Philippine Peso",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 29,
                            Code = "SGD",
                            Description = "Singapore Dollar",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 30,
                            Code = "THB",
                            Description = "Thai Baht",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 31,
                            Code = "ZAR",
                            Description = "South African Rand",
                            IsActive = true,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 32,
                            Code = "HRK",
                            Description = "Croatian Kuna",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 33,
                            Code = "RUB",
                            Description = "Russian Ruble",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 34,
                            Code = "LTL",
                            Description = "Lithuanian Litas",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 35,
                            Code = "LVL",
                            Description = "Latvian Lats",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 36,
                            Code = "EEK",
                            Description = "Estonian Kroon",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 37,
                            Code = "SKK",
                            Description = "Slovak Koruna",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 38,
                            Code = "CYP",
                            Description = "Cypriot Pound",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 39,
                            Code = "MTL",
                            Description = "Maltese Lira",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 40,
                            Code = "SIT",
                            Description = "Slovenian Tolar",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 41,
                            Code = "ROL",
                            Description = "Old Romanian Leu",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 42,
                            Code = "TRL",
                            Description = "Old Turkish Lira",
                            IsActive = false,
                            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("RateWatch.Domain.Entities.ExchangeRateRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("FromCurrencyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric(18,6)");

                    b.Property<int>("ToCurrencyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FromCurrencyId");

                    b.HasIndex("ToCurrencyId");

                    b.HasIndex("Date", "FromCurrencyId", "ToCurrencyId")
                        .IsUnique();

                    b.ToTable("ExchangeRateRecords");
                });

            modelBuilder.Entity("RateWatch.Domain.Entities.SystemState", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<bool>("IsSet")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Key");

                    b.ToTable("SystemStates");
                });

            modelBuilder.Entity("RateWatch.Domain.Entities.ExchangeRateRecord", b =>
                {
                    b.HasOne("RateWatch.Domain.Entities.Currency", "FromCurrency")
                        .WithMany("FromRates")
                        .HasForeignKey("FromCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RateWatch.Domain.Entities.Currency", "ToCurrency")
                        .WithMany("ToRates")
                        .HasForeignKey("ToCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");
                });

            modelBuilder.Entity("RateWatch.Domain.Entities.Currency", b =>
                {
                    b.Navigation("FromRates");

                    b.Navigation("ToRates");
                });
#pragma warning restore 612, 618
        }
    }
}
