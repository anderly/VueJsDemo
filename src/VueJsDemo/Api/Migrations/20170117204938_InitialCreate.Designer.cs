using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using VueJsDemo.Api.Contexts;

namespace VueJsDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170117204938_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VueJsDemo.Api.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company")
                        .HasMaxLength(255);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("JobTitle")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });
        }
    }
}
