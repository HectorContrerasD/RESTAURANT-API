using FluentValidation.Results;

namespace Restaurant.Api.Services.Utils
{
    public static class PayloadUtils
    {
        public static IDictionary<string, string[]> ToPayload(this IEnumerable<ValidationFailure> failures)
        {
            return failures.GroupBy(x => x.PropertyName, y => y.ErrorMessage).ToDictionary(i => i.Key, j => j.ToArray());
        }
    }
}
