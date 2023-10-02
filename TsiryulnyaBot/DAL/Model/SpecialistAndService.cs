namespace TsiryulnyaBot.DAL.Model
{
    public class SpecialistAndService
    {
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Guid? ServiceId { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? SpecialistId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Service? Service { get; set; }

        public virtual Specialist? Specialist { get; set; }
    }
}
