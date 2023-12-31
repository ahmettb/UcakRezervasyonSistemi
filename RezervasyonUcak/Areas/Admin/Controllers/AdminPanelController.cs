using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Entity;
using NuGet.Protocol;
using RezervasyonUcak.Areas.Admin.Model.Dto;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Dto;
using RezervasyonUcak.Areas.Employees.Models.Repository;
using RezervasyonUcak.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace RezervasyonUcak.Areas.Admin.Controllers
{
   [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class AdminPanelController:Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = HttpContext.User;
            var email = user.FindFirst(ClaimTypes.Name)?.Value;
            var currentUser = appContext.Users.Where(u => u.Email == email).FirstOrDefault();
            ViewBag.CurrentUser = currentUser;

            base.OnActionExecuting(filterContext);
        }


        private readonly AppDbContext appContext;
        private readonly ImusteriRepository musteriRepository;
        public AdminPanelController(AppDbContext appContext, ImusteriRepository imusteriRepository)
        {
            this.appContext = appContext;
            this.musteriRepository = imusteriRepository;


        }

        [HttpPost]

        public void delete(int id)
        {

            User user = appContext.Users.Where(user => user.Id == id).FirstOrDefault();

            user.Deleted = true;

            appContext.SaveChanges();


        }

        public IActionResult YetkiliKullaniciEkrani()
        {
            List<User> admins = appContext.Users.Where(user => user.Role == Role.Admin && user.Deleted == false).ToList();
            return View(admins);
        }

        public void update(int id,[FromBody]User user)
        {
            User userOld=appContext.Users.Where(user=>user.Id==id) .FirstOrDefault();
            userOld.Name = user.Name;
            userOld.Surname= user.Surname;
            userOld.Email=user.Email;
            userOld.Password=user.Password;
            appContext.SaveChanges();
        }

        public IActionResult kullaniciEkle(UserRequest userRequest)
        {

            if(ModelState.IsValid)
            {
                User user = new User();
                user.Name = userRequest.Name;
                user.Email = userRequest.Email;
                user.Password = userRequest.Password;
                user.Surname = userRequest.Surname;
                user.Role = userRequest.Role;
                user.Deleted = false;

                appContext.Users.Add(user);

                if (user.Role == Role.User)
                {
                    Musteri musteri = new Musteri();
                    musteri.User = user;
                    appContext.Musteri.Add(musteri);
                }

                appContext.SaveChanges();
            }
            return View("MusteriEkle");

            




        }

        [HttpPost]
        public void BiletIptalEt(int id)
        {

            Bilet bilet = appContext.Bilets.Where(bilet => bilet.Id == id&& bilet.IptalMi==false).FirstOrDefault();

            bilet.IptalMi = true;
            appContext.SaveChanges();
        
        }

        public List<Ucak>ucakBilgileri()
        {



            List<Ucak>ucak= appContext.Ucak.Select(
                
                ucak=>new Ucak
                {

                    UcakId=ucak.UcakId,
                    Firma=ucak.Firma,
                    KoltukSayisi=ucak.KoltukSayisi,
                    ModelNo=ucak.ModelNo,   

                }
                ).OrderBy(ucak=>ucak.Firma.FirmaId).ToList();


            return ucak;    

        }

        public IActionResult BiletBilgileri()

        {
            List<Bilet> biletler = BiletGoruntule();




            return View(biletler);

        }


        [HttpPost]
        public void SeferEkle([FromBody] SeferEkleRequest request)
        {
            UcusKonum konum = new UcusKonum();
            konum.BaslangicKonum = request.bKonum;
            konum.VarisKonum = request.vKonum;

            UcusTarih tarih = appContext.Tarih.Where(tarih => tarih.UcusTarihi == request.date).FirstOrDefault();
            if (tarih == null)
            {
                tarih = new UcusTarih();
                tarih.UcusTarihi = request.date;
                appContext.Tarih.Add(tarih);


            }
            konum.Tarih = tarih;
            tarih.Konumlar = new List<UcusKonum>();
            tarih.Konumlar.Add(konum);

            Ucak ucak = appContext.Ucak.Where(ucak => ucak.UcakId == request.selectedUcakId).FirstOrDefault();
            Firma firma = appContext.Firma.Where(firma => firma.FirmaId == request.selectedFirmaId).FirstOrDefault();

            UcusSefer sefer = new UcusSefer();
            sefer.UcusKonum = konum;
            sefer.Ucak = ucak;
            sefer.BaslangicSaat = request.selectedTime;
            sefer.VarisSaati = request.selectedTime2;
            sefer.UcusFiyat = sefer.UcusFiyat;

            appContext.UcusSefers.Add(sefer);
            appContext.UcusKonum.Add(konum);

            appContext.SaveChanges();









        }



        public List<Bilet> BiletGoruntule()
        {
            List<Bilet> bilet = (from a in appContext.Bilets
                                 select new Bilet
                                 {
                                     Id = a.Id,
                                     Musteri = a.Musteri,
                                     BiletFiyat = a.BiletFiyat,
                                     IptalMi = a.IptalMi,
                                     KesimTarihi = a.KesimTarihi,
                                     Koltuk = a.Koltuk,
                                     UcusSefer = appContext.UcusSefers.Where(konum=>konum.UcusId==a.UcusSefer.UcusId).FirstOrDefault(),
                                    
                                 }).ToList();


            List<Bilet> biletler = appContext.Bilets
                            .Include(b => b.UcusSefer).
                            Include(b=>b.UcusSefer.Ucak)
            .Include(b => b.UcusSefer.UcusKonum).
               Include(b => b.UcusSefer.UcusKonum.Tarih)


            .Include(b=>b.Koltuk).Where(bilet=>bilet.IptalMi==false)
          
                              .Select(bilet =>
                       new Bilet
                       {
                           Id = bilet.Id,
                           BiletFiyat = bilet.BiletFiyat,
                           IptalMi = bilet.IptalMi,
                           Koltuk = bilet.Koltuk,
                           Musteri = bilet.Musteri,
                           KesimTarihi = bilet.KesimTarihi,
                           UcusSefer = appContext.UcusSefers.Where(sefer => sefer.UcusId == bilet.UcusSefer.UcusId).Select(sefer => new UcusSefer
                           {
                               Ucak = appContext.Ucak.Where(ucak => ucak.UcakId == sefer.Ucak.UcakId).Select(ucak => new Ucak
                               {
                                   Firma = ucak.Firma,
                                   UcakId = ucak.UcakId,
                                   ModelNo = ucak.ModelNo,
                                   Koltuklar = ucak.Koltuklar,
                                   KoltukSayisi = ucak.KoltukSayisi

                               }).FirstOrDefault(),
                               UcusFiyat = sefer.UcusFiyat,
                               UcusId = sefer.UcusId,
                               BaslangicSaat = sefer.BaslangicSaat,
                               VarisSaati = sefer.VarisSaati,
                               UcusKonum = appContext.UcusKonum.Where(konum => konum.Id == bilet.UcusSefer.UcusKonum.Id).Select(konum =>
                               new UcusKonum
                               {
                                   Id = konum.Id,
                                   BaslangicKonum = konum.BaslangicKonum,
                                   VarisKonum = konum.VarisKonum,
                                   Tarih = konum.Tarih,
                                   Seferler = konum.Seferler,

                               }
                               ).FirstOrDefault(),

                           }).FirstOrDefault()

                       }

                       ).ToList();

            return biletler;

        }


        public List<UcusSefer>ucuslariGetir()
        {


            List<UcusSefer> ucuslar = appContext.UcusSefers.Select(uc => new UcusSefer
            {

                UcusId = uc.UcusId,
                Ucak = appContext.Ucak.Where(ucak => ucak.UcakId == uc.Ucak.UcakId).Select(u => new Ucak
                {

                    UcakId = u.UcakId,
                    Firma = u.Firma,
                    ModelNo = u.ModelNo,
                    KoltukSayisi = u.KoltukSayisi,

                }).FirstOrDefault(),
                UcusKonum = appContext.UcusKonum.Where(ucak => ucak.Id == uc.UcusKonum.Id).Select(u => new UcusKonum
                {

                     Id= u.Id,
                    BaslangicKonum = u.BaslangicKonum,
                    VarisKonum = u.VarisKonum,
                    Tarih = u.Tarih,

                }).FirstOrDefault(),
                BaslangicSaat = uc.BaslangicSaat,
                UcusFiyat = uc.UcusFiyat,
                VarisSaati = uc.VarisSaati,

            }).ToList();
            return ucuslar;


        }


        public IActionResult UcusBilgileriGoruntule()
            {
            return View(ucuslariGetir());

        }
        [HttpGet]
        public List<Firma>GetUcakFirma()
        {

           return appContext.Firma.ToList();

        }
        [HttpPost]
        public IActionResult UcakEkle(UcakEkleRequest request)
        {

            if(ModelState.IsValid)
            {

                Ucak ucak = new Ucak();
              Firma firma = appContext.Firma.Where(firma => firma.FirmaId == request.FirmaId).FirstOrDefault();


                ucak.ModelNo = request.ModelNo;
                ucak.KoltukSayisi = request.KoltukSayisi;
                ucak.Firma = firma!;
                appContext.Ucak.Add(ucak);
                appContext.SaveChanges();
                ViewBag.msg = "Kayıt başarılı";
                return View("UcakEkleEkrani");

            }
            ViewBag.msg = "Kayıt başarısız";

            return View ("UcakEkleEkrani");


        }

        public IActionResult UcakEkleEkrani()
        {
            return View();
        }

        public IActionResult UcakBilgileriGoruntule()
        {

            return View(ucakBilgileri());
            


        }

        [HttpGet]
        public List<Ucak> GetUcaklar(int firmaId)
        {
            var query = appContext.Ucak
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
        public List<User> getAllEmployee()
        {



            List<User> musteriler = appContext.Users.Where(user=>user.Role==Role.User &&user.Deleted==false).ToList();

       

            if (musteriler == null)
            {
                return null;
            }

            //  return Ok(employees);
            return musteriler;

        }


       
        public IActionResult Anasayfa()
        {
            return View();
        }

        public IActionResult MusteriGoruntule()
        {
            List<User> employees = getAllEmployee();

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
                bool mailKontrol = musteriRepository.mailKontrol(userRequest.Email);

                if (mailKontrol)
                {
                    ModelState.AddModelError("Email", "Bu mail zaten kayıtlı");
                }
              

 


            }
            ViewBag.Message = "Lütfen bilgileri eksiksiz doldurun";

            return View();



        }



        public IActionResult UcakEkle()
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

