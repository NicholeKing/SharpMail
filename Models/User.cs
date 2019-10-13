using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_email.Models
{
	public class User
	{
		[Key]
		public int UserId {get;set;}

		[Required(ErrorMessage="EName is required!")]
		[MinLength(2, ErrorMessage="EName must be at least 2 characters in length!")]
		public string EName {get;set;}

		[Required(ErrorMessage="Email is required!")]
		[EmailAddress]
		public string Email {get;set;}

		[Required(ErrorMessage="Password is required!")]
		[MinLength(8, ErrorMessage="Password must be at least 8 characters in length!")]
		[RegularExpression(@"^[a-zA-Z]+[!@#$%^&*]",ErrorMessage="Password must start with a letter and must contain at least one special character")]
		[DataType(DataType.Password)]
		public string Password {get;set;}

		public DateTime CreatedAt {get;set;} = DateTime.Now;
		public DateTime UpdatedAt {get;set;} = DateTime.Now;

		[NotMapped]
		[Required(ErrorMessage="Must confirm password!")]
		[Compare("Password", ErrorMessage="Passwords must match!")]
		[DataType(DataType.Password)]
		public string Confirm {get;set;}
	}
}
