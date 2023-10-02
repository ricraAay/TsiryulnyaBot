namespace TsiryulnyaBot.DAL.Model
{
    public class RecordParameter
    {
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<RecordParameterClient> RecordParameterClients { get; set; } = new List<RecordParameterClient>();
    }
}
