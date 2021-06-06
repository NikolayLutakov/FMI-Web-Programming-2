using System.ComponentModel.DataAnnotations;

namespace SampleLogin.ViewModels.Users
{
    public class CreateVM
    {
        [Required(ErrorMessage = "*This field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*This field is required")]
        public string LastName { get; set; }
    }
}
