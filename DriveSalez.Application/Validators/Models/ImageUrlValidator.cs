using DriveSalez.Domain.Entities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class ImageUrlValidator : AbstractValidator<ImageUrl>
{
    public ImageUrlValidator()
    {
        
    }
}