﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationApi.DAL;

namespace NotificationApi.DAL.Migrations
{
    [DbContext(typeof(NotificationsApiDbContext))]
    [Migration("20210215151010_AddMultiDayNotification")]
    partial class AddMultiDayNotification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotificationApi.Domain.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DeliveryStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HearingRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NotificationType")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParticipantRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notification");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Notification");
                });

            modelBuilder.Entity("NotificationApi.Domain.Template", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MessageType")
                        .HasColumnType("int");

                    b.Property<int>("NotificationType")
                        .HasColumnType("int");

                    b.Property<Guid>("NotifyTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Parameters")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("NotificationApi.Domain.EmailNotification", b =>
                {
                    b.HasBaseType("NotificationApi.Domain.Notification");

                    b.Property<string>("ToEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("EmailNotification");
                });

            modelBuilder.Entity("NotificationApi.Domain.SmsNotification", b =>
                {
                    b.HasBaseType("NotificationApi.Domain.Notification");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("SmsNotification");
                });
#pragma warning restore 612, 618
        }
    }
}
