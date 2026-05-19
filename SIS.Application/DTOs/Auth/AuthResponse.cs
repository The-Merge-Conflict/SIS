using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Application.DTOs.Auth
{
    public record AuthResponse(
        bool IsSuccess,
        string? Token,
        string? Message
    );
}
