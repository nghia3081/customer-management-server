﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Entities;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(CustomerManageContext))]
    partial class CustomerManageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MenuUser", b =>
                {
                    b.Property<string>("MenusMenuId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UsersUsername")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MenusMenuId", "UsersUsername");

                    b.HasIndex("UsersUsername");

                    b.ToTable("MenuUser");
                });

            modelBuilder.Entity("Repository.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActivatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("InvoiceExportedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LicenseEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LicenseStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Repository.Entities.ContractDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<int>("LicenseMonthQuantity")
                        .HasColumnType("int");

                    b.Property<int>("LicenseNumberQuantity")
                        .HasColumnType("int");

                    b.Property<string>("PackageCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TaxValue")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("PackageCode");

                    b.ToTable("ContractDetails");
                });

            modelBuilder.Entity("Repository.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("TaxCode")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Repository.Entities.Menu", b =>
                {
                    b.Property<string>("MenuId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ParentMenuId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TabTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuId");

                    b.HasIndex("ParentMenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Repository.Entities.Package", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LicenseMonthQuantity")
                        .HasColumnType("int");

                    b.Property<int>("LicenseNumberQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxCategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("Code");

                    b.HasIndex("TaxCategoryId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Repository.Entities.StatusCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusCategories");
                });

            modelBuilder.Entity("Repository.Entities.TaxCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaxCategories");
                });

            modelBuilder.Entity("Repository.Entities.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MenuUser", b =>
                {
                    b.HasOne("Repository.Entities.Menu", null)
                        .WithMany()
                        .HasForeignKey("MenusMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repository.Entities.Contract", b =>
                {
                    b.HasOne("Repository.Entities.Customer", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.Entities.StatusCategory", "StatusCategory")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("StatusCategory");
                });

            modelBuilder.Entity("Repository.Entities.ContractDetail", b =>
                {
                    b.HasOne("Repository.Entities.Contract", "Contract")
                        .WithMany("Details")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.Entities.Package", "Package")
                        .WithMany("ContractDetails")
                        .HasForeignKey("PackageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Repository.Entities.Customer", b =>
                {
                    b.HasOne("Repository.Entities.User", "CreatedByNavigation")
                        .WithMany("Customers")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");
                });

            modelBuilder.Entity("Repository.Entities.Menu", b =>
                {
                    b.HasOne("Repository.Entities.Menu", "ParentMenu")
                        .WithMany("ChildMenus")
                        .HasForeignKey("ParentMenuId");

                    b.Navigation("ParentMenu");
                });

            modelBuilder.Entity("Repository.Entities.Package", b =>
                {
                    b.HasOne("Repository.Entities.TaxCategory", "TaxCategory")
                        .WithMany("Packages")
                        .HasForeignKey("TaxCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxCategory");
                });

            modelBuilder.Entity("Repository.Entities.Contract", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("Repository.Entities.Customer", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Repository.Entities.Menu", b =>
                {
                    b.Navigation("ChildMenus");
                });

            modelBuilder.Entity("Repository.Entities.Package", b =>
                {
                    b.Navigation("ContractDetails");
                });

            modelBuilder.Entity("Repository.Entities.TaxCategory", b =>
                {
                    b.Navigation("Packages");
                });

            modelBuilder.Entity("Repository.Entities.User", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
