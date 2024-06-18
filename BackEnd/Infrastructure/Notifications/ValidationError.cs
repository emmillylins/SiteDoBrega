namespace Infrastructure.Notifications
{
    public class ValidationError
    {
        public string ErrorMessage { get; set; }

        public ValidationError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    public class MultipleValidationExceptions : Exception
    {
        public List<ValidationError> ValidationErrors { get; private set; }

        public MultipleValidationExceptions(List<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
