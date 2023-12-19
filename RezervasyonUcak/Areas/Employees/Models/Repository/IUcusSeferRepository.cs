using RezervasyonUcak.Areas.Employees.Models;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
    public interface IUcusSeferRepository
    {

        void addUcusSefer(UcusSefer sefer);
        void updateUcusSefer(UcusSefer sefer);
        void deleteUcusSefer(UcusSefer sefer);

        void getUcusSefer(UcusSefer sefer);
        List<UcusSefer> getAllUcusSefer();

        List<UcusKonum> getAllUcusSeferKonum();


    }
}
