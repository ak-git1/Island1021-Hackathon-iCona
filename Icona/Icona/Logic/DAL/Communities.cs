using System;
using System.Data;
using System.Data.Common;
using Icona.Logic.Entities;
using Icona.Logic.Filters;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    /// <summary>
    /// Работа с сообществами
    /// </summary>
    public static class Communities
    {
        public static int Add(Community community)
        {
            Database db = new DatabaseProviderFactory().CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Communities_Add",
                community.Name,
                community.CreatorId);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            return Convert.ToInt32(db.ExecuteScalar(command));
        }

        public static void Update(Community community)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Communities_Update", community.Id, community.Name);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        public static Community Get(int id)
        {
            Database db = new DatabaseProviderFactory().CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Communities_Get", id);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;

            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                    return new Community(reader);
            }

            return null;
        }

        public static void Delete(int id)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Communities_Delete", id);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        public static ListEx<Community> GetList(CommunitiesFilter f)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Communities_GetList",
                f.Name.Trim(),
                f.Paging.CurrentPage,
                f.Paging.RecordsPerPage);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                return new ListEx<Community>(r, x => new Community(x));
            }
        }
    }
}