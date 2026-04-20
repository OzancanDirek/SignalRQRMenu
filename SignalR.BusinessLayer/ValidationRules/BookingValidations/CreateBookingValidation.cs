using FluentValidation;
using SignalR.DtoLayer.BookingDto;

namespace SignalR.BusinessLayer.ValidationRules.BookingValidations
{
    public class CreateBookingValidation : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidation()
        {
            RuleFor(x=> x.Name).NotEmpty().WithMessage("İsim Alanı Boş bırakılamaz");
            RuleFor(x=> x.Phone).NotEmpty().WithMessage("Telefon Alanı Boş bırakılamaz");
            RuleFor(x=> x.Mail).NotEmpty().WithMessage("Mail Alanı Boş bırakılamaz");
            RuleFor(x=> x.PersonCount).NotEmpty().WithMessage("Kişi Adeti Boş bırakılamaz");
            RuleFor(x=> x.Date).NotEmpty().WithMessage("Randevu Tarihi Boş bırakılamaz");

            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Lütfen isim alanına az az 5 karakter giriniz")
                .MaximumLength(50).WithMessage("Lütfen isim alanına en fazla 50 karakter giriniz");

            RuleFor(x => x.Name).MaximumLength(500).WithMessage("Lütfen açıklama alanına en fazla 500 karakter giriniz");

            RuleFor(x=> x.Mail).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
        }
    }
}
