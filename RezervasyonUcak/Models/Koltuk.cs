using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Models
{
    public class Koltuk
    {
        [Key]

        private int koltukId;
        private UcakModel ucakModel;
        private bool doluMu;



        public UcakModel UcakModel { get => ucakModel; set => ucakModel = value; }
        public bool DoluMu { get => doluMu; set => doluMu = value; }
        public int KoltukId { get => koltukId; set => koltukId = value; }
    }
}
