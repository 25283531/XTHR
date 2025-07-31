namespace XTHR.Core.Entities
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}