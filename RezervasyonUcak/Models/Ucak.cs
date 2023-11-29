using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;


namespace RezervasyonUcak.Models
{
    public class Ucak
    {
        [Key] 
        private int ucakId;

        public int firmaId;

        private Firma firma;

        private int modelId;

        private ICollection<UcakModel> ucakModel;



        public Firma Firma { get => firma; set => firma = value; }
        public ICollection<UcakModel> UcakModel { get => ucakModel; set => ucakModel = value; }
        public int UcakId { get => ucakId; set => ucakId = value; }
    }
}
