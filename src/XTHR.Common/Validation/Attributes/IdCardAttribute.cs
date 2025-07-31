using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace XTHR.Common.Validation.Attributes
{
    /// <summary>
    /// 验证中国身份证号码（18位）的有效性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class IdCardAttribute : ValidationAttribute
    {
        public IdCardAttribute()
        {
            ErrorMessage = "无效的身份证号码格式。";
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true; // 空值不验证，由 [Required] 特性处理
            }

            var idCard = value.ToString();

            if (!Regex.IsMatch(idCard, @"^\d{17}(\d|X)$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            // 校验码验证
            var weights = new[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            var checkCodes = new[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += (idCard[i] - '0') * weights[i];
            }

            var checkCode = checkCodes[sum % 11];

            return idCard[17].ToString().ToUpper() == checkCode.ToString();
        }
    }
}