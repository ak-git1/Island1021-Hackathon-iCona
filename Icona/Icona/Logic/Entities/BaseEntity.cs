using System;
using System.Data;
using Ak.Framework.Core.Extensions;

namespace Icona.Logic.Entities
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    [Serializable]    
    public abstract class BaseEntity
    {
        #region Свойства

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Признак удаленной сущности
        /// </summary>
        public virtual bool Deleted { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        protected BaseEntity()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        protected BaseEntity(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dr">Данные</param>
        protected BaseEntity(IDataReader dr)
        {
            Id = dr["Id"].ToInt32();
            Deleted = dr.FieldExists("Deleted") && dr["Deleted"].ToBoolean();
        }

        #endregion
    }
}