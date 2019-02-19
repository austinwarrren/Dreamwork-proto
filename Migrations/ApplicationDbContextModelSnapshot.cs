﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dreamwork_proto;

namespace dreamworkproto.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("dreamwork_proto.Models.Run", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Distance");

                    b.Property<DateTime>("RunDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("RunData");
                });

            modelBuilder.Entity("dreamwork_proto.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("content");

                    b.Property<DateTime>("endDate");

                    b.Property<DateTime>("startDate");

                    b.HasKey("Id");

                    b.ToTable("WorkoutData");
                });
#pragma warning restore 612, 618
        }
    }
}