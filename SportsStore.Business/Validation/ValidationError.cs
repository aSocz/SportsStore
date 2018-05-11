namespace SportsStore.Business.Validation
{
    public class ValidationError
    {
        public ValidationError(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public string ErrorMessage { get; }
        public string PropertyName { get; }
    }
}