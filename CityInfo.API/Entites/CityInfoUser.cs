using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Entites
{
    public class CityInfoUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string? password { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string City { get; set; }
    }
}
