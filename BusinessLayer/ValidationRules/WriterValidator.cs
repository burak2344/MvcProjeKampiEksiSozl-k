using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class WriterValidator : AbstractValidator<Writer>
	{
		public WriterValidator()
		{
			RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adını boş geçemezsiniz.");
			RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karater girişi yapınız.");
			RuleFor(x => x.WriterSurnam).MaximumLength(50).WithMessage("Lütfen en fazla 50 karater girişi yapınız.");
			RuleFor(x => x.WriterSurnam).NotEmpty().WithMessage("Yazar soy adını  boş geçemezsiniz.");
			RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmını  boş geçemezsiniz.");
			RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan kısmını  boş geçemezsiniz.");
			RuleFor(x => x.WriterSurnam).MinimumLength(2).WithMessage("Lütfen en az 2 karater girişi yapınız.");
			RuleFor(x => x.WriterSurnam).MaximumLength(50).WithMessage("Lütfen en fazla 50 karater girişi yapınız.");
			RuleFor(x => x.WriterAbout).Must(IsAboutValid).WithMessage("Hakkında kısmında en az bir defa a harfi kullanılmalıdır");
		}

		private bool IsAboutValid(string arg)
		{
			var result = arg.Contains("A") || arg.Contains("a");
			return result;
		}
	}
}
