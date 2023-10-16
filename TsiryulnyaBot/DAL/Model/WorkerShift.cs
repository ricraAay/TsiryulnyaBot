namespace TsiryulnyaBot.DAL.Model
{
    public partial class WorkerShift
    {
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateOnly? Date { get; set; }

        public Guid? SpecialistId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? StatusId { get; set; }

        public TimeOnly? Time { get; set; }

        public virtual Specialist? Specialist { get; set; }

        public virtual WorkerShiftStatus? Status { get; set; }
    }
}
