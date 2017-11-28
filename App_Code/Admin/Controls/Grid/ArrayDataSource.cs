using System;
using System.Collections;
using System.Web.UI;

namespace FlyerMe.Admin.Controls.Grid
{
    public sealed class ArrayDataSource : IDataSource
    {
        public event EventHandler DataSourceChanged;

        public DataSourceView GetView(String viewName)
        {
            throw new NotImplementedException();
        }

        public ICollection GetViewNames()
        {
            throw new NotImplementedException();
        }

        public Array Items { get; set; }

        public void OnDataSourceChanged(Object sender, EventArgs args)
        {
            EventHandler handler = DataSourceChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}