namespace TsiryulnyaBot.DAL.Model
{
    /// <summary>
    /// Справочник предостовляемых услуг
    /// </summary>
    public class ServiceProvided
    {
        /// <summary>
        /// Идентификатор услуги
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Кем создана
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Кем обновлена
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Наименование услуги
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<SpecialistAndService> SpecialistAndServices { get; set; } = new List<SpecialistAndService>();
    }
}
