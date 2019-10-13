using System;
using System.ComponentModel.DataAnnotations;

namespace csharp_email.Models
{
	public class Mail
	{
		[Key]
		public int MailId {get;set;}

		public string Subject {get;set;}

		[Required(ErrorMessage="Receiver is required")]
		public string Receiver {get;set;}

		[Required(ErrorMessage="Sender is required")]
		public string Sender {get;set;}

		public string Content {get;set;}

		public bool Read {get;set;} = false;

		public bool Deleted {get;set;} = false;

		public bool PermDeleted {get;set;} = false;

		public DateTime CreatedAt {get;set;} = DateTime.Now;
		public DateTime UpdatedAt {get;set;} = DateTime.Now;
	}
}