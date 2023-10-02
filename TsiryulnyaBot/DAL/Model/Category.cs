namespace TsiryulnyaBot.DAL.Model
{
    /// <summary>
    /// Справочник категорий
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Идентификатор категории
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
        /// Наименование
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<SpecialistAndService> SpecialistAndServices { get; set; } = new List<SpecialistAndService>();
    }
}
