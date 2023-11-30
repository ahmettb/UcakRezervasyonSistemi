using RezervasyonUcak.Models.Dto.DtoRequest;

namespace RezervasyonUcak.Models.Repository
{
    public class MusteriRepository : ImusteriRepository
    {


        readonly AppDbContext _context;

        public MusteriRepository(AppDbContext context) {
        _context = context;
        }
        public void deleteEmployee(MusteriRequest musteri)
        {
            throw new NotImplementedException();
        }

        public bool existByEmail(string email)
        {
            return _context.Musteri.Any(musteri => musteri.Mail == email);
        }

        public bool existByUsername(string username)
        {

            return _context.Musteri.Any(musteri => musteri.Surname == username );

        }

        public ICollection<MusteriResponse> getAllEmployee(string usernameOrMail)
        {
            throw new NotImplementedException();
        }

        public void getEmployeeByUsernameOrMail(string usernameOrMail)
        {
            throw new NotImplementedException();
        }

        public void saveEmployee(MusteriRequest musteri)
        {
            throw new NotImplementedException();
        }

        public void updateEmployee(MusteriRequest musteri)
        {
            throw new NotImplementedException();
        }
    }
}
