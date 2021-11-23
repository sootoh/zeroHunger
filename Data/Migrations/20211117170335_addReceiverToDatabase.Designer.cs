﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroHunger.Data;

namespace ZeroHunger.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211117170335_addReceiverToDatabase")]
    partial class addReceiverToDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ZeroHunger.Model.ReceiverFamily", b =>
                {
                    b.Property<string>("familyIC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("familyDOB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("familyOccupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("familySalaryGroupID")
                        .HasColumnType("int");

                    b.Property<string>("receiverIC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("familyIC");

                    b.ToTable("ReceiverFamily");
                });

            modelBuilder.Entity("ZeroHunger.Model.SalaryGroup", b =>
                {
                    b.Property<int>("salaryGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("salaryRange")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("salaryGroupID");

                    b.ToTable("SalaryGroup");
                });

            modelBuilder.Entity("ZeroHunger.Models.Receiver", b =>
                {
                    b.Property<string>("receiverIC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("applicationStatusID")
                        .HasColumnType("int");

                    b.Property<string>("receiverAdrs1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("receiverAdrs2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("receiverDOB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("receiverEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("receiverFamilyNo")
                        .HasColumnType("int");

                    b.Property<string>("receiverGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("receiverName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("receiverOccupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("receiverPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("receiverSalaryGroupID")
                        .HasColumnType("int");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("receiverIC");

                    b.ToTable("Receiver");
                });
#pragma warning restore 612, 618
        }
    }
}
