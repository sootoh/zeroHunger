﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zero_Hunger.Model;

namespace Zero_Hunger.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Zero_Hunger.Model.CookedFoodDonation", b =>
                {
                    b.Property<int>("CookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("CookLatitude")
                        .HasColumnType("real");

                    b.Property<float>("CookLongtitude")
                        .HasColumnType("real");

                    b.Property<string>("CookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CookQuantity")
                        .HasColumnType("int");

                    b.Property<int>("DonorUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RemainQuantity")
                        .HasColumnType("int");

                    b.Property<int>("Reservation")
                        .HasColumnType("int");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CookID");

                    b.HasIndex("DonorUserID");

                    b.ToTable("CookedFoodDonation");
                });

            modelBuilder.Entity("Zero_Hunger.Model.DryFoodDonation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("DelivererId")
                        .HasColumnType("int");

                    b.Property<int?>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("DryFoodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DryFoodPickDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DryFoodQuantity")
                        .HasColumnType("int");

                    b.Property<string>("DryFoodRemark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DelivererId");

                    b.HasIndex("DonorId");

                    b.ToTable("DryFoodDonation");
                });

            modelBuilder.Entity("Zero_Hunger.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("RememberMe")
                        .HasColumnType("bit");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserAdrs1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAdrs2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("TypeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Zero_Hunger.Model.UserType", b =>
                {
                    b.Property<int>("TypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TypeInterface")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("TypeID");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("Zero_Hunger.Model.CookedFoodDonation", b =>
                {
                    b.HasOne("Zero_Hunger.Model.User", "DonorId")
                        .WithMany()
                        .HasForeignKey("DonorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonorId");
                });

            modelBuilder.Entity("Zero_Hunger.Model.DryFoodDonation", b =>
                {
                    b.HasOne("Zero_Hunger.Model.User", "deliverer_Id")
                        .WithMany()
                        .HasForeignKey("DelivererId");

                    b.HasOne("Zero_Hunger.Model.User", "donor_Id")
                        .WithMany()
                        .HasForeignKey("DonorId");

                    b.Navigation("deliverer_Id");

                    b.Navigation("donor_Id");
                });

            modelBuilder.Entity("Zero_Hunger.Model.User", b =>
                {
                    b.HasOne("Zero_Hunger.Model.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("UserType");
                });
#pragma warning restore 612, 618
        }
    }
}
