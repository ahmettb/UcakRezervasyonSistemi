using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RezervasyonUcak.Models;
using RezervasyonUcak.Models.Dto;
using RezervasyonUcak.Models.Repository;
using RezervasyonUcak.Models.Token;

namespace RezervasyonUcak.Controllers.AuthController
{
    public class AuthController:Controller
    {

        private readonly  AppDbContext _context;
        private readonly ITokenHandler _tokenHandler;
        private readonly ImusteriRepository musteriRepository;

        public AuthController (AppDbContext context, ITokenHandler tokenHandler,ImusteriRepository ımusteriRepository)
        {
            _context = context;
            _tokenHandler = tokenHandler;
            musteriRepository = ımusteriRepository;
        }

        [HttpPost]
        public IActionResult _Register(Register register)
        {

         

            if (ModelState.IsValid)
            {
                bool mailKontrol = musteriRepository.existByEmail(register.Email);
                bool usernameControl=musteriRepository.existByUsername(register.Username);

                        if ( mailKontrol)
                {
                    ModelState.AddModelError("Email", "Bu mail zaten kayıtlı");
                }
                if (usernameControl)
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kayıtlı");
                }

                if(!mailKontrol && !usernameControl)
                {

                    Musteri musteri=new Musteri();
                    musteri.Name= register.Name;    
                    musteri.Surname= register.Surname;
                    musteri.Mail = register.Email;
                    musteri.Password = register.Password;


                    _context.Musteri.Add(musteri);
                    _context.SaveChanges();
                    ViewBag.Message = "Kayıt işlemi başarılı.Lütfen giriş yapınız.";
                    return View("Login");


                }

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
            return RedirectToAction("Login");
    
             }


        public IActionResult Login()
        {
            return View();
        }
    }
}
