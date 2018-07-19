using System;
using System.Data;
using System.Data.Common;
using Icona.Logic.Entities;
using Icona.Logic.Enums;
using Icona.Logic.Filters;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    /// <summary>
    /// Работа с новостями
    /// </summary>
    public static class NewsItems
    {
        public static void UpdateState(Guid id, NewsItemsStates state)
        {
            Database db = new DatabaseProviderFactory().CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_NewsItems_UpdateState", id, state);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        public static void Delete(Guid id)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_NewsItems_Delete", id);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        public static ListEx<NewsItem> GetList(NewsItemsListFilter f)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_NewsItems_GetList",
                f.CommunityId,
                f.Title.Trim(),
                f.DateFrom,
                f.DateTill,
                f.State,
                f.Paging.CurrentPage,
                f.Paging.RecordsPerPage);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                return new ListEx<NewsItem>(r, x => new NewsItem(x));
            }
        }
    }
}