﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using R6.Core.Enums;
using R6.Infra.Context;

#nullable disable

namespace R6.Infra.Migrations
{
    [DbContext(typeof(R6Context))]
    partial class R6ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "armor_type", new[] { "light", "medium", "heavy" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "dificult_type", new[] { "easy", "medium", "hard" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "speed_type", new[] { "slow", "medium", "fast" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("R6.Domain.Entities.Operator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<ArmorType>("Armor")
                        .HasColumnType("armor_type")
                        .HasColumnName("Armor");

                    b.Property<DificultType>("Dificult")
                        .HasColumnType("dificult_type")
                        .HasColumnName("Dificult");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("Name");

                    b.Property<SpeedType>("Speed")
                        .HasColumnType("speed_type")
                        .HasColumnName("Speed");

                    b.HasKey("Id");

                    b.ToTable("Operator", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
