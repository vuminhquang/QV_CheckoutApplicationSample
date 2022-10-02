namespace PosApplication.Shared.Constants
{
    public static class ErrorConstants
    {
        public const string ErrValidation = "error.validation";
        
        //TODO:
        // var urls = server.Features.Get<IServerAddressesFeature>()!.Addresses;
        // var localUrl = urls.Single(u => u.StartsWith("https://"));

        private const string ProblemBaseUrl = "https://127.0.0.1/problem";
        public static readonly string DefaultType = $"{ProblemBaseUrl}/problem-with-message";
        public static readonly string ConstraintViolationType = $"{ProblemBaseUrl}/constraint-violation";
        public static readonly string ParametrizedType = $"{ProblemBaseUrl}/parametrized";
        public static readonly string EntityNotFoundType = $"{ProblemBaseUrl}/entity-not-found";
        public static readonly string InvalidPasswordType = $"{ProblemBaseUrl}/invalid-password";
        public static readonly string EmailAlreadyUsedType = $"{ProblemBaseUrl}/email-already-used";
        public static readonly string LoginAlreadyUsedType = $"{ProblemBaseUrl}/login-already-used";
        public static readonly string EmailNotFoundType = $"{ProblemBaseUrl}/email-not-found";
    }
}
