using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RezervasyonUcak.Models.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration; 
        }



        public string getUsernameFromToken(string token)

        {
          var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtOptions:SigningKey"]);
            var handler = new JwtSecurityTokenHandler();
            var token2 = handler.ReadJwtToken(token);

            return token2.Claims.First(claim => claim.Type == "usermail").Value;


        }


        public Dto.Token CreateAccessToken(Login loginRequest)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("usermail",loginRequest.Mail),
                new Claim(ClaimTypes.Role,"User"),

            };

            Dto.Token token = new Dto.Token();
            SymmetricSecurityKey symmetricSecurityKey = new(
                Encoding.UTF8.GetBytes(_configuration["JwtOptions:SigningKey"]));
            
            SigningCredentials signingCredentials=new(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(6);
            JwtSecurityToken tokenSecurity = new(
                claims:claims,
                audience: _configuration["JwtOptions:Audience"],
                issuer: _configuration["JwtOptions:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials:signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken= tokenHandler.WriteToken(tokenSecurity);
            
            return token;
        }
    }
}
