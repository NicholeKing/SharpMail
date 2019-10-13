﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using csharp_email.Models;

namespace csharp_email.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20191011231222_secondMig")]
    partial class secondMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("csharp_email.Models.Mail", b =>
                {
                    b.Property<int>("MailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("PermDeleted");

                    b.Property<bool>("Read");

                    b.Property<string>("Receiver")
                        .IsRequired();

                    b.Property<string>("Sender")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("MailId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("csharp_email.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("EName")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
