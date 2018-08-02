using System;
using System.Data;
using Ak.Framework.Core.Extensions;
using Icona.Logic.DAL;
using Icona.Logic.Filters;

namespace Icona.Logic.Entities
{
    /// <summary>
    /// Сообщество
    /// </summary>
    public class Community : BaseEntity
    {
        public string Name { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Community()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dr">Данные</param>
        internal Community(IDataReader dr) : base(dr)
        {
            Name = dr["Name"].ToString();
            CreatorId = dr["CreatorId"].ToInt32();
            CreationDate = dr["CreationDate"].ToDateTime();
        }

        public static int Add(Community community)
        {
            return Communities.Add(community);
        }

        public static Community Get(int id)
        {
            return Communities.Get(id);
        }

        public void Update()
        {
            Communities.Update(this);
        }

        public void Delete()
        {
            Communities.Delete(Id);
        }

        public static ListEx<Community> GetList(CommunitiesFilter f)
        {
            return Communities.GetList(f);
        }
    }
}