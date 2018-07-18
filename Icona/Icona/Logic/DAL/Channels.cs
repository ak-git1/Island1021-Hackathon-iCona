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
    /// Работа с новостными каналами
    /// </summary>
    public class Channels
    {
        public static int Add(Channel channel)
        {
            Database db = new DatabaseProviderFactory().CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Channels_Add",
                channel.CommunityId,
                channel.Title,
                channel.Description,
                channel.Type,
                channel.Url,
                channel.Attributes,
                channel.Tags);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            return Convert.ToInt32(db.ExecuteScalar(command));
        }

        public static void Update(Channel channel)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Channels_Update", 
                                                                                channel.Id,
                                                                                channel.Title,
                                                                                channel.Description,
                                                                                channel.Type,
                                                                                channel.Url,
                                                                                channel.Attributes,
                                                                                channel.Tags);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        public static Channel Get(int id)
        {
            Database db = new DatabaseProviderFactory().CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Channels_Get", id);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;

            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                    return new Channel(reader);
            }

            return null;
        }

        public static void Delete(int id)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Channels_Delete", id);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        public static ListEx<Channel> GetList(ChannelsFilter f)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Channels_GetList",
                f.CommunityId,
                f.Name.Trim(),
                f.Paging.CurrentPage,
                f.Paging.RecordsPerPage);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                return new ListEx<Channel>(r, x => new Channel(x));
            }
        }
    }
}