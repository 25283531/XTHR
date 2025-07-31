using System;
using System.Collections.Generic;
using System.Linq;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 验证结果类
    /// </summary>
    public class ValidationResult
    {
        private readonly List<string> _errors;
        private readonly List<string> _warnings;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ValidationResult()
        {
            _errors = new List<string>();
            _warnings = new List<string>();
        }

        /// <summary>
        /// 是否验证成功（无错误）
        /// </summary>
        public bool IsValid => !_errors.Any();

        /// <summary>
        /// 是否有警告
        /// </summary>
        public bool HasWarnings => _warnings.Any();

        /// <summary>
        /// 错误列表
        /// </summary>
        public IReadOnlyList<string> Errors => _errors.AsReadOnly();

        /// <summary>
        /// 警告列表
        /// </summary>
        public IReadOnlyList<string> Warnings => _warnings.AsReadOnly();

        /// <summary>
        /// 错误数量
        /// </summary>
        public int ErrorCount => _errors.Count;

        /// <summary>
        /// 警告数量
        /// </summary>
        public int WarningCount => _warnings.Count;

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="error">错误信息</param>
        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error) && !_errors.Contains(error))
            {
                _errors.Add(error);
            }
        }

        /// <summary>
        /// 添加多个错误信息
        /// </summary>
        /// <param name="errors">错误信息列表</param>
        public void AddErrors(IEnumerable<string> errors)
        {
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(error);
                }
            }
        }

        /// <summary>
        /// 添加警告信息
        /// </summary>
        /// <param name="warning">警告信息</param>
        public void AddWarning(string warning)
        {
            if (!string.IsNullOrWhiteSpace(warning) && !_warnings.Contains(warning))
            {
                _warnings.Add(warning);
            }
        }

        /// <summary>
        /// 添加多个警告信息
        /// </summary>
        /// <param name="warnings">警告信息列表</param>
        public void AddWarnings(IEnumerable<string> warnings)
        {
            if (warnings != null)
            {
                foreach (var warning in warnings)
                {
                    AddWarning(warning);
                }
            }
        }

        /// <summary>
        /// 合并其他验证结果
        /// </summary>
        /// <param name="other">其他验证结果</param>
        public void Merge(ValidationResult other)
        {
            if (other != null)
            {
                AddErrors(other.Errors);
                AddWarnings(other.Warnings);
            }
        }

        /// <summary>
        /// 清空所有错误和警告
        /// </summary>
        public void Clear()
        {
            _errors.Clear();
            _warnings.Clear();
        }

        /// <summary>
        /// 清空错误信息
        /// </summary>
        public void ClearErrors()
        {
            _errors.Clear();
        }

        /// <summary>
        /// 清空警告信息
        /// </summary>
        public void ClearWarnings()
        {
            _warnings.Clear();
        }

        /// <summary>
        /// 获取所有错误信息的字符串表示
        /// </summary>
        /// <param name="separator">分隔符，默认为换行符</param>
        /// <returns>错误信息字符串</returns>
        public string GetErrorsString(string separator = "\n")
        {
            return string.Join(separator, _errors);
        }

        /// <summary>
        /// 获取所有警告信息的字符串表示
        /// </summary>
        /// <param name="separator">分隔符，默认为换行符</param>
        /// <returns>警告信息字符串</returns>
        public string GetWarningsString(string separator = "\n")
        {
            return string.Join(separator, _warnings);
        }

        /// <summary>
        /// 获取所有消息（错误+警告）的字符串表示
        /// </summary>
        /// <param name="separator">分隔符，默认为换行符</param>
        /// <returns>所有消息字符串</returns>
        public string GetAllMessagesString(string separator = "\n")
        {
            var allMessages = new List<string>();
            
            if (_errors.Any())
            {
                allMessages.Add("错误:");
                allMessages.AddRange(_errors.Select(e => $"  - {e}"));
            }
            
            if (_warnings.Any())
            {
                if (allMessages.Any())
                    allMessages.Add("");
                allMessages.Add("警告:");
                allMessages.AddRange(_warnings.Select(w => $"  - {w}"));
            }
            
            return string.Join(separator, allMessages);
        }

        /// <summary>
        /// 创建成功的验证结果
        /// </summary>
        /// <returns>成功的验证结果</returns>
        public static ValidationResult Success()
        {
            return new ValidationResult();
        }

        /// <summary>
        /// 创建包含错误的验证结果
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns>包含错误的验证结果</returns>
        public static ValidationResult Error(string error)
        {
            var result = new ValidationResult();
            result.AddError(error);
            return result;
        }

        /// <summary>
        /// 创建包含多个错误的验证结果
        /// </summary>
        /// <param name="errors">错误信息列表</param>
        /// <returns>包含错误的验证结果</returns>
        public static ValidationResult FromErrors(params string[] errors)
        {
            var result = new ValidationResult();
            result.AddErrors(errors);
            return result;
        }

        /// <summary>
        /// 创建包含警告的验证结果
        /// </summary>
        /// <param name="warning">警告信息</param>
        /// <returns>包含警告的验证结果</returns>
        public static ValidationResult Warning(string warning)
        {
            var result = new ValidationResult();
            result.AddWarning(warning);
            return result;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            if (IsValid && !HasWarnings)
                return "验证成功";
            
            var parts = new List<string>();
            
            if (!IsValid)
                parts.Add($"错误: {ErrorCount}个");
            
            if (HasWarnings)
                parts.Add($"警告: {WarningCount}个");
            
            return string.Join(", ", parts);
        }
    }
}