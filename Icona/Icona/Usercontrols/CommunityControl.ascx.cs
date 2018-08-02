using System;
using Ak.Framework.Core.Extensions;
using Icona.Logic.Entities;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols
{
    public partial class CommunityControl : BaseControl
    {
        public int? CommunityId
        {
            get => ViewState["CommunityId"].ToInt32(null);
            set => ViewState.Add("CommunityId", value);
        }

        /// <summary>
        /// Событие сохранения
        /// </summary>
        public event EventHandler Saved;

        /// <summary>
        /// Показать контрол
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        public void Show(int? id = null)
        {
            FillCommunity(id);
            Page.OpenDialog(CommunityPanel.ClientID, "Сообщество", 600, 330);
        }

        /// <summary>
        /// Скрыть контрол
        /// </summary>
        public void Close()
        {
            Page.CloseDialog(CommunityPanel.ClientID);
        }

        private void FillCommunity(int? id)
        {
            CommunityId = id;

            if (id.HasValue)
            {
                Community сommunity = Community.Get(id.Value);
                NameTxt.Text = сommunity.Name;
            }
            else
            {
                NameTxt.Text = string.Empty;
            }

            CommunityUPnl.Update();
        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                if (CommunityId.HasValue)
                {
                    Community community = Community.Get(CommunityId.Value);
                    community.Name = NameTxt.Text;
                    community.Update();
                }
                else
                {
                    Community.Add(new Community
                    {
                        CreatorId = CurrentUserId,
                        Name = NameTxt.Text
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