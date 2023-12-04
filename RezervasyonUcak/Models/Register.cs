﻿using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Models
{
    public class Register
    {

        private string role;


        [Display(Name ="İsim")]
        [Required(ErrorMessage = "İsim Boş Bırakılamaz")]
        public string Name { get; set; }


        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "Soyisim Boş Bırakılamaz")]
        public string Surname { get; set; }


        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Soyisim Boş Bırakılamaz")]
        public string Username { get; set; }


        [Display(Name = "Mail")]
        [EmailAddress(ErrorMessage = "Geçerli bir mail giriniz")]
       [Required(ErrorMessage = "E-Mail Boş Bırakılamaz")]
           public string Email { get; set; }


        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        public string Password { get; set; }
        public string Role { get => role; set => role = value; }
    }
}
