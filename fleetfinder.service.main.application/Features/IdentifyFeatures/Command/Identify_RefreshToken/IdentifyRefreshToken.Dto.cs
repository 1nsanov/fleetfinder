﻿using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_RefreshToken;

public static partial class IdentifyRefreshToken
{
    public record ResponseDto(
        TokenDto? Token
    );
}