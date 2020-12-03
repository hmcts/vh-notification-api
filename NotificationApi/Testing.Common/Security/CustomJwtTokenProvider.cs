using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Testing.Common.Security
{
    public class CustomJwtTokenProvider
    {
        public string GenerateTokenForCallbackEndpoint(string callbackSecret, int expiresInMinutes)
        {
            return BuildToken(expiresInMinutes, Convert.FromBase64String(callbackSecret));
        }
        
        private static string BuildToken(int expiresInMinutes, byte[] key)
        {
            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow.AddMinutes(-1),
                NotBefore = DateTime.UtcNow.AddMinutes(-1),
                Issuer = "hmcts.video.hearings",
                Expires =  DateTime.UtcNow.AddMinutes(expiresInMinutes + 1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
