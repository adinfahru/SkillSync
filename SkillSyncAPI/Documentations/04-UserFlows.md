# User Flows

Dokumen ini menjelaskan alur kerja untuk setiap role dalam SkillSync API.

---

## 1. HR Flow

### Overview
HR bertanggung jawab untuk setup master data dan maintain talent information sebelum talent bisa dicari oleh PM.

### Detailed Flow

```
┌─────────────────────────────────────────────────────────────┐
│ 1. HR Login                                                  │
│    - POST /api/auth/login                                    │
│    - Get JWT token with role "HR"                           │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 2. Setup Skill Master Data                                   │
│    a. Create Skill Categories                                │
│       - POST /api/skill-categories                           │
│       - Examples: Backend, Frontend, Mobile, DevOps          │
│                                                              │
│    b. Create Skills                                          │
│       - POST /api/skills                                     │
│       - Link to categories                                   │
│       - Examples: C#, React, PostgreSQL, Docker              │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 3. Manage Talents                                            │
│    a. View All Talents                                       │
│       - GET /api/talents                                     │
│                                                              │
│    b. Update Talent Information                              │
│       - PUT /api/talents/{id}                                │
│       - Update: name, email, phone, department, position     │
│                                                              │
│    c. Set Availability Status                                │
│       - PUT /api/talents/{id}/availability                   │
│       - Set: AVAILABLE / ON_LEAVE                            │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 4. Skill Mapping (Per Talent)                                │
│    a. Add Skills to Talent                                   │
│       - POST /api/talents/{id}/skills                        │
│       - Select skill from master data                        │
│                                                              │
│    b. Set Skill Level                                        │
│       - Beginner / Intermediate / Advanced / Expert          │
│       - Set years of experience                              │
│       - Set last used date                                   │
│                                                              │
│    c. Review & Adjust                                        │
│       - PUT /api/talents/{id}/skills/{skillId}               │
│       - Update levels as talents grow                        │
│       - DELETE /api/talents/{id}/skills/{skillId}            │
│       - Remove outdated skills                               │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 5. Talent Ready                                              │
│    ✅ Talent profile complete                                │
│    ✅ Skills mapped with levels                              │
│    ✅ Availability set to AVAILABLE                          │
│    ✅ Ready to be searched by PM                             │
└─────────────────────────────────────────────────────────────┘
```

### Key Actions

| Step | Action | Endpoint | Frequency |
|------|--------|----------|-----------|
| 1 | Login | `POST /api/auth/login` | Daily |
| 2a | Create Skill Category | `POST /api/skill-categories` | As needed |
| 2b | Create Skill | `POST /api/skills` | As needed |
| 3a | View Talents | `GET /api/talents` | Daily |
| 3b | Update Talent | `PUT /api/talents/{id}` | As needed |
| 3c | Update Availability | `PUT /api/talents/{id}/availability` | Weekly |
| 4a | Add Talent Skill | `POST /api/talents/{id}/skills` | Monthly |
| 4b | Update Skill Level | `PUT /api/talents/{id}/skills/{skillId}` | Quarterly |

### Sample Scenario: Onboarding New Talent

```
1. HR receives info about new hire "John Doe"
   - Admin creates user account
   - TalentProfile auto-created

2. HR updates talent information
   PUT /api/talents/{john-id}
   {
     "fullName": "John Doe",
     "email": "john@company.com",
     "department": "Engineering",
     "position": "Senior Developer"
   }

3. HR sets availability
   PUT /api/talents/{john-id}/availability
   {
     "availabilityStatus": "AVAILABLE"
   }

4. HR maps skills based on interview/CV
   POST /api/talents/{john-id}/skills
   [
     { "skillId": "csharp-id", "level": "Advanced", "years": 5 },
     { "skillId": "postgresql-id", "level": "Intermediate", "years": 3 },
     { "skillId": "docker-id", "level": "Beginner", "years": 1 }
   ]

5. John Doe is now searchable by PMs ✅
```

---

## 2. Project Manager Flow

### Overview
PM creates projects, defines requirements, searches for talents, dan assigns them to projects.

### Detailed Flow

```
┌─────────────────────────────────────────────────────────────┐
│ 1. PM Login                                                  │
│    - POST /api/auth/login                                    │
│    - Get JWT token with role "PM"                           │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 2. Create Project                                            │
│    - POST /api/projects                                      │
│    - Define: name, description, timeline                     │
│    - Status auto-set to "Planning"                           │
│    - PM auto-set as owner                                    │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 3. Set Skill Requirements                                    │
│    - POST /api/projects/{id}/skills (multiple times)         │
│    - Define skills needed                                    │
│    - Set minimum level per skill                             │
│    - Mark mandatory vs optional                              │
│                                                              │
│    Example:                                                  │
│    - C# (Advanced, Mandatory)                                │
│    - PostgreSQL (Intermediate, Mandatory)                    │
│    - React (Intermediate, Optional)                          │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 4. Search Talents                                            │
│    Option A: Manual Search                                   │
│    - POST /api/talents/search                                │
│    - Input skill filters                                     │
│    - System returns AVAILABLE talents                        │
│                                                              │
│    Option B: Auto-Match                                      │
│    - GET /api/talents/match/{projectId}                      │
│    - System auto-matches based on requirements               │
│    - Sorted by match score (highest first)                   │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 5. Review & Select Talents                                   │
│    - Review search results                                   │
│    - Check talent profiles (GET /api/talents/{id})           │
│    - View skills & levels                                    │
│    - Check match percentage                                  │
│    - Compare multiple candidates                             │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 6. Assign Talents to Project                                 │
│    - POST /api/projects/{id}/assignments                     │
│    - Select talent                                           │
│    - Set role on project (Backend/Frontend/QA/etc)           │
│    - System auto-updates availability → ON_PROJECT           │
│    - Repeat for multiple talents                             │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 7. Manage Project                                            │
│    a. Update Project Status                                  │
│       - PUT /api/projects/{id}                               │
│       - Planning → Ongoing (project started)                 │
│       - Ongoing → Completed (project done)                   │
│       - Ongoing → On-Hold (project paused)                   │
│                                                              │
│    b. Adjust Team (if needed)                                │
│       - Add more talents (POST assignments)                  │
│       - Remove talents (DELETE assignments)                  │
│       - Availability auto-updated when removed               │
│                                                              │
│    c. Update Roles                                           │
│       - PUT /api/projects/{id}/assignments/{assignmentId}    │
│       - Adjust role on project                               │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 8. Project Completion                                        │
│    - Remove all talents from project                         │
│    - All talents back to AVAILABLE                           │
│    - Update status to Completed                              │
│    - Project archived                                        │
└─────────────────────────────────────────────────────────────┘
```

### Key Actions

| Step | Action | Endpoint | Frequency |
|------|--------|----------|-----------|
| 1 | Login | `POST /api/auth/login` | Daily |
| 2 | Create Project | `POST /api/projects` | Monthly |
| 3 | Set Requirements | `POST /api/projects/{id}/skills` | Per project |
| 4a | Search Talents | `POST /api/talents/search` | Weekly |
| 4b | Auto-Match | `GET /api/talents/match/{projectId}` | Per project |
| 5 | View Talent Details | `GET /api/talents/{id}` | As needed |
| 6 | Assign Talent | `POST /api/projects/{id}/assignments` | Weekly |
| 7a | Update Project | `PUT /api/projects/{id}` | Monthly |
| 7b | Remove Talent | `DELETE /api/projects/{id}/assignments/{id}` | As needed |

### Sample Scenario: Staffing E-Commerce Project

```
1. PM creates project
   POST /api/projects
   {
     "projectName": "E-Commerce Platform",
     "description": "Build modern online shopping system",
     "startDate": "2025-12-01",
     "endDate": "2026-03-31"
   }
   Response: projectId = "proj-123"

2. PM sets skill requirements
   POST /api/projects/proj-123/skills (multiple calls)
   - C# Advanced (Mandatory)
   - PostgreSQL Intermediate (Mandatory)
   - React Intermediate (Mandatory)
   - Docker Beginner (Optional)

3. PM searches for matching talents
   GET /api/talents/match/proj-123
   
   Results (sorted by score):
   1. John Doe - 95% match (C#: Advanced, PostgreSQL: Expert, React: Intermediate)
   2. Jane Smith - 85% match (C#: Advanced, PostgreSQL: Intermediate, React: Advanced)
   3. Bob Wilson - 75% match (C#: Intermediate, PostgreSQL: Advanced, React: Intermediate)

4. PM reviews profiles and assigns
   POST /api/projects/proj-123/assignments
   { "talentId": "john-id", "roleOnProject": "Backend Lead" }
   
   POST /api/projects/proj-123/assignments
   { "talentId": "jane-id", "roleOnProject": "Full Stack Developer" }

5. Talents auto-updated to ON_PROJECT ✅

6. PM starts project
   PUT /api/projects/proj-123
   { "status": "Ongoing" }
```

---

## 3. Talent Flow

### Overview
Talent dapat melihat informasi dirinya, skills, dan project assignments secara self-service.

### Detailed Flow

```
┌─────────────────────────────────────────────────────────────┐
│ 1. Talent Login                                              │
│    - POST /api/auth/login                                    │
│    - Get JWT token with role "Talent"                       │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 2. View Own Profile                                          │
│    - GET /api/me/profile                                     │
│                                                              │
│    See:                                                      │
│    ✅ Full Name                                              │
│    ✅ Email & Phone                                          │
│    ✅ Department & Position                                  │
│    ✅ Availability Status                                    │
│    ❌ Cannot modify                                          │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 3. View Own Skills                                           │
│    - GET /api/me/skills                                      │
│                                                              │
│    See:                                                      │
│    ✅ List of skills                                         │
│    ✅ Skill categories                                       │
│    ✅ Skill levels (Beginner/Intermediate/Advanced/Expert)   │
│    ✅ Years of experience per skill                          │
│    ❌ Cannot add/remove/update                               │
└────────────────┬────────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────────────────────────┐
│ 4. View Project Assignments                                  │
│    - GET /api/me/assignments                                 │
│                                                              │
│    See Active Assignments:                                   │
│    ✅ Project name & description                             │
│    ✅ Role on project                                        │
│    ✅ Project Manager name                                   │
│    ✅ Assignment date                                        │
│    ✅ Project timeline                                       │
│    ✅ Project status                                         │
│                                                              │
│    See Assignment History:                                   │
│    ✅ Past projects                                          │
│    ✅ Release dates                                          │
│    ✅ Roles performed                                        │
└─────────────────────────────────────────────────────────────┘
```

### Key Actions

| Step | Action | Endpoint | Frequency |
|------|--------|----------|-----------|
| 1 | Login | `POST /api/auth/login` | Daily/Weekly |
| 2 | View Profile | `GET /api/me/profile` | Weekly |
| 3 | View Skills | `GET /api/me/skills` | Monthly |
| 4 | View Assignments | `GET /api/me/assignments` | Weekly |

### Sample Scenario: Talent Checking Own Info

```
1. John logs in
   POST /api/auth/login
   { "username": "john", "password": "***" }
   
   Response: JWT token with talentId claim

2. John views profile
   GET /api/me/profile
   
   Response:
   {
     "fullName": "John Doe",
     "email": "john@company.com",
     "phone": "+1234567890",
     "department": "Engineering",
     "position": "Senior Developer",
     "availabilityStatus": "ON_PROJECT"
   }

3. John views skills
   GET /api/me/skills
   
   Response:
   {
     "skills": [
       {
         "skillName": "C#",
         "categoryName": "Backend Development",
         "level": "Advanced",
         "yearsOfExperience": 5,
         "lastUsedDate": "2025-11-28"
       },
       {
         "skillName": "PostgreSQL",
         "categoryName": "Database",
         "level": "Expert",
         "yearsOfExperience": 6,
         "lastUsedDate": "2025-11-28"
       }
     ]
   }

4. John views assignments
   GET /api/me/assignments
   
   Response:
   {
     "activeAssignments": [
       {
         "projectName": "E-Commerce Platform",
         "roleOnProject": "Backend Lead",
         "projectManager": "Jane Smith",
         "assignedDate": "2025-11-15",
         "projectStatus": "Ongoing",
         "startDate": "2025-12-01",
         "endDate": "2026-03-31"
       }
     ],
     "assignmentHistory": [
       {
         "projectName": "Mobile Banking App",
         "roleOnProject": "Backend Developer",
         "assignedDate": "2025-06-01",
         "releaseDate": "2025-10-31",
         "duration": "5 months"
       }
     ]
   }
```

### Use Cases for Talent

1. **Career Development**
   - Track skill growth over time
   - Identify skill gaps
   - Plan learning path

2. **Project Visibility**
   - Know current assignments
   - See project timeline
   - Know project manager contact

3. **Profile Awareness**
   - Verify contact information is correct
   - Check availability status
   - Review department/position

4. **Assignment History**
   - Track past projects
   - Build portfolio
   - Reference for performance reviews

---

## 🔄 Cross-Role Interactions

### HR → Talent → PM Flow

```
HR sets up talent
      ↓
Talent becomes searchable
      ↓
PM searches & finds talent
      ↓
PM assigns to project
      ↓
Talent's availability auto-updated (ON_PROJECT)
      ↓
Talent can view assignment
```

### PM → System → HR Flow

```
PM removes talent from project
      ↓
System auto-updates availability (AVAILABLE)
      ↓
Talent becomes searchable again
      ↓
HR can see talent is available
      ↓
HR can manually set to ON_LEAVE if needed
```

---

**Next:** [Folder Structure](./05-FolderStructure.md)
