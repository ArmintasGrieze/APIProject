using FluentValidation;
using ProjectAPI.Models;

namespace ProjectAPI.Validators
{
    public class RegisterAndLogInValidator : AbstractValidator<User>
    {
        public RegisterAndLogInValidator()
        {
            RuleFor(u => u.Username).NotEmpty().Length(4, 50);
            RuleFor(u => u.Password).NotEmpty().Length(4, 50);
        }
    }
}
