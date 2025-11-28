# Data Models

Dokumentasi lengkap schema database untuk semua entities dalam SkillSync API.

---

## 1. Users

**Table Name:** `users`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | GUID | PK | Primary key |
| role_id | GUID | FK, NOT NULL | Foreign key to Roles |
| username | VARCHAR(100) | NOT NULL | Login username |
| email | VARCHAR(255) | UNIQUE, NOT NULL | User email |
| password | VARCHAR(255) | NOT NULL | Password (hashed) |
| Otp | UINT | NULL | One-time password |
| Expired | DATETIME | NULL | OTP expiration time |
| IsUsed | BIT | NOT NULL | OTP usage status |
| created_at | DATETIME | NOT NULL, DEFAULT GETDATE() | Creation timestamp |
| updated_at | DATETIME | NOT NULL, DEFAULT GETDATE() | Last update timestamp |
| is_active | BIT | NOT NULL, DEFAULT 1 | User active status |

### Relationships

```
Users 1:1 TalentProfiles (optional)
Users N:1 Roles
Users 1:N ProjectAssignments
```

### Indexes
- `IX_Users_Email` (UNIQUE)

### Sample Data

```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "roleId": "role-talent-guid",
  "userName": "john.doe",
  "email": "john@company.com",
  "password": "hashedPassword123",
  "otp": null,
  "expired": null,
  "isUsed": false,
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z",
  "isActive": true
}
```

---

## 2. Roles

**Table Name:** `roles`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | GUID | PK | Primary key |
| Name | ENUM | NOT NULL | Role name (UserRole enum) |

### UserRole Enum Values

| Value | Description |
|-------|-------------|
| Admin | System administrator |
| HR | Human Resources |
| ProjectManager | Project Manager |
| Talent | Company Employee/Talent |

### Relationships

```
Roles 1:N Users
```

### Sample Data

```json
{
  "id": "admin-guid",
  "name": "Admin"
}
```

---

## 3. TalentProfiles

**Table Name:** `talent_profiles`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| user_id | GUID | PK, FK | Primary key & Foreign key to Users |
| first_name | VARCHAR(100) | NOT NULL | First name |
| last_name | VARCHAR(100) | NOT NULL | Last name |
| department | VARCHAR(100) | NULL | Department name |
| availability_status | VARCHAR(50) | NOT NULL, DEFAULT 'Available' | Availability status (TalentStatus enum) |
| bio | TEXT | NOT NULL | Biography/description |
| created_at | DATETIME | NOT NULL, DEFAULT GETDATE() | Creation timestamp |
| updated_at | DATETIME | NOT NULL, DEFAULT GETDATE() | Last update timestamp |

### TalentStatus Enum Values

| Status | Description | Can be Assigned? |
|--------|-------------|------------------|
| Available | Talent tersedia | ✅ Yes |
| Unavailable | Tidak tersedia sementara | ❌ No |
| OnLeave | Cuti/tidak tersedia | ❌ No |
| Inactive | Tidak aktif | ❌ No |

### Relationships

```
TalentProfiles 1:1 Users
TalentProfiles 1:N TalentSkills
```

### Sample Data

```json
{
  "userId": "user-guid",
  "firstName": "John",
  "lastName": "Doe",
  "department": "Engineering",
  "availabilityStatus": "Available",
  "bio": "Experienced software developer with 5+ years in .NET development",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

## 4. Skills

**Table Name:** `skills`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | GUID | PK | Primary key |
| Name | VARCHAR | NOT NULL | Skill name |
| Category | VARCHAR | NOT NULL | Skill category |

### Relationships

```
Skills 1:N TalentSkills
```

### Sample Data

```json
{
  "id": "skill-guid",
  "name": "ASP.NET Core",
  "category": "Backend Development"
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

**DevOps & Infrastructure:**
- Docker, Kubernetes
- Azure, AWS, GCP
- CI/CD, Jenkins

---

## 5. TalentSkills

**Table Name:** `talent_skills`

### Schema (Composite Primary Key)

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| UserId | GUID | PK, FK | Foreign key to TalentProfiles (part of composite PK) |
| SkillId | GUID | PK, FK | Foreign key to Skills (part of composite PK) |
| Level | ENUM | NOT NULL | Skill proficiency level (SkillLevel enum) |

### SkillLevel Enum Values

| Level | Description | Experience Level |
|-------|-------------|------------------|
| Beginner | Basic understanding | Entry level |
| Intermediate | Practical experience | Mid level |
| Advanced | Proficient & independent | Senior level |
| Expert | Master level | Expert level |

### Relationships

```
TalentSkills N:1 TalentProfiles
TalentSkills N:1 Skills
```

### Primary Key
- Composite Primary Key: (`UserId`, `SkillId`)

### Sample Data

```json
{
  "userId": "talent-user-guid",
  "skillId": "csharp-skill-guid",
  "level": "Advanced"
}
```

---

## 6. Projects

**Table Name:** `projects`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | GUID | PK | Primary key |
| Name | VARCHAR | NOT NULL | Project name |
| Status | ENUM | NOT NULL, DEFAULT 'Active' | Project status (ProjectStatus enum) |
| Description | VARCHAR | NOT NULL | Project description |
| CreatedAt | DATETIME | NOT NULL | Creation timestamp |
| UpdatedAt | DATETIME | NOT NULL | Last update timestamp |

### ProjectStatus Enum Values

| Status | Description | 
|--------|-------------|
| Active | Project aktif |
| Inactive | Project tidak aktif |
| Completed | Project selesai |
| OnHold | Project ditunda |
| Cancelled | Project dibatalkan |

### Relationships

```
Projects 1:N ProjectAssignments
```

### Sample Data

```json
{
  "id": "project-guid",
  "name": "E-Commerce Platform",
  "status": "Active",
  "description": "Build modern online shopping system",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

## 7. ProjectAssignments

**Table Name:** `project_assignments`

### Schema

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | GUID | PK | Primary key |
| ProjectId | GUID | FK, NOT NULL | Foreign key to Projects |
| UserId | GUID | FK, NOT NULL | Foreign key to Users |
| RoleOnProject | VARCHAR | NOT NULL | Role in the project |
| Status | ENUM | NOT NULL | Assignment status (ProjectAssignmentStatus enum) |
| AssignedAt | DATETIME | NOT NULL | Date and time assigned |
| CompletedAt | DATETIME | NULL | Date and time completed (NULL if active) |

### ProjectAssignmentStatus Enum Values

| Status | Description |
|--------|-------------|
| Active | Assignment aktif |
| Inactive | Assignment tidak aktif |
| Completed | Assignment selesai |
| Replaced | Assignment digantikan |
| Dropped | Assignment dibatalkan |

### Relationships

```
ProjectAssignments N:1 Projects
ProjectAssignments N:1 Users
```

### Sample Data

**Active Assignment:**
```json
{
  "id": "assignment-guid",
  "projectId": "project-guid",
  "userId": "user-guid",
  "roleOnProject": "Backend Developer",
  "status": "Active",
  "assignedAt": "2025-11-15T10:00:00Z",
  "completedAt": null
}
```

**Completed Assignment:**
```json
{
  "id": "assignment-guid-2",
  "projectId": "old-project-guid",
  "userId": "user-guid",
  "roleOnProject": "Backend Developer",
  "status": "Completed",
  "assignedAt": "2025-06-01T10:00:00Z",
  "completedAt": "2025-10-31T17:00:00Z"
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
       │ 1:N                          │ 1:N
       ↓                              ↓
┌─────────────┐              ┌──────────────────┐              ┌─────────────────┐
│  Projects   │──────1:N────→│ ProjectAssign.   │              │  TalentSkills   │
└─────────────┘              └──────────────────┘              └────────┬────────┘
                                                                        │ N:1
                                                                        ↓
                                                                ┌───────────────┐
                                                                │    Skills     │
                                                                └───────────────┘
```

---

## 📝 Business Logic Constraints

### 1. User & Role Management
- Users memiliki FK ke Roles (role_id)
- User dengan role "Talent" dapat memiliki TalentProfile (1:1 optional)
- User aktif/nonaktif diatur dengan field `is_active`
- OTP system untuk authentication (Otp, Expired, IsUsed fields)

### 2. Talent Profile Management
- TalentProfile menggunakan UserId sebagai PK (1:1 dengan Users)
- Default availability status: "Available"
- Availability status: Available | Unavailable | OnLeave | Inactive
- Name dipisah menjadi FirstName dan LastName
- Bio wajib diisi untuk talent profile

### 3. Skill Management
- Skills memiliki name dan category (tidak ada separate SkillCategories table)
- TalentSkills menggunakan composite primary key (UserId, SkillId)
- Skill level: Beginner | Intermediate | Advanced | Expert
- Satu talent tidak bisa memiliki duplicate skill (enforced by composite PK)

### 4. Project Management
- Project tidak memiliki project manager field secara langsung
- Project status: Active | Inactive | Completed | OnHold | Cancelled
- Default status: Active

### 5. Assignment Rules
- ProjectAssignments menghubungkan Users dengan Projects (bukan TalentProfiles)
- Assignment status: Active | Inactive | Completed | Replaced | Dropped
- CompletedAt = NULL → assignment masih aktif
- CompletedAt != NULL → assignment sudah selesai
- Tracking waktu dengan AssignedAt dan CompletedAt

---

## 🗂️ Sample Database Seeding

### Initial Roles
```sql
INSERT INTO roles (Id, Name) VALUES
('admin-guid', 'Admin'),
('hr-guid', 'HR'),
('pm-guid', 'ProjectManager'),
('talent-guid', 'Talent');
```

### Sample Users
```sql
INSERT INTO users (id, role_id, username, email, password, created_at, updated_at, is_active) VALUES
('user1-guid', 'admin-guid', 'admin.user', 'admin@skillsync.com', 'hashedpassword1', GETDATE(), GETDATE(), 1),
('user2-guid', 'talent-guid', 'john.doe', 'john.doe@company.com', 'hashedpassword2', GETDATE(), GETDATE(), 1);
```

### Sample Skills
```sql
INSERT INTO skills (Id, Name, Category) VALUES
('csharp-guid', 'C#', 'Backend Development'),
('aspnet-guid', 'ASP.NET Core', 'Backend Development'),
('postgresql-guid', 'PostgreSQL', 'Database'),
('react-guid', 'React', 'Frontend Development'),
('docker-guid', 'Docker', 'DevOps');
```

### Sample TalentProfiles
```sql
INSERT INTO talent_profiles (user_id, first_name, last_name, department, availability_status, bio, created_at, updated_at) VALUES
('user2-guid', 'John', 'Doe', 'Engineering', 'Available', 'Experienced .NET developer with 5+ years experience', GETDATE(), GETDATE());
```

---

## 📊 Database Performance Considerations

### Indexing Strategy
1. **Unique Indexes** untuk fields yang unique (Email)
2. **Foreign Key Indexes** untuk semua FK relationships (role_id, user_id, project_id, skill_id)
3. **Composite Primary Keys** untuk many-to-many relationships (TalentSkills)
4. **Status Indexes** untuk filtering berdasarkan status

### Key Indexes
```sql
-- Users table
CREATE UNIQUE INDEX IX_Users_Email ON users(email);
CREATE INDEX IX_Users_RoleId ON users(role_id);

-- TalentProfiles table  
CREATE INDEX IX_TalentProfiles_AvailabilityStatus ON talent_profiles(availability_status);
CREATE INDEX IX_TalentProfiles_Department ON talent_profiles(department);

-- ProjectAssignments table
CREATE INDEX IX_ProjectAssignments_ProjectId ON project_assignments(project_id);
CREATE INDEX IX_ProjectAssignments_UserId ON project_assignments(user_id);
CREATE INDEX IX_ProjectAssignments_Status ON project_assignments(status);

-- TalentSkills composite primary key
ALTER TABLE talent_skills ADD CONSTRAINT PK_TalentSkills PRIMARY KEY (UserId, SkillId);
```

### Query Optimization
- Use `Include()` untuk eager loading related entities
- Implement pagination untuk large result sets
- Use `AsNoTracking()` untuk read-only queries
- Filter berdasarkan status untuk performance yang lebih baik

### Data Retention
- Keep assignment history dengan status tracking (Active → Completed/Inactive)
- Archive completed projects (optional)
- Audit trail via CreatedAt/UpdatedAt timestamps
- Soft delete dengan IsActive flag pada Users table

---

**Next:** [Security & Business Rules](./08-SecurityAndBusinessRules.md)
