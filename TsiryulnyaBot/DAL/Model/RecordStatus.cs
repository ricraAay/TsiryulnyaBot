namespace TsiryulnyaBot.DAL.Model
{
    public class RecordStatus
    {
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public string? Name { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<RecordClient> RecordClients { get; set; } = new List<RecordClient>();
    }
}
