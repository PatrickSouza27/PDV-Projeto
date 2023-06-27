using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServerAPI.Extension
{
    public static class ExtensionError
    {
        public static List<string> GetError(this ModelStateDictionary model)
        {
            var errors = new List<string>();
            foreach(var value in model.Values)
            {
                errors.AddRange(value.Errors.Select(value => value.ErrorMessage));
            }
            return errors;
        }
    }
}
