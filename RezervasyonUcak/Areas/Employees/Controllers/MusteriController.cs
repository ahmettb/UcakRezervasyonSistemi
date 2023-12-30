using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Dto;
using RezervasyonUcak.Areas.Employees.Models.Repository;
using RezervasyonUcak.Models;

using System.Data.Entity;
using System.Security.Claims;

namespace RezervasyonUcak.Areas.Employees.Controllers
{
    [Area("Employees")]
    [Authorize(Roles ="User")]
    public class MusteriController:Controller
    {

        private readonly AppDbContext appContext;
		private readonly IUcusSeferRepository ucusSeferRepository;

        


		public MusteriController(AppDbContext appContext,IUcusSeferRepository ucusSeferRepository)
        {
            this.appContext = appContext;
            this.ucusSeferRepository = ucusSeferRepository;
        }

        public IActionResult HesapBilgileri(User user)
        {
            user = getEmployee();

            return View(user);
        }



        public IActionResult UcusBilgileriGoruntule(int konumId,DateTime tarih_)
        {
			List<UcusSefer> sefer = ucusSeferRepository.UcusSeferiGetir(konumId, tarih_);

			return View(sefer);  
        }

        public void olustur(Ucak ucak)
        {

            for(int i = 0;i<ucak.KoltukSayisi;i++)
            {

                ucak.Koltuklar.Add(new Koltuk
                {

                    DoluMu=false,
                    KoltukNo="A"+i,


                });

            }

        }

        [HttpGet]
        public Ucak getUcak(int ucakId)
        {

            Ucak ucak = ucusSeferRepository.ucakGetir(ucakId);


            if (ucak.Koltuklar==null)
            {
                for(int i = 0;i < ucak.KoltukSayisi; i++)
                {
                    Koltuk koltuk = new Koltuk();
                    koltuk.DoluMu = false;
                    koltuk.KoltukNo = "A"+i;    
                    appContext.Koltuk.Add(koltuk);
                    ucak.Koltuklar.Add(koltuk);
                }
                appContext.SaveChanges();
                
            }

            return ucak;
         
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Tüm yetkilendirme bilgilerini siler
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult BiletAlmaEkrani(UcusSeferResponse ucus)
        {
            return View(ucus);
        }

        public UcusSeferResponse response(int id)
        {
            UcusSeferResponse model = (from ucus in appContext.UcusSefers
                                       where ucus.UcusId == id
                                       select new UcusSeferResponse
                                       {
                                           UcusId = ucus.UcusId,
                                           UcakId = ucus.Ucak.UcakId,
                                           BaslangicKonum = ucus.UcusKonum.BaslangicKonum,
                                           VarisKonum = ucus.UcusKonum.VarisKonum,
                                           BaslangicSaat = ucus.BaslangicSaat,
                                           VarisSaati = ucus.VarisSaati,
                                           UcakModelNo = ucus.Ucak.ModelNo,
                                           FirmaAdi = ucus.Ucak.Firma.FirmaAdi,
                                           UcusFiyat = ucus.UcusFiyat,
                                           koltuklar = ucus.Ucak.Koltuklar

                                       }).FirstOrDefault();




            Ucak ucak2
              = appContext.Ucak.Include(u => u.Koltuklar).Include(u => u.Firma).Where(ucak => ucak.UcakId == model.UcakId).Select(ucak =>
              new Ucak
              {
                  KoltukSayisi = ucak.KoltukSayisi,
                  Koltuklar = ucak.Koltuklar,
                  ModelNo = ucak.ModelNo,
                  UcakId = ucak.UcakId,
                  Firma = ucak.Firma,
              }).FirstOrDefault();
            if (ucak2.Koltuklar.Count == 0)
            {
             //   ucak2.Koltuklar = new List<Koltuk>();

                for (int i = 0; i < ucak2.KoltukSayisi; i++)
                {
                    Koltuk koltuk = new Koltuk();
                    koltuk.DoluMu = false;
                    koltuk.KoltukNo = "A" + i;

                    appContext.Koltuk.Add(koltuk);
                    ucak2.Koltuklar.Add(koltuk);

                }
                appContext.SaveChanges();

                //ucuseSefer.Ucak.Koltuklar=ucak.Koltuklar;

            }

            model.koltuklar = ucak2.Koltuklar;
            return model;



        }


        public IActionResult KoltukGetir(int id)
        {





            /*       Ucak ucak
                  = appContext.Ucak.Include(u => u.Koltuklar).Include(u => u.Firma).Where(ucak => ucak.UcakId == model.UcusId).Select(ucak =>
                  new Ucak
                  {
                      KoltukSayisi = ucak.KoltukSayisi,
                      Koltuklar = ucak.Koltuklar,
                      ModelNo = ucak.ModelNo,
                      UcakId = ucak.UcakId,
                      Firma = ucak.Firma,
                  }).FirstOrDefault();
            */




            return View("BiletAlmaEkrani",response(id));




        }

        public BiletResponse biletBilgiGoster(int ucusId, string koltukNo)
        {

           UcusSeferResponse ucus= response(ucusId);


            BiletResponse biletResponse = new BiletResponse();
            biletResponse.BiletFiyat = ucus.UcusFiyat;
            biletResponse.BaslangicKonum=ucus.BaslangicKonum;
            biletResponse.YolcuSoyadi = getEmployee().Surname;
            biletResponse.YolcuAdi = getEmployee().Name;
            biletResponse.YolcuMail = getEmployee().Email;
            biletResponse.BiletKesimTarihi = DateTime.Now;
            biletResponse.VarisKonum = ucus.VarisKonum;
            biletResponse.BaslangicSaat = ucus.BaslangicSaat;
            biletResponse.BitisSaat = ucus.VarisSaati;
            biletResponse.FirmaAdi = ucus.FirmaAdi;
            biletResponse.UcakModelNo = ucus.UcakModelNo;
           





            Bilet bilet = new Bilet();
            bilet.BiletFiyat = ucus.UcusFiyat;
            Koltuk koltuk = new Koltuk();
             koltuk=   ucus.koltuklar.Find(koltuk => koltuk.KoltukNo == koltukNo);
            biletResponse.KoltukNo = koltuk.KoltukNo;
            bilet.UcusSefer = appContext.UcusSefers.Where(sefer=>sefer.Ucak.UcakId==ucus.UcakId).Select(ucus=>new UcusSefer
            {

                Ucak=ucus.Ucak,
                BaslangicSaat=ucus.BaslangicSaat,
                VarisSaati=ucus.VarisSaati,
                UcusFiyat=ucus.UcusFiyat,   
                UcusId=ucus.UcusId,
                UcusKonum=ucus.UcusKonum,


            }).FirstOrDefault();
            bilet.IptalMi = false;
            bilet.Musteri = getEmployee();
            bilet.KesimTarihi = DateTime.Now;
            bilet.Koltuk = koltuk;


            return biletResponse;

        }

        [HttpPost]
        public void BiletIptal(int biletId)
        {

            Bilet bilet = appContext.Bilets.Include(bilet=>bilet.Koltuk).Where(bilet => bilet.Id == biletId).FirstOrDefault();
            bilet.IptalMi = true;
            Bilet bilet2 = appContext.Bilets.Include(bilet => bilet.Koltuk).Where(bilet => bilet.Id == biletId).Select(bilet=>new Bilet
            {
                Koltuk=bilet.Koltuk,
                IptalMi=bilet.IptalMi,
            }).FirstOrDefault();

            //     bilet.Koltuk.DoluMu = false;

            Koltuk koltuk = appContext.Koltuk.Where(koltuk => koltuk.Id == bilet2.Koltuk.Id).FirstOrDefault();
            koltuk.DoluMu = false;
            appContext.SaveChanges();

        }

        public  IActionResult musteriGuncelle(User user)
        {
           User oldUser= appContext.Users.Where(user_ => user_.Email == user.Email).FirstOrDefault();
            oldUser.Name=user.Name;
            oldUser.Surname = user.Surname;
            oldUser.Email = user.Email;
            oldUser.Password=user.Password;
            appContext.SaveChanges();

            return View("HesapBilgileri",getEmployee());
        
        }

        public List<Bilet>biletByUser()
        {
            User user = getEmployee();

            List<Bilet> bilet = appContext.Bilets
     .Include(b => b.UcusSefer.UcusKonum)
            .Include(b => b.UcusSefer.Ucak)
     .Where(b => b.Musteri.Email == user.Email)
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

            return bilet;

        }






        public IActionResult BiletlerGetir()
        {
             User user=getEmployee();
              List<Bilet> bilet = appContext.Bilets
        .Include(b => b.UcusSefer.UcusKonum)
        .Include(b => b.UcusSefer.Ucak)
        .Where(b => b.Musteri.Email == user.Email)
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
                        Ucak = appContext.Ucak.Where(ucak=>ucak.UcakId==sefer.Ucak.UcakId).Select(ucak=>new Ucak
                        {
                            Firma=ucak.Firma,
                            UcakId=ucak.UcakId,
                            ModelNo=ucak.ModelNo,
                            Koltuklar=ucak.Koltuklar,
                            KoltukSayisi=ucak.KoltukSayisi

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
            
          //  List<Bilet> bilet = biletByUser();
            List< BiletResponse> biletResponse = new List<BiletResponse>();
            foreach(var x in bilet)
            {
                BiletResponse biletRsp= new BiletResponse();
                biletRsp.Id=x.Id;
                biletRsp.BiletFiyat = x.BiletFiyat;
                biletRsp.BaslangicKonum = x.UcusSefer.UcusKonum.BaslangicKonum;
                biletRsp.VarisKonum = x.UcusSefer.UcusKonum.VarisKonum;
                biletRsp.BaslangicSaat = x.UcusSefer.BaslangicSaat;
                biletRsp.BitisSaat = x.UcusSefer.VarisSaati;
                biletRsp.BiletKesimTarihi = x.KesimTarihi;
                biletRsp.FirmaAdi = x.UcusSefer.Ucak.Firma.FirmaAdi;
                biletRsp.UcakModelNo = x.UcusSefer.Ucak.ModelNo;
                biletRsp.YolcuAdi = x.Musteri.Name;
                biletRsp.YolcuSoyadi = x.Musteri.Surname;
                biletRsp.KoltukNo = x.Koltuk.KoltukNo;
                biletRsp.UcusTarihi = x.UcusSefer.UcusKonum.Tarih.UcusTarihi;
                biletRsp.YolcuMail = x.Musteri.Email;
                biletRsp.Iptal = x.IptalMi;

                biletResponse.Add(biletRsp);

            


            }
            

            return View(biletResponse);

        }

        public IActionResult BiletAl(int ucusId,string koltukNo)
        {
            BiletResponse bilet = biletBilgiGoster(ucusId, koltukNo);
            UcusSeferResponse ucus = response(ucusId);

            Ucak ucak
            = appContext.Ucak.Include(u => u.Koltuklar).Include(u => u.Firma).Where(ucak => ucak.UcakId == ucus.UcakId).Select(ucak =>
            new Ucak
            {
                KoltukSayisi = ucak.KoltukSayisi,
                Koltuklar = ucak.Koltuklar,
                ModelNo = ucak.ModelNo,
                UcakId = ucak.UcakId,
                Firma = ucak.Firma,
            }).FirstOrDefault();





            Bilet bilet1 = new Bilet();
        //    Ucak ucak= appContext.Ucak.Where(ucak => ucak.UcakId == ucus.UcakId).FirstOrDefault();
            //bilet1.Koltuk = ucus.koltuklar.Find(koltuk => koltuk.KoltukNo == koltukNo);
            bilet1.IptalMi = false;
    

         

            Koltuk koltuk = ucak.Koltuklar.FirstOrDefault(x => x.KoltukNo == koltukNo);
         //   koltuk = ucak.Koltuklar.FirstOrDefault(x => x.KoltukNo == koltukNo);
            bilet1.Koltuk = koltuk;
            bilet1.Musteri = getEmployee();
            bilet1.BiletFiyat = ucus.UcusFiyat;
            bilet1.KesimTarihi = DateTime.Now;
            UcusSefer sefer=appContext.UcusSefers.Where(sefer => sefer.UcusId == ucusId).FirstOrDefault();
            bilet1.UcusSefer = sefer;
           appContext.Bilets.Add(bilet1);
            koltuk.DoluMu = true;
            appContext.SaveChanges();
            return View("BiletBilgiEkrani",bilet);
        }

        public IActionResult BiletBilgiEkrani( BiletResponse response )
        {
            return View(response);
        }

        /*   public IActionResult KoltukGetir(int ucakId,int ucusId)
           {

               Ucak ucak
                   = appContext.Ucak.Include(u => u.Koltuklar).Include(u=>u.Firma). Where(ucak => ucak.UcakId == ucakId).Select(ucak =>
                   new Ucak
                   {
                       KoltukSayisi=ucak.KoltukSayisi,
                       Koltuklar = ucak.Koltuklar,
                       ModelNo = ucak.ModelNo,
                       UcakId = ucakId,
                       Firma = ucak.Firma,
                   }).FirstOrDefault();







               if (ucak.Koltuklar.Count==0)
               {
                   ucak.Koltuklar = new List<Koltuk>();

                   for (int i = 0; i < ucak.KoltukSayisi; i++)
                   {
                       Koltuk koltuk = new Koltuk();
                       koltuk.DoluMu = false;
                       koltuk.KoltukNo = "A" + i;
                       appContext.Koltuk.Add(koltuk);
                       ucak.Koltuklar.Add(koltuk);
                   }
                   appContext.SaveChanges();

               }



               return View("BiletAlmaEkrani",ucak);
        }
           */

        public IActionResult Anasayfa()
        {
            


            return View(ucusSeferRepository.UcusSeferKonumGetir());
        }



        [HttpGet]
        public List<UcusSefer> UcusGetir(int konumId,DateTime tarih_)
            {



         List<UcusSefer>sefer=  ucusSeferRepository.UcusSeferiGetir(konumId,tarih_);

            return sefer;

        }
        public User getEmployee()
        {

            var user = HttpContext.User;
            var email = user.FindFirst(ClaimTypes.Name)?.Value;
           return appContext.Users.Where(user=>user.Email == email).FirstOrDefault();
        }
       

    }
}
