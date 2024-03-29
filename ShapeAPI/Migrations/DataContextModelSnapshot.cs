﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShapeAPI.Data;

#nullable disable

namespace ShapeAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShapeAPI.Models.Shapes.BaseShape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ShapeGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShapeGroupId");

                    b.ToTable("BaseShape");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseShape");
                });

            modelBuilder.Entity("ShapeAPI.Models.Shapes.ShapeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ShapesGroups");
                });

            modelBuilder.Entity("ShapeAPI.Models.Shapes.CircleShape", b =>
                {
                    b.HasBaseType("ShapeAPI.Models.Shapes.BaseShape");

                    b.Property<double>("Diameter")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("CircleShape");
                });

            modelBuilder.Entity("ShapeAPI.Models.Shapes.RectangleShape", b =>
                {
                    b.HasBaseType("ShapeAPI.Models.Shapes.BaseShape");

                    b.Property<double>("Lenght")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("RectangleShape");
                });

            modelBuilder.Entity("ShapeAPI.Models.Shapes.TriangleShape", b =>
                {
                    b.HasBaseType("ShapeAPI.Models.Shapes.BaseShape");

                    b.Property<double>("BaseLenght")
                        .HasColumnType("double precision");

                    b.Property<double>("SideOne")
                        .HasColumnType("double precision");

                    b.Property<double>("SideTwo")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("TriangleShape");
                });

            modelBuilder.Entity("ShapeAPI.Models.Shapes.BaseShape", b =>
                {
                    b.HasOne("ShapeAPI.Models.Shapes.ShapeGroup", null)
                        .WithMany("Shapes")
                        .HasForeignKey("ShapeGroupId");
                });

            modelBuilder.Entity("ShapeAPI.Models.Shapes.ShapeGroup", b =>
                {
                    b.Navigation("Shapes");
                });
#pragma warning restore 612, 618
        }
    }
}
