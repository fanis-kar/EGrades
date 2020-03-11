using EGrades.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            //--------------------------------------------------------------------//

            context.Database.EnsureCreated();

            if (context.ApplicationUsers.Any() && context.Students.Any() && context.Secretaries.Any()
                && context.Instructors.Any() && context.CourseInstructors.Any() && context.Courses.Any()
                && context.Enrollments.Any() && context.Grades.Any() && context.Directions.Any()
                && context.AcademicPeriods.Any() && context.ExamPeriods.Any() && context.Certificates.Any()
                && context.StudentCertificates.Any())
            {
                return;   // DB has been seeded
            }

            string password = "123456!Aa";

            //--------------------------------------------------------------------//

            if (await roleManager.FindByNameAsync("Student") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }
            if (await roleManager.FindByNameAsync("Secretary") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Secretary"));
            }

            //--------------------------------------------------------------------//

            if (await userManager.FindByNameAsync("student1@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "student1",
                    Email = "student1@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            if (await userManager.FindByNameAsync("student2@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "student2",
                    Email = "student2@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            if (await userManager.FindByNameAsync("student3@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "student3",
                    Email = "student3@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            if (await userManager.FindByNameAsync("student4@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "student4",
                    Email = "student4@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            if (await userManager.FindByNameAsync("student5@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "student5",
                    Email = "student5@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            if (await userManager.FindByNameAsync("student6@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "student6",
                    Email = "student6@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            if (await userManager.FindByNameAsync("secretary1@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "secretary1",
                    Email = "secretary1@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Secretary");
                }
            }

            if (await userManager.FindByNameAsync("secretary2@unia.gr") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "secretary2",
                    Email = "secretary2@unia.gr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Secretary");
                }
            }

            //--------------------------------------------------------------------//

            if (!context.AcademicPeriods.Any())
            {
                var academicPeriods = new List<AcademicPeriod>
                {
                    new AcademicPeriod {
                        Year = 2019,
                        Semester = "Χειμερινό Εξάμηνο",
                        StartDate = DateTime.Parse("2019-10-01"),
                        EndDate = DateTime.Parse("2020-02-15"),
                        Current = true,
                        OpenEnrollment = true
                    },
                    new AcademicPeriod {
                        Year = 2019,
                        Semester = "Εαρινό Εξάμηνο",
                        StartDate = DateTime.Parse("2020-03-01"),
                        EndDate = DateTime.Parse("2020-07-15"),
                        Current = false,
                        OpenEnrollment = false
                    }
                };

                foreach (AcademicPeriod ap in academicPeriods)
                {
                    context.AcademicPeriods.Add(ap);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.ExamPeriods.Any())
            {
                var examPeriods = new List<ExamPeriod>
                {
                    new ExamPeriod {
                        Year = 2019,
                        Period = "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020",
                        StartDate = DateTime.Parse("2020-01-20"),
                        EndDate = DateTime.Parse("2020-02-15")
                    },
                    new ExamPeriod {
                        Year = 2019,
                        Period = "Περίοδος Εξετάσεων Εαρινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020",
                        StartDate = DateTime.Parse("2020-05-20"),
                        EndDate = DateTime.Parse("2020-06-15")
                    },
                    new ExamPeriod {
                        Year = 2019,
                        Period = "Επαναληπτική Εξεταστική Περιόδος - Ακαδημαϊκού έτους: 2019-2020",
                        StartDate = DateTime.Parse("2020-08-20"),
                        EndDate = DateTime.Parse("2020-09-15")
                    }
                };

                foreach (ExamPeriod ep in examPeriods)
                {
                    context.ExamPeriods.Add(ep);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Directions.Any())
            {
                var directions = new List<Direction>
                {
                    new Direction {
                        Name = "Γενική Κατεύθυνση"
                    },
                    new Direction {
                        Name = "Κατεύθυνση Λογισμικού"
                    },
                    new Direction {
                        Name = "Κατεύθυνση Υλικού"
                    },
                    new Direction {
                        Name = "Κατεύθυνση Δικτύων"
                    }
                };

                foreach (Direction d in directions)
                {
                    context.Directions.Add(d);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Secretaries.Any())
            {
                var secretaries = new List<Secretary>
                {
                    new Secretary {
                        RegistrationNumber = "secretary1",
                        FirstName = "Όνομα1",
                        LastName = "Επώνυμο1",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο1",
                        MotherName = "Μητρώνυμο1",
                        Address = "Διεύθυνση1",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06"),
                        ApplicationUser = context.ApplicationUsers.Single(s => s.UserName == "secretary1")
                    },
                    new Secretary {
                        RegistrationNumber = "secretary2",
                        FirstName = "Όνομα2",
                        LastName = "Επώνυμο2",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο2",
                        MotherName = "Μητρώνυμο2",
                        Address = "Διεύθυνση2",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06"),
                        ApplicationUser = context.ApplicationUsers.Single(s => s.UserName == "secretary2")
                    }
                };

                foreach (Secretary s in secretaries)
                {
                    context.Secretaries.Add(s);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student {
                        RegistrationNumber = "student1",
                        FirstName = "Όνομα1",
                        LastName = "Επώνυμο1",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο1",
                        MotherName = "Μητρώνυμο1",
                        Address = "Διεύθυνση1",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        ApplicationUser = context.ApplicationUsers.Single(d => d.UserName == "student1"),
                        EnrollmentDate = DateTime.Parse("2019-10-25")
                    },
                    new Student {
                        RegistrationNumber = "student2",
                        FirstName = "Όνομα2",
                        LastName = "Επώνυμο2",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο2",
                        MotherName = "Μητρώνυμο2",
                        Address = "Διεύθυνση2",
                        BirthDate = DateTime.Parse("1994-12-06"),
                        Semester = 3,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        ApplicationUser = context.ApplicationUsers.Single(d => d.UserName == "student2"),
                        EnrollmentDate = DateTime.Parse("2018-10-25")
                    },
                    new Student {
                        RegistrationNumber = "student3",
                        FirstName = "Όνομα3",
                        LastName = "Επώνυμο3",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο3",
                        MotherName = "Μητρώνυμο3",
                        Address = "Διεύθυνση3",
                        BirthDate = DateTime.Parse("1994-12-06"),
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        ApplicationUser = context.ApplicationUsers.Single(d => d.UserName == "student3"),
                        EnrollmentDate = DateTime.Parse("2018-10-25")
                    },
                    new Student {
                        RegistrationNumber = "student4",
                        FirstName = "Όνομα4",
                        LastName = "Επώνυμο4",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο4",
                        MotherName = "Μητρώνυμο4",
                        Address = "Διεύθυνση4",
                        BirthDate = DateTime.Parse("1994-12-06"),
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        ApplicationUser = context.ApplicationUsers.Single(d => d.UserName == "student4"),
                        EnrollmentDate = DateTime.Parse("2018-10-25")
                    },
                    new Student {
                        RegistrationNumber = "student5",
                        FirstName = "Όνομα5",
                        LastName = "Επώνυμο5",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο5",
                        MotherName = "Μητρώνυμο5",
                        Address = "Διεύθυνση5",
                        BirthDate = DateTime.Parse("1994-12-06"),
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        ApplicationUser = context.ApplicationUsers.Single(d => d.UserName == "student5"),
                        EnrollmentDate = DateTime.Parse("2018-10-25")
                    },
                    new Student {
                        RegistrationNumber = "student6",
                        FirstName = "Όνομα6",
                        LastName = "Επώνυμο6",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο6",
                        MotherName = "Μητρώνυμο6",
                        Address = "Διεύθυνση6",
                        BirthDate = DateTime.Parse("1994-12-06"),
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        ApplicationUser = context.ApplicationUsers.Single(d => d.UserName == "student6"),
                        EnrollmentDate = DateTime.Parse("2018-10-25")
                    },
                };

                foreach (Student s in students)
                {
                    context.Students.Add(s);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Instructors.Any())
            {
                var instructors = new List<Instructor>
                {
                    new Instructor {
                        RegistrationNumber = "professor1",
                        FirstName = "Όνομα1",
                        LastName = "Επώνυμο1",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο1",
                        MotherName = "Μητρώνυμο1",
                        Address = "Διεύθυνση1",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor2",
                        FirstName = "Όνομα2",
                        LastName = "Επώνυμο2",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο2",
                        MotherName = "Μητρώνυμο2",
                        Address = "Διεύθυνση2",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor3",
                        FirstName = "Όνομα3",
                        LastName = "Επώνυμο3",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο3",
                        MotherName = "Μητρώνυμο3",
                        Address = "Διεύθυνση3",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor4",
                        FirstName = "Όνομα4",
                        LastName = "Επώνυμο4",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο4",
                        MotherName = "Μητρώνυμο4",
                        Address = "Διεύθυνση4",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor5",
                        FirstName = "Όνομα5",
                        LastName = "Επώνυμο5",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο5",
                        MotherName = "Μητρώνυμο5",
                        Address = "Διεύθυνση5",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor6",
                        FirstName = "Όνομα6",
                        LastName = "Επώνυμο6",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο6",
                        MotherName = "Μητρώνυμο6",
                        Address = "Διεύθυνση6",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor7",
                        FirstName = "Όνομα7",
                        LastName = "Επώνυμο7",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο7",
                        MotherName = "Μητρώνυμο7",
                        Address = "Διεύθυνση7",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor8",
                        FirstName = "Όνομα8",
                        LastName = "Επώνυμο8",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο8",
                        MotherName = "Μητρώνυμο8",
                        Address = "Διεύθυνση8",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor9",
                        FirstName = "Όνομα9",
                        LastName = "Επώνυμο9",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο9",
                        MotherName = "Μητρώνυμο9",
                        Address = "Διεύθυνση9",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor10",
                        FirstName = "Όνομα10",
                        LastName = "Επώνυμο10",
                        Gender = "Άρρεν",
                        FatherName = "Πατρώνυμο10",
                        MotherName = "Μητρώνυμο10",
                        Address = "Διεύθυνση10",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor11",
                        FirstName = "Όνομα11",
                        LastName = "Επώνυμο11",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο11",
                        MotherName = "Μητρώνυμο11",
                        Address = "Διεύθυνση11",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor12",
                        FirstName = "Όνομα12",
                        LastName = "Επώνυμο12",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο12",
                        MotherName = "Μητρώνυμο12",
                        Address = "Διεύθυνση12",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor13",
                        FirstName = "Όνομα13",
                        LastName = "Επώνυμο13",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο13",
                        MotherName = "Μητρώνυμο13",
                        Address = "Διεύθυνση13",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor14",
                        FirstName = "Όνομα14",
                        LastName = "Επώνυμο14",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο14",
                        MotherName = "Μητρώνυμο14",
                        Address = "Διεύθυνση14",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor15",
                        FirstName = "Όνομα15",
                        LastName = "Επώνυμο15",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο15",
                        MotherName = "Μητρώνυμο15",
                        Address = "Διεύθυνση15",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor16",
                        FirstName = "Όνομα16",
                        LastName = "Επώνυμο16",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο16",
                        MotherName = "Μητρώνυμο16",
                        Address = "Διεύθυνση16",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor17",
                        FirstName = "Όνομα17",
                        LastName = "Επώνυμο17",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο17",
                        MotherName = "Μητρώνυμο17",
                        Address = "Διεύθυνση17",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor18",
                        FirstName = "Όνομα18",
                        LastName = "Επώνυμο18",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο18",
                        MotherName = "Μητρώνυμο18",
                        Address = "Διεύθυνση18",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor19",
                        FirstName = "Όνομα19",
                        LastName = "Επώνυμο19",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο19",
                        MotherName = "Μητρώνυμο19",
                        Address = "Διεύθυνση19",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    },
                    new Instructor {
                        RegistrationNumber = "professor20",
                        FirstName = "Όνομα20",
                        LastName = "Επώνυμο20",
                        Gender = "Θήλυ",
                        FatherName = "Πατρώνυμο20",
                        MotherName = "Μητρώνυμο20",
                        Address = "Διεύθυνση20",
                        BirthDate = DateTime.Parse("1993-12-06"),
                        HireDate = DateTime.Parse("2005-12-06")
                    }
                };

                foreach (Instructor i in instructors)
                {
                    context.Instructors.Add(i);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new Course {
                        Title = "Μάθημα ΚΛ11",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ12",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ13",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ14",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ15",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ21",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ22",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ23",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ24",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΛ25",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Λογισμικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ11",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ12",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ13",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ14",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ15",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ21",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ22",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ23",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ24",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΔ25",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Δικτύων"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ11",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ12",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ13",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ14",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ15",
                        Type = "Υποχρεωτικό",
                        Semester = 1,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ21",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ22",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ23",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ24",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα ΚΥ25",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Κατεύθυνση Υλικού"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα Ε1",
                        Type = "Επιλογής",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Γενική Κατεύθυνση"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα Ε2",
                        Type = "Επιλογής",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Γενική Κατεύθυνση"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Μάθημα Ε3",
                        Type = "Επιλογής",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Γενική Κατεύθυνση"),
                        TheoryHours = 2,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 6
                    },
                    new Course {
                        Title = "Ερευνητική εργασία",
                        Type = "Επιλογής",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Γενική Κατεύθυνση"),
                        TheoryHours = 0,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 12
                    },
                    new Course {
                        Title = "Μεταπτυχιακή Διπλωματική Εργασία",
                        Type = "Υποχρεωτικό",
                        Semester = 2,
                        Direction = context.Directions.Single(d => d.Name == "Γενική Κατεύθυνση"),
                        TheoryHours = 0,
                        ExerciseHours = 0,
                        LabHours = 0,
                        Ects = 12
                    }
                };

                foreach (Course c in courses)
                {
                    context.Courses.Add(c);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.CourseInstructors.Any())
            {
                var courseInstructors = new List<CourseInstructor>
                {
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ11"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor1"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ12"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor2"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ13"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor3"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ14"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor4"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ15"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor5"),
                    },

                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ11"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor6"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ12"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor7"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ13"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor8"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ14"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor9"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ15"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor10"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ21"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor11"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ22"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor12"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ23"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor13"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ24"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor14"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ25"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor15"),
                    },

                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ11"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor16"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ12"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor17"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ13"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor18"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ14"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor19"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ15"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor20"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ21"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor1"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ22"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor2"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ23"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor3"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ24"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor4"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΥ25"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor5"),
                    },

                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα Ε1"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor6"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα Ε2"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor7"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα Ε3"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor8"),
                    },

                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Ερευνητική εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor1"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Ερευνητική εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor12"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Ερευνητική εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor3"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Ερευνητική εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor14"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Ερευνητική εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor5"),
                    },

                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor11"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor12"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor13"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor14"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor15"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor16"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor17"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor18"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor19"),
                    },
                    new CourseInstructor {
                        Course = context.Courses.Single(c => c.Title == "Μεταπτυχιακή Διπλωματική Εργασία"),
                        Instructor = context.Instructors.Single(i => i.RegistrationNumber == "professor20"),
                    },
                };

                foreach (CourseInstructor ci in courseInstructors)
                {
                    context.CourseInstructors.Add(ci);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Enrollments.Any())
            {
                var enrollments = new List<Enrollment>
                {
                    new Enrollment {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student1"),
                        AcademicPeriod = context.AcademicPeriods.Where(ap => ap.Year == 2019).Where(ap => ap.Semester == "Χειμερινό Εξάμηνο").Single(),
                        DateAdded = DateTime.Parse("2019-10-20")
                    },
                    new Enrollment {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student2"),
                        AcademicPeriod = context.AcademicPeriods.Where(ap => ap.Year == 2019).Where(ap => ap.Semester == "Χειμερινό Εξάμηνο").Single(),
                        DateAdded = DateTime.Parse("2019-10-20")
                    },
                    new Enrollment {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student3"),
                        AcademicPeriod = context.AcademicPeriods.Where(ap => ap.Year == 2019).Where(ap => ap.Semester == "Χειμερινό Εξάμηνο").Single(),
                        DateAdded = DateTime.Parse("2019-10-20")
                    },
                    new Enrollment {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student4"),
                        AcademicPeriod = context.AcademicPeriods.Where(ap => ap.Year == 2019).Where(ap => ap.Semester == "Χειμερινό Εξάμηνο").Single(),
                        DateAdded = DateTime.Parse("2019-10-20")
                    },
                    new Enrollment {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student5"),
                        AcademicPeriod = context.AcademicPeriods.Where(ap => ap.Year == 2019).Where(ap => ap.Semester == "Χειμερινό Εξάμηνο").Single(),
                        DateAdded = DateTime.Parse("2019-10-20")
                    },
                    new Enrollment {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student6"),
                        AcademicPeriod = context.AcademicPeriods.Where(ap => ap.Year == 2019).Where(ap => ap.Semester == "Χειμερινό Εξάμηνο").Single(),
                        DateAdded = DateTime.Parse("2019-10-20")
                    },
                };

                foreach (Enrollment e in enrollments)
                {
                    context.Enrollments.Add(e);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Grades.Any())
            {
                var grades = new List<Grade>
                {
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ11"),
                        Value = 10,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student1")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-20"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary1"),
                        EntryDate = DateTime.Parse("2020-02-05")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ12"),
                        Value = 8.2,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student1")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-22"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary2"),
                        EntryDate = DateTime.Parse("2020-02-08")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ13"),
                        Value = 4.2,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student1")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-22"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary1"),
                        EntryDate = DateTime.Parse("2020-02-08")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ15"),
                        //Value = 10,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student1")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-22"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary1"),
                        EntryDate = DateTime.Parse("2020-02-08")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΔ12"),
                        //Value = 8,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student2")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single()
                        //ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        //ExamDate = DateTime.Parse("2020-01-22"),
                        //Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary1"),
                        //EntryDate = DateTime.Parse("2020-02-08")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ11"),
                        Value = 8.4,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student2")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-20"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary2"),
                        EntryDate = DateTime.Parse("2020-02-05")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ11"),
                        Value = 8,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student3")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-20"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary1"),
                        EntryDate = DateTime.Parse("2020-02-05")
                    },
                    new Grade {
                        Course = context.Courses.Single(c => c.Title == "Μάθημα ΚΛ11"),
                        Value = 6.5,
                        Enrollment = context
                            .Enrollments
                            .Where(e => e.Student.RegistrationNumber == "student4")
                            .Where(e => e.AcademicPeriod.Year == 2019)
                            .Where(e => e.AcademicPeriod.Semester == "Χειμερινό Εξάμηνο")
                            .Single(),
                        ExamPeriod = context.ExamPeriods.Single(ep => ep.Period == "Περίοδος Εξετάσεων Χειμερινού Εξαμήνου - Ακαδημαϊκού έτους: 2019-2020"),
                        ExamDate = DateTime.Parse("2020-01-20"),
                        Secretary = context.Secretaries.Single(s => s.RegistrationNumber == "secretary2"),
                        EntryDate = DateTime.Parse("2020-02-05")
                    },
                };

                foreach (Grade g in grades)
                {
                    context.Grades.Add(g);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.Certificates.Any())
            {
                var certificates = new List<Certificate>
                {
                    new Certificate {
                        Title = "Βεβαίωση Σπουδών",
                        Description = "Μη Διαθέσιμη"
                    },
                    new Certificate {
                        Title = "Αναλυτική Βαθμολογία",
                        Description = "Μη Διαθέσιμη"
                    },
                };

                foreach (Certificate c in certificates)
                {
                    context.Certificates.Add(c);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//

            if (!context.StudentCertificates.Any())
            {
                var studentCertificates = new List<StudentCertificate>
                {
                    new StudentCertificate {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student1"),
                        Certificate = context.Certificates.Single(c => c.Title == "Βεβαίωση Σπουδών"),
                        Status = "Έτοιμο",
                        RequestedDate = DateTime.Parse("2019-12-20")
                    },
                    new StudentCertificate {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student1"),
                        Certificate = context.Certificates.Single(c => c.Title == "Αναλυτική Βαθμολογία"),
                        RequestedDate = DateTime.Parse("2019-12-26")
                    },
                    new StudentCertificate {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student1"),
                        Certificate = context.Certificates.Single(c => c.Title == "Βεβαίωση Σπουδών"),
                        RequestedDate = DateTime.Parse("2019-12-25")
                    },
                    new StudentCertificate {
                        Student = context.Students.Single(s => s.RegistrationNumber == "student2"),
                        Certificate = context.Certificates.Single(c => c.Title == "Αναλυτική Βαθμολογία"),
                        RequestedDate = DateTime.Parse("2019-12-16")
                    },
                };

                foreach (StudentCertificate sc in studentCertificates)
                {
                    context.StudentCertificates.Add(sc);
                }
                context.SaveChanges();
            }

            //--------------------------------------------------------------------//
        }
    }
}