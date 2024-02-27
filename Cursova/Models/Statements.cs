using Cursova.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cursova.Models
{
    public class Statements
    {
        [Key]
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Type { get; set; }
        public string MainInfo { get; set; }
        public string MoreDetail { get; set; }
        public string? ImageUrl { get; set; }
        public Status Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
