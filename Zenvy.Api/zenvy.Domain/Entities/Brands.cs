namespace zenvy.Domain.Entities
{
    public class Brand
    {
        //public long BrandId { get; set; }

        public string BrandName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? LogoUrl { get; set; }

        public bool Status { get; set; }

        //public long CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}