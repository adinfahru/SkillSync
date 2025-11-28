# Data Models

Dokumentasi lengkap schema database untuk semua entities dalam SkillSync API.

---

## 📊 Database Schema Overview

```
Users ──────┐
            ├──→ TalentProfiles ──┬──→ TalentSkills ──→ Skills ──→ SkillCategories
            │                     │
Roles ──────┘                     └──→ ProjectAssignments ──→ Projects ──┬──→ ProjectSkills ──→ Skills
                                                                           │
                                                                           └──→ Users (PM)
```

---

## 1. Users

**Table Name:** `Users`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| UserId | GUID | PK | Primary key |
| Username | VARCHAR(50) | UNIQUE, NOT NULL | Login username |
| Email | VARCHAR(100) | UNIQUE, NOT NULL | User email |
| PasswordHash | VARCHAR(255) | NOT NULL | Hashed password |
| RoleId | GUID | FK, NOT NULL | Foreign key to Roles |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |
| UpdatedAt | TIMESTAMP | NOT NULL | Last update timestamp |

### Relationships

```
Users 1:1 TalentProfiles (optional)
Users N:1 Roles
Users 1:N Projects (as ProjectManager)
```

### Indexes

- `IX_Users_Username` (UNIQUE)
- `IX_Users_Email` (UNIQUE)
- `IX_Users_RoleId`

### Sample Data

```json
{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "username": "john.doe",
  "email": "john@company.com",
  "passwordHash": "$2a$11$...",
  "roleId": "role-talent-guid",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

## 2. Roles

**Table Name:** `Roles`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| RoleId | GUID | PK | Primary key |
| RoleName | VARCHAR(50) | UNIQUE, NOT NULL | Role name |
| Description | VARCHAR(255) | NULL | Role description |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |

### Predefined Roles

| RoleId (Example) | RoleName | Description |
|------------------|----------|-------------|
| admin-guid | Admin | System administrator |
| hr-guid | HR | Human Resources |
| pm-guid | PM | Project Manager |
| talent-guid | Talent | Company Employee |

### Relationships

```
Roles 1:N Users
```

### Indexes

- `IX_Roles_RoleName` (UNIQUE)

### Sample Data

```json
{
  "roleId": "admin-guid",
  "roleName": "Admin",
  "description": "System administrator with full access",
  "createdAt": "2025-11-01T00:00:00Z"
}
```

---

## 3. TalentProfiles

**Table Name:** `TalentProfiles`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| TalentId | GUID | PK | Primary key |
| UserId | GUID | FK, UNIQUE, NOT NULL | Foreign key to Users |
| FullName | VARCHAR(100) | NOT NULL | Full name |
| Email | VARCHAR(100) | UNIQUE, NOT NULL | Contact email |
| Phone | VARCHAR(20) | NULL | Phone number |
| Department | VARCHAR(50) | NULL | Department name |
| Position | VARCHAR(50) | NULL | Job position |
| AvailabilityStatus | VARCHAR(20) | NOT NULL | Availability status |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |
| UpdatedAt | TIMESTAMP | NOT NULL | Last update timestamp |

### Availability Status Values

| Status | Description | Can be Assigned? |
|--------|-------------|------------------|
| AVAILABLE | Talent tersedia | ✅ Yes |
| ON_PROJECT | Sedang di project | ❌ No |
| ON_LEAVE | Cuti/tidak tersedia | ❌ No |

### Relationships

```
TalentProfiles 1:1 Users
TalentProfiles 1:N TalentSkills
TalentProfiles 1:N ProjectAssignments
```

### Indexes

- `IX_TalentProfiles_UserId` (UNIQUE)
- `IX_TalentProfiles_Email` (UNIQUE)
- `IX_TalentProfiles_Department`
- `IX_TalentProfiles_AvailabilityStatus`

### Constraints

- `CK_TalentProfiles_AvailabilityStatus` CHECK (AvailabilityStatus IN ('AVAILABLE', 'ON_PROJECT', 'ON_LEAVE'))

### Sample Data

```json
{
  "talentId": "talent-guid",
  "userId": "user-guid",
  "fullName": "John Doe",
  "email": "john@company.com",
  "phone": "+1234567890",
  "department": "Engineering",
  "position": "Senior Developer",
  "availabilityStatus": "AVAILABLE",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

## 4. SkillCategories

**Table Name:** `SkillCategories`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| CategoryId | GUID | PK | Primary key |
| CategoryName | VARCHAR(100) | UNIQUE, NOT NULL | Category name |
| Description | VARCHAR(255) | NULL | Category description |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |

### Relationships

```
SkillCategories 1:N Skills
```

### Indexes

- `IX_SkillCategories_CategoryName` (UNIQUE)

### Sample Data

```json
{
  "categoryId": "category-guid",
  "categoryName": "Backend Development",
  "description": "Server-side programming skills",
  "createdAt": "2025-11-01T10:00:00Z"
}
```

### Example Categories

- Backend Development
- Frontend Development
- Mobile Development
- DevOps & Infrastructure
- Database
- Cloud Services
- Testing & QA
- UI/UX Design

---

## 5. Skills

**Table Name:** `Skills`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| SkillId | GUID | PK | Primary key |
| CategoryId | GUID | FK, NOT NULL | Foreign key to SkillCategories |
| SkillName | VARCHAR(100) | UNIQUE, NOT NULL | Skill name |
| Description | VARCHAR(255) | NULL | Skill description |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |

### Relationships

```
Skills N:1 SkillCategories
Skills 1:N TalentSkills
Skills 1:N ProjectSkills
```

### Indexes

- `IX_Skills_SkillName` (UNIQUE)
- `IX_Skills_CategoryId`

### Sample Data

```json
{
  "skillId": "skill-guid",
  "categoryId": "backend-category-guid",
  "skillName": "ASP.NET Core",
  "description": "Modern .NET web framework",
  "createdAt": "2025-11-01T10:00:00Z"
}
```

### Example Skills by Category

**Backend Development:**
- C#, Java, Python, Node.js
- ASP.NET Core, Spring Boot, Django
- RESTful API, GraphQL

**Frontend Development:**
- JavaScript, TypeScript
- React, Angular, Vue.js
- HTML5, CSS3, Tailwind CSS

**Database:**
- PostgreSQL, MySQL, MongoDB
- SQL Server, Redis
- Entity Framework Core

---

## 6. TalentSkills

**Table Name:** `TalentSkills`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| TalentSkillId | GUID | PK | Primary key |
| TalentId | GUID | FK, NOT NULL | Foreign key to TalentProfiles |
| SkillId | GUID | FK, NOT NULL | Foreign key to Skills |
| SkillLevel | VARCHAR(20) | NOT NULL | Skill proficiency level |
| YearsOfExperience | DECIMAL(4,2) | NULL | Years of experience |
| LastUsedDate | DATE | NULL | Last time skill was used |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |
| UpdatedAt | TIMESTAMP | NOT NULL | Last update timestamp |

### Skill Level Values

| Level | Description | Years Range |
|-------|-------------|-------------|
| Beginner | Basic understanding | 0-1 years |
| Intermediate | Practical experience | 1-3 years |
| Advanced | Proficient & independent | 3-5 years |
| Expert | Master level | 5+ years |

### Relationships

```
TalentSkills N:1 TalentProfiles
TalentSkills N:1 Skills
```

### Indexes

- `IX_TalentSkills_TalentId`
- `IX_TalentSkills_SkillId`
- `UQ_TalentSkills_TalentId_SkillId` (UNIQUE)

### Constraints

- `CK_TalentSkills_SkillLevel` CHECK (SkillLevel IN ('Beginner', 'Intermediate', 'Advanced', 'Expert'))
- `UQ_TalentSkills_TalentId_SkillId` UNIQUE (TalentId, SkillId)

### Sample Data

```json
{
  "talentSkillId": "ts-guid",
  "talentId": "talent-guid",
  "skillId": "csharp-skill-guid",
  "skillLevel": "Advanced",
  "yearsOfExperience": 5.5,
  "lastUsedDate": "2025-11-28",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

## 7. Projects

**Table Name:** `Projects`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| ProjectId | GUID | PK | Primary key |
| ProjectManagerId | GUID | FK, NOT NULL | Foreign key to Users |
| ProjectName | VARCHAR(200) | NOT NULL | Project name |
| Description | TEXT | NULL | Project description |
| Status | VARCHAR(20) | NOT NULL | Project status |
| StartDate | DATE | NULL | Project start date |
| EndDate | DATE | NULL | Project end date |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |
| UpdatedAt | TIMESTAMP | NOT NULL | Last update timestamp |

### Project Status Values

| Status | Description | Can Delete? | Can Assign? |
|--------|-------------|-------------|-------------|
| Planning | Dalam perencanaan | ✅ Yes | ✅ Yes |
| Ongoing | Sedang berjalan | ❌ No | ✅ Yes |
| Completed | Selesai | ❌ No | ❌ No |
| On-Hold | Ditunda | ❌ No | ✅ Yes |

### Relationships

```
Projects N:1 Users (ProjectManager)
Projects 1:N ProjectSkills
Projects 1:N ProjectAssignments
```

### Indexes

- `IX_Projects_ProjectManagerId`
- `IX_Projects_Status`
- `IX_Projects_StartDate`

### Constraints

- `CK_Projects_Status` CHECK (Status IN ('Planning', 'Ongoing', 'Completed', 'On-Hold'))
- `CK_Projects_Dates` CHECK (EndDate IS NULL OR EndDate >= StartDate)

### Sample Data

```json
{
  "projectId": "project-guid",
  "projectManagerId": "pm-user-guid",
  "projectName": "E-Commerce Platform",
  "description": "Build modern online shopping system",
  "status": "Ongoing",
  "startDate": "2025-12-01",
  "endDate": "2026-03-31",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

## 8. ProjectSkills

**Table Name:** `ProjectSkills`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| ProjectSkillId | GUID | PK | Primary key |
| ProjectId | GUID | FK, NOT NULL | Foreign key to Projects |
| SkillId | GUID | FK, NOT NULL | Foreign key to Skills |
| MinimumLevel | VARCHAR(20) | NOT NULL | Minimum skill level required |
| IsMandatory | BOOLEAN | NOT NULL | Is skill mandatory? |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |

### Relationships

```
ProjectSkills N:1 Projects
ProjectSkills N:1 Skills
```

### Indexes

- `IX_ProjectSkills_ProjectId`
- `IX_ProjectSkills_SkillId`
- `UQ_ProjectSkills_ProjectId_SkillId` (UNIQUE)

### Constraints

- `CK_ProjectSkills_MinimumLevel` CHECK (MinimumLevel IN ('Beginner', 'Intermediate', 'Advanced', 'Expert'))
- `UQ_ProjectSkills_ProjectId_SkillId` UNIQUE (ProjectId, SkillId)

### Sample Data

```json
{
  "projectSkillId": "ps-guid",
  "projectId": "project-guid",
  "skillId": "csharp-skill-guid",
  "minimumLevel": "Advanced",
  "isMandatory": true,
  "createdAt": "2025-11-01T10:00:00Z"
}
```

---

## 9. ProjectAssignments

**Table Name:** `ProjectAssignments`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| AssignmentId | GUID | PK | Primary key |
| ProjectId | GUID | FK, NOT NULL | Foreign key to Projects |
| TalentId | GUID | FK, NOT NULL | Foreign key to TalentProfiles |
| RoleOnProject | VARCHAR(100) | NOT NULL | Role in the project |
| AssignedDate | DATE | NOT NULL | Date assigned |
| ReleaseDate | DATE | NULL | Date released (NULL if active) |
| CreatedAt | TIMESTAMP | NOT NULL | Creation timestamp |
| UpdatedAt | TIMESTAMP | NOT NULL | Last update timestamp |

### Relationships

```
ProjectAssignments N:1 Projects
ProjectAssignments N:1 TalentProfiles
```

### Indexes

- `IX_ProjectAssignments_ProjectId`
- `IX_ProjectAssignments_TalentId`
- `IX_ProjectAssignments_ReleaseDate`
- `UQ_ProjectAssignments_Active` (UNIQUE filtered: WHERE ReleaseDate IS NULL)

### Constraints

- `UQ_ProjectAssignments_ProjectId_TalentId` UNIQUE (ProjectId, TalentId) WHERE ReleaseDate IS NULL
  - Prevents duplicate active assignments

### Sample Data

**Active Assignment:**
```json
{
  "assignmentId": "assignment-guid",
  "projectId": "project-guid",
  "talentId": "talent-guid",
  "roleOnProject": "Backend Developer",
  "assignedDate": "2025-11-15",
  "releaseDate": null,
  "createdAt": "2025-11-15T10:00:00Z",
  "updatedAt": "2025-11-15T10:00:00Z"
}
```

**Historical Assignment:**
```json
{
  "assignmentId": "assignment-guid-2",
  "projectId": "old-project-guid",
  "talentId": "talent-guid",
  "roleOnProject": "Backend Developer",
  "assignedDate": "2025-06-01",
  "releaseDate": "2025-10-31",
  "createdAt": "2025-06-01T10:00:00Z",
  "updatedAt": "2025-10-31T10:00:00Z"
}
```

### Role on Project Examples

- Backend Developer
- Frontend Developer
- Full Stack Developer
- Senior Backend Developer
- QA Engineer
- DevOps Engineer
- UI/UX Designer
- Tech Lead
- Scrum Master
- Product Owner

---

## 🔗 Entity Relationships Diagram (ERD)

```
┌─────────────┐
│    Roles    │
└──────┬──────┘
       │ 1:N
       ↓
┌─────────────┐              ┌──────────────────┐
│    Users    │──────1:1────→│ TalentProfiles   │
└──────┬──────┘              └────────┬─────────┘
       │                              │
       │ 1:N (PM)                     │ 1:N
       ↓                              ↓
┌─────────────┐              ┌──────────────────┐              ┌─────────────────┐
│  Projects   │──────1:N────→│ ProjectAssign.   │←────N:1─────│  TalentSkills   │
└──────┬──────┘              └──────────────────┘              └────────┬────────┘
       │                                                                 │
       │ 1:N                                                            │ N:1
       ↓                                                                 ↓
┌──────────────┐                                                ┌───────────────┐
│ ProjectSkills│←──────────────────N:1──────────────────────────│    Skills     │
└──────────────┘                                                └───────┬───────┘
                                                                        │ N:1
                                                                        ↓
                                                                ┌───────────────────┐
                                                                │ SkillCategories   │
                                                                └───────────────────┘
```

---

## 📝 Business Logic Constraints

### 1. User & Talent Profile
- User dengan role "Talent" harus memiliki TalentProfile
- One user = one talent profile (jika role = Talent)
- Email di Users dan TalentProfiles harus sync

### 2. Availability Management
- Default: AVAILABLE
- Auto ON_PROJECT saat assigned
- Auto AVAILABLE saat removed
- HR dapat manual set AVAILABLE ↔ ON_LEAVE
- HR **tidak bisa** manual set ON_PROJECT

### 3. Skill Mapping
- Talent tidak bisa memiliki duplicate skill
- Skill level harus: Beginner | Intermediate | Advanced | Expert
- Cannot delete skill if assigned to talents

### 4. Project Management
- PM hanya bisa manage project miliknya
- Cannot delete project with active assignments
- Status transition rules enforced

### 5. Assignment Rules
- Talent harus AVAILABLE untuk di-assign
- No duplicate active assignment (same project + talent)
- ReleaseDate = NULL → active assignment
- ReleaseDate != NULL → historical assignment

---

## 🗂️ Sample Database Seeding

### Initial Roles
```sql
INSERT INTO Roles (RoleId, RoleName, Description) VALUES
('admin-guid', 'Admin', 'System administrator'),
('hr-guid', 'HR', 'Human Resources'),
('pm-guid', 'PM', 'Project Manager'),
('talent-guid', 'Talent', 'Company Employee');
```

### Sample Skill Categories
```sql
INSERT INTO SkillCategories (CategoryId, CategoryName, Description) VALUES
('backend-guid', 'Backend Development', 'Server-side programming'),
('frontend-guid', 'Frontend Development', 'Client-side programming'),
('database-guid', 'Database', 'Database management'),
('devops-guid', 'DevOps', 'DevOps & Infrastructure');
```

### Sample Skills
```sql
INSERT INTO Skills (SkillId, CategoryId, SkillName, Description) VALUES
('csharp-guid', 'backend-guid', 'C#', '.NET programming language'),
('aspnet-guid', 'backend-guid', 'ASP.NET Core', 'Modern .NET web framework'),
('postgresql-guid', 'database-guid', 'PostgreSQL', 'Relational database'),
('react-guid', 'frontend-guid', 'React', 'JavaScript UI library'),
('docker-guid', 'devops-guid', 'Docker', 'Containerization platform');
```

---

## 📊 Database Performance Considerations

### Indexing Strategy
1. **Unique Indexes** untuk fields yang unique (Username, Email, etc.)
2. **Foreign Key Indexes** untuk semua FK relationships
3. **Composite Indexes** untuk query patterns umum
4. **Filtered Indexes** untuk active assignments

### Query Optimization
- Use `Include()` untuk eager loading related entities
- Implement pagination untuk large result sets
- Use `AsNoTracking()` untuk read-only queries
- Index pada `AvailabilityStatus`, `Status`, `RoleId`

### Data Retention
- Keep assignment history (soft delete with ReleaseDate)
- Archive completed projects (optional)
- Audit trail via CreatedAt/UpdatedAt timestamps

---

**Next:** [Security & Business Rules](./08-SecurityAndBusinessRules.md)
