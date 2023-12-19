using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class Bilet
    {
        [Key]

        private int id;
        private Musteri musteri;
        private DateTime kesimTarihi;
        private bool iptalMi;

        private int ucusSeferId;
        private UcusSefer ucusSefer;
        private double biletFiyat;

        public UcusSefer UcusSefer { get => ucusSefer; set => ucusSefer = value; }
        public int UcusSeferId { get => ucusSeferId; set => ucusSeferId = value; }
        public int Id { get => id; set => id = value; }
        public Musteri Musteri { get => musteri; set => musteri = value; }
        public DateTime KesimTarihi { get => kesimTarihi; set => kesimTarihi = value; }
        public double BiletFiyat { get => biletFiyat; set => biletFiyat = value; }
    }
}
