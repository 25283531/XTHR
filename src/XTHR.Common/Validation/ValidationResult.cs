using System.Collections.Generic;
using System.Linq;

namespace XTHR.Common.Validation
{
    /// <summary>
    /// 表示验证操作的结果。
    /// </summary>
    public class ValidationResult
    {
        private readonly List<ValidationError> _errors = new List<ValidationError>();

        /// <summary>
        /// 获取验证错误的列表。
        /// </summary>
        public IReadOnlyCollection<ValidationError> Errors => _errors.AsReadOnly();

        /// <summary>
        /// 获取一个值，该值指示验证是否成功。
        /// </summary>
        public bool IsValid => !_errors.Any();

        /// <summary>
        /// 添加一个验证错误。
        /// </summary>
        /// <param name="propertyName">导致错误的属性名称。</param>
        /// <param name="errorMessage">错误消息。</param>
        public void AddError(string propertyName, string errorMessage)
        {
            _errors.Add(new ValidationError(propertyName, errorMessage));
        }

        /// <summary>
        /// 添加一个验证错误。
        /// </summary>
        /// <param name="error">验证错误实例。</param>
        public void AddError(ValidationError error)
        {
            _errors.Add(error);
        }

        /// <summary>
        /// 合并另一个验证结果。
        /// </summary>
        /// <param name="result">要合并的验证结果。</param>
        public void Merge(ValidationResult result)
        {
            if (result != null)
            {
                _errors.AddRange(result.Errors);
            }
        }

        /// <summary>
        /// 创建一个表示成功的验证结果。
        /// </summary>
        public static ValidationResult Success => new ValidationResult();

        /// <summary>
        /// 创建一个包含单个错误的验证结果。
        /// </summary>
        /// <param name="propertyName">属性名。</param>
        /// <param name="errorMessage">错误消息。</param>
        /// <returns>验证结果实例。</returns>
        public static ValidationResult Fail(string propertyName, string errorMessage)
        {
            var result = new ValidationResult();
            result.AddError(propertyName, errorMessage);
            return result;
        }
    }

    /// <summary>
    /// 表示单个验证错误。
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// 获取导致错误的属性名称。
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// 获取错误消息。
        /// </summary>
        public string ErrorMessage { get; }

        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName ?? string.Empty;
            ErrorMessage = errorMessage;
        }
    }
}