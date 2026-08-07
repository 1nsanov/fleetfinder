using System.ComponentModel.DataAnnotations;

namespace fleetfinder.service.main.application.Common.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required(ErrorMessage = "Jwt:Key required")]
    public string Key { get; set; } = null!;

    [Required(ErrorMessage = "Jwt:Issuer required")]
    public string Issuer { get; set; } = null!;

    [Required(ErrorMessage = "Jwt:Audience required")]
    public string Audience { get; set; } = null!;
}
