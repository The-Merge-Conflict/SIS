using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Application.DTOs.Auth
{
    public record RegisterRequest(
        string Email, 
        string Password, 
        string FullName
    );
}
