using FluentValidation.Results;

namespace UnitTest.Core
{
    public class Entity
    {
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; set; } = null!;
    }
}
