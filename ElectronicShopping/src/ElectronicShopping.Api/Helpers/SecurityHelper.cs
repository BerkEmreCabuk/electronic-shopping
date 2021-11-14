using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ElectronicShopping.Api.Helpers
{
    public static class SecurityHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] rawDataBytes = Encoding.UTF8.GetBytes(rawData);
                byte[] saltBytes = Encoding.UTF8.GetBytes(CommonKeyConstant.ENCRYPT_KEY);
                byte[] saltedInput = new byte[saltBytes.Length + rawDataBytes.Length];

                saltBytes.CopyTo(saltedInput, 0);
                rawDataBytes.CopyTo(saltedInput, saltBytes.Length);

                byte[] hashedBytes = sha256Hash.ComputeHash(saltedInput);

                return BitConverter.ToString(hashedBytes);
            }
        }

        public static string GenerateToken(UserModel user, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user", JsonSerializer.Serialize(user)) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static UserModel ValidateToken(string token, string secretKey)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var exist = jwtToken.Claims.Any(x => x.Type == "user");
                if (!exist)
                    return null;

                var userJson = jwtToken.Claims.First(x => x.Type == "user").Value;

                // return user from JWT token if validation successful
                return JsonSerializer.Deserialize<UserModel>(userJson);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
