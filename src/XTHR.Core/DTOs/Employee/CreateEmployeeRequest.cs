using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 创建员工请求DTO
    /// </summary>
    public class CreateEmployeeRequest
    {
        /// <summary>
        /// 员工姓名
        /// </summary>
        [Required(ErrorMessage = "员工姓名不能为空")]
        [StringLength(50, ErrorMessage = "员工姓名不能超过50个字符")]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 工号
        /// </summary>
        [Required(ErrorMessage = "工号不能为空")]
        [StringLength(20, ErrorMessage = "工号不能超过20个字符")]
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门ID
        /// </summary>
        [Required(ErrorMessage = "部门不能为空")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [Required(ErrorMessage = "职位不能为空")]
        [StringLength(50, ErrorMessage = "职位不能超过50个字符")]
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// 入职日期
        /// </summary>
        [Required(ErrorMessage = "入职日期不能为空")]
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        [Required(ErrorMessage = "员工状态不能为空")]
        public string Status { get; set; } = "Active";

        /// <summary>
        /// 联系电话
        /// </summary>
        [Phone(ErrorMessage = "请输入有效的电话号码")]
        [StringLength(20, ErrorMessage = "联系电话不能超过20个字符")]
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailAddress(ErrorMessage = "请输入有效的邮箱地址")]
        [StringLength(100, ErrorMessage = "邮箱不能超过100个字符")]
        public string? Email { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(18, ErrorMessage = "身份证号不能超过18个字符")]
        public string? IdCard { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(200, ErrorMessage = "地址不能超过200个字符")]
        public string? Address { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [StringLength(50, ErrorMessage = "紧急联系人不能超过50个字符")]
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        [Phone(ErrorMessage = "请输入有效的紧急联系电话")]
        [StringLength(20, ErrorMessage = "紧急联系电话不能超过20个字符")]
        public string? EmergencyPhone { get; set; }
    }
}