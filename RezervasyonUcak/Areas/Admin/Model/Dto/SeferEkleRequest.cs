namespace RezervasyonUcak.Areas.Admin.Model.Dto
{
    public class SeferEkleRequest
    {
        public string bKonum { get; set; }
        public string vKonum { get; set; }
        public DateTime date { get; set; }
        public int selectedFirmaId { get; set; }
        public double fiyat { get; set; }

        public int selectedUcakId { get; set; }
        public string selectedTime { get; set; }
        public string selectedTime2 { get; set; }
    }
}