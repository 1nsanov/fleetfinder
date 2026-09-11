namespace FleetFinder.Application.Common.Validation;

public static class ValidationMessages
{
    public const string NotEmpty = "'{PropertyName}' must not be empty.";
    public const string MaxLength = "'{PropertyName}' must not exceed {MaxLength} characters.";
    public const string MinLength = "'{PropertyName}' must be at least {MinLength} characters.";
    public const string GreaterThanZero = "'{PropertyName}' must be greater than 0.";
    public const string InvalidEmail = "'{PropertyName}' is not a valid email address.";
    public const string ImageUrlRequired = "Image URL must not be empty.";
    public const string ImageUrlInvalid = "Image URL must be a valid URL.";
}
