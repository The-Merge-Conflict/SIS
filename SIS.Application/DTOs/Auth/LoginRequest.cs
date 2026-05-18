using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Application.DTOs.Auth
{
    public record LoginRequest(
        string Email, 
        string Password, 
        bool RememberMe
    );

}
