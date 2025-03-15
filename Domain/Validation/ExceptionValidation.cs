namespace Domain.Validation
{
    public class ExceptionValidation : Exception
    {
        public ExceptionValidation(string error) : base(error) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new ExceptionValidation(error);
        }
    }
}
