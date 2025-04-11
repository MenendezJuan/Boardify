namespace Boardify.Application.Utils
{
    public class ValidatorsUtils
    {
        #region Messages
        public static string RequiredField = "El campo {PropertyName} es requerido";
        public static string MaximumLengthMessage = "El campo {PropertyName} debe tener menos de {MaxLength} caracteres";
        public static string FirstLetterCap = "El campo {PropertyName} debe comenzar con mayúsculas";
        public static string EmailMessage = "El campo {PropertyName} debe ser un email válido";
        public static string LengthMessage = "El campo {PropertyName} debe contar con {MaxLength} caracteres";
        public static string GreaterThanMessage = "El campo {PropertyName} debe ser mayor a {GreaterThan}";
        public static string LessThanMessage = "El campo {PropertyName} debe tener menos de {MaxLength} caracteres";
        public static string MinimumLengthMessage = "El campo {PropertyName} debe tener al menos {MinLength} caracteres";
        public static string NotExistingMessage = "No se ha encontrado el elemento {PropertyName}";
        public static string InvalidVisibilityMessage = "Invalid visibility value.";
        public static string InvalidDateMessage = "El campo {PropertyName} es inválido.";
        public static string DateRangeMessage = "La fecha de inicio debe ser anterior o igual a la fecha de vencimiento.";
        public static string FutureDateMessage = "La fecha de vencimiento debe ser hoy o en el futuro.";
        public static string InvalidPriorityMessage = "El valor de prioridad no es válido.";
        public static string InvalidCardOrderMessage = "El orden de tarjetas es inválido.";
        public static string InvalidColumnOrderMessage = "El orden de columnas es inválido.";

        public static string PasswordUppercaseMessage = "El campo {PropertyName} debe contener al menos una letra mayúscula.";
        public static string PasswordLowercaseMessage = "El campo {PropertyName} debe contener al menos una letra minúscula.";
        public static string PasswordDigitMessage = "El campo {PropertyName} debe contener al menos un número.";
        public static string PasswordSpecialCharMessage = "El campo {PropertyName} debe contener al menos un carácter especial.";

        #endregion 

        #region Methods
        public static string NotFoundMessage(string entity) => $"No se encontró el {entity}.";

        public static bool BeAllowedEmailDomain(string email)
        {
            var allowedDomains = new[] { "gmail.com", "hotmail.com", "hotmail.es", "cedeira.com.ar" };
            var domain = email.Split('@').LastOrDefault();
            return allowedDomains.Contains(domain);
        }

        public static bool BeAValidDate(DateTime? date)
        {
            return date.HasValue && date.Value != DateTime.MinValue;
        }
        #endregion
    }
}