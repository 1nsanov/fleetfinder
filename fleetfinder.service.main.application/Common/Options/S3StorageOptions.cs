using System.ComponentModel.DataAnnotations;

namespace fleetfinder.service.main.application.Common.Options;

public class S3StorageOptions
{
    public const string SectionName = "S3Storage";

    [Required]
    public string ServiceUrl { get; set; } = null!;

    [Required]
    public string PublicBaseUrl { get; set; } = null!;

    [Required]
    public string AccessKey { get; set; } = null!;

    [Required]
    public string SecretKey { get; set; } = null!;

    [Required]
    public string Bucket { get; set; } = null!;

    public Dictionary<string, string> Folders { get; set; } = new();
}
