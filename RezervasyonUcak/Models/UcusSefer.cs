using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Models
{
    public class UcusSefer
    {
        [Key]
        public int UcusId { get; set; }

        private string baslangicSaat;
        private string varisSaati;
        private Ucak ucak;
        private ICollection<Bilet> biletler;
        private string baslangicKonum;
        private string varisKonum;

        public string BaslangicSaat { get => baslangicSaat; set => baslangicSaat = value; }
        public string VarisSaati { get => varisSaati; set => varisSaati = value; }
        public Ucak Ucak { get => ucak; set => ucak = value; }
        public ICollection<Bilet> Biletler { get => biletler; set => biletler = value; }
        public string BaslangicKonum { get => baslangicKonum; set => baslangicKonum = value; }
        public string VarisKonum { get => varisKonum; set => varisKonum = value; }
    }
}
