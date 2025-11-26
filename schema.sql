-- SkillSync Database Schema Visualization Script
-- Run this in SQL Server to see the complete schema

-- 1. ROLES TABLE
CREATE TABLE Roles (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name INT NOT NULL
);

-- 2. USERS TABLE
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    RoleId UNIQUEIDENTIFIER NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- 3. TALENT_PROFILES TABLE
CREATE TABLE TalentProfiles (
    UserId UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Department NVARCHAR(255),
    AvailabilityStatus INT NOT NULL DEFAULT 0,
    Bio NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- 4. SKILLS TABLE
CREATE TABLE Skills (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    Category NVARCHAR(255) NOT NULL
);

-- 5. TALENT_SKILLS TABLE (Many-to-Many with composite key)
CREATE TABLE TalentSkills (
    UserId UNIQUEIDENTIFIER NOT NULL,
    SkillId UNIQUEIDENTIFIER NOT NULL,
    Level INT NOT NULL,
    PRIMARY KEY (UserId, SkillId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (SkillId) REFERENCES Skills(Id)
);

-- 6. PROJECTS TABLE
CREATE TABLE Projects (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Status INT NOT NULL DEFAULT 0,
    Description NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- 7. PROJECT_ASSIGNMENTS TABLE
CREATE TABLE ProjectAssignments (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ProjectId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleOnProject NVARCHAR(255) NOT NULL,
    Status INT NOT NULL DEFAULT 0,
    AssignedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CompletedAt DATETIME2 NULL,
    FOREIGN KEY (ProjectId) REFERENCES Projects(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- INDEXES untuk performa
CREATE INDEX IX_Users_RoleId ON Users(RoleId);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_TalentSkills_UserId ON TalentSkills(UserId);
CREATE INDEX IX_TalentSkills_SkillId ON TalentSkills(SkillId);
CREATE INDEX IX_ProjectAssignments_ProjectId ON ProjectAssignments(ProjectId);
CREATE INDEX IX_ProjectAssignments_UserId ON ProjectAssignments(UserId);

-- VIEW SCHEMA DENGAN SP_HELP COMMAND
-- Jalankan di SQL Server Management Studio atau SQL Server extension:
-- EXEC sp_help 'Roles';
-- EXEC sp_help 'Users';
-- EXEC sp_help 'TalentProfiles';
-- EXEC sp_help 'Skills';
-- EXEC sp_help 'TalentSkills';
-- EXEC sp_help 'Projects';
-- EXEC sp_help 'ProjectAssignments';
