using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class InternValidator: AbstractValidator<Intern>
	{
        public InternValidator()
        {
            RuleFor(x => x.Mail).EmailAddress().WithMessage("Girilen Mail, Mail Adresi Türünde Değil!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad ve Soyad Bilgisi Boş Olmaz!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon Bilgisi Boş Olmaz!");

        }

    }
}
