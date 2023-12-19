using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class UcakModel
    {
        [Key]
        private int ucakModelId;

        private string modelNumara;
        private int koltukSayisi;
        private ICollection<Koltuk> koltuklar;

        [ForeignKey("Ucak")]
        public int UcakFK { get; set; }
        private Ucak ucak;

        public string ModelNumara { get => modelNumara; set => modelNumara = value; }
        public int KoltukSayisi { get => koltukSayisi; set => koltukSayisi = value; }
        public ICollection<Koltuk> Koltuklar { get => koltuklar; set => koltuklar = value; }
        public Ucak Ucak { get => ucak; set => ucak = value; }
        public int UcakModelId { get => ucakModelId; set => ucakModelId = value; }
    }
}
