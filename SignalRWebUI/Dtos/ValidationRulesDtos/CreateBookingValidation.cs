using FluentValidation;
using SignalRWebUI.Dtos.BookingDtos;

namespace SignalRWebUI.ValidationRules
{
    public class CreateBookingValidation : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş bırakılamaz")
                .MinimumLength(5).WithMessage("En az 5 karakter giriniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter giriniz");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon alanı boş bırakılamaz");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Mail alanı boş bırakılamaz")
                .EmailAddress().WithMessage("Geçerli mail giriniz");

            RuleFor(x => x.PersonCount)
                .NotEmpty().WithMessage("Kişi sayısı boş bırakılamaz");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarih boş bırakılamaz");
        }
    }
}