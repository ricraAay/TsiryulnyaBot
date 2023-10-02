namespace TsiryulnyaBot.DAL.Model
{
    public class Specialist
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Guid? ClientId { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public virtual Client? Client { get; set; }

        public virtual ICollection<SpecialistAndService> SpecialistAndServices { get; set; } = new List<SpecialistAndService>();

        public virtual ICollection<WorkerShift> WorkerShifts { get; set; } = new List<WorkerShift>();
    }
}
