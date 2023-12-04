using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace RezervasyonUcak.Models.Token
{
    public class TokenHandler : ITokenHandler
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }



        public string getUsernameFromToken(string token)

        {
          var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtOptions:SigningKey"]);
            var handler = new JwtSecurityTokenHandler();
            var token2 = handler.ReadJwtToken(token);

            return token2.Claims.First(claim => claim.Type == "usermail").Value;


        }


        public async  Task<Dto.Token> CreateAccessTokenAsync(Login loginRequest)
        {

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginRequest.Mail));
            identity.AddClaim(new Claim(ClaimTypes.Name, loginRequest.Mail));
            var principal = new ClaimsPrincipal(identity);



         



            List<Claim> claims = new List<Claim>
            {
                new Claim("usermail",loginRequest.Mail),
                new Claim(ClaimTypes.Role,"User"),
                new Claim(JwtRegisteredClaimNames.Email,loginRequest.Mail),

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

        Dto.Token ITokenHandler.CreateAccessTokenAsync(Login loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
