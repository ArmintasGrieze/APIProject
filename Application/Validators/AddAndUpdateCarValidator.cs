using APIdemo.Models;
using FluentValidation;

namespace ProjectAPI.Validators
{
    public class AddAndUpdateCarValidator : AbstractValidator<Car>
    {
        public AddAndUpdateCarValidator()
        {
            RuleFor(c => c.ReleaseYear).NotEmpty();
            RuleFor(c => c.Model).NotEmpty().Length(1,30);
            RuleFor(c => c.Name).NotEmpty().Length(1, 30);
            RuleFor(c => c.Color).NotEmpty().Length(1, 30);
        }
    }
}
