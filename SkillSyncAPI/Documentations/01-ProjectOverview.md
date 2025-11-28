# Project Overview

## 🎯 Tentang SkillSync API

**SkillSync API** adalah backend service internal yang dirancang untuk membantu HR dan Project Manager melakukan pencarian, pengelolaan, dan penempatan talent internal secara efisien. Sistem ini memetakan kompetensi talent, ketersediaan real-time, dan kebutuhan skill project untuk menghasilkan rekomendasi talent paling relevan.

---

## 🎯 Core Objectives

1. **Memetakan kompetensi talent secara komprehensif**
   - Track skills dan skill levels setiap talent
   - Maintain skill master data yang terstruktur
   - Link skills dengan categories untuk better organization

2. **Mengelola ketersediaan talent secara real-time**
   - Track availability status (AVAILABLE, ON_PROJECT, ON_LEAVE)
   - Automatic update saat talent di-assign atau di-remove dari project
   - Visibility untuk PM dalam mencari available talent

3. **Matching talent dengan kebutuhan project**
   - Intelligent matching algorithm berdasarkan skills
   - Filter berdasarkan skill level requirements
   - Sort by relevance score

4. **Memberikan rekomendasi talent berbasis skill dan availability**
   - Search engine untuk PM
   - Ranking berdasarkan skill match
   - Display match percentage

---

## 🏗️ System Architecture

SkillSync API dibangun menggunakan arsitektur **Clean Architecture** dengan pemisahan concerns yang jelas:

```
┌─────────────────────────────────────┐
│   Presentation Layer (Controllers)  │  ← HTTP Requests/Responses
└──────────────┬──────────────────────┘
               ↓
┌──────────────────────────────────────┐
│  Business Logic Layer (Services)     │  ← Business Rules & Validation
└──────────────┬───────────────────────┘
               ↓
┌──────────────────────────────────────┐
│   Data Access Layer (Repositories)   │  ← Database Operations
└──────────────┬───────────────────────┘
               ↓
┌──────────────────────────────────────┐
│   Database (Entity Framework Core)   │  ← PostgreSQL
└──────────────────────────────────────┘
```

---

## 🏛️ Architecture Principles

### 1. Separation of Concerns
Setiap layer memiliki tanggung jawab yang jelas dan terdefinisi:
- **Controllers**: Handle HTTP requests & responses
- **Services**: Implement business logic
- **Repositories**: Handle data access
- **Models**: Represent domain entities

### 2. Dependency Inversion
Layer yang lebih tinggi tidak bergantung pada implementasi detail:
- Services bergantung pada Repository **interfaces**, bukan implementations
- Controllers bergantung pada Service **interfaces**, bukan implementations
- Memudahkan testing dan maintainability

### 3. Testability
Struktur yang memudahkan testing:
- Unit testing untuk Services (mock repositories)
- Integration testing untuk Repositories
- API testing untuk Controllers

### 4. Maintainability
Kode yang mudah dipahami dan di-maintain:
- Clear naming conventions
- Consistent structure
- Well-documented code
- Single Responsibility Principle

---

## 🛠️ Technology Stack

### Backend Framework
- **ASP.NET Core 8.0** - Modern, high-performance web framework
- **C# 12** - Latest language features

### Database
- **PostgreSQL** - Relational database
- **Entity Framework Core 8** - ORM untuk data access

### Authentication & Authorization
- **JWT (JSON Web Tokens)** - Stateless authentication
- **ASP.NET Core Identity** - User management (optional)
- **Role-based Authorization** - RBAC implementation

### Additional Libraries
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** (optional) - Complex validation rules
- **Serilog** (optional) - Structured logging

---

## 👥 Target Users

### 1. Admin
- System administrators
- Manage users dan roles
- Full system access

### 2. HR (Human Resources)
- HR team members
- Manage talent data
- Manage skills dan categories
- Set talent availability

### 3. Project Manager
- Project leads
- Create dan manage projects
- Search dan assign talents
- View talent information (read-only)

### 4. Talent
- Company employees
- View own profile
- View own skills
- View project assignments

---

## 🎯 Business Value

### For HR Team
- ✅ Centralized talent database
- ✅ Easy skill management
- ✅ Track talent availability
- ✅ Better talent utilization

### For Project Managers
- ✅ Quick talent search
- ✅ Skills-based matching
- ✅ Easy talent assignment
- ✅ Project team management

### For Company
- ✅ Optimize resource allocation
- ✅ Reduce talent search time
- ✅ Better project staffing
- ✅ Data-driven decisions

### For Talents
- ✅ Visibility of their skills
- ✅ Track project assignments
- ✅ Career development insights

---

## 📈 Success Metrics (KPIs)

1. **Time to Find Talent**
   - Reduce dari manual search (hours) ke automated search (minutes)

2. **Talent Utilization Rate**
   - Increase percentage of time talents are assigned to projects

3. **Skill Match Accuracy**
   - Percentage of successful talent-project matches

4. **User Adoption**
   - Active users per role (HR, PM, Talent)

---

## 🚀 MVP Scope

Fokus pada core features yang essential:
- ✅ Authentication & RBAC
- ✅ Talent & Skills Management
- ✅ Project Management
- ✅ Basic Search & Matching
- ✅ Talent Assignment

**Out of Scope (Post-MVP):**
- ❌ Advanced analytics
- ❌ Skill endorsements
- ❌ Training recommendations
- ❌ Performance tracking
- ❌ Notification system

---

## 🔄 Development Phases

### Phase 1: Foundation (Week 1-2)
- Setup project structure
- Database design & migrations
- Authentication implementation
- Basic CRUD operations

### Phase 2: Core Features (Week 3-4)
- Talent management
- Skills management
- Project management
- RBAC implementation

### Phase 3: Matching Engine (Week 5)
- Search algorithm
- Matching logic
- Sorting & filtering

### Phase 4: Testing & Refinement (Week 6)
- Unit testing
- Integration testing
- Bug fixes
- Documentation

---

**Next:** [Role-Based Access Control (RBAC)](./02-RBAC.md)
