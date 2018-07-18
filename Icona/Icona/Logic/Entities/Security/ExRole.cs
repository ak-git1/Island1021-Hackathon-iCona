using System;
using System.Collections.Generic;

namespace Icona.Logic.Entities.Security
{
    /// <summary>
    /// Расширенная роль пользователя
    /// </summary>
    [Serializable]
    public class ExRole
    {
        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ExRole()
        {
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        /// <param name="description">Описание</param>
        public ExRole(Guid id, string name, string description)
        {
            RoleId = id;
            RoleName = name;
            Description = description;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение списка ролей
        /// </summary>
        public static List<ExRole> GetList()
        {
            return DAL.Roles.GetList();
        }

        #endregion
    }
}
