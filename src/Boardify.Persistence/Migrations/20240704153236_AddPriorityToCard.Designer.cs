﻿// <auto-generated />
using System;
using Boardify.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Boardify.Persistence.Migrations
{
    [DbContext(typeof(DatabaseService))]
    [Migration("20240704153236_AddPriorityToCard")]
    partial class AddPriorityToCard
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Boardify.Domain.Entities.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Visibility")
                        .HasColumnType("int");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColumnId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int?>("ReporterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Column", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("varchar(90)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Workspace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(72)
                        .HasColumnType("varchar(72)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.BoardLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<string>("Colour")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("BoardLabels");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.BoardMember", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BoardId");

                    b.HasIndex("BoardId");

                    b.ToTable("BoardsMember");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.CardAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardAttachments");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.CardLabel", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("LabelId")
                        .HasColumnType("int");

                    b.HasKey("CardId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("CardLabels");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.CardMember", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("CardMembers");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.ChecklistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int?>("ChecklistItemId")
                        .HasColumnType("int");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("ChecklistItemId");

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.UserWorkspace", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "WorkspaceId");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("UserWorkspaces");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Board", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Workspace", "Workspace")
                        .WithMany("Boards")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Card", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Column", "Column")
                        .WithMany("Cards")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boardify.Domain.Entities.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");

                    b.Navigation("Column");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Column", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Board", "Board")
                        .WithMany("Columns")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.BoardLabel", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Board", "Board")
                        .WithMany("BoardLabels")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.BoardMember", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Board", "Board")
                        .WithMany("BoardMembers")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boardify.Domain.Entities.User", "User")
                        .WithMany("Boards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.CardAttachment", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Card", "Card")
                        .WithMany("CardAttachments")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.CardLabel", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Card", "Card")
                        .WithMany("CardLabels")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boardify.Domain.Relationships.BoardLabel", "BoardLabel")
                        .WithMany("CardLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardLabel");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.CardMember", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Card", "Card")
                        .WithMany("CardMembers")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boardify.Domain.Entities.User", "User")
                        .WithMany("CardMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.ChecklistItem", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.Card", "Card")
                        .WithMany("ChecklistItem")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boardify.Domain.Relationships.ChecklistItem", null)
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistItemId");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.UserWorkspace", b =>
                {
                    b.HasOne("Boardify.Domain.Entities.User", "User")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boardify.Domain.Entities.Workspace", "Workspace")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Board", b =>
                {
                    b.Navigation("BoardLabels");

                    b.Navigation("BoardMembers");

                    b.Navigation("Columns");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Card", b =>
                {
                    b.Navigation("CardAttachments");

                    b.Navigation("CardLabels");

                    b.Navigation("CardMembers");

                    b.Navigation("ChecklistItem");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Column", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.User", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("CardMembers");

                    b.Navigation("UserWorkspaces");
                });

            modelBuilder.Entity("Boardify.Domain.Entities.Workspace", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("UserWorkspaces");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.BoardLabel", b =>
                {
                    b.Navigation("CardLabels");
                });

            modelBuilder.Entity("Boardify.Domain.Relationships.ChecklistItem", b =>
                {
                    b.Navigation("ChecklistItems");
                });
#pragma warning restore 612, 618
        }
    }
}
