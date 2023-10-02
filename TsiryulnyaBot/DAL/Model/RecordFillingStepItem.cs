namespace TsiryulnyaBot.DAL.Model
{
    /// <summary>
    /// Справочник шагов записи
    /// </summary>
    public class RecordFillingStepItem
    {
        /// <summary>
        /// Идентификатор шага записи
        /// </summary>
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string? Name { get; set; }

        public int? StepPosition { get; set; }

        public virtual ICollection<RecordFillingStep> RecordFillingSteps { get; set; } = new List<RecordFillingStep>();
    }
}
