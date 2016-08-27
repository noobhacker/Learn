using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Learn.Models;

namespace Learn.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Learn.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Points");

                    b.HasKey("Id");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Learn.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Learn.Models.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DueDate");

                    b.Property<string>("Name");

                    b.Property<int>("Points");

                    b.HasKey("Id");

                    b.ToTable("Homework");
                });

            modelBuilder.Entity("Learn.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerString");

                    b.Property<int>("BookId");

                    b.Property<int>("QuestionImageId");

                    b.Property<string>("QuestionString");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Learn.Models.Skin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Name");

                    b.Property<bool>("Owned");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.ToTable("Skin");
                });

            modelBuilder.Entity("Learn.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComboMultiplierLevel");

                    b.Property<int>("CurrentExp");

                    b.Property<int>("Gold");

                    b.Property<int>("GoldMultiplierLevel");

                    b.Property<int>("HomeworkEXP");

                    b.Property<int>("Level");

                    b.Property<int>("LevelUpExp");

                    b.Property<string>("Name");

                    b.Property<int>("ReadingEXP");

                    b.Property<string>("SkinColor");

                    b.Property<int>("TestEXP");

                    b.Property<int>("WarningLevel");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
        }
    }
}
