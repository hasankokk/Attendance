using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementApi.Helpers;

public static class ValidationHelper
{
    public static bool ValidateModel<T> (T model, out List<ValidationResult> validationResults)
    {
        var validationContext = new ValidationContext(model);
        validationResults = [];
        return Validator.TryValidateObject(model, validationContext, validationResults, true);
    }
}