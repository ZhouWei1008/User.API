﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User.Api.Data;

namespace User.Api.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("User.Api.Models.AppUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Avatar");

                    b.Property<string>("City");

                    b.Property<int>("CityID");

                    b.Property<string>("Company");

                    b.Property<string>("Email");

                    b.Property<byte>("Gender");

                    b.Property<string>("NameCard");

                    b.Property<string>("Phone");

                    b.Property<string>("Province");

                    b.Property<int>("ProvinceID");

                    b.Property<string>("Tel");

                    b.Property<string>("Title");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("User.Api.Models.BPFile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("FileName");

                    b.Property<string>("FormatFilePath");

                    b.Property<string>("OriginFilePath");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("UserBPFiles");
                });

            modelBuilder.Entity("User.Api.Models.UserPorperty", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(100);

                    b.Property<int>("AppUserID");

                    b.Property<string>("Value")
                        .HasMaxLength(100);

                    b.Property<string>("Text");

                    b.HasKey("Key", "AppUserID", "Value");

                    b.HasIndex("AppUserID");

                    b.ToTable("UserPorperty");
                });

            modelBuilder.Entity("User.Api.Models.UserTag", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<string>("Tag")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedTime");

                    b.HasKey("UserID", "Tag");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("User.Api.Models.UserPorperty", b =>
                {
                    b.HasOne("User.Api.Models.AppUser")
                        .WithMany("Porpertys")
                        .HasForeignKey("AppUserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}