using DriveSalez.Domain.Entities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class WorkHourValidator : AbstractValidator<WorkHour>
{
    public WorkHourValidator()
    {
        RuleFor(workHour => workHour.Id)
            .GreaterThan(0).WithMessage("ID must be a positive integer.");

        RuleFor(workHour => workHour.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(workHour => workHour.User)
            .NotNull().WithMessage("User reference is required.");

        RuleFor(workHour => workHour.DayOfWeek)
            .IsInEnum().WithMessage("Day of Week must be a valid enum value.");

        RuleFor(workHour => workHour.OpenTime)
            .LessThanOrEqualTo(workHour => workHour.CloseTime)
            .When(workHour => workHour.OpenTime.HasValue && workHour.CloseTime.HasValue)
            .WithMessage("Open Time must be before Close Time.");

        RuleFor(workHour => workHour.CloseTime)
            .GreaterThanOrEqualTo(workHour => workHour.OpenTime)
            .When(workHour => workHour.OpenTime.HasValue && workHour.CloseTime.HasValue)
            .WithMessage("Close Time must be after Open Time.");
    }
}