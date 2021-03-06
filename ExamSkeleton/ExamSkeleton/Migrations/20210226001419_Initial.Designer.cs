// <auto-generated />
using ExamSkeleton.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamSkeleton.Migrations
{
    [DbContext(typeof(SkeletonDbContext))]
    [Migration("20210226001419_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExamSkeleton.Data.Entities.SomeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColumnFour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColumnOne")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColumnThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColumnTwo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SomeEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
