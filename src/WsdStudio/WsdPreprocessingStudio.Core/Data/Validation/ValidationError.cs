namespace WsdPreprocessingStudio.Core.Data.Validation
{
    public class ValidationError
    {
        public string ErrorMessage { get; }
        public string MemberName { get; }
        
        public ValidationError(string errorMessage, string memberName = null)
        {
            ErrorMessage = errorMessage;
            MemberName = memberName;
        }

        public static ValidationError NotNullOrEmpty(string memberName)
        {
            return new ValidationError($"Member {memberName} cannot be null or empty.", memberName);
        }
    }
}
