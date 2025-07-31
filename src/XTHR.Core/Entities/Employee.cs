using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 员工实体
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(18)]
        public string IdCard { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 户籍地址
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? ResignationDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(50)]
        public string EmergencyContact { get; set; } = string.Empty;

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        [MaxLength(20)]
        public string EmergencyPhone { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新人
        /// </summary>
        [MaxLength(50)]
        public string UpdatedBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 员工状态枚举
    /// </summary>
    public enum EmployeeStatus
    {
        /// <summary>
        /// 在职
        /// </summary>
        Active = 0,

        /// <summary>
        /// 试用期
        /// </summary>
        Probation = 1,

        /// <summary>
        /// 离职
        /// </summary>
        Resigned = 2,

        /// <summary>
        /// 退休
        /// </summary>
        Retired = 3
    }
}