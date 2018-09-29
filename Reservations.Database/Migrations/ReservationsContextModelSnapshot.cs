﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Reservations.Database;

namespace Reservations.Database.Migrations
{
    [DbContext(typeof(ReservationsContext))]
    partial class ReservationsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Reservations.Database.Models.Extra", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GuestId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.ToTable("Extras");
                });

            modelBuilder.Entity("Reservations.Database.Models.Guest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<int>("TotalExtras");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Reservations.Database.Models.Extra", b =>
                {
                    b.HasOne("Reservations.Database.Models.Guest")
                        .WithMany()
                        .HasForeignKey("GuestId");
                });
#pragma warning restore 612, 618
        }
    }
}
