using System;
using System.Collections.Generic;
using System.Text;
using EGrades.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EGrades.Models.ViewModels;

namespace EGrades.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Secretary> Secretaries { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<CourseInstructor> CourseInstructors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<AcademicPeriod> AcademicPeriods { get; set; }

        public DbSet<ExamPeriod> ExamPeriods { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<StudentCertificate> StudentCertificates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ------------------------------------------------------------------------- //
            // Set Up Identity Model

            modelBuilder.Entity<ApplicationUser>().Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled);

            modelBuilder.Entity<ApplicationUser>().ToTable("User").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");

            // ------------------------------------------------------------------------- //
            // Set Table Names

            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Secretary>().ToTable("Secretary");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<CourseInstructor>().ToTable("CourseInstructor");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Grade>().ToTable("Grade");
            modelBuilder.Entity<Direction>().ToTable("Direction");
            modelBuilder.Entity<AcademicPeriod>().ToTable("AcademicPeriod");
            modelBuilder.Entity<ExamPeriod>().ToTable("ExamPeriod");
            modelBuilder.Entity<Certificate>().ToTable("Certificate");
            modelBuilder.Entity<StudentCertificate>().ToTable("StudentCertificate");

            // ------------------------------------------------------------------------- //
            // Set Primary Keys

            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Secretary>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Instructor>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new { ci.CourseId, ci.InstructorId });

            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Direction>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<AcademicPeriod>()
                .HasKey(ap => ap.Id);

            modelBuilder.Entity<ExamPeriod>()
                .HasKey(ep => ep.Id);

            modelBuilder.Entity<Certificate>()
                .HasKey(c => c.Id);

            // ------------------------------------------------------------------------- //
            // Set Unique Fields

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(au => au.UserName)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.RegistrationNumber)
                .IsUnique();

            modelBuilder.Entity<Secretary>()
                .HasIndex(s => s.RegistrationNumber)
                .IsUnique();

            modelBuilder.Entity<Instructor>()
                .HasIndex(i => i.RegistrationNumber)
                .IsUnique();

            modelBuilder.Entity<Enrollment>()
                        .HasIndex(e => new { e.StudentId, e.AcademicPeriodId })
                        .IsUnique();

            modelBuilder.Entity<AcademicPeriod>()
                        .HasIndex(ap => new { ap.Year, ap.Semester })
                        .IsUnique();

            // ------------------------------------------------------------------------- //
            // Set Default values
            
            modelBuilder.Entity<AcademicPeriod>()
                .Property("Current")
                .HasDefaultValue(false);

            modelBuilder.Entity<AcademicPeriod>()
                .Property("OpenEnrollment")
                .HasDefaultValue(false);

            modelBuilder.Entity<StudentCertificate>()
                .Property("Status")
                .HasDefaultValue("Εκκρεμεί");

            // ------------------------------------------------------------------------- //

            // Set Student Table Foreign Keys (One to One)

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(au => au.Student)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey<Student>(au => au.UserId);

            // Set Student Table Foreign Keys (One to Many)

            modelBuilder.Entity<Direction>()
                .HasMany(d => d.Students)
                .WithOne(s => s.Direction)
                .IsRequired();

            // ------------------------------------------------------------------------- //
            // Set Secretary Table Foreign Keys (One to One)

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(au => au.Secretary)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey<Secretary>(au => au.UserId);

            // ------------------------------------------------------------------------- //
            // Set Course Table Foreign Keys (One to Many)

            modelBuilder.Entity<Direction>()
                .HasMany(c => c.Courses)
                .WithOne(d => d.Direction)
                .IsRequired();

            // ------------------------------------------------------------------------- //
            // Set CourseInstructor Table Foreign Keys (Many to Many)

            modelBuilder.Entity<CourseInstructor>()
                .HasOne<Course>(ci => ci.Course)
                .WithMany(c => c.CourseInstructors)
                .HasForeignKey(ci => ci.CourseId);


            modelBuilder.Entity<CourseInstructor>()
                .HasOne<Instructor>(ci => ci.Instructor)
                .WithMany(i => i.CourseInstructors)
                .HasForeignKey(ci => ci.InstructorId);

            // ------------------------------------------------------------------------- //
            // Set Enrollment Table Foreign Keys

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicPeriod>()
                .HasMany(ap => ap.Enrollments)
                .WithOne(e => e.AcademicPeriod)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------------------------------------------------- //
            // Set Grade Table Foreign Keys

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Grades)
                .WithOne(g => g.Course)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamPeriod>()
                .HasMany(ep => ep.Grades)
                .WithOne(g => g.ExamPeriod)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Secretary>()
                .HasMany(s => s.Grades)
                .WithOne(e => e.Secretary)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------------------------------------------------- //
            // Set StudentCertificate Table Foreign Keys

            modelBuilder.Entity<Student>()
                .HasMany(s => s.StudentCertificates)
                .WithOne(sc => sc.Student)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certificate>()
                .HasMany(c => c.StudentCertificates)
                .WithOne(sc => sc.Certificate)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------------------------------------------------------- //

        }
    }
}