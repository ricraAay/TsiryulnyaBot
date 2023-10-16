namespace TsiryulnyaBot.DAL.Model
{
    public partial class WorkerShiftStatus
    {
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public string? Name { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<WorkerShift> WorkerShifts { get; set; } = new List<WorkerShift>();
    }
}