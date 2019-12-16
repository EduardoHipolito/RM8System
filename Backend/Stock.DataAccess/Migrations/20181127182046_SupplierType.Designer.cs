﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock.DataAccess;

namespace Stock.DataAccess.Migrations
{
    [DbContext(typeof(StockContext))]
    [Migration("20181127182046_SupplierType")]
    partial class SupplierType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Stock.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .HasMaxLength(130);

                    b.Property<int?>("IdCompanyPermition");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("Status")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_UniqueCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Stock.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarCode")
                        .IsRequired();

                    b.Property<string>("Color");

                    b.Property<double>("CostPrice");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .HasMaxLength(130);

                    b.Property<int>("IdCategory");

                    b.Property<int?>("IdCompanyPermition");

                    b.Property<int?>("IdSupplier");

                    b.Property<string>("InternalNumber");

                    b.Property<double>("MinPrice");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("MoreInformation");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Packing")
                        .IsRequired();

                    b.Property<byte[]>("Picture");

                    b.Property<double>("Price");

                    b.Property<int>("Status")
                        .HasMaxLength(2);

                    b.Property<int>("UnityType");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdSupplier");

                    b.HasIndex("Name", "Color")
                        .IsUnique()
                        .HasName("IX_UniqueProduct")
                        .HasFilter("[Color] IS NOT NULL");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Stock.Domain.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int?>("IdCompanyPermition");

                    b.Property<int>("IdLegalPerson");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("MoreInformation");

                    b.Property<int>("Status")
                        .HasMaxLength(2);

                    b.Property<int>("SupplierType");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Stock.Domain.Product", b =>
                {
                    b.HasOne("Stock.Domain.Category", "FKCategory")
                        .WithMany()
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Stock.Domain.Supplier", "FKSupplier")
                        .WithMany()
                        .HasForeignKey("IdSupplier");
                });
#pragma warning restore 612, 618
        }
    }
}
