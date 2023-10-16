using FluentValidation;

namespace Application.Features.CommonLookups.Commands;
public class CreateCommonLookupValidator : AbstractValidator<CreateCommonLookupCommand>
{
    public CreateCommonLookupValidator()
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .NotNull().WithMessage("Code can not ");

        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull().WithMessage("Name can not be null");

        RuleFor(e => e.NameBN)
            .NotEmpty()
            .NotNull().WithMessage("Name Bangla can not be null");
    }
}
