using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Validation
{
    /// <summary>
    /// 使用 DataAnnotations 对对象进行验证的验证器。
    /// </summary>
    /// <typeparam name="T">要验证的对象类型。</typeparam>
    public class DataAnnotationsValidator<T> : IValidator<T>
    {
        /// <summary>
        /// 验证指定的对象实例。
        /// </summary>
        /// <param name="instance">要验证的对象实例。</param>
        /// <returns>验证结果。</returns>
        public ValidationResult Validate(T instance)
        {
            var validationContext = new ValidationContext(instance ?? throw new ArgumentNullException(nameof(instance)));
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var result = new ValidationResult();

            var isValid = Validator.TryValidateObject(instance, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    if (validationResult.MemberNames != null)
                    {
                        foreach (var memberName in validationResult.MemberNames)
                        {
                            result.AddError(memberName, validationResult.ErrorMessage ?? "验证失败");
                        }
                    }
                    else
                    {
                        result.AddError(string.Empty, validationResult.ErrorMessage ?? "验证失败");
                    }
                }
            }

            return result;
        }
    }
}