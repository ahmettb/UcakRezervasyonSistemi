using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class UcusKonum
    {
        [Key]
        private int id;


        private List<UcusSefer> seferler;

        private DateTime ucusTarihi;
        private string baslangicKonum;
        private string varisKonum;

        private  UcusTarih tarih;

        public int Id { get => id; set => id = value; }
        public List<UcusSefer> Seferler { get => seferler; set => seferler = value; }
        public DateTime UcusTarihi { get => ucusTarihi; set => ucusTarihi = value; }
        public string BaslangicKonum { get => baslangicKonum; set => baslangicKonum = value; }
        public string VarisKonum { get => varisKonum; set => varisKonum = value; }
		public UcusTarih Tarih { get => tarih; set => tarih = value; }
	}
}
