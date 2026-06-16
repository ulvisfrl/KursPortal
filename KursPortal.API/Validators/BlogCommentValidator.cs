using FluentValidation;
using KursPortal.DTOs.DTOs.BlogCommentDtos;

namespace KursPortal.API.Validators
{
    public class BlogCommentValidator : AbstractValidator<CreateBlogCommentDto>
    {
        public BlogCommentValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad və soyad boş ola bilməz")
                .MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır")
                .MaximumLength(50).WithMessage("Maksimum 50 simvol ola bilər");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Düzgün email daxil edin");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Koment boş ola bilməz")
                .MinimumLength(10).WithMessage("Koment minimum 5 simvol olmalıdır")
                .MaximumLength(1000).WithMessage("Koment maksimum 150 simvol olmalıdır");
        }
    }
}
