using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
	public class RegisterDTO
	{

		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string DisplayName { get; set; }
		[Required]
		[Phone]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+]).*$",
		ErrorMessage = "Password must contain 1 Uppercase, 1 Lowercase, 1 Digit, 1 Special Character")]

		public string Password { get; set; }
	}
}