namespace TsiryulnyaBot.DAL.Model
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Идентификатор клиента
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
        /// Имя клиента
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Логин в телеграмм
        /// </summary>
        public string? TlgUserName { get; set; }

        /// <summary>
        /// Идентификатор телеграмм
        /// </summary>
        public long? TlgId { get; set; }

        /// <summary>
        /// Идентификатор приватного чата
        /// </summary>
        public long? TlgChatId { get; set; }
        
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime? UpdateAt { get; set; } = DateTime.Now;

        public virtual ICollection<RecordClient> RecordClients { get; set; } = new List<RecordClient>();

        public virtual ICollection<Specialist> Specialists { get; set; } = new List<Specialist>();
    }
}
