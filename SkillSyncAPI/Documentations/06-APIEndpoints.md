# API Endpoints

Dokumentasi lengkap semua API endpoints dalam SkillSync API.

---

## 📋 **Complete Endpoints List**

### **🔐 Authentication**
- `POST /api/auth/register` - Register user baru
- `POST /api/auth/login` - Login dan dapatkan JWT token
- `POST /api/auth/refresh` - Refresh JWT token

### **👥 Users Management (Admin Only)** 
- `GET /api/users` - Get all users (with search)
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create new user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### **👤 Talent Management**
- `GET /api/talents` - Get all talents (HR, PM)
- `GET /api/talents/{id}` - Get talent by ID
- `PUT /api/talents/{id}` - Update talent (HR)
- `PUT /api/talents/{id}/availability` - Update availability status (HR)

### **🛠️ Skills Management (HR Only)**
- `GET /api/skills` - Get all skills 
- `GET /api/skills/{id}` - Get skill by ID
- `POST /api/skills` - Create new skill
- `PUT /api/skills/{id}` - Update skill
- `DELETE /api/skills/{id}` - Delete skill

### **🎯 Talent Skills Management**
- `GET /api/talents/{id}/skills` - Get talent's skills
- `POST /api/talents/{id}/skills` - Add skill to talent (HR)
- `PUT /api/talents/{talentId}/skills/{skillId}` - Update skill level (HR)
- `DELETE /api/talents/{talentId}/skills/{skillId}` - Remove skill from talent (HR)

### **📁 Projects Management (PM Only)**
- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project
- `PUT /api/projects/{id}` - Update project (owner only)
- `DELETE /api/projects/{id}` - Delete project (owner only)

### **👥 Project Assignments (PM Only)**
- `GET /api/projects/{id}/assignments` - Get project assignments
- `POST /api/projects/{id}/assignments` - Assign talent to project
- `PUT /api/projects/{projectId}/assignments/{assignmentId}` - Update assignment
- `DELETE /api/projects/{projectId}/assignments/{assignmentId}` - Remove talent from project

### **🔍 Talent Search & Matching (PM Only)**
- `POST /api/talents/search` - Search talents by criteria
- `GET /api/talents/match/{projectId}` - Auto-match talents for project


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
      "isActive": true,
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
  "isActive": true,
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
  "roleId": "new-role-guid",
  "isActive": true
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
    "name": "Admin"
  },
  {
    "roleId": "role-guid-2",
    "name": "HR"
  },
  {
    "roleId": "role-guid-3",
    "name": "ProjectManager"
  },
  {
    "roleId": "role-guid-4",
    "name": "Talent"
  }
]
```

---

### Get Role by ID
**GET** `/api/roles/{id}`

**Authorization:** Admin

**Success Response:** `200 OK`
```json
{
  "roleId": "role-guid",
  "name": "Admin"
}
```

---

### Create Role
**POST** `/api/roles`

**Authorization:** Admin

**Request Body:**
```json
{
  "name": "CustomRole"
}
```

**Success Response:** `201 Created`

---

### Update Role
**PUT** `/api/roles/{id}`

**Authorization:** Admin

**Request Body:**
```json
{
  "name": "UpdatedRoleName"
}
```

**Success Response:** `200 OK`

---

### Delete Role
**DELETE** `/api/roles/{id}`

**Authorization:** Admin

**Note:** Cannot delete if role has users assigned

**Success Response:** `204 No Content`

---

## 👤 Talent Management

### Get All Talents
**GET** `/api/talents`

**Authorization:** HR, PM

**Query Parameters:**
- `department` (optional): Filter by department
- `availabilityStatus` (optional): Filter by status (Available, Unavailable, OnLeave, Inactive)
- `page`, `pageSize` (optional): Pagination

**Success Response:** `200 OK`
```json
{
  "data": [
    {
      "userId": "talent-guid",
      "firstName": "John",
      "lastName": "Doe",
      "fullName": "John Doe",
      "department": "Engineering",
      "availabilityStatus": "Available",
      "bio": "Experienced backend developer",
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
  "userId": "talent-guid",
  "firstName": "John",
  "lastName": "Doe",
  "fullName": "John Doe",
  "department": "Engineering",
  "availabilityStatus": "Available",
  "bio": "Experienced backend developer with 5+ years in .NET",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

### Create Talent Profile
**POST** `/api/talents`

**Authorization:** HR

**Request Body:**
```json
{
  "userId": "user-guid",
  "firstName": "John",
  "lastName": "Doe",
  "department": "Engineering",
  "bio": "Experienced backend developer"
}
```

**Success Response:** `201 Created`

---

### Update Talent
**PUT** `/api/talents/{id}`

**Authorization:** HR

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe Updated",
  "department": "Engineering",
  "bio": "Updated bio description"
}
```

**Success Response:** `200 OK`

---

### Delete Talent Profile
**DELETE** `/api/talents/{id}`

**Authorization:** HR

**Note:** Cannot delete if talent has active assignments

**Success Response:** `204 No Content`

---

### Update Availability
**PUT** `/api/talents/{id}/availability`

**Authorization:** HR

**Request Body:**
```json
{
  "availabilityStatus": "OnLeave"
}
```

**Allowed Values:** `Available`, `Unavailable`, `OnLeave`, `Inactive`

**Success Response:** `200 OK`

---

## 🛠️ Skills Management (HR Only)

### Get All Skills
**GET** `/api/skills`

**Authorization:** HR, PM (read-only), Talent (read-only)

**Query Parameters:**
- `category` (optional): Filter by category
- `search` (optional): Search by skill name

**Success Response:** `200 OK`
```json
[
  {
    "id": "skill-guid",
    "name": "C#",
    "category": "Backend Development"
  },
  {
    "id": "skill-guid-2",
    "name": "React",
    "category": "Frontend Development"
  }
]
```

---

### Get Skill by ID
**GET** `/api/skills/{id}`

**Authorization:** HR, PM, Talent

**Success Response:** `200 OK`
```json
{
  "id": "skill-guid",
  "name": "ASP.NET Core",
  "category": "Backend Development"
}
```

---

### Create Skill
**POST** `/api/skills`

**Authorization:** HR

**Request Body:**
```json
{
  "name": "Docker",
  "category": "DevOps"
}
```

**Success Response:** `201 Created`

---

### Update Skill
**PUT** `/api/skills/{id}`

**Authorization:** HR

**Request Body:**
```json
{
  "name": "Docker Updated",
  "category": "DevOps & Infrastructure"
}
```

**Success Response:** `200 OK`

---

### Delete Skill
**DELETE** `/api/skills/{id}`

**Authorization:** HR

**Note:** Cannot delete if assigned to talents

**Success Response:** `204 No Content`

---

## 🎯 Talent Skills Management

### Get Talent Skills
**GET** `/api/talents/{id}/skills`

**Authorization:** HR, PM, Talent (own only)

**Success Response:** `200 OK`
```json
[
  {
    "userId": "talent-guid",
    "skillId": "skill-guid",
    "skillName": "C#",
    "category": "Backend Development",
    "level": "Advanced"
  },
  {
    "userId": "talent-guid",
    "skillId": "skill-guid-2",
    "skillName": "React",
    "category": "Frontend Development",
    "level": "Intermediate"
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
  "level": "Intermediate"
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
  "level": "Advanced"
}
```

**Success Response:** `200 OK`

---

### Remove Skill from Talent
**DELETE** `/api/talents/{talentId}/skills/{skillId}`

**Authorization:** HR

**Success Response:** `204 No Content`

---

## 📁 Projects Management (PM Only)

### Get All Projects
**GET** `/api/projects`

**Authorization:** PM

**Query Parameters:**
- `status` (optional): Filter by status (Planning, Active, Completed, OnHold)

**Success Response:** `200 OK`
```json
[
  {
    "id": "project-guid",
    "name": "E-Commerce Platform",
    "description": "Build modern shopping system",
    "status": "Active",
    "createdAt": "2025-11-01T10:00:00Z"
  }
]
```

---

### Get Project by ID
**GET** `/api/projects/{id}`

**Authorization:** PM, Talent (if assigned)

**Success Response:** `200 OK`
```json
{
  "id": "project-guid",
  "name": "E-Commerce Platform",
  "description": "Build modern online shopping system with microservices architecture",
  "status": "Active",
  "createdAt": "2025-11-01T10:00:00Z",
  "updatedAt": "2025-11-28T10:00:00Z"
}
```

---

### Create Project
**POST** `/api/projects`

**Authorization:** PM

**Request Body:**
```json
{
  "name": "Mobile Banking App",
  "description": "Secure mobile banking application",
  "status": "Planning"
}
```

**Status Options:** `Planning`, `Active`, `Completed`, `OnHold`

**Success Response:** `201 Created`

---

### Update Project
**PUT** `/api/projects/{id}`

**Authorization:** PM

**Request Body:**
```json
{
  "name": "Mobile Banking App v2",
  "description": "Updated description with new features",
  "status": "Active"
}
```

**Success Response:** `200 OK`

---

### Delete Project
**DELETE** `/api/projects/{id}`

**Authorization:** PM

**Note:** Cannot delete if has active assignments

**Success Response:** `204 No Content`

---

## 👥 Project Assignments (PM Only)

### Get Project Assignments
**GET** `/api/projects/{id}/assignments`

**Authorization:** PM, Talent (if assigned)

**Success Response:** `200 OK`
```json
[
  {
    "id": "assignment-guid",
    "projectId": "project-guid",
    "userId": "talent-guid",
    "talentName": "John Doe",
    "status": "Active",
    "assignedAt": "2025-11-15T10:00:00Z",
    "completedAt": null
  }
]
```

---

### Assign Talent to Project
**POST** `/api/projects/{id}/assignments`

**Authorization:** PM

**Request Body:**
```json
{
  "userId": "talent-guid"
}
```

**Validations:**
- Talent must be Available
- No duplicate assignment to same project
- Talent must have TalentProfile

**Success Response:** `201 Created`

**Auto Actions:**
- Assignment status → Active

---

### Update Assignment Status
**PUT** `/api/projects/{projectId}/assignments/{assignmentId}`

**Authorization:** PM

**Request Body:**
```json
{
  "status": "Completed"
}
```

**Status Options:** `Active`, `Completed`, `OnHold`

**Success Response:** `200 OK`

**Auto Actions:**
- If status = Completed: set CompletedAt timestamp

---

### Remove Talent from Project
**DELETE** `/api/projects/{projectId}/assignments/{assignmentId}`

**Authorization:** PM

**Success Response:** `204 No Content`

**Auto Actions:**
- Set CompletedAt timestamp
- Assignment status → Completed

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
  "availabilityStatus": "Available",
  "department": "Engineering"
}
```

**Success Response:** `200 OK`
```json
{
  "results": [
    {
      "userId": "talent-guid",
      "fullName": "John Doe",
      "department": "Engineering",
      "availabilityStatus": "Available",
      "matchScore": 90,
      "matchedSkills": [
        {
          "skillName": "C#",
          "talentLevel": "Advanced",
          "requiredLevel": "Intermediate"
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

**Description:** Auto-match talents based on project requirements (future feature)

**Success Response:** `200 OK`
```json
{
  "projectId": "project-guid",
  "projectName": "E-Commerce Platform",
  "matchedTalents": [
    {
      "userId": "talent-guid",
      "fullName": "John Doe",
      "matchScore": 95,
      "matchPercentage": "95%"
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
  "userId": "talent-guid",
  "firstName": "John",
  "lastName": "Doe",
  "fullName": "John Doe",
  "department": "Engineering",
  "availabilityStatus": "Available",
  "bio": "Experienced backend developer"
}
```

---

### Update Own Profile
**PUT** `/api/me/profile`

**Authorization:** Talent

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe Updated",
  "bio": "Updated bio with more details about experience"
}
```

**Note:** Cannot update department or availabilityStatus (HR only)

**Success Response:** `200 OK`

---

### Get Own Skills
**GET** `/api/me/skills`

**Authorization:** Talent

**Success Response:** `200 OK`
```json
[
  {
    "skillName": "C#",
    "category": "Backend Development",
    "level": "Advanced"
  },
  {
    "skillName": "React",
    "category": "Frontend Development",
    "level": "Intermediate"
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
      "projectDescription": "Build modern shopping system",
      "status": "Active",
      "assignedAt": "2025-11-15T10:00:00Z"
    }
  ],
  "assignmentHistory": [
    {
      "projectName": "Mobile Banking App",
      "assignedAt": "2025-06-01T10:00:00Z",
      "completedAt": "2025-10-31T10:00:00Z",
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
| 400 | Bad Request | Validation errors, invalid data |
| 401 | Unauthorized | Missing/invalid token |
| 403 | Forbidden | Insufficient permissions |
| 404 | Not Found | Resource not found |
| 409 | Conflict | Duplicate resource, business rule violation |
| 422 | Unprocessable Entity | Validation failed |
| 500 | Server Error | Internal server error |

---

## 🔒 Authorization Summary

| Endpoint Group | Admin | HR | PM | Talent |
|----------------|-------|----|----|--------|
| **Auth** | ✅ | ✅ | ✅ | ✅ |
| **Users** | ✅ | ❌ | ❌ | ❌ |
| **Roles** | ✅ | ❌ | ❌ | ❌ |
| **Talents** | 👁️ | ✅ | 👁️ | 👁️ Own |
| **Skills** | 👁️ | ✅ | 👁️ | 👁️ |
| **Talent Skills** | 👁️ | ✅ | 👁️ | 👁️ Own |
| **Projects** | 👁️ | 👁️ | ✅ | 👁️ Assigned |
| **Assignments** | 👁️ | 👁️ | ✅ | 👁️ Own |
| **Search** | ❌ | ❌ | ✅ | ❌ |
| **Self-Service** | ❌ | ❌ | ❌ | ✅ |

**Legend:**
- ✅ Full Access (CRUD)
- 👁️ Read Only
- ❌ No Access

---

**Next:** [Data Models](./07-DataModels.md)