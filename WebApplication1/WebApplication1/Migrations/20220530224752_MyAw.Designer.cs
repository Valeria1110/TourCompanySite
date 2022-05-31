﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(TourContext))]
    [Migration("20220530224752_MyAw")]
    partial class MyAw
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Data.Models.Order", b =>
                {
                    b.Property<int>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_order")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdOrder"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("f_name");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("l_name");

                    b.Property<string>("MName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("m_name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("ScheduleIdS")
                        .HasColumnType("integer")
                        .HasColumnName("schedule_id_s");

                    b.HasKey("IdOrder")
                        .HasName("pk_orders");

                    b.HasIndex("ScheduleIdS")
                        .HasDatabaseName("ix_orders_schedule_id_s");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Data.Models.Schedule", b =>
                {
                    b.Property<int>("IdS")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_s")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdS"));

                    b.Property<string>("DateBegin")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("date_begin");

                    b.Property<string>("DateEnd")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("date_end");

                    b.Property<int>("FreePlaces")
                        .HasColumnType("integer")
                        .HasColumnName("free_places");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<int>("TourId")
                        .HasColumnType("integer")
                        .HasColumnName("tour_id");

                    b.HasKey("IdS")
                        .HasName("pk_schedules");

                    b.HasIndex("TourId")
                        .HasDatabaseName("ix_schedules_tour_id");

                    b.ToTable("schedules", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Data.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Cost")
                        .HasColumnType("integer")
                        .HasColumnName("cost");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<int>("Days")
                        .HasColumnType("integer")
                        .HasColumnName("days");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("desc");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tours");

                    b.ToTable("tours", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Data.Models.Order", b =>
                {
                    b.HasOne("WebApplication1.Data.Models.Schedule", "Schedule")
                        .WithMany("Orders")
                        .HasForeignKey("ScheduleIdS")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_schedules_schedule_temp_id");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("WebApplication1.Data.Models.Schedule", b =>
                {
                    b.HasOne("WebApplication1.Data.Models.Tour", "Tour")
                        .WithMany("Schedules")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_schedules_tours_tour_id");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("WebApplication1.Data.Models.Schedule", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication1.Data.Models.Tour", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
