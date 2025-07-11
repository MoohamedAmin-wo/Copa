namespace Cupa.Domain.Common
{
    public class BaseEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}
