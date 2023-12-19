using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Models;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
    public interface ImusteriRepository
    {

        void saveEmployee(Musteri musteri);
        void getEmployeeByUsernameOrMail(string usernameOrMail);
        ICollection<Musteri> getAllEmployee(string usernameOrMail);

        void deleteEmployee(Musteri musteri);
        void updateEmployee(Musteri musteri);
        bool existByUsername(string username);
        bool existByEmail(string email);


        User getEmployeeByUsernameAndPassword(string mail, string password);
    }
}

