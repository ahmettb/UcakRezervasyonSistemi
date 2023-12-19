using RezervasyonUcak.Models;
using System.Drawing;

namespace RezervasyonUcak.Areas.Admin.Model.Dto
{
    public class UserRequest
    {

        private string name;
        private string username;
        private string email;
        private string password;
        private string surname;
        private Role role;

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Surname { get => surname; set => surname = value; }
        public Role Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
    }
}
