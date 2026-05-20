using FluentValidation;
using KursPortal.DTOs.DTOs.ContactDtos;

namespace KursPortal.API.Validators
{
    public class ContactValidator : AbstractValidator<CreateContactDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad və soyad boş ola bilməz")
                .MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır")
                .MaximumLength(50).WithMessage("Maksimum 50 simvol ola bilər");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Düzgün email daxil edin");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlıq boş ola bilməz")
                .MaximumLength(100);

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Mesaj boş ola bilməz")
                .MinimumLength(10)
                .MaximumLength(1000);

        }
    }
}
