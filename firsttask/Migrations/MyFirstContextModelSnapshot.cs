﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using firsttask.Data;

#nullable disable

namespace firsttask.Migrations
{
    [DbContext(typeof(MyFirstContext))]
    partial class MyFirstContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("firsttask.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "لباس"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "شلوار"
                        });
                });

            modelBuilder.Entity("firsttask.Models.Havale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("HavaleTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceOfAll")
                        .HasColumnType("int");

                    b.Property<int>("PriceOfOne")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Havales");
                });

            modelBuilder.Entity("firsttask.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Price = 0,
                            ProductName = "پیراهن مردانه"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Price = 0,
                            ProductName = "لی"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Price = 0,
                            ProductName = "پیراهن زنانه"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Price = 0,
                            ProductName = "کتان"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Price = 0,
                            ProductName = "هودی"
                        });
                });

            modelBuilder.Entity("firsttask.Models.Resid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PriceOfAll")
                        .HasColumnType("int");

                    b.Property<int>("PriceOfOne")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("ResidTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Resids");
                });

            modelBuilder.Entity("firsttask.Models.Product", b =>
                {
                    b.HasOne("firsttask.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("firsttask.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
