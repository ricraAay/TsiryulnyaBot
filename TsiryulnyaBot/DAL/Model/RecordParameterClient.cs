namespace TsiryulnyaBot.DAL.Model
{
    public class RecordParameterClient
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Guid? ParameterId { get; set; }

        public Guid? RecordClientId { get; set; }

        public string? StringValue { get; set; }

        public TimeOnly? DatetimeValue { get; set; }

        public int? IntegerValue { get; set; }

        public Guid? UuidValue { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public virtual RecordParameter? Parameter { get; set; }

        public virtual RecordClient? RecordClient { get; set; }
    }
}
