using Cursova.Data.Enum;
using Cursova.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cursova.ViewModels
{
    public class AllStatementInfoViewModel
    {
        public string UserFullName { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string MainInfo { get; set; }
        public string MoreDetail { get; set; }
        public string ImageUrl { get; set; }
        public Status Status { get; set; }
        public DateTime DateTime { get; set; }
        public List<AppUser>? AppUsers { get; set; }
    }
}
