using System;
using System.ComponentModel.DataAnnotations;

namespace csharp_email.Models
{
	public class Login
	{
		[Key]
		public int LUID {get;set;}

		[Required(ErrorMessage="Email is required!")]
		public string LogEmail {get;set;}

		[Required(ErrorMessage="Password is required!")]
		[DataType(DataType.Password)]
		public string LogPassword {get;set;}
	}
}