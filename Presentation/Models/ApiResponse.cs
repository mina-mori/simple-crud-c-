namespace Presentation.Model
{
    public class ApiResponseModel
    {
        public ApiResponseModel(object? data, bool isValid = true, string[]? errorMessages = null)
        {
            Data = data;
            IsValid = isValid;
            ValidationMessages = errorMessages;
        }

        public ApiResponseModel((bool isValid, string[]? errors, int? id) result)
        {
            Data = new { result.id };
            IsValid = result.isValid;
            ValidationMessages = result.errors;
        }


        public bool IsValid { get; set; }

        public string[]? ValidationMessages { get; set; }

        public object? Data { get; set; } = new { };
    }
}
