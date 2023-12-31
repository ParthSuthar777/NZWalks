using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace NZWalks.Models.DTOs
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
    }
}
