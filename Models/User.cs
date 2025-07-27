namespace Care3._0.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy{ get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy{ get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy{ get;set; }

    }
}

