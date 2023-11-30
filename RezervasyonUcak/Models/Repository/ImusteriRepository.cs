using RezervasyonUcak.Models.Dto.DtoRequest;

namespace RezervasyonUcak.Models.Repository
{
    public interface ImusteriRepository
    {

        void saveEmployee(MusteriRequest musteri);
        void getEmployeeByUsernameOrMail(string usernameOrMail);
        ICollection<MusteriResponse> getAllEmployee(string usernameOrMail);

        void deleteEmployee(MusteriRequest musteri);
        void  updateEmployee(MusteriRequest musteri);
        bool existByUsername(string username);
        bool existByEmail(string email);

    }
}
