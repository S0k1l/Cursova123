using System.ComponentModel.DataAnnotations;

namespace Cursova.ViewModels
{
    public class CreateStatementsViewModel
    {
        [Required]
        public string MainInfo { get; set; }
        [Required]
        public string MoreDetail { get; set; }
        [Required]
        [Display(Name = ("Прикрипіть фото"))]
        public IFormFile Image { get; set; }
    }
}
