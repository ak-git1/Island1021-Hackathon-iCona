using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Icona.Logic.Entities;
using Icona.Logic.Filters;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    public static class TagsStatistics
    {
        public static ListEx<TagsStatisticsItem> GetList(TagsStatisticsFilter f)
        {
            Database db = new DatabaseProviderFactory().CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_TagsStatistics_GetList",
                f.Paging.CurrentPage,
                f.Paging.RecordsPerPage);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                return new ListEx<TagsStatisticsItem>(r, x => new TagsStatisticsItem(x));
            }
        }
    }
}