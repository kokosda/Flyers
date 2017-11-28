using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public abstract class GridControlBase : UserControl
    {
        public GridControlBase()
        {
            GridDataSource = new GridDataSource();
        }

        public GridDefault Grid { get; set; }

        public GridDataSource GridDataSource { get; set; }

        public Boolean PagingDisabled { get; set; }

        public Int32 PageSize { get; set; }

        public abstract Int32 StartRowIndex { get; }

        public String PageName { get; set; }

        public NameValueCollection EncodeUrlParametersForPager { get; set; }

        public Boolean ExcelExporterEnabled { get; set; }

        public abstract Int32 TotalRecords { get; }

        public Boolean TableFixed { get; set; }

        public Boolean TableNews { get; set; }

        public Boolean TableSmall { get; set; }

        public String PreheadLiteralText { get; set; }

        public event EventHandler<RowEditedEventArgs> RowEdited;
        public event EventHandler<RowEditCanceledEventArgs> RowEditCanceled;
        public event EventHandler<RowEditingEventArgs> RowEditing;
        public event EventHandler<RowDeletedEventArgs> RowDeleted;
        public event EventHandler<RowHeadBindingEventArgs> RowHeadBinding;
        public event EventHandler<RowDataBindingEventArgs> RowDataBinding;
        public event EventHandler<RowCommandedEventArgs> RowCommanded;
        public event EventHandler<ActionCellCustomBoundEventArgs> ActionCellCustomBound;
        public event EventHandler<ActionCellCustomCommandEventArgs> ActionCellCustomCommand;

        public void OnRowEdited(RowEditedEventArgs args)
        {
            EventHandler<RowEditedEventArgs> handler = RowEdited;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnRowEditCanceled(RowEditCanceledEventArgs args)
        {
            EventHandler<RowEditCanceledEventArgs> handler = RowEditCanceled;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnRowEditing(RowEditingEventArgs args)
        {
            EventHandler<RowEditingEventArgs> handler = RowEditing;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnRowDeleted(RowDeletedEventArgs args)
        {
            EventHandler<RowDeletedEventArgs> handler = RowDeleted;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnRowHeadBinding(RowHeadBindingEventArgs args)
        {
            EventHandler<RowHeadBindingEventArgs> handler = RowHeadBinding;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnRowDataBinding(RowDataBindingEventArgs args)
        {
            EventHandler<RowDataBindingEventArgs> handler = RowDataBinding;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnRowCommanded(RowCommandedEventArgs args)
        {
            EventHandler<RowCommandedEventArgs> handler = RowCommanded;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnActionCellCustomBound(ActionCellCustomBoundEventArgs args)
        {
            EventHandler<ActionCellCustomBoundEventArgs> handler = ActionCellCustomBound;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnActionCellCustomCommand(ActionCellCustomCommandEventArgs args)
        {
            EventHandler<ActionCellCustomCommandEventArgs> handler = ActionCellCustomCommand;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public GridDefault BuildGridFromDataSource()
        {
            var result = new GridDefault();
            var dataSource = GridDataSource.GetDataSource();

            if (dataSource is SqlDataSource)
            {
                result = BuildGridFromSqlDataSource(dataSource as SqlDataSource);
            }
            else if (dataSource is ArrayDataSource)
            {
                result = BuildGridFromArrayDataSource(dataSource as ArrayDataSource);
            }

            return result;
        }

        #region private

        private GridDefault BuildGridFromSqlDataSource(SqlDataSource sds)
        {
            var result = new GridDefault();

            OnRowHeadBinding(new RowHeadBindingEventArgs(result));

            DataView dataView = null;

            if (!PagingDisabled)
            {
                if (GridDataSource.SqlDataSourceSelectCommandType == SqlDataSourceCommandType.StoredProcedure)
                {
                    var nvc = new NameValueCollection(Request.QueryString);

                    nvc.Remove("page");

                    var dataKey = Request.Url.AbsolutePath;

                    if (nvc.Count > 0)
                    {
                        dataKey += "?" + nvc.NameValueToQueryString(false);
                    }

                    dataView = Cache[dataKey] as DataView;

                    if (dataView == null)
                    {
                        dataView = sds.Select(DataSourceSelectArguments.Empty) as DataView;

                        if (dataView == null)
                        {
                            dataView = new DataView(new DataTable());
                        }

                        var expires = DateTime.Now;

                        expires = expires.Add(new TimeSpan(0, 1, 0));
                        Cache.Add(dataKey, dataView, null, expires, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                    }

                    var rowsCount = dataView.Table.AsEnumerable().Skip(GridDataSource.SqlDataSourceStartRowIndex).Take(GridDataSource.SqlDataSourceMaximumRows).Count();

                    for (var i = 0; i < rowsCount; i++)
                    {
                        OnRowDataBinding(new RowDataBindingEventArgs(i, dataView.Table.Rows[GridDataSource.SqlDataSourceStartRowIndex + i], result));
                    }

                    result.Body.TotalRecords = dataView.Table.Rows.Count;
                }
            }

            if (dataView == null)
            {
                dataView = sds.Select(DataSourceSelectArguments.Empty) as DataView;

                for (var i = 0; i < dataView.Table.Rows.Count; i++)
                {
                    OnRowDataBinding(new RowDataBindingEventArgs(i, dataView.Table.Rows[i], result));
                }

                result.Body.TotalRecords = dataView.Table.Rows.Count > 0 && dataView.Table.Columns.Contains("TotalRecords") ? (Int32)dataView.Table.Rows[0]["TotalRecords"] : 0;

                if (result.Body.TotalRecords == 0 && PagingDisabled)
                {
                    result.Body.TotalRecords = dataView.Table.Rows.Count;
                }
            }

            return result;
        }

        private GridDefault BuildGridFromArrayDataSource(ArrayDataSource ads)
        {
            var result = new GridDefault();

            OnRowHeadBinding(new RowHeadBindingEventArgs(result));
            
            for (var i = 0; i < ads.Items.Length; i++)
            {
                OnRowDataBinding(new RowDataBindingEventArgs(i, ads.Items.GetValue(i), result));
            }

            return result;
        }

        #endregion
    }
}