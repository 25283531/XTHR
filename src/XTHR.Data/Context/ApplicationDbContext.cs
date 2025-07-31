using Microsoft.EntityFrameworkCore;
using XTHR.Common.Entities;

namespace XTHR.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=xthr.db");
        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeFinancialAdjustment> EmployeeFinancialAdjustments { get; set; }
        public DbSet<EmployeeSalaryComponent> EmployeeSalaryComponents { get; set; }
        public DbSet<PayrollResult> PayrollResults { get; set; }
        public DbSet<PayrollResultDetail> PayrollResultDetails { get; set; }
        public DbSet<PayrollRule> PayrollRules { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<SalaryComponent> SalaryComponents { get; set; }
        public DbSet<SocialSecurityAccount> SocialSecurityAccounts { get; set; }
        public DbSet<SocialSecurityItem> SocialSecurityItems { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        // 在这里配置您的实体
            // 例如，可以为实体配置主键、索引、关系等
            // modelBuilder.Entity<Employee>().HasKey(e => e.Id);

        }
    }
}