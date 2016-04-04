﻿using SM.Metadata;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SM.Dto
{

    internal class MedSimDtoContextPretender : DbContext
    {
        static MedSimDtoContextPretender()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<MedSimDtoContextPretender>(null);
        }
        public virtual DbSet<ActivityTeachingResourceDto> ActivityTeachingResources { get; set; }
        public virtual DbSet<ChosenTeachingResourceDto> ChosenTeachingResources { get; set; }
        public virtual DbSet<CountryDto> Countries { get; set; }
        public virtual DbSet<CourseDto> Courses { get; set; }
        public virtual DbSet<CourseActivityDto> CourseActivities { get; set; }
        public virtual DbSet<CourseFormatDto> CourseFormats { get; set; }
        public virtual DbSet<CourseParticipantDto> CourseParticipants { get; set; }
        public virtual DbSet<CourseScenarioFacultyRoleDto> CourseScenarioFacultyRoles { get; set; }
        public virtual DbSet<CourseSlotDto> CourseSlots { get; set; }
        public virtual DbSet<CourseSlotPresenterDto> CourseSlotPresenters { get; set; }
        public virtual DbSet<CourseSlotScenarioDto> CourseSlotScenarios { get; set; }
        public virtual DbSet<CourseTypeDto> CourseTypes { get; set; }
        public virtual DbSet<DepartmentDto> Departments { get; set; }
        public virtual DbSet<FacultySimRoleDto> FacultySimRoles { get; set; }
        public virtual DbSet<InstitutionDto> Institutions { get; set; }
        public virtual DbSet<ManequinDto> Manequins { get; set; }
        public virtual DbSet<ManequinManufacturerDto> ManequinManufacturers { get; set; }
        public virtual DbSet<ProfessionalRoleDto> ProfessionalRoles { get; set; }
        public virtual DbSet<RoomDto> Rooms { get; set; }
        public virtual DbSet<ScenarioDto> Scenarios { get; set; }
        public virtual DbSet<ScenarioResourceDto> ScenarioResources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add(new FixedLengthAttributeConvention());

            modelBuilder.Entity<ActivityTeachingResourceDto>()
                .HasMany(e => e.ChosenTeachingResources)
                .WithRequired(e => e.ActivityTeachingResource)
                .HasForeignKey(e => e.ActivityTeachingResourceId);

            modelBuilder.Entity<ActivityTeachingResourceDto>()
                .HasMany(e => e.ChosenTeachingResources)
                .WithRequired(e => e.ActivityTeachingResource)
                .HasForeignKey(e => e.ActivityTeachingResourceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CountryDto>()
                .HasMany(e => e.Institutions)
                .WithRequired(e => e.Country)
                .HasForeignKey(e => e.LocaleCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseDto>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseDto>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseDto>()
                .HasMany(e => e.CourseSlotScenarios)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseDto>()
                .HasMany(e => e.CourseSlotPresenters)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseDto>()
                .HasMany(e => e.ChosenTeachingResources)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseActivityDto>()
                    .HasMany(e => e.CourseSlots)
                    .WithOptional(e => e.Activity)
                    .HasForeignKey(e => e.ActivityId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseActivityDto>()
                .HasMany(e => e.ActivityChoices)
                .WithRequired(e => e.CourseActivity)
                .HasForeignKey(e => e.CourseActivityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseFormatDto>()
                .HasMany(e => e.CourseSlots)
                .WithRequired(e => e.CourseFormat)
                .HasForeignKey(e => e.CourseFormatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseFormatDto>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CourseFormat)
                .HasForeignKey(e => e.CourseTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlotDto>()
                .HasMany(e => e.ChosenTeachingResources)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlotDto>()
                .HasMany(e => e.CourseSlotPresenters)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlotDto>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlotDto>()
                .HasMany(e => e.CourseSlotScenarios)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlotDto>()
                .HasMany(e => e.ChosenTeachingResources)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseTypeDto>()
                .HasMany(e => e.FacultySimRoles)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseTypeDto>()
                .HasMany(e => e.CourseFormats)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseTypeDto>()
                .HasMany(e => e.Scenarios)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId);

            modelBuilder.Entity<CourseTypeDto>()
                .HasMany(e => e.CourseActivities)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.CourseTypes)
                .WithMany(e => e.Departments);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DefaultDepartmentId);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.OutreachCourses)
                .WithOptional(e => e.OutreachingDepartment)
                .HasForeignKey(e => e.OutreachingDepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.Scenarios)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DepartmentDto>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacultySimRoleDto>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.FacultySimRole)
                .HasForeignKey(e => e.FacultySimRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InstitutionDto>()
                .Property(e => e.LocaleCode)
                .IsFixedLength();

            modelBuilder.Entity<InstitutionDto>()
                .HasMany(e => e.Departments)
                .WithRequired(e => e.Institution)
                .HasForeignKey(e => e.InstitutionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InstitutionDto>()
                .HasMany(e => e.ProfessionalRoles)
                .WithMany(e => e.Institutions);

            modelBuilder.Entity<ManequinDto>()
                .HasMany(e => e.ManequinServices)
                .WithRequired(e => e.Manequin)
                .HasForeignKey(e => e.ManequinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManequinDto>()
                .HasMany(e => e.CourseSlotScenarios)
                .WithRequired(e => e.Manequin)
                .HasForeignKey(e => e.ManequinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManequinManufacturerDto>()
                .HasMany(e => e.ManequinModels)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.ManufacturerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManequinModelDto>()
                .HasMany(e => e.Manequins)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.ModelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManequinModelDto>()
                .HasMany(e => e.Scenarios)
                .WithOptional(e => e.ManequinModel)
                .HasForeignKey(e => e.ManequinModelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParticipantDto>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.Participant)
                .HasForeignKey(e => e.ParticipantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParticipantDto>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.FacultyMember)
                .HasForeignKey(e => e.FacultyMemberId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParticipantDto>()
                .HasMany(e => e.CourseSlotPresentations)
                .WithRequired(e => e.Presenter)
                .HasForeignKey(e => e.PresenterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfessionalRoleDto>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.ProfessionalRole)
                .HasForeignKey(e => e.DefaultProfessionalRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfessionalRoleDto>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.ProfessionalRole)
                .HasForeignKey(e => e.ProfessionalRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomDto>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Room)
                .HasForeignKey(e => e.RoomId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScenarioDto>()
                .HasMany(e => e.CourseSlotScenarios)
                .WithRequired(e => e.Scenario)
                .HasForeignKey(e => e.ScenarioId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
