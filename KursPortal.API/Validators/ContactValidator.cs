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
                .MaximumLength(100).WithMessage("Başlıq maksimum 100 simvol olmalıdır");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Mesaj boş ola bilməz")
                .MinimumLength(10).WithMessage("Mesaj minimum 10 simvol olmalıdır")
                .MaximumLength(1000).WithMessage("Mesaj maksimum 1000 simvol olmalıdır");

        }
    }
}
