namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤设备DTO
    /// </summary>
    public class AttendanceDeviceDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; } = string.Empty;

        /// <summary>
        /// 设备状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 设备位置
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}