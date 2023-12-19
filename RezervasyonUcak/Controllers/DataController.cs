using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RezervasyonUcak.Areas.Employees.Models;
using System.Net.Http.Headers;

namespace RezervasyonUcak.Controllers

{
    [Authorize(Roles ="Admin")]
    public class DataController:Controller
    {


       private readonly AppDbContext appContext;
        public DataController(AppDbContext appContext)
        {
            this.appContext = appContext;
        }


        //Get all employee
        [Route("employee/getAll")]
        [HttpGet]
        public ICollection <Musteri>getAllEmployee()
        {


            ICollection<Musteri> employees = (from musteri in appContext.Musteri
                                              //where musteri.deleted==false
                                             select musteri).ToList();

            if(employees==null)
            {
                return null;
            }

            //  return Ok(employees);
            return employees;

        }

        public  IActionResult MusteriGoruntule()
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

            //var readTask = result.Content.ReadAsAsync<ICollection<Musteri>>();
            // readTask.Wait();
            //  var content = await result.Content.ReadAsStringAsync();
            // var k = JsonConvert.DeserializeObject<ICollection<Musteri>>(content);


            /*  var readTask = result.Content.ReadAsAsync<ICollection<Musteri>>();
              readTask.Wait();

              var students = readTask.Result;



             employees = readTask.Result;    

          }
*/
            return View(employees);
        }

        public IActionResult MusteriEkle()
        {


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


            return View();
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
