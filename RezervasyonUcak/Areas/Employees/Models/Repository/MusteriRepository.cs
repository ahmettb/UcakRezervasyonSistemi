using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Models;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
    public class MusteriRepository :ImusteriRepository
    {


        readonly AppDbContext _context;

        public MusteriRepository(AppDbContext context)
        {
            _context = context;
        }
        public void deleteEmployee(Musteri musteri)
        {
            throw new NotImplementedException();
        }

        public void existByEmail(string email)
        {
           // return _context.Musteri.Any(musteri => musteri.Mail == email);
        }

        public bool existByUsername(string username)
        {

            return _context.Musteri.Any(musteri => musteri.User.Surname == username);

        }

        public ICollection<Musteri> getAllEmployee(string usernameOrMail)
        {
            throw new NotImplementedException();
        }

        public void getEmployeeByUsernameOrMail(string usernameOrMail)
        {
            throw new NotImplementedException();
        }

        public void saveEmployee(Musteri musteri)
        {
            throw new NotImplementedException();
        }

        public void updateEmployee(Musteri musteri)
        {
            throw new NotImplementedException();
        }

        public User getEmployeeByUsernameAndPassword(string mail, string password)
        {


            User user = (from _user in _context.Users
                                where _user.Email == mail &&
                                _user.Password == password
                                select _user).FirstOrDefault();


            return user;
        }



        ICollection<Musteri> ImusteriRepository.getAllEmployee(string usernameOrMail)
        {
            throw new NotImplementedException();
        }

        bool ImusteriRepository.existByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
