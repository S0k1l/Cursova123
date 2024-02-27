using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cursova.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = ("Електрона пошта"))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = ("Номер телефону"))]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = ("Ім'я"))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = ("По батькові"))]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = ("Прізвище"))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = ("Вулиця прожтвання"))]
        public string Street { get; set; }

        [Required]
        [Display(Name = ("Будинок"))]
        public string Buildind { get; set; }

        [Required]
        [Display(Name = ("Квартира"))]
        public string Apartment { get; set; }

        [Required]
        [Display(Name = ("Документи які підтверджують особу"))]
        public IFormFile Image { get; set; }
    }
}
