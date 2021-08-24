using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class ContactValidator : AbstractValidator<Contact>
	{
		public ContactValidator()
		{
			RuleFor(x => x.UserMail).NotEmpty().WithMessage("Mail Adresini Boş Geçemezsiniz!");
			RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu  adını boş geçemezsiniz.");
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adını boş geçemezsiniz.");
			RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karater girişi yapınız.");
			RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Lütfen en az 3 karater girişi yapınız.");
			RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen en fazla 100 karater girişi yapınız.");
		}
	}
}
