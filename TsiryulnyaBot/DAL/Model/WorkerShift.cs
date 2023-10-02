namespace TsiryulnyaBot.DAL.Model
{
    public class WorkerShift
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? Date { get; set; }

        public Guid? SpecialistId { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public virtual Specialist? Specialist { get; set; }
    }
}
