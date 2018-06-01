using FluentValidation;
using NoCreamCheesePls.Api.Models.Command;

namespace NoCreamCheesePls.Business.Validators
{
  public class CreateShoppingListItemValidator : AbstractValidator<CreateShoppingListItem>
  {
    public CreateShoppingListItemValidator()
    {
      RuleFor(x => x.ShoppingListId).NotEmpty().WithMessage("ShoppingListId is required");
      RuleFor(x => x.Text).NotNull().MinimumLength(1).MaximumLength(255).WithMessage("Text must be between 1 and 255 characters in length");
    }
  }
}
