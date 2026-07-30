using System.ComponentModel.DataAnnotations;

namespace fleetfinder.service.main.application.Common.Options;

public class PasswordOptions
{
    public const string SectionName = "Password";

    [Required(ErrorMessage = "Password:EncryptionKey required")]
    public string EncryptionKey { get; set; } = null!;
}
