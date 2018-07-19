using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elar.Framework.Core.Extensions;
using Elar.Framework.Core.Helpers;
using Icona.Logic.Entities;
using Icona.Logic.Enums;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols
{
    public partial class ChannelControl : System.Web.UI.UserControl
    {
        public int? ChannelId
        {
            get => ViewState["ChannelId"].ToInt32(null);
            set => ViewState.Add("ChannelId", value);
        }

        public int? CommunityId
        {
            get => ViewState["CommunityId"].ToInt32(null);
            set => ViewState.Add("CommunityId", value);
        }

        /// <summary>
        /// Событие сохранения
        /// </summary>
        public event EventHandler Saved;

        public void Show(int? id, int communityId)
        {
            FillChannel(id, communityId);
            Page.OpenDialog(ChannelPanel.ClientID, "Новостной канал", 600, 400);
        }

        public void Show(int id)
        {
            FillChannel(id, null);
            Page.OpenDialog(ChannelPanel.ClientID, "Новостной канал", 600, 400);
        }

        /// <summary>
        /// Скрыть контрол
        /// </summary>
        public void Close()
        {
            Page.CloseDialog(ChannelPanel.ClientID);
        }

        private void FillChannel(int? id, int? communityId)
        {
            ChannelId = id;
            CommunityId = communityId;

            if (id.HasValue)
            {
                Channel channel = Channel.Get(id.Value);
                NameTxt.Text = channel.Title;
                TypesDdl.SetSelectedValue(channel.Type);
                UrlTxt.Text = channel.Url;
                AttributesTxt.Text = channel.Attributes;
                TagsTxt.Text = channel.Tags;
                DescriptionTxt.Text = channel.Description;
            }
            else
            {
                NameTxt.Text = string.Empty;
                TypesDdl.SelectedIndex = 0;
                UrlTxt.Text =
                AttributesTxt.Text =
                TagsTxt.Text =
                DescriptionTxt.Text= string.Empty;
            }

            ChannelUPnl.Update();
        }

        /// <summary>
        /// Заполнение списков
        /// </summary>
        private void FillLists()
        {
            TypesDdl.Items.Clear();
            TypesDdl.DataSource = EnumNamesHelper.EnumToList<ChannelTypes>();
            TypesDdl.DataTextField = "Text";
            TypesDdl.DataValueField = "Value";
            TypesDdl.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillLists();
            }
        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                if (ChannelId.HasValue)
                {
                    Channel channel = Channel.Get(ChannelId.Value);
                    channel.Title = NameTxt.Text;
                    channel.Type = TypesDdl.SelectedItem.Value.ToInt32();
                    channel.Url = UrlTxt.Text;
                    channel.Attributes = AttributesTxt.Text;
                    channel.Tags = TagsTxt.Text;
                    channel.Description = DescriptionTxt.Text;
                    channel.Update();
                }
                else
                {
                    Channel.Add(new Channel
                    {
                        CommunityId = CommunityId.Value,                
                        Title = NameTxt.Text,
                        Type = TypesDdl.SelectedItem.Value.ToInt32(),
                        Url = UrlTxt.Text,
                        Attributes = AttributesTxt.Text,
                        Tags = TagsTxt.Text,
                        Description = DescriptionTxt.Text
                });
                }

                Saved?.Invoke(this, e);
                Close();
            }
            catch (Exception ex)
            {
                Page.StandardErrorScript();
            }
        }
    }
}