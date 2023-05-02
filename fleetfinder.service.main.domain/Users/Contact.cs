using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.domain.Contacts;

[Owned]
public class Contact
{
    public string? PhoneViber { get; set; }
    public string? PhoneTelegram { get; set; }
    public string? PhoneWhatsapp { get; set; }
    public string? WorkingMode { get; set; }
}