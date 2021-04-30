using System.ComponentModel.DataAnnotations;

namespace TestNg.Models.identity
{
    public class RegisterUSerRequestModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }

        public string phone { get; set; }
    }
}
