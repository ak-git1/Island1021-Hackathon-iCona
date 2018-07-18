using System.ComponentModel;
using System.Web.UI;

namespace Icona.Usercontrols
{
    public partial class FilterButton : UserControl
    {
        #region Свойства

        /// <summary>
        /// Название кнопки применения фильтра
        /// </summary>
        [Bindable(true)]
        public string ApplyButtonName
        {
            set
            {
                visiblePnl.Attributes.Add("applybuttonname", value);
            }
        }

        #endregion
    }
}