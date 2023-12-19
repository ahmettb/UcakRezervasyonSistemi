using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RezervasyonUcak.Areas.Admin.Model.Dto;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Dto;
using RezervasyonUcak.Areas.Employees.Models.Repository;
using RezervasyonUcak.Models;
using System.Data.Entity;

namespace RezervasyonUcak.Areas.Admin.Controllers
{
   // [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class AdminPanelController:Controller
    {


        private readonly AppDbContext appContext;
        private readonly ImusteriRepository musteriRepository;
        public AdminPanelController(AppDbContext appContext, ImusteriRepository imusteriRepository)
        {
            this.appContext = appContext;
            this.musteriRepository = imusteriRepository;
            
        }

        [HttpGet]
        public List<Ucak> GetUcaklar(int firmaId)
        {
            var query = appContext.Ucak
                .Include(u => u.Firma)
                .Include(u => u.UcakModel)
                .Where(ucak => ucak.Firma.FirmaId == firmaId)
                .Select(ucak => new Ucak
                {
                  UcakId=  ucak.UcakId,
                    Firma=ucak.Firma,
                    ModelNo=ucak.ModelNo,
                    
                   
                })
                .ToList();

            return  query;
        }

        //Get all employee
        [Route("employee/getAll")]
        [HttpGet]
        public ICollection<Musteri> getAllEmployee()
        {



            ICollection<Musteri> employees = (from musteri in appContext.Musteri
                                                  //where musteri.deleted==false
                                              select musteri).ToList();

            if (employees == null)
            {
                return null;
            }

            //  return Ok(employees);
            return employees;

        }
        [HttpPost]
        public void delete(int id)
        {

            Musteri musteri = appContext.Musteri.Where(musteri => musteri.User.Id == id).FirstOrDefault();

            appContext.Musteri.Remove(musteri);

            appContext.SaveChanges();




        }


       
        public IActionResult Anasayfa()
        {
            return View();
        }

        public IActionResult MusteriGoruntule()
        {
            ICollection<Musteri> employees = getAllEmployee();

            //HttpClient client = new HttpClient();

            //             client.BaseAddress=new Uri("https://localhost:7004/");
            //   client.DefaultRequestHeaders.Accept.Clear();

            // client.DefaultRequestHeaders
            //.Accept
            //.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = client.GetAsync("employee/getAll");

            //      response.Wait();

            //    var result=response.Result;

            //  if(result.IsSuccessStatusCode)
            //    {

            return View(employees);

        }

    


        public IActionResult MusteriEkle()
        {


            return View();
        }
        

        public IActionResult _MusteriEkle(UserRequest userRequest)
        {
            if(ModelState.IsValid)
            {
                bool mailKontrol = musteriRepository.existByEmail(userRequest.Email);
                bool usernameControl = musteriRepository.existByUsername(userRequest.Username);

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



                }


            }
            ViewBag.Message = "Lütfen bilgileri eksiksiz doldurun";

            return View();



        }



        public IActionResult UcakEkle()
        {


            return View();
        }

        public IActionResult UcakBilgileriGoruntule()
        {


            return View();
        }
        public IActionResult UcusSeferiEkle()
        {


            return View(appContext.Firma.ToList());
        }
        public IActionResult UcusBilgiGoruntule()
        {


            return View();
        }
        public IActionResult BiletEkle()
        {


            return View();
        }
        public IActionResult BiletBilgiGoruntule()
        {


            return View();
        }



    }


}

