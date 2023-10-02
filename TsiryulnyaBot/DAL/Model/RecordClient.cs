namespace TsiryulnyaBot.DAL.Model
{
    /// <summary>
    /// Записи
    /// </summary>
    public class RecordClient
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Кем создана
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Кем обновлена
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid? ClientId { get; set; }

        /// <summary>
        /// Иденитфикатор статуса записи
        /// </summary>
        public Guid? StatusId { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public virtual Client? Client { get; set; }

        public virtual ICollection<RecordParameterClient> RecordParameterClients { get; set; } = new List<RecordParameterClient>();

        public virtual ICollection<RecordFillingStep> RecordFillingSteps { get; set; } = new List<RecordFillingStep>();

        public virtual RecordStatus? Status { get; set; }
    }
}
