# API Endpoints

Dokumentasi lengkap semua API endpoints dalam SkillSync API.

---

## 🔐 Authentication

### Register
**POST** `/api/auth/register`

Register user baru.

**Authorization:** None (Public)

**Request Body:**
```json
{
  "username": "john.doe",
  "email": "john@company.com",
  "password": "SecurePass123!",
  "confirmPassword": "SecurePass123!",
  "roleId": "role-guid"
}
```

**Success Response:** `201 Created`
```json
{
  "userId": "user-guid",
  "username": "john.doe",
  "email": "john@company.com",
  "role": "Talent"
}
```

---

### Login
**POST** `/api/auth/login`

Login dan dapatkan JWT token.

**Authorization:** None (Public)

**Request Body:**
```json
{
  "username": "john.doe",
  "password": "SecurePass123!"
}
```

**Success Response:** `200 OK`
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "refresh-token-string",
  "expiresIn": 3600,
  "tokenType": "Bearer",
  "user": {
    "userId": "user-guid",
    "username": "john.doe",
    "email": "john@company.com",
    "role": "Talent"
  }
}
```

**Error Response:** `401 Unauthorized`
```json
{
  "error": "Invalid username or password"
}
```

---

### Refresh Token
**POST** `/api/auth/refresh`

Refresh JWT token.

**Authorization:** None

**Request Body:**
```json
{
  "refreshToken": "refresh-token-string"
}
```

**Success Response:** `200 OK`
```json
{
  "accessToken": "new-access-token",
  "refreshToken": "new-refresh-token",
  "expiresIn": 3600,
  "tokenType": "Bearer"
}
```

---

## 👥 Users Management (Admin Only)

### Get All Users
**GET** `/api/users`

**Authorization:** Admin

**Query Parameters:**
- `page` (optional): Page number (default: 1)
- `pageSize` (optional): Items per page (default: 10)
- `search` (optional): Search by username or email

**Success Response:** `200 OK`
```json
{
  "data": [
    {
      "userId": "user-guid",
      "username": "john.doe",
      "email": "john@company.com",
      "roleId": "role-guid",
      "roleName": "Talent",
      "createdAt": "2025-11-01T10:00:00Z"
    }
  ],
  "totalRecords": 50,
  "currentPage": 1,
  "pageSize": 10
}
```

---

### Get User by ID
**GET** `/api/users/{id}`

**Authorization:** Admin

**Success Response:** `200 OK`
```json
{
  "userId": "user-guid",
  "username": "john.doe",
  "email": "john@company.com",
  "roleId": "role-guid",
  "roleName": "Talent",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

### Create User
**POST** `/api/users`

**Authorization:** Admin

**Request Body:**
```json
{
  "username": "jane.smith",
  "email": "jane@company.com",
  "password": "SecurePass123!",
  "roleId": "role-guid"
}
```

**Success Response:** `201 Created`

---

### Update User
**PUT** `/api/users/{id}`

**Authorization:** Admin

**Request Body:**
```json
{
  "email": "newemail@company.com",
  "roleId": "new-role-guid"
}
```

**Success Response:** `200 OK`

---

### Delete User
**DELETE** `/api/users/{id}`

**Authorization:** Admin

**Success Response:** `204 No Content`

---

## 🔑 Roles Management (Admin Only)

### Get All Roles
**GET** `/api/roles`

**Authorization:** Admin

**Success Response:** `200 OK`
```json
[
  {
    "roleId": "role-guid",
    "roleName": "Admin",
    "description": "System administrator"
  },
  {
    "roleId": "role-guid-2",
    "roleName": "HR",
    "description": "Human Resources"
  }
]
```

---

### Create Role
**POST** `/api/roles`

**Authorization:** Admin

**Request Body:**
```json
{
  "roleName": "Custom Role",
  "description": "Custom role description"
}
```

**Success Response:** `201 Created`

---

## 👤 Talent Management

### Get All Talents
**GET** `/api/talents`

**Authorization:** HR, PM

**Query Parameters:**
- `department` (optional): Filter by department
- `availabilityStatus` (optional): Filter by status
- `page`, `pageSize` (optional): Pagination

**Success Response:** `200 OK`
```json
{
  "data": [
    {
      "talentId": "talent-guid",
      "fullName": "John Doe",
      "email": "john@company.com",
      "phone": "+1234567890",
      "department": "Engineering",
      "position": "Senior Developer",
      "availabilityStatus": "AVAILABLE",
      "createdAt": "2025-11-01T10:00:00Z"
    }
  ],
  "totalRecords": 100,
  "currentPage": 1,
  "pageSize": 10
}
```

---

### Get Talent by ID
**GET** `/api/talents/{id}`

**Authorization:** HR, PM, Talent (own only)

**Success Response:** `200 OK`
```json
{
  "talentId": "talent-guid",
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

### Update Talent
**PUT** `/api/talents/{id}`

**Authorization:** HR

**Request Body:**
```json
{
  "fullName": "John Doe Updated",
  "email": "john.new@company.com",
  "phone": "+1234567890",
  "department": "Engineering",
  "position": "Lead Developer"
}
```

**Success Response:** `200 OK`

---

### Update Availability
**PUT** `/api/talents/{id}/availability`

**Authorization:** HR

**Request Body:**
```json
{
  "availabilityStatus": "ON_LEAVE"
}
```

**Allowed Values:** `AVAILABLE`, `ON_LEAVE`
**Note:** Cannot manually set to `ON_PROJECT`

**Success Response:** `200 OK`

---

## 📚 Skill Categories (HR Only)

### Get All Categories
**GET** `/api/skill-categories`

**Authorization:** HR, PM (read-only)

**Success Response:** `200 OK`
```json
[
  {
    "categoryId": "category-guid",
    "categoryName": "Backend Development",
    "description": "Server-side programming",
    "createdAt": "2025-11-01T10:00:00Z"
  }
]
```

---

### Get Category by ID
**GET** `/api/skill-categories/{id}`

**Authorization:** HR, PM

**Success Response:** `200 OK`

---

### Create Category
**POST** `/api/skill-categories`

**Authorization:** HR

**Request Body:**
```json
{
  "categoryName": "Backend Development",
  "description": "Server-side programming skills"
}
```

**Success Response:** `201 Created`

---

### Update Category
**PUT** `/api/skill-categories/{id}`

**Authorization:** HR

**Success Response:** `200 OK`

---

### Delete Category
**DELETE** `/api/skill-categories/{id}`

**Authorization:** HR

**Note:** Cannot delete if category has skills

**Success Response:** `204 No Content`

---

## 🛠️ Skills (HR Only)

### Get All Skills
**GET** `/api/skills`

**Authorization:** HR, PM (read-only)

**Query Parameters:**
- `categoryId` (optional): Filter by category
- `search` (optional): Search by skill name

**Success Response:** `200 OK`
```json
[
  {
    "skillId": "skill-guid",
    "skillName": "C#",
    "categoryId": "category-guid",
    "categoryName": "Backend Development",
    "description": ".NET programming language",
    "createdAt": "2025-11-01T10:00:00Z"
  }
]
```

---

### Create Skill
**POST** `/api/skills`

**Authorization:** HR

**Request Body:**
```json
{
  "skillName": "ASP.NET Core",
  "categoryId": "category-guid",
  "description": "Modern .NET web framework"
}
```

**Success Response:** `201 Created`

---

### Update Skill
**PUT** `/api/skills/{id}`

**Authorization:** HR

**Success Response:** `200 OK`

---

### Delete Skill
**DELETE** `/api/skills/{id}`

**Authorization:** HR

**Note:** Cannot delete if assigned to talents

**Success Response:** `204 No Content`

---

## 🎯 Talent Skills (HR Only)

### Get Talent Skills
**GET** `/api/talents/{id}/skills`

**Authorization:** HR, PM, Talent (own only)

**Success Response:** `200 OK`
```json
[
  {
    "talentSkillId": "ts-guid",
    "talentId": "talent-guid",
    "skillId": "skill-guid",
    "skillName": "C#",
    "categoryName": "Backend Development",
    "skillLevel": "Advanced",
    "yearsOfExperience": 5,
    "lastUsedDate": "2025-11-28",
    "createdAt": "2025-11-01T10:00:00Z"
  }
]
```

---

### Add Skill to Talent
**POST** `/api/talents/{id}/skills`

**Authorization:** HR

**Request Body:**
```json
{
  "skillId": "skill-guid",
  "skillLevel": "Intermediate",
  "yearsOfExperience": 3,
  "lastUsedDate": "2025-11-28"
}
```

**Skill Levels:** `Beginner`, `Intermediate`, `Advanced`, `Expert`

**Success Response:** `201 Created`

---

### Update Skill Level
**PUT** `/api/talents/{talentId}/skills/{skillId}`

**Authorization:** HR

**Request Body:**
```json
{
  "skillLevel": "Advanced",
  "yearsOfExperience": 5,
  "lastUsedDate": "2025-11-28"
}
```

**Success Response:** `200 OK`

---

### Remove Skill from Talent
**DELETE** `/api/talents/{talentId}/skills/{skillId}`

**Authorization:** HR

**Success Response:** `204 No Content`

---

## 📁 Projects (PM Only)

### Get All Projects
**GET** `/api/projects`

**Authorization:** PM

**Query Parameters:**
- `status` (optional): Filter by status
- `projectManagerId` (optional): Filter by PM

**Success Response:** `200 OK`
```json
[
  {
    "projectId": "project-guid",
    "projectName": "E-Commerce Platform",
    "description": "Build modern shopping system",
    "status": "Ongoing",
    "projectManagerId": "pm-guid",
    "projectManagerName": "Jane Smith",
    "startDate": "2025-12-01",
    "endDate": "2026-03-31",
    "createdAt": "2025-11-01T10:00:00Z"
  }
]
```

---

### Get Project by ID
**GET** `/api/projects/{id}`

**Authorization:** PM, Talent (if assigned)

**Success Response:** `200 OK`

---

### Create Project
**POST** `/api/projects`

**Authorization:** PM

**Request Body:**
```json
{
  "projectName": "E-Commerce Platform",
  "description": "Build modern online shopping system",
  "status": "Planning",
  "startDate": "2025-12-01",
  "endDate": "2026-03-31"
}
```

**Status Options:** `Planning`, `Ongoing`, `Completed`, `On-Hold`

**Success Response:** `201 Created`

---

### Update Project
**PUT** `/api/projects/{id}`

**Authorization:** PM (owner only)

**Request Body:**
```json
{
  "projectName": "E-Commerce Platform v2",
  "description": "Updated description",
  "status": "Ongoing",
  "startDate": "2025-12-01",
  "endDate": "2026-06-30"
}
```

**Success Response:** `200 OK`

**Error Response:** `403 Forbidden` if not owner

---

### Delete Project
**DELETE** `/api/projects/{id}`

**Authorization:** PM (owner only)

**Note:** Cannot delete if has active assignments

**Success Response:** `204 No Content`

---

## 🎯 Project Skills (PM Only)

### Get Project Skills
**GET** `/api/projects/{id}/skills`

**Authorization:** PM

**Success Response:** `200 OK`
```json
[
  {
    "projectSkillId": "ps-guid",
    "projectId": "project-guid",
    "skillId": "skill-guid",
    "skillName": "C#",
    "categoryName": "Backend Development",
    "minimumLevel": "Advanced",
    "isMandatory": true,
    "createdAt": "2025-11-01T10:00:00Z"
  }
]
```

---

### Add Skill Requirement
**POST** `/api/projects/{id}/skills`

**Authorization:** PM (owner only)

**Request Body:**
```json
{
  "skillId": "skill-guid",
  "minimumLevel": "Intermediate",
  "isMandatory": true
}
```

**Minimum Levels:** `Beginner`, `Intermediate`, `Advanced`, `Expert`

**Success Response:** `201 Created`

---

### Update Skill Requirement
**PUT** `/api/projects/{projectId}/skills/{skillId}`

**Authorization:** PM (owner only)

**Request Body:**
```json
{
  "minimumLevel": "Advanced",
  "isMandatory": false
}
```

**Success Response:** `200 OK`

---

### Remove Skill Requirement
**DELETE** `/api/projects/{projectId}/skills/{skillId}`

**Authorization:** PM (owner only)

**Success Response:** `204 No Content`

---

## 👥 Project Assignments (PM Only)

### Get Project Assignments
**GET** `/api/projects/{id}/assignments`

**Authorization:** PM (owner), Talent (if assigned)

**Success Response:** `200 OK`
```json
[
  {
    "assignmentId": "assignment-guid",
    "projectId": "project-guid",
    "talentId": "talent-guid",
    "talentName": "John Doe",
    "roleOnProject": "Backend Developer",
    "assignedDate": "2025-11-15",
    "releaseDate": null,
    "createdAt": "2025-11-15T10:00:00Z"
  }
]
```

---

### Assign Talent
**POST** `/api/projects/{id}/assignments`

**Authorization:** PM (owner only)

**Request Body:**
```json
{
  "talentId": "talent-guid",
  "roleOnProject": "Backend Developer"
}
```

**Validations:**
- Talent must be AVAILABLE
- No duplicate assignment
- Project owned by current PM

**Success Response:** `201 Created`

**Auto Actions:**
- Talent availability → ON_PROJECT

---

### Update Assignment
**PUT** `/api/projects/{projectId}/assignments/{assignmentId}`

**Authorization:** PM (owner only)

**Request Body:**
```json
{
  "roleOnProject": "Senior Backend Developer"
}
```

**Success Response:** `200 OK`

---

### Remove Talent from Project
**DELETE** `/api/projects/{projectId}/assignments/{assignmentId}`

**Authorization:** PM (owner only)

**Success Response:** `204 No Content`

**Auto Actions:**
- Talent availability → AVAILABLE
- Set ReleaseDate

---

## 🔍 Talent Search & Matching (PM Only)

### Search Talents
**POST** `/api/talents/search`

**Authorization:** PM

**Request Body:**
```json
{
  "skills": ["skill-id-1", "skill-id-2"],
  "minimumLevels": {
    "skill-id-1": "Intermediate",
    "skill-id-2": "Advanced"
  },
  "availabilityStatus": "AVAILABLE",
  "department": "Engineering"
}
```

**Success Response:** `200 OK`
```json
{
  "results": [
    {
      "talentId": "talent-guid",
      "fullName": "John Doe",
      "department": "Engineering",
      "position": "Senior Developer",
      "availabilityStatus": "AVAILABLE",
      "matchScore": 90,
      "matchedSkills": [
        {
          "skillName": "C#",
          "talentLevel": "Advanced",
          "requiredLevel": "Intermediate",
          "yearsOfExperience": 5
        }
      ],
      "missingSkills": ["Docker"]
    }
  ],
  "totalResults": 5
}
```

---

### Match Talents for Project
**GET** `/api/talents/match/{projectId}`

**Authorization:** PM

**Description:** Auto-match talents based on project skill requirements

**Success Response:** `200 OK`
```json
{
  "projectId": "project-guid",
  "projectName": "E-Commerce Platform",
  "requiredSkills": [
    {
      "skillName": "C#",
      "minimumLevel": "Advanced",
      "isMandatory": true
    }
  ],
  "matchedTalents": [
    {
      "talentId": "talent-guid",
      "fullName": "John Doe",
      "matchScore": 95,
      "matchedSkillsCount": 3,
      "totalRequiredSkills": 3,
      "matchPercentage": "100%"
    }
  ]
}
```

---

## 🙋 Talent Self-Service (Talent Only)

### Get Own Profile
**GET** `/api/me/profile`

**Authorization:** Talent

**Success Response:** `200 OK`
```json
{
  "talentId": "talent-guid",
  "fullName": "John Doe",
  "email": "john@company.com",
  "phone": "+1234567890",
  "department": "Engineering",
  "position": "Senior Developer",
  "availabilityStatus": "ON_PROJECT"
}
```

---

### Get Own Skills
**GET** `/api/me/skills`

**Authorization:** Talent

**Success Response:** `200 OK`
```json
[
  {
    "skillName": "C#",
    "categoryName": "Backend Development",
    "level": "Advanced",
    "yearsOfExperience": 5,
    "lastUsedDate": "2025-11-28"
  }
]
```

---

### Get Own Assignments
**GET** `/api/me/assignments`

**Authorization:** Talent

**Success Response:** `200 OK`
```json
{
  "activeAssignments": [
    {
      "projectName": "E-Commerce Platform",
      "roleOnProject": "Backend Developer",
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

---

## 📊 HTTP Status Codes

| Code | Meaning | Usage |
|------|---------|-------|
| 200 | OK | Successful GET, PUT |
| 201 | Created | Successful POST |
| 204 | No Content | Successful DELETE |
| 400 | Bad Request | Validation errors |
| 401 | Unauthorized | Missing/invalid token |
| 403 | Forbidden | Insufficient permissions |
| 404 | Not Found | Resource not found |
| 409 | Conflict | Duplicate resource |
| 500 | Server Error | Internal server error |

---

## 🔒 Authorization Summary

| Endpoint Group | Admin | HR | PM | Talent |
|----------------|-------|----|----|--------|
| Auth | ✅ | ✅ | ✅ | ✅ |
| Users | ✅ | ❌ | ❌ | ❌ |
| Roles | ✅ | ❌ | ❌ | ❌ |
| Talents | 👁️ | ✅ | 👁️ | 👁️ Own |
| Skill Categories | 👁️ | ✅ | 👁️ | ❌ |
| Skills | 👁️ | ✅ | 👁️ | 👁️ Own |
| Talent Skills | 👁️ | ✅ | 👁️ | 👁️ Own |
| Projects | 👁️ | 👁️ | ✅ | 👁️ Assigned |
| Project Skills | ❌ | ❌ | ✅ | ❌ |
| Assignments | 👁️ | 👁️ | ✅ | 👁️ Own |
| Search | ❌ | ❌ | ✅ | ❌ |
| Self-Service | ❌ | ❌ | ❌ | ✅ |

**Legend:**
- ✅ Full Access (CRUD)
- 👁️ Read Only
- ❌ No Access

---

**Next:** [Data Models](./07-DataModels.md)
