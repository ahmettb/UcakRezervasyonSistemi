using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RezervasyonUcak.Models;
using RezervasyonUcak.Models.Dto;
using RezervasyonUcak.Models.Token;

namespace RezervasyonUcak.Controllers.AuthController
{
    public class AuthController:Controller
    {

        private readonly  AppDbContext _context;
        private readonly ITokenHandler _tokenHandler;

        public AuthController (AppDbContext context, ITokenHandler tokenHandler)
        {
            _context = context;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public IActionResult _Register(Register register)
        {
            if(ModelState.IsValid)
            {
               


                Musteri musteri = new Musteri();
                musteri.Name = register.Name;
                musteri.Surname = register.Surname;
                musteri.Password = register.Password;
                musteri.Mail = register.Email;

                _context.Musteri.Add(musteri);
                _context.SaveChanges(); 

                return RedirectToAction("Login");
                   
            }

            return View("Register");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult _Login(Login login)
            {

            if(ModelState.IsValid)
            {
                       if(login.Mail=="sabis@gmail.com")
                {


                    var jwt = Request.Headers["Bearer"];
                    Token token = _tokenHandler.CreateAccessToken(login);
                 var response = new { login, Token = token, jwt };

                    HttpContext.Response.Cookies.Append("token", token.AccessToken,
                        new CookieOptions
                        {

                            Expires = DateTime.Now.AddDays(1),
                            Secure = true,
                            HttpOnly = true,
                            IsEssential = true,
                            SameSite=SameSiteMode.None


                        }


                        );
                    ;
                    _tokenHandler.getUsernameFromToken(token.AccessToken);


                    return Ok(_tokenHandler.getUsernameFromToken(token.AccessToken));

                }


            }
            return RedirectToAction("Login");
    
             }


        public IActionResult Login()
        {
            return View();
        }
    }
}
