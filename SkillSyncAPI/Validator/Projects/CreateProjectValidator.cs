using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SkillSyncAPI.DTOs.Projects;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Validator.Projects;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        // Validasi Name - Nama Project wajib diisi
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required")
            .MinimumLength(3)
            .WithMessage("Projectsss name must be at least 3 characters")
            .MaximumLength(100)
            .WithMessage("Projectsss name must not exceed 100 characters");

        // Validasi Description - Deskripsi wajib diisi
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Projectttt description is required")
            .MinimumLength(10)
            .WithMessage("Projectyy description must be at least 10 characters")
            .MaximumLength(500)
            .WithMessage("Projectyy description must not exceed 500 characters");

        // Validasi Status - Status harus valid enum
        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Projectyy status is required")
            .Must(IsValidStatus)
            .WithMessage(
                "Invalid statusasdasda. Valid values: Active, Inactive, Completed, OnHold, Cancelled"
            );
    }

    // Helper method untuk validasi apakah status valid
    private bool IsValidStatus(string status)
    {
        // Cek apakah string bisa di-convert ke ProjectStatus enum
        return Enum.TryParse<ProjectStatus>(status, true, out _);
    }
}
