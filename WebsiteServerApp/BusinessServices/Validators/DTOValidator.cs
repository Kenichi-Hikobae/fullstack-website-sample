using FluentValidation;
using MongoDB.Bson;
using WebsiteServerApp.BusinessServices.DTOs;

namespace WebsiteServerApp.BusinessServices.Validators;

/// <summary>
/// Utils methods used for validation on DTO models.
/// </summary>
public static class ValidatorUtils
{
    /// <summary>
    /// Whether the given string is a valid ObjectId
    /// </summary>
    /// <param name="id">The string contents.</param>
    /// <returns>True if is it valid, otherwise false.</returns>
    public static bool IsObjectIdValid(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}

public class PropertyDTOValidator : AbstractValidator<PropertyDTO>
{
    public PropertyDTOValidator()
    {
        RuleFor(property => property.Name).NotEmpty().WithMessage("Property name is required");
        RuleFor(property => property.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        RuleFor(property => property.Address).NotEmpty().WithMessage("Address is required");

        RuleFor(property => property.Id)
            .Must(ValidatorUtils.IsObjectIdValid)
            .WithMessage("Id must be a valid ObjectId string");

        RuleFor(property => property.OwnerId)
            .Must(id => string.IsNullOrEmpty(id) || ValidatorUtils.IsObjectIdValid(id))
            .WithMessage("OwnerId must be a valid ObjectId string if provided");
    }
}

public class PropertyTraceDTOValidator : AbstractValidator<PropertyTraceDTO>
{
    public PropertyTraceDTOValidator()
    {
        RuleFor(trace => trace.Name).NotEmpty().WithMessage("Trace name is required");
        RuleFor(trace => trace.Value).GreaterThan(0).WithMessage("Trace value should be greater than zero");
        RuleFor(trace => trace.Tax).GreaterThan(0).WithMessage("Trace tax should be greater than zero");
        
        RuleFor(trace => trace.PropertyId)
            .NotEmpty()
            .Must(ValidatorUtils.IsObjectIdValid)
            .WithMessage("Trace propertyId must be a valid ObjectId string is required");

        RuleFor(trace => trace.Id)
            .NotEmpty()
            .Must(ValidatorUtils.IsObjectIdValid)
            .WithMessage("Id must be a valid ObjectId string is required");
    }
}

public class PropertyImageDTOValidator : AbstractValidator<PropertyImageDTO>
{
    public PropertyImageDTOValidator()
    {
        RuleFor(image => image.File).NotEmpty().WithMessage("Image file is required");

        RuleFor(image => image.PropertyId)
            .NotEmpty()
            .Must(ValidatorUtils.IsObjectIdValid)
            .WithMessage("Trace propertyId must be a valid ObjectId string is required");

        RuleFor(image => image.Id)
            .NotEmpty()
            .Must(ValidatorUtils.IsObjectIdValid)
            .WithMessage("Id must be a valid ObjectId string is required");
    }
}

public class OwnerDTOValidator : AbstractValidator<OwnerDTO>
{
    public OwnerDTOValidator()
    {
        RuleFor(owner => owner.Name).NotEmpty().WithMessage("Owner name is required");
        RuleFor(owner => owner.Email).EmailAddress().WithMessage("Valid email is required");
        RuleFor(owner => owner.Address).NotEmpty().WithMessage("Owner address is required");

        RuleFor(owner => owner.Id)
            .NotEmpty()
            .Must(ValidatorUtils.IsObjectIdValid)
            .WithMessage("Id must be a valid ObjectId string is required");
    }
}
