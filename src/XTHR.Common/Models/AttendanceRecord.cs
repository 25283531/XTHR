using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 考勤记录实体类
    /// </summary>
    public class AttendanceRecord
    {
        /// <summary>
        /// 考勤记录ID（主键）
        /// </summary>
        public int AttendanceID { get; set; }

        /// <summary>
        /// 员工ID（外键）
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 考勤日期
        /// </summary>
        [Required(ErrorMessage = "考勤日期不能为空")]
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 星期几（1=周一，7=周日）
        /// </summary>
        [Range(1, 7, ErrorMessage = "星期几必须在1-7之间")]
        public int DayOfWeek { get; set; }

        /// <summary>
        /// 上班打卡时间
        /// </summary>
        public TimeSpan? CheckInTime { get; set; }

        /// <summary>
        /// 下班打卡时间
        /// </summary>
        public TimeSpan? CheckOutTime { get; set; }

        /// <summary>
        /// 实际工作时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "实际工作时长必须在0-24小时之间")]
        public decimal ActualWorkHours { get; set; } = 0;

        /// <summary>
        /// 标准工作时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "标准工作时长必须在0-24小时之间")]
        public decimal StandardWorkHours { get; set; } = 8;

        /// <summary>
        /// 迟到时长（分钟）
        /// </summary>
        [Range(0, 1440, ErrorMessage = "迟到时长必须在0-1440分钟之间")]
        public int LateMinutes { get; set; } = 0;

        /// <summary>
        /// 早退时长（分钟）
        /// </summary>
        [Range(0, 1440, ErrorMessage = "早退时长必须在0-1440分钟之间")]
        public int EarlyLeaveMinutes { get; set; } = 0;

        /// <summary>
        /// 加班时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "加班时长必须在0-24小时之间")]
        public decimal OvertimeHours { get; set; } = 0;

        /// <summary>
        /// 周六加班时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "周六加班时长必须在0-24小时之间")]
        public decimal SaturdayOvertimeHours { get; set; } = 0;

        /// <summary>
        /// 周日加班时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "周日加班时长必须在0-24小时之间")]
        public decimal SundayOvertimeHours { get; set; } = 0;

        /// <summary>
        /// 法定节假日加班时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "法定节假日加班时长必须在0-24小时之间")]
        public decimal HolidayOvertimeHours { get; set; } = 0;

        /// <summary>
        /// 考勤状态
        /// </summary>
        [Required(ErrorMessage = "考勤状态不能为空")]
        [StringLength(20, ErrorMessage = "考勤状态长度不能超过20个字符")]
        public string AttendanceStatus { get; set; } = "正常";

        /// <summary>
        /// 是否请假
        /// </summary>
        public bool IsOnLeave { get; set; } = false;

        /// <summary>
        /// 请假类型
        /// </summary>
        [StringLength(20, ErrorMessage = "请假类型长度不能超过20个字符")]
        public string? LeaveType { get; set; }

        /// <summary>
        /// 请假时长（小时）
        /// </summary>
        [Range(0, 24, ErrorMessage = "请假时长必须在0-24小时之间")]
        public decimal LeaveHours { get; set; } = 0;

        /// <summary>
        /// 是否出差
        /// </summary>
        public bool IsOnBusinessTrip { get; set; } = false;

        /// <summary>
        /// 出差地点
        /// </summary>
        [StringLength(100, ErrorMessage = "出差地点长度不能超过100个字符")]
        public string? BusinessTripLocation { get; set; }

        /// <summary>
        /// 是否外勤
        /// </summary>
        public bool IsFieldWork { get; set; } = false;

        /// <summary>
        /// 外勤地点
        /// </summary>
        [StringLength(100, ErrorMessage = "外勤地点长度不能超过100个字符")]
        public string? FieldWorkLocation { get; set; }

        /// <summary>
        /// 是否法定节假日
        /// </summary>
        public bool IsHoliday { get; set; } = false;

        /// <summary>
        /// 节假日名称
        /// </summary>
        [StringLength(50, ErrorMessage = "节假日名称长度不能超过50个字符")]
        public string? HolidayName { get; set; }

        /// <summary>
        /// 是否调休
        /// </summary>
        public bool IsCompensatoryLeave { get; set; } = false;

        /// <summary>
        /// 异常说明
        /// </summary>
        [StringLength(500, ErrorMessage = "异常说明长度不能超过500个字符")]
        public string? ExceptionRemark { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        [StringLength(20, ErrorMessage = "审批状态长度不能超过20个字符")]
        public string ApprovalStatus { get; set; } = "无需审批";

        /// <summary>
        /// 审批人
        /// </summary>
        [StringLength(50, ErrorMessage = "审批人长度不能超过50个字符")]
        public string? Approver { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [StringLength(20, ErrorMessage = "数据来源长度不能超过20个字符")]
        public string DataSource { get; set; } = "手工录入";

        /// <summary>
        /// 原始数据（JSON格式，用于存储钉钉等系统的原始数据）
        /// </summary>
        public string? OriginalData { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        [StringLength(50, ErrorMessage = "创建人长度不能超过50个字符")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [StringLength(50, ErrorMessage = "更新人长度不能超过50个字符")]
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// 导航属性：员工信息
        /// </summary>
        public Employee? Employee { get; set; }

        /// <summary>
        /// 是否全勤（无迟到、早退、请假、旷工）
        /// </summary>
        public bool IsPerfectAttendance
        {
            get
            {
                return LateMinutes == 0 && 
                       EarlyLeaveMinutes == 0 && 
                       !IsOnLeave && 
                       AttendanceStatus != "旷工" && 
                       AttendanceStatus != "缺勤";
            }
        }

        /// <summary>
        /// 是否工作日
        /// </summary>
        public bool IsWorkday
        {
            get
            {
                return DayOfWeek >= 1 && DayOfWeek <= 5 && !IsHoliday;
            }
        }

        /// <summary>
        /// 是否周末
        /// </summary>
        public bool IsWeekend
        {
            get
            {
                return DayOfWeek == 6 || DayOfWeek == 7;
            }
        }

        /// <summary>
        /// 是否周六
        /// </summary>
        public bool IsSaturday => DayOfWeek == 6;

        /// <summary>
        /// 是否周日
        /// </summary>
        public bool IsSunday => DayOfWeek == 7;

        /// <summary>
        /// 总加班时长（包括平时、周六、周日、节假日）
        /// </summary>
        public decimal TotalOvertimeHours
        {
            get
            {
                return OvertimeHours + SaturdayOvertimeHours + SundayOvertimeHours + HolidayOvertimeHours;
            }
        }

        /// <summary>
        /// 应出勤时长（考虑请假、出差等情况）
        /// </summary>
        public decimal ExpectedWorkHours
        {
            get
            {
                if (IsOnLeave)
                    return Math.Max(0, StandardWorkHours - LeaveHours);
                    
                if (IsOnBusinessTrip || IsFieldWork)
                    return StandardWorkHours;
                    
                if (IsHoliday && !IsCompensatoryLeave)
                    return 0;
                    
                return StandardWorkHours;
            }
        }

        /// <summary>
        /// 缺勤时长
        /// </summary>
        public decimal AbsentHours
        {
            get
            {
                var expected = ExpectedWorkHours;
                var actual = ActualWorkHours;
                return Math.Max(0, expected - actual);
            }
        }

        /// <summary>
        /// 计算实际工作时长
        /// </summary>
        public void CalculateActualWorkHours()
        {
            if (CheckInTime.HasValue && CheckOutTime.HasValue)
            {
                var workDuration = CheckOutTime.Value - CheckInTime.Value;
                
                // 如果跨天，需要加24小时
                if (workDuration.TotalHours < 0)
                    workDuration = workDuration.Add(TimeSpan.FromHours(24));
                
                // 减去午休时间（假设1小时）
                var lunchBreak = TimeSpan.FromHours(1);
                if (workDuration > lunchBreak)
                    workDuration = workDuration.Subtract(lunchBreak);
                
                ActualWorkHours = Math.Max(0, (decimal)workDuration.TotalHours);
            }
        }

        /// <summary>
        /// 计算迟到和早退时间
        /// </summary>
        /// <param name="standardCheckIn">标准上班时间</param>
        /// <param name="standardCheckOut">标准下班时间</param>
        public void CalculateLateAndEarlyLeave(TimeSpan standardCheckIn, TimeSpan standardCheckOut)
        {
            // 计算迟到
            if (CheckInTime.HasValue && CheckInTime.Value > standardCheckIn)
            {
                var lateDuration = CheckInTime.Value - standardCheckIn;
                LateMinutes = (int)lateDuration.TotalMinutes;
            }
            else
            {
                LateMinutes = 0;
            }

            // 计算早退
            if (CheckOutTime.HasValue && CheckOutTime.Value < standardCheckOut)
            {
                var earlyLeaveDuration = standardCheckOut - CheckOutTime.Value;
                EarlyLeaveMinutes = (int)earlyLeaveDuration.TotalMinutes;
            }
            else
            {
                EarlyLeaveMinutes = 0;
            }
        }

        /// <summary>
        /// 计算加班时间
        /// </summary>
        /// <param name="standardCheckOut">标准下班时间</param>
        public void CalculateOvertime(TimeSpan standardCheckOut)
        {
            if (!CheckOutTime.HasValue)
                return;

            // 重置所有加班时间
            OvertimeHours = 0;
            SaturdayOvertimeHours = 0;
            SundayOvertimeHours = 0;
            HolidayOvertimeHours = 0;

            // 计算超出标准下班时间的时长
            var overtimeDuration = TimeSpan.Zero;
            if (CheckOutTime.Value > standardCheckOut)
            {
                overtimeDuration = CheckOutTime.Value - standardCheckOut;
            }

            var overtimeHours = (decimal)overtimeDuration.TotalHours;
            if (overtimeHours <= 0)
                return;

            // 根据日期类型分配加班时间
            if (IsHoliday)
            {
                HolidayOvertimeHours = overtimeHours;
            }
            else if (IsSaturday)
            {
                SaturdayOvertimeHours = overtimeHours;
            }
            else if (IsSunday)
            {
                SundayOvertimeHours = overtimeHours;
            }
            else if (IsWorkday)
            {
                OvertimeHours = overtimeHours;
            }
        }

        /// <summary>
        /// 自动设置星期几
        /// </summary>
        public void SetDayOfWeek()
        {
            var dayOfWeek = AttendanceDate.DayOfWeek;
            DayOfWeek = dayOfWeek == System.DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
        }

        /// <summary>
        /// 验证考勤记录
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateAttendance()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (EmployeeID <= 0)
                result.AddError("员工ID必须大于0");

            if (AttendanceDate == default)
                result.AddError("考勤日期不能为空");

            if (string.IsNullOrWhiteSpace(AttendanceStatus))
                result.AddError("考勤状态不能为空");

            // 检查日期合理性
            if (AttendanceDate > DateTime.Today)
                result.AddError("考勤日期不能晚于今天");

            if (AttendanceDate < new DateTime(2000, 1, 1))
                result.AddError("考勤日期不能早于2000年");

            // 检查打卡时间逻辑
            if (CheckInTime.HasValue && CheckOutTime.HasValue)
            {
                var workDuration = CheckOutTime.Value - CheckInTime.Value;
                if (workDuration.TotalHours < 0)
                    workDuration = workDuration.Add(TimeSpan.FromHours(24));
                    
                if (workDuration.TotalHours > 24)
                    result.AddError("工作时长不能超过24小时");
            }

            // 检查请假逻辑
            if (IsOnLeave)
            {
                if (string.IsNullOrWhiteSpace(LeaveType))
                    result.AddError("请假时必须指定请假类型");
                    
                if (LeaveHours <= 0)
                    result.AddError("请假时长必须大于0");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(LeaveType))
                    result.AddWarning("未请假但设置了请假类型");
                    
                if (LeaveHours > 0)
                    result.AddWarning("未请假但设置了请假时长");
            }

            // 检查出差逻辑
            if (IsOnBusinessTrip && string.IsNullOrWhiteSpace(BusinessTripLocation))
                result.AddError("出差时必须指定出差地点");

            // 检查外勤逻辑
            if (IsFieldWork && string.IsNullOrWhiteSpace(FieldWorkLocation))
                result.AddError("外勤时必须指定外勤地点");

            // 检查节假日逻辑
            if (IsHoliday && string.IsNullOrWhiteSpace(HolidayName))
                result.AddError("法定节假日必须指定节假日名称");

            // 检查数值范围
            if (ActualWorkHours < 0 || ActualWorkHours > 24)
                result.AddError("实际工作时长必须在0-24小时之间");

            if (TotalOvertimeHours > 12)
                result.AddWarning("总加班时长超过12小时，请确认是否正确");

            return result;
        }

        /// <summary>
        /// 克隆考勤记录对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public AttendanceRecord Clone()
        {
            return new AttendanceRecord
            {
                AttendanceID = this.AttendanceID,
                EmployeeID = this.EmployeeID,
                AttendanceDate = this.AttendanceDate,
                DayOfWeek = this.DayOfWeek,
                CheckInTime = this.CheckInTime,
                CheckOutTime = this.CheckOutTime,
                ActualWorkHours = this.ActualWorkHours,
                StandardWorkHours = this.StandardWorkHours,
                LateMinutes = this.LateMinutes,
                EarlyLeaveMinutes = this.EarlyLeaveMinutes,
                OvertimeHours = this.OvertimeHours,
                SaturdayOvertimeHours = this.SaturdayOvertimeHours,
                SundayOvertimeHours = this.SundayOvertimeHours,
                HolidayOvertimeHours = this.HolidayOvertimeHours,
                AttendanceStatus = this.AttendanceStatus,
                IsOnLeave = this.IsOnLeave,
                LeaveType = this.LeaveType,
                LeaveHours = this.LeaveHours,
                IsOnBusinessTrip = this.IsOnBusinessTrip,
                BusinessTripLocation = this.BusinessTripLocation,
                IsFieldWork = this.IsFieldWork,
                FieldWorkLocation = this.FieldWorkLocation,
                IsHoliday = this.IsHoliday,
                HolidayName = this.HolidayName,
                IsCompensatoryLeave = this.IsCompensatoryLeave,
                ExceptionRemark = this.ExceptionRemark,
                ApprovalStatus = this.ApprovalStatus,
                Approver = this.Approver,
                ApprovalDate = this.ApprovalDate,
                DataSource = this.DataSource,
                OriginalData = this.OriginalData,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy
            };
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"Attendance[{EmployeeID}]: {AttendanceDate:yyyy-MM-dd} - {AttendanceStatus} - 工时:{ActualWorkHours}h";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is AttendanceRecord other)
            {
                return AttendanceID == other.AttendanceID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return AttendanceID.GetHashCode();
        }
    }
}