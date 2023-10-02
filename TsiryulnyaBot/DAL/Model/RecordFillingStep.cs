namespace TsiryulnyaBot.DAL.Model
{
    /// <summary>
    /// Шаг записи
    /// </summary>
    public class RecordFillingStep
    {
        /// <summary>
        /// Идентификатор шага записи
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Кем создан
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Кем обновлен
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdateAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Идентификатор наименования шага записи
        /// </summary>
        public Guid? StepItemId { get; set; }

        /// <summary>
        /// Признак проходжения шага
        /// </summary>
        public bool? Passed { get; set; }

        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid? RecordId { get; set; }

        /// <summary>
        /// Позиция шага
        /// </summary>
        public int? StepPosition { get; set; }

        public virtual RecordClient? Record { get; set; }

        public virtual RecordFillingStepItem? StepItem { get; set; }
    }
}
