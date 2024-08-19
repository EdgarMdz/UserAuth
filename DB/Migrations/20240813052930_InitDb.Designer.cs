﻿// <auto-generated />
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20240813052930_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = new byte[] { 109, 121, 69, 82, 122, 47, 50, 99, 66, 101, 67, 86, 112, 105, 85, 114, 100, 121, 102, 116, 70, 82, 82, 98, 77, 79, 70, 97, 115, 114, 97, 98, 75, 116, 88, 103, 122, 118, 112, 78, 55, 87, 48, 113, 89, 104, 110, 74, 72, 86, 118, 109, 101, 88, 104, 51, 87, 114, 79, 77, 114, 122, 43, 112, 49, 55, 65, 74, 85, 102, 121, 73, 87, 67, 115, 72, 105, 116, 81, 109, 105, 43, 111, 43, 121, 119, 61, 61 },
                            PasswordSalt = new byte[] { 56, 53, 80, 82, 108, 43, 121, 54, 81, 81, 97, 56, 51, 47, 88, 107, 88, 79, 117, 85, 77, 105, 113, 79, 53, 103, 76, 78, 66, 81, 104, 108, 122, 81, 83, 73, 83, 107, 51, 77, 74, 50, 81, 108, 109, 115, 109, 104, 82, 52, 101, 122, 72, 106, 67, 55, 122, 116, 98, 118, 49, 114, 72, 109, 101, 119, 67, 109, 51, 102, 99, 67, 85, 107, 107, 80, 66, 53, 121, 86, 85, 109, 106, 57, 56, 68, 108, 73, 116, 50, 100, 114, 105, 86, 56, 57, 67, 119, 106, 119, 80, 113, 81, 56, 104, 52, 72, 87, 121, 80, 120, 113, 69, 87, 65, 86, 85, 85, 65, 71, 74, 108, 65, 87, 70, 72, 80, 88, 118, 57, 107, 71, 73, 77, 83, 85, 71, 107, 104, 109, 87, 98, 78, 107, 48, 53, 70, 99, 80, 73, 57, 74, 69, 55, 71, 55, 111, 83, 77, 114, 118, 80, 88, 112, 85, 47, 108, 84, 66, 121, 111, 61 },
                            Role = 1,
                            UserName = "Administrator"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
