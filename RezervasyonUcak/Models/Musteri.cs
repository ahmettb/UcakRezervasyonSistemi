namespace RezervasyonUcak.Models
{
    public class Musteri
    {

        private int id;
        private string name;
        private string surname;
        private string mail;
        private string password;

        private ICollection<Bilet> biletler;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public ICollection<Bilet> Biletler { get => biletler; set => biletler = value; }
    }
}
