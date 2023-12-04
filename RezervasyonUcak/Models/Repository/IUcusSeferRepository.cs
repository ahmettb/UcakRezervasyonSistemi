namespace RezervasyonUcak.Models.Repository
{
    public interface IUcusSeferRepository
    {

        void addUcusSefer(UcusSefer sefer);
        void updateUcusSefer(UcusSefer sefer);
        void deleteUcusSefer(UcusSefer sefer);

        void getUcusSefer(UcusSefer sefer);
        void getAllUcusSefer(UcusSefer sefer);



    }
}
