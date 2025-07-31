using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SunEduProject.Model;
using SunEduProject.Service.NewFolder;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace SunEduProject.Service
{
    public class TokenServiceRespository : ITokenRespository
    {
        private readonly IOptions<JwtOptions> jwtOptions;

        public TokenServiceRespository(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions;
        }
        public string GenerateTokenAsync(ApplicationUser user)
        {
            //tokenHanlder
            var tokenHandler = new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes(jwtOptions.Value.SecretKey);

            //claims
            var claims = new List<Claim> 
            {
               new Claim(JwtRegisteredClaimNames.Sub,user.Id),
               new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),


            };

            //tokenDescriptor
            var tokenDescriptor= new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = jwtOptions.Value.Issuer,
                Audience = jwtOptions.Value.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //create token
            var token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
