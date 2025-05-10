using FluentValidation.Results;

namespace BusinessLayers
{
    public static class ValidateToArrayList
    {
        public static List<string> Convert(IList<ValidationFailure> failures)
        {
            List<string> result = new List<string>();
            foreach (var item in failures)
            {
                result.Add(item.PropertyName + " : " + item.ErrorMessage);
            }
            return result;
        }
    }
}
