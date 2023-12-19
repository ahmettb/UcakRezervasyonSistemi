using Microsoft.AspNetCore.Mvc;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Repository;

namespace RezervasyonUcak.Areas.Employees.Controllers
{
    [Area("Employees")]
    public class MusteriController:Controller
    {

        private readonly AppDbContext appContext;
		private readonly IUcusSeferRepository ucusSeferRepository;


		public MusteriController(AppDbContext appContext,IUcusSeferRepository ucusSeferRepository)
        {
            this.appContext = appContext;
            this.ucusSeferRepository = ucusSeferRepository;
        }



        public IActionResult Anasayfa()
        {


            return View(ucusSeferRepository.getAllUcusSeferKonum());
        }

        public ICollection<UcusSefer> UcusGetir(int konumId,DateTime tarih_)
            {

            UcusTarih tarih = appContext.Tarih.Where(tarih => tarih.UcusTarihi==tarih_).FirstOrDefault();

            if(tarih==null)
            {
                return null;//böyle tarihte bir uçuş yok

            }
			UcusKonum konum = appContext.UcusKonum.Where(konum => konum.Id == konumId && konum.Tarih==tarih).FirstOrDefault();


            ICollection<UcusSefer> seferler = appContext.UcusSefers.Where(s => s.UcusKonum == konum).ToList();

            return seferler;
        }

       

    }
}
