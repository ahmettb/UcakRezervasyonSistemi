using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class UcusSefer
    {
        [Key]
        public int UcusId { get; set; }

        private string baslangicSaat;
        private string varisSaati;
        private Ucak ucak;
        private UcusKonum ucusKonum;

        // private ICollection<Bilet> biletler;
        public UcusKonum UcusKonum { get => ucusKonum; set => ucusKonum = value; }

        //  private DateTime ucusTarih;
        //private UcusTarihler ucusTarihi;


     
        public string BaslangicSaat { get => baslangicSaat; set => baslangicSaat = value; }
        public string VarisSaati { get => varisSaati; set => varisSaati = value; }
        public Ucak Ucak { get => ucak; set => ucak = value; }
     
	}
}
