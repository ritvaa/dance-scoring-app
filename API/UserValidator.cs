using API.Contracts;
using FluentValidation;

namespace UserControllerExample
{
    public class UserValidator : AbstractValidator<UserWriteModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Name is required.");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address.");
            RuleFor(user => user.Password).NotEmpty().MinimumLength(8);
        }
    }
}