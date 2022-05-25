using FluentValidation;

namespace TodoApp.GraphQL.DTO
{
    public class StorageSwitchInputValidator : AbstractValidator<StorageSwitchInput>
    {
        public StorageSwitchInputValidator()
        {
            RuleFor(c => c.Type).Matches("xml|mssql");
        }
    }
}
