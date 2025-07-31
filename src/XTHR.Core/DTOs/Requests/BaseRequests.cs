using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 基础查询请求
    /// </summary>
    public abstract class BaseQueryRequest
    {
        /// <summary>
        /// 页码（从1开始）
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// 每页大小
        /// </summary>
        [Range(1, 1000, ErrorMessage = "每页大小必须在1-1000之间")]
        public int PageSize { get; set; } = 20;
        
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortBy { get; set; }
        
        /// <summary>
        /// 排序方向（asc/desc）
        /// </summary>
        public string SortDirection { get; set; } = "asc";
        
        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string SearchKeyword { get; set; }
        
        /// <summary>
        /// 创建开始时间
        /// </summary>
        public DateTime CreatedAtStart { get; set; }
        
        /// <summary>
        /// 创建结束时间
        /// </summary>
        public DateTime CreatedAtEnd { get; set; }
        
        /// <summary>
        /// 跳过记录数
        /// </summary>
        public int Skip => (PageNumber - 1) * PageSize;
        
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int Take => PageSize;
        
        /// <summary>
        /// 是否降序排序
        /// </summary>
        public bool IsDescending => SortDirection?.ToLower() == "desc";
    }
    
    /// <summary>
    /// 工资期间请求
    /// </summary>
    public class PayrollPeriodRequest
    {
        /// <summary>
        /// 年份
        /// </summary>
        [Required(ErrorMessage = "年份不能为空")]
        [Range(2000, 3000, ErrorMessage = "年份必须在2000-3000之间")]
        public int Year { get; set; }
        
        /// <summary>
        /// 月份
        /// </summary>
        [Required(ErrorMessage = "月份不能为空")]
        [Range(1, 12, ErrorMessage = "月份必须在1-12之间")]
        public int Month { get; set; }
        
        /// <summary>
        /// 期间描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 期间开始日期
        /// </summary>
        public DateTime StartDate => new DateTime(Year, Month, 1);
        
        /// <summary>
        /// 期间结束日期
        /// </summary>
        public DateTime EndDate => new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
        
        /// <summary>
        /// 期间标识
        /// </summary>
        public string PeriodKey => $"{Year:0000}-{Month:00}";
        
        /// <summary>
        /// 创建当前月份的工资期间
        /// </summary>
        /// <returns>当前月份工资期间</returns>
        public static PayrollPeriodRequest Current()
        {
            var now = DateTime.Now;
            return new PayrollPeriodRequest
            {
                Year = now.Year,
                Month = now.Month,
                Description = $"{now.Year}年{now.Month}月工资"
            };
        }
        
        /// <summary>
        /// 创建上个月的工资期间
        /// </summary>
        /// <returns>上个月工资期间</returns>
        public static PayrollPeriodRequest Previous()
        {
            var now = DateTime.Now.AddMonths(-1);
            return new PayrollPeriodRequest
            {
                Year = now.Year,
                Month = now.Month,
                Description = $"{now.Year}年{now.Month}月工资"
            };
        }
        
        /// <summary>
        /// 创建指定年月的工资期间
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资期间</returns>
        public static PayrollPeriodRequest Create(int year, int month)
        {
            return new PayrollPeriodRequest
            {
                Year = year,
                Month = month,
                Description = $"{year}年{month}月工资"
            };
        }
        
        public override string ToString()
        {
            return PeriodKey;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is PayrollPeriod other)
            {
                return Year == other.Year && Month == other.Month;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month);
        }
    }
    
    /// <summary>
    /// 批量操作请求基类
    /// </summary>
    public abstract class BaseBatchRequest
    {
        /// <summary>
        /// 操作的ID列表
        /// </summary>
        [Required(ErrorMessage = "ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少需要选择一个项目")]
        public List<int> Ids { get; set; } = new List<int>();
        
        /// <summary>
        /// 操作原因/备注
        /// </summary>
        [StringLength(500, ErrorMessage = "操作原因长度不能超过500个字符")]
        public string Reason { get; set; }
        
        /// <summary>
        /// 是否强制执行（忽略警告）
        /// </summary>
        public bool ForceExecute { get; set; } = false;
    }
    
    /// <summary>
    /// 日期范围请求
    /// </summary>
    public class DateRangeRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [Required(ErrorMessage = "开始日期不能为空")]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// 结束日期
        /// </summary>
        [Required(ErrorMessage = "结束日期不能为空")]
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// 验证日期范围
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateDateRange()
        {
            var result = new ValidationResult();
            
            if (StartDate > EndDate)
            {
                result.AddError("开始日期不能大于结束日期");
            }
            
            if ((EndDate - StartDate).TotalDays > 365)
            {
                result.AddError("日期范围不能超过365天");
            }
            
            return result;
        }
    }
    
    /// <summary>
    /// 导入请求基类
    /// </summary>
    public abstract class BaseImportRequest
    {
        /// <summary>
        /// 是否覆盖已存在的数据
        /// </summary>
        public bool OverwriteExisting { get; set; } = false;
        
        /// <summary>
        /// 是否跳过错误行
        /// </summary>
        public bool SkipErrors { get; set; } = true;
        
        /// <summary>
        /// 批次标识
        /// </summary>
        public string BatchId { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// 导入描述
        /// </summary>
        [StringLength(200, ErrorMessage = "导入描述长度不能超过200个字符")]
        public string Description { get; set; }
    }
    
    /// <summary>
    /// 验证结果
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid => !Errors.Any();
        
        /// <summary>
        /// 错误列表
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
        
        /// <summary>
        /// 警告列表
        /// </summary>
        public List<string> Warnings { get; set; } = new List<string>();
        
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="error">错误信息</param>
        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                Errors.Add(error);
            }
        }
        
        /// <summary>
        /// 添加警告
        /// </summary>
        /// <param name="warning">警告信息</param>
        public void AddWarning(string warning)
        {
            if (!string.IsNullOrWhiteSpace(warning))
            {
                Warnings.Add(warning);
            }
        }
        
        /// <summary>
        /// 合并验证结果
        /// </summary>
        /// <param name="other">其他验证结果</param>
        public void Merge(ValidationResult other)
        {
            if (other != null)
            {
                Errors.AddRange(other.Errors);
                Warnings.AddRange(other.Warnings);
            }
        }
        
        /// <summary>
        /// 获取所有错误信息
        /// </summary>
        /// <returns>错误信息字符串</returns>
        public string GetErrorMessage()
        {
            return string.Join("; ", Errors);
        }
    }
}