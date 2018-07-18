using System;

namespace Icona.Logic.Filters
{
    /// <summary>
    /// Фильтр на странице управлениям пользователями
    /// </summary>
    public class UsersRegistryFilter : BaseFilter
    {
        #region Свойства

        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; }
        
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
               
        /// <summary>
        /// Дата регистрации с
        /// </summary>
        public DateTime? RegistrationDateFrom { get; set; }
        
        /// <summary>
        /// Дата регистрации по
        /// </summary>
        public DateTime? RegistrationDateTill { get; set; }
        
        /// <summary>
        /// Дата последенего входа с
        /// </summary>
        public DateTime? LastEnterDateFrom { get; set; }
        
        /// <summary>
        /// Дата последенего входа по
        /// </summary>        
        public DateTime? LastEnterDateTill { get; set; }
        
        /// <summary>
        /// Заблокирован
        /// </summary>
        public bool? IsBlocked { get; set; }

        #endregion
    }
}
