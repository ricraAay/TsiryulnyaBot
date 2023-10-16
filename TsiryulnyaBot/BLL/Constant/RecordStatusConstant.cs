namespace TsiryulnyaBot.BLL.Constant
{

    /// <summary>
    /// Конастанты статуса записи
    /// </summary>
    public static class RecordStatusConstant
    {
        /// <summary>
        /// Отмена
        /// </summary>
        public static readonly Guid Cancel = new Guid("d3c4323e-0bc2-49c0-b645-644728ed8dbb");

        /// <summary>
        /// Подтверждена
        /// </summary>
        public static readonly Guid Confirmed = new Guid("e131e14c-c38e-40f0-aed9-a0ce166a6d8c");

        /// <summary>
        /// Выполнена
        /// </summary>
        public static readonly Guid Done = new Guid("8120fb47-3bda-4eed-84e0-cd997717ba47");

        /// <summary>
        /// В процессе оформления
        /// </summary>
        public static readonly Guid ProcessRegistration = new Guid("84a68145-6a1e-46e8-a83c-1e2eb636ffd3");
    }
}
