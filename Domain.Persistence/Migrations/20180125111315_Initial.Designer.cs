﻿// <auto-generated />
using Domain.Model.Enums;
using Infraestructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Infraestructure.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180125111315_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Domain.Model.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("EndedAt");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Domain.Model.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SongId");

                    b.Property<string>("URL")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SongId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Domain.Model.Sheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<int>("Instrument");

                    b.Property<bool>("IsAnnotated");

                    b.Property<int>("Voice");

                    b.HasKey("Id");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("Domain.Model.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("Domain.Model.Link", b =>
                {
                    b.HasOne("Domain.Model.Song")
                        .WithMany("Videos")
                        .HasForeignKey("SongId");
                });

            modelBuilder.Entity("Domain.Model.Song", b =>
                {
                    b.HasOne("Domain.Model.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });
#pragma warning restore 612, 618
        }
    }
}