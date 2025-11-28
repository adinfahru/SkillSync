# Role-Based Access Control (RBAC)

Sistem mengimplementasikan RBAC dengan **4 level pengguna** untuk menjamin keamanan data dan integritas sistem.

---

## 🔐 Role Hierarchy

```
Admin (Full Access)
  ↓
HR (Talent & Skills Management)
  ↓
Project Manager (Project & Assignment Management)
  ↓
Talent (Read-Only Own Data)
```

---

## 1️⃣ Admin

### Tanggung Jawab
- Mengelola user dan role
- Mengatur struktur role-based access control (RBAC)
- Full access ke semua fitur sistem
- System configuration dan maintenance

### Permissions

| Feature | Permissions |
|---------|------------|
| Users | ✅ Create, Read, Update, Delete |
| Roles | ✅ Create, Read, Update, Delete |
| Talents | ✅ Full Access (Read Only) |
| Skills | ✅ Full Access (Read Only) |
| Projects | ✅ Full Access (Read Only) |
| System Config | ✅ Full Access |

### Key Capabilities
- ✅ Create new users dengan role assignment
- ✅ Modify user roles
- ✅ Deactivate/activate users
- ✅ View all system data (monitoring purpose)
- ✅ Manage role permissions

### Restrictions
- ❌ Tidak boleh modify business data (talents, skills, projects)
- ❌ Focus hanya pada user & access management

---

## 2️⃣ HR (Human Resources)

### Tanggung Jawab
- Mengelola talent internal
- Mengelola availability talent
- Maintain skill categories & data skills
- Menentukan skill mapping & skill level talent

### Permissions

| Feature | Create | Read | Update | Delete |
|---------|--------|------|--------|--------|
| Talent Profiles | ❌ | ✅ | ✅ | ❌ |
| Availability Status | - | ✅ | ✅ | - |
| Skill Categories | ✅ | ✅ | ✅ | ✅ |
| Skills | ✅ | ✅ | ✅ | ✅ |
| Talent Skills Mapping | ✅ | ✅ | ✅ | ✅ |
| Projects | ❌ | ✅ | ❌ | ❌ |
| Project Assignments | ❌ | ✅ | ❌ | ❌ |

### Key Capabilities

#### Talent Management
- ✅ Update talent information (name, email, phone, department, position)
- ✅ Set availability status: `AVAILABLE` | `ON_PROJECT` | `ON_LEAVE`
- ✅ View all talents
- ✅ View talent project history

#### Skill Master Data
- ✅ Create skill categories (e.g., Backend, Frontend, Mobile, DevOps)
- ✅ Update category information
- ✅ Delete categories (if not used)
- ✅ Create skills with category assignment
- ✅ Update skill information
- ✅ Delete skills (if not assigned to talents)

#### Talent Skill Mapping
- ✅ Add skill ke talent profile
- ✅ Remove skill dari talent profile
- ✅ Set skill level: `Beginner` | `Intermediate` | `Advanced` | `Expert`
- ✅ Update years of experience
- ✅ Update last used date

### Restrictions
- ❌ Cannot create/delete talent profiles (managed by Admin)
- ❌ Cannot create/manage projects
- ❌ Cannot assign talents to projects
- ❌ Cannot modify project data

### Availability Status Options

| Status | Description | Can be Assigned? |
|--------|-------------|------------------|
| `AVAILABLE` | Talent tersedia untuk project baru | ✅ Yes |
| `ON_PROJECT` | Sedang mengerjakan project | ❌ No |
| `ON_LEAVE` | Cuti/tidak tersedia | ❌ No |

**Note:** Status `ON_PROJECT` akan otomatis di-set saat talent di-assign ke project oleh PM.

---

## 3️⃣ Project Manager (PM)

### Tanggung Jawab
- Membuat & mengelola proyek
- Mencari talent berdasarkan skill relevan
- Meng-assign talent ke project
- Monitor project progress

### Permissions

| Feature | Create | Read | Update | Delete |
|---------|--------|------|--------|--------|
| Projects | ✅ | ✅ | ✅ (own) | ✅ (own) |
| Project Skills | ✅ | ✅ | ✅ (own) | ✅ (own) |
| Project Assignments | ✅ | ✅ | ✅ (own) | ✅ (own) |
| Talents | ❌ | ✅ | ❌ | ❌ |
| Skills | ❌ | ✅ | ❌ | ❌ |
| Talent Skills | ❌ | ✅ | ❌ | ❌ |

### Key Capabilities

#### Project Management
- ✅ Create new project
- ✅ Update project name & description
- ✅ Update project status
- ✅ Delete project (own only, no active assignments)
- ✅ View all projects
- ✅ Filter projects by status

#### Project Skill Requirements
- ✅ Add skill requirements to project
- ✅ Set minimum skill level per skill
- ✅ Mark skills as mandatory/optional
- ✅ Remove skill requirements
- ✅ Update minimum level

#### Talent Search & Matching
- ✅ Search talents by skills
- ✅ Filter by skill level
- ✅ Filter by availability (AVAILABLE only)
- ✅ View talent profiles (read-only)
- ✅ View talent skills & levels
- ✅ View match percentage

#### Talent Assignment
- ✅ Assign AVAILABLE talent to project
- ✅ Set role on project (Backend/Frontend/QA/DevOps/etc)
- ✅ Update talent role on project
- ✅ Remove talent from project
- ✅ View assignment history

### Restrictions
- ❌ Cannot modify talent data
- ❌ Cannot create/modify skills or categories
- ❌ Cannot manually set talent availability
- ❌ Cannot manage projects owned by other PMs
- ❌ Cannot assign talent yang sedang ON_PROJECT atau ON_LEAVE

### Project Status Options

| Status | Description | Can Delete? | Can Assign Talent? |
|--------|-------------|-------------|-------------------|
| `Planning` | Project dalam tahap perencanaan | ✅ Yes | ✅ Yes |
| `Ongoing` | Project sedang berjalan | ❌ No | ✅ Yes |
| `Completed` | Project selesai | ❌ No | ❌ No |
| `On-Hold` | Project ditunda | ❌ No | ✅ Yes |

### Ownership Rules
- PM **hanya dapat** update/delete **project miliknya sendiri**
- PM **dapat melihat** semua projects (read-only untuk project orang lain)
- Ownership di-track via `ProjectManagerId` field

---

## 4️⃣ Talent

### Tanggung Jawab
- Melihat profil dan skill set dirinya
- Melihat assignment project aktif
- Self-service information access

### Permissions

| Feature | Create | Read | Update | Delete |
|---------|--------|------|--------|--------|
| Own Profile | ❌ | ✅ | ❌ | ❌ |
| Own Skills | ❌ | ✅ | ❌ | ❌ |
| Own Assignments | ❌ | ✅ | ❌ | ❌ |
| Other Talents | ❌ | ❌ | ❌ | ❌ |
| Projects | ❌ | ✅ (assigned only) | ❌ | ❌ |

### Key Capabilities

#### Profile Access
- ✅ View own profile information
- ✅ View contact details
- ✅ View department & position
- ✅ View availability status

#### Skills Access
- ✅ View list of own skills
- ✅ View skill levels
- ✅ View years of experience per skill
- ✅ View skill categories

#### Assignment Access
- ✅ View active project assignments
- ✅ View role on project
- ✅ View project details (name, description, PM)
- ✅ View project timeline
- ✅ View assignment history

### Restrictions
- ❌ Cannot modify any data
- ❌ Cannot view other talents
- ❌ Cannot view projects they're not assigned to
- ❌ Cannot update own skills or availability
- ❌ Purely read-only access

---

## 🔒 Authorization Implementation

### Endpoint Protection
```csharp
// Example authorization attributes
[Authorize(Roles = "Admin")]              // Admin only
[Authorize(Roles = "HR")]                 // HR only
[Authorize(Roles = "PM")]                 // PM only
[Authorize(Roles = "Talent")]             // Talent only
[Authorize(Roles = "HR,PM")]              // HR or PM
[Authorize(Policy = "ProjectOwner")]      // PM + own project
[Authorize(Policy = "SelfOrAdmin")]       // Own data or Admin
```

### Custom Policies
1. **ProjectOwner Policy**
   - PM can only manage their own projects
   - Check `ProjectManagerId == CurrentUserId`

2. **SelfOrAdmin Policy**
   - Talent can view own data
   - Admin can view any data

3. **AvailableTalentOnly Policy**
   - Validate talent availability before assignment
   - Check `AvailabilityStatus == AVAILABLE`

---

## 🔐 Security Best Practices

### JWT Claims
```json
{
  "sub": "userId",
  "name": "username",
  "email": "user@email.com",
  "role": "PM",
  "talentId": "talent-guid",  // if applicable
  "exp": "expiration-timestamp"
}
```

### Authorization Flow
1. User login → Generate JWT with role claims
2. Request to endpoint → Validate JWT
3. Check role authorization → Allow/Deny
4. For owner-based resources → Validate ownership
5. Return response or 403 Forbidden

---

## 📊 Permission Matrix Summary

| Feature | Admin | HR | PM | Talent |
|---------|-------|----|----|--------|
| User Management | ✅ Full | ❌ | ❌ | ❌ |
| Role Management | ✅ Full | ❌ | ❌ | ❌ |
| Talent Profiles | 👁️ View | ✅ Update | 👁️ View All | 👁️ Own Only |
| Availability | 👁️ View | ✅ Manage | 👁️ View | 👁️ View Own |
| Skill Categories | 👁️ View | ✅ CRUD | 👁️ View | ❌ |
| Skills | 👁️ View | ✅ CRUD | 👁️ View | 👁️ View Own |
| Talent Skills | 👁️ View | ✅ CRUD | 👁️ View | 👁️ View Own |
| Projects | 👁️ View | 👁️ View | ✅ CRUD (own) | 👁️ View Assigned |
| Project Skills | 👁️ View | ❌ | ✅ Manage (own) | ❌ |
| Assignments | 👁️ View | 👁️ View | ✅ Manage (own) | 👁️ View Own |
| Search Talents | ❌ | ❌ | ✅ | ❌ |

**Legend:**
- ✅ Full Access (CRUD)
- 👁️ Read Only
- ❌ No Access

---

**Next:** [Key Features](./03-KeyFeatures.md)
