using FlyerMe.Admin.Controls.Grid.Events;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public abstract class CellControlBase : UserControl
    {
        public abstract HtmlTableCell TdHtmlTableCell { get; }

        public CellsListControlBase CellsList { get; set; }

        public void AddTdHtmlTableCellAttributes(Dictionary<String, String> attributes)
        {
            foreach (var a in attributes)
            {
                TdHtmlTableCell.Attributes.Add(a.Key, a.Value);
            }
        }

        public String GetPropertyValue(String property)
        {
            String result = null;
            var propertyInfo = GetType().GetProperty(property);

            if (propertyInfo.PropertyType == typeof(String))
            {
                result = propertyInfo.GetValue(this) as String;
            }
            else
            {
                var temp = propertyInfo.GetValue(this);

                if (temp != null)
                {
                    result = temp.ToString();
                }
            }

            return result;
        }
    }
}