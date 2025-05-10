using FluentValidation;
using Models.RequestModel;
using System.Text.RegularExpressions;
namespace BusinessLayers.Validationns
{
    public class ValidPersonelLogin : AbstractValidator<RequestLogin>
    {
        public ValidPersonelLogin()
        {
            RuleFor(x => x.Username).Length(1, 6).WithMessage("Maximum 6 Karakter Olabilir.");
            RuleFor(x => x.Password).Length(1, 40).WithMessage("Maximum 40 Karakter Olabilir.");
            RuleFor(x => x.Password).Must(IsValidPassword).WithMessage("Güvensiz Şifre");
        }

        public static bool IsValidPassword(string plainText)
        {
            Regex regex = new Regex(@"^(.{0,7}|[^0-9]*|[^A-Z])$");
            Match match = regex.Match(plainText);
            return match.Success;
        }
    }
}