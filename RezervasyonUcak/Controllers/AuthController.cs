using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RezervasyonUcak.Models;
using RezervasyonUcak.Models.Dto;
using RezervasyonUcak.Models.Repository;
using RezervasyonUcak.Models.Token;
using System.Security.Claims;

namespace RezervasyonUcak.Controllers.AuthController
{
   // [ApiController]
  //  [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly  AppDbContext appDbContext;

        private readonly ImusteriRepository musteriRepository;

        public AuthController( AppDbContext appDbContext,ImusteriRepository musteriRepository )
        {
            this.appDbContext = appDbContext;
             this.musteriRepository = musteriRepository;    
        
        }


        public IActionResult Login()
        {
            return View();
        }


    

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Tüm yetkilendirme bilgilerini siler
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task< IActionResult> _Login( Login login)
        {


            if (ModelState.IsValid)
            {


                Musteri musteri = musteriRepository.getEmployeeByUsernameAndPassword(login.Mail, login.Password);
                if (musteri == null)
                {

                    ViewBag.LoginErrorMessage = "Mail ya da şifre hatalı";
                    return View("Login");
                }
                var claims = new List<Claim>
                {

                new Claim(ClaimTypes.Name,musteri.Mail),
                new Claim(ClaimTypes.Role,musteri.Role),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index","Home");

            }
            return View("Login");

        }



        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterAdmin()
        //{
        //    RegistrationModel model = new RegistrationModel
        //    {
        //        Username="admin",
        //        Email="admin@gmail.com",
        //        FirstName="John",
        //        LastName="Doe",
        //        Password="Admin@12345#"
        //    };
        //    model.Role = "admin";
        //    var result = await this._authService.RegisterAsync(model);
        //    return Ok(result);
        //}





    }























    /* private readonly RoleManager<IdentityRole> _roleManager;

     private readonly AppDbContext _context;
     private readonly ITokenHandler _tokenHandler;
     private readonly ImusteriRepository musteriRepository;
     DataController _controller;
     private readonly SignInManager<IdentityUser> _signInManager;
     public AuthController(AppDbContext context, ITokenHandler tokenHandler, ImusteriRepository ımusteriRepository, DataController dataController,SignInManager signInManager)
     {
         _context = context;
         _tokenHandler = tokenHandler;
         musteriRepository = ımusteriRepository;
         _controller = dataController;
         signInManager = signInManager;  
     }

     [HttpPost]
     public IActionResult _Register(Register register)
     {



         if (ModelState.IsValid)
         {
             bool mailKontrol = musteriRepository.existByEmail(register.Email);
             bool usernameControl = musteriRepository.existByUsername(register.Username);

             if (mailKontrol)
             {
                 ModelState.AddModelError("Email", "Bu mail zaten kayıtlı");
             }
             if (usernameControl)
             {
                 ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kayıtlı");
             }

             if (!mailKontrol && !usernameControl)
             {

                 Musteri musteri = new Musteri();
                 musteri.Name = register.Name;
                 musteri.Surname = register.Surname;
                 musteri.Mail = register.Email;
                 musteri.Password = register.Password;


                 _context.Musteri.Add(musteri);
                 _context.SaveChanges();
                 ViewBag.Message = "Kayıt işlemi başarılı.Lütfen giriş yapınız.";
                 return Ok(musteri);


             }

         }

         return View("Register");
     }









     public IActionResult Register()
     {
         return View();
     }

     [HttpPost("login")]

     public IActionResult _Login([FromBody] Login login)
     {


if (ModelState.IsValid)
         {


             Musteri musteri = musteriRepository.getEmployeeByUsernameAndPassword(login.Mail, login.Password);
             if (musteri == null)
             {

                 ViewBag.LoginErrorMessage = "Mail ya da şifre hatalı";
                 return View("Login");
             }


             var jwt = Request.Headers["Bearer"];
             Token token = _tokenHandler.CreateAccessTokenAsync(login);
             var response = new { login, Token = token, jwt };




         Response.Cookies.Append("Cookies", token.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
Response.Cookies.Append("", login.Mail, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });



               _tokenHandler.getUsernameFromToken(token.AccessToken);
             return Ok(response);



return Ok(_tokenHandler.getUsernameFromToken(token.AccessToken));

         }
   return Ok("Login");

      }


 public IActionResult Login()
 {
     return View();
 }
}

*/
}
