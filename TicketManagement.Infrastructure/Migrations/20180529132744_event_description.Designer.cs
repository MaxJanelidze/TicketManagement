﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TicketManagement.Domain.Sales;
using TicketManagement.Infrastructure.Db;

namespace TicketManagement.Infrastructure.Migrations
{
    [DbContext(typeof(TicketManagementDbContext))]
    [Migration("20180529132744_event_description")]
    partial class event_description
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TicketManagement.Domain.Event.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Poster");

                    b.Property<int>("SeatCount");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.ToTable("Events","WriteModel");
                });

            modelBuilder.Entity("TicketManagement.Domain.Event.ReadModel.EventDetailsReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("EventId");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("Poster");

                    b.Property<int>("SeatCount");

                    b.Property<string>("VenueName");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.ToTable("EventsDetails","ReadModel");
                });

            modelBuilder.Entity("TicketManagement.Domain.Event.ReadModel.EventsListReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("EventId");

                    b.Property<bool>("IsSoldOut");

                    b.Property<string>("Name");

                    b.Property<string>("VenueName");

                    b.HasKey("Id");

                    b.ToTable("EventsList","ReadModel");
                });

            modelBuilder.Entity("TicketManagement.Domain.Sales.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email");

                    b.Property<float>("Price");

                    b.Property<int>("Status");

                    b.Property<int>("TicketCount");

                    b.HasKey("Id");

                    b.ToTable("Orders","WriteModel");
                });

            modelBuilder.Entity("TicketManagement.Domain.Event.Event", b =>
                {
                    b.OwnsOne("TicketManagement.Domain.Event.Venue", "Venue", b1 =>
                        {
                            b1.Property<int>("EventId");

                            b1.Property<float>("Latitude")
                                .HasColumnName("Latitude");

                            b1.Property<float>("Longitude")
                                .HasColumnName("Longitude");

                            b1.Property<string>("Name")
                                .HasColumnName("VenueName");

                            b1.ToTable("Events","WriteModel");

                            b1.HasOne("TicketManagement.Domain.Event.Event")
                                .WithOne("Venue")
                                .HasForeignKey("TicketManagement.Domain.Event.Venue", "EventId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TicketManagement.Domain.Sales.Order", b =>
                {
                    b.OwnsOne("TicketManagement.Domain.Sales.Event", "Event", b1 =>
                        {
                            b1.Property<int>("OrderId");

                            b1.Property<int>("Id")
                                .HasColumnName("EventId");

                            b1.Property<string>("Name")
                                .HasColumnName("EventName");

                            b1.ToTable("Orders","WriteModel");

                            b1.HasOne("TicketManagement.Domain.Sales.Order")
                                .WithOne("Event")
                                .HasForeignKey("TicketManagement.Domain.Sales.Event", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
