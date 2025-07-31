using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 员工信息实体类
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 员工ID（主键）
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 员工工号（唯一标识）
        /// </summary>
        [Required(ErrorMessage = "员工工号不能为空")]
        [StringLength(20, ErrorMessage = "员工工号长度不能超过20个字符")]
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Required(ErrorMessage = "员工姓名不能为空")]
        [StringLength(50, ErrorMessage = "员工姓名长度不能超过50个字符")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required(ErrorMessage = "所属部门不能为空")]
        [StringLength(100, ErrorMessage = "部门名称长度不能超过100个字符")]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 职位
        /// </summary>
        [Required(ErrorMessage = "职位不能为空")]
        [StringLength(100, ErrorMessage = "职位名称长度不能超过100个字符")]
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// 职级
        /// </summary>
        [StringLength(20, ErrorMessage = "职级长度不能超过20个字符")]
        public string? JobGrade { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        [Required(ErrorMessage = "入职日期不能为空")]
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [StringLength(18, MinimumLength = 15, ErrorMessage = "身份证号码长度应为15-18位")]
        [RegularExpression(@"^[1-9]\d{5}(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$|^[1-9]\d{5}\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}$", 
            ErrorMessage = "身份证号码格式不正确")]
        public string? IDNumber { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [StringLength(100, ErrorMessage = "联系方式长度不能超过100个字符")]
        [Phone(ErrorMessage = "联系方式格式不正确")]
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 获取员工显示名称（工号 - 姓名）
        /// </summary>
        public string DisplayName => $"{EmployeeCode} - {Name}";

        /// <summary>
        /// 获取工作年限
        /// </summary>
        public int WorkYears
        {
            get
            {
                var today = DateTime.Today;
                var years = today.Year - HireDate.Year;
                if (HireDate.Date > today.AddYears(-years))
                    years--;
                return Math.Max(0, years);
            }
        }

        /// <summary>
        /// 获取工作月数
        /// </summary>
        public int WorkMonths
        {
            get
            {
                var today = DateTime.Today;
                var months = (today.Year - HireDate.Year) * 12 + today.Month - HireDate.Month;
                if (HireDate.Day > today.Day)
                    months--;
                return Math.Max(0, months);
            }
        }

        /// <summary>
        /// 验证员工信息是否完整
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateEmployee()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (string.IsNullOrWhiteSpace(EmployeeCode))
                result.AddError("员工工号不能为空");

            if (string.IsNullOrWhiteSpace(Name))
                result.AddError("员工姓名不能为空");

            if (string.IsNullOrWhiteSpace(Department))
                result.AddError("所属部门不能为空");

            if (string.IsNullOrWhiteSpace(Position))
                result.AddError("职位不能为空");

            if (HireDate == default)
                result.AddError("入职日期不能为空");

            // 检查入职日期是否合理
            if (HireDate > DateTime.Today)
                result.AddError("入职日期不能晚于今天");

            if (HireDate < new DateTime(1900, 1, 1))
                result.AddError("入职日期不能早于1900年");

            // 检查身份证号码格式（如果提供）
            if (!string.IsNullOrWhiteSpace(IDNumber))
            {
                if (!IsValidIDNumber(IDNumber))
                    result.AddError("身份证号码格式不正确");
            }

            return result;
        }

        /// <summary>
        /// 验证身份证号码格式
        /// </summary>
        /// <param name="idNumber">身份证号码</param>
        /// <returns>是否有效</returns>
        private bool IsValidIDNumber(string idNumber)
        {
            if (string.IsNullOrWhiteSpace(idNumber))
                return false;

            // 18位身份证号码验证
            if (idNumber.Length == 18)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(idNumber,
                    @"^[1-9]\d{5}(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$");
            }
            // 15位身份证号码验证
            else if (idNumber.Length == 15)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(idNumber,
                    @"^[1-9]\d{5}\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}$");
            }

            return false;
        }

        /// <summary>
        /// 克隆员工对象
        /// </summary>
        /// <returns>克隆的员工对象</returns>
        public Employee Clone()
        {
            return new Employee
            {
                EmployeeID = this.EmployeeID,
                EmployeeCode = this.EmployeeCode,
                Name = this.Name,
                Department = this.Department,
                Position = this.Position,
                JobGrade = this.JobGrade,
                HireDate = this.HireDate,
                IDNumber = this.IDNumber,
                ContactInfo = this.ContactInfo,
                IsActive = this.IsActive,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate
            };
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"Employee[{EmployeeCode}]: {Name} - {Department} - {Position}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Employee other)
            {
                return EmployeeID == other.EmployeeID && 
                       EmployeeCode == other.EmployeeCode;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(EmployeeID, EmployeeCode);
        }
    }
}