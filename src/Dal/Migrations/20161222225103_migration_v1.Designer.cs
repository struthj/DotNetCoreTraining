using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dal;

namespace Dal.Migrations
{
    [DbContext(typeof(BusinessProContext))]
    [Migration("20161222225103_migration_v1")]
    partial class migration_v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Dal.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Dal.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Dal.Models.PersonRole", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("RoleId");

                    b.HasKey("PersonId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PersonRole");
                });

            modelBuilder.Entity("Dal.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Dal.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Dal.Models.Department", b =>
                {
                    b.HasOne("Dal.Models.Room")
                        .WithMany("Departments")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("Dal.Models.Person", b =>
                {
                    b.HasOne("Dal.Models.Department")
                        .WithMany("People")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("Dal.Models.PersonRole", b =>
                {
                    b.HasOne("Dal.Models.Person", "Person")
                        .WithMany("PersonRole")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dal.Models.Role", "Role")
                        .WithMany("PersonRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
