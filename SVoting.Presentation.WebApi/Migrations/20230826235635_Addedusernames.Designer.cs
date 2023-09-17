﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SVoting.Persistence.Data;

#nullable disable

namespace SVoting.Presentation.WebApi.Migrations
{
    [DbContext(typeof(SVotingDbContext))]
    [Migration("20230826235635_Addedusernames")]
    partial class Addedusernames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("SVoting.Domain.Entities.Code", b =>
                {
                    b.Property<string>("Identifier")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PollId")
                        .HasColumnType("TEXT");

                    b.HasKey("Identifier");

                    b.HasIndex("PollId");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Nominee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Photograph")
                        .HasColumnType("BLOB");

                    b.Property<string>("Photomime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Nominees");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.NomineeCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("NomineeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PollCategoryId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NomineeId");

                    b.HasIndex("PollCategoryId");

                    b.ToTable("NomineeCategories");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Poll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PollingSpaceId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Published")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PollingSpaceId");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PollId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PollId");

                    b.ToTable("PollCategories");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollingCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Identifyer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PollingCategories");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollingSpace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PollingSpaces");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("NomineeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PollCategoryId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("VotingCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NomineeId");

                    b.HasIndex("PollCategoryId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Code", b =>
                {
                    b.HasOne("SVoting.Domain.Entities.Poll", "Poll")
                        .WithMany()
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Poll");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.NomineeCategory", b =>
                {
                    b.HasOne("SVoting.Domain.Entities.Nominee", "Nominee")
                        .WithMany("NomineeCategories")
                        .HasForeignKey("NomineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SVoting.Domain.Entities.PollCategory", "PollCategory")
                        .WithMany("NomineeCategories")
                        .HasForeignKey("PollCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nominee");

                    b.Navigation("PollCategory");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Poll", b =>
                {
                    b.HasOne("SVoting.Domain.Entities.PollingSpace", "PollingSpace")
                        .WithMany("Polls")
                        .HasForeignKey("PollingSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PollingSpace");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollCategory", b =>
                {
                    b.HasOne("SVoting.Domain.Entities.PollingCategory", "Category")
                        .WithMany("PollCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SVoting.Domain.Entities.Poll", "Poll")
                        .WithMany("PollCategories")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Poll");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Vote", b =>
                {
                    b.HasOne("SVoting.Domain.Entities.Nominee", "Nominee")
                        .WithMany("Votes")
                        .HasForeignKey("NomineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SVoting.Domain.Entities.PollCategory", "PollCategory")
                        .WithMany("Votes")
                        .HasForeignKey("PollCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nominee");

                    b.Navigation("PollCategory");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Nominee", b =>
                {
                    b.Navigation("NomineeCategories");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.Poll", b =>
                {
                    b.Navigation("PollCategories");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollCategory", b =>
                {
                    b.Navigation("NomineeCategories");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollingCategory", b =>
                {
                    b.Navigation("PollCategories");
                });

            modelBuilder.Entity("SVoting.Domain.Entities.PollingSpace", b =>
                {
                    b.Navigation("Polls");
                });
#pragma warning restore 612, 618
        }
    }
}