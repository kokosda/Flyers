using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public sealed class GridDataSource
    {
        public GridDataSource()
        {
            SqlDataSourceSelectParameters = new ParameterCollection();
            SqlDataSourceUpdateParameters = new ParameterCollection();
            SqlDataSourceInsertParameters = new ParameterCollection();
            SqlDataSourceDeleteParameters = new ParameterCollection();
        }

        public String SqlDataSourceConnectionString { get; set; }

        public String SqlDataSourceSelectCommand { get; set; }

        public ParameterCollection SqlDataSourceSelectParameters { get; set; }

        public SqlDataSourceCommandType? SqlDataSourceSelectCommandType { get; set; }

        public String SqlDataSourceFromCommand { get; set; }

        public String SqlDataSourceOrderByCommand { get; set; }

        public String SqlDataSourceWhereCommand { get; set; }

        public String SqlDataSourceUpdateCommand { get; set; }

        public ParameterCollection SqlDataSourceUpdateParameters { get; set; }

        public String SqlDataSourceInsertCommand { get; set; }

        public ParameterCollection SqlDataSourceInsertParameters { get; set; }

        public String SqlDataSourceDeleteCommand { get; set; }

        public ParameterCollection SqlDataSourceDeleteParameters { get; set; }

        public Int32 SqlDataSourceStartRowIndex { get; set; }

        public Int32 SqlDataSourceMaximumRows { get; set; }

        public String SqlExcelExportCommand { get; set; }

        public Boolean PagingDisabled { get; set; }

        public ArrayDataSource ArrayDataSource { get; set; }

        public IDataSource GetDataSource()
        {
            IDataSource result = null;

            if (SqlDataSourceSelectCommand.HasText())
            {
                result = GetSqlDataSource();
            }
            else if (ArrayDataSource != null)
            {
                result = ArrayDataSource;
            }

            return result;
        }

        #region private

        private SqlDataSource GetSqlDataSource()
        {
            SqlDataSource result;

            if (SqlDataSourceSelectCommandType == SqlDataSourceCommandType.StoredProcedure)
            {
                result = GetSqlStoredProceduteDataSource();
            }
            else
            {
                result = GetSqlTextSelectDataSource();
            }

            foreach (Parameter p in SqlDataSourceSelectParameters)
            {
                result.SelectParameters.Add(p);
            }

            foreach (Parameter p in SqlDataSourceUpdateParameters)
            {
                result.UpdateParameters.Add(p);
            }

            foreach (Parameter p in SqlDataSourceInsertParameters)
            {
                result.InsertParameters.Add(p);
            }

            foreach (Parameter p in SqlDataSourceDeleteParameters)
            {
                result.DeleteParameters.Add(p);
            }

            return result;
        }

        private SqlDataSource GetSqlStoredProceduteDataSource()
        {
            var selectCommand = SqlDataSourceSelectCommand;
            var connectionString = SqlDataSourceConnectionString.HasText() ? SqlDataSourceConnectionString : ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString;
            var result = new SqlDataSource(connectionString, selectCommand)
                                {
                                    SelectCommandType = SqlDataSourceCommandType.StoredProcedure,
                                    UpdateCommand = SqlDataSourceUpdateCommand,
                                    InsertCommand = SqlDataSourceInsertCommand,
                                    DeleteCommand = SqlDataSourceDeleteCommand
                                };

            return result;
        }

        private SqlDataSource GetSqlTextSelectDataSource()
        {
            var selectCommand = MakeSqlPaginationQuery(SqlDataSourceSelectCommand, SqlDataSourceFromCommand, SqlDataSourceWhereCommand, SqlDataSourceOrderByCommand);
            var connectionString = SqlDataSourceConnectionString.HasText() ? SqlDataSourceConnectionString : ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString;
            var result = new SqlDataSource(connectionString, selectCommand)
                                {
                                    UpdateCommand = SqlDataSourceUpdateCommand,
                                    InsertCommand = SqlDataSourceInsertCommand,
                                    DeleteCommand = SqlDataSourceDeleteCommand
                                };

            if (!PagingDisabled)
            {
                result.SelectParameters.Add("startRowIndex", DbType.Int32, SqlDataSourceStartRowIndex.ToString());
                result.SelectParameters.Add("maximumRows", DbType.Int32, SqlDataSourceMaximumRows.ToString());
            }

            return result;
        }

        private String MakeSqlPaginationQuery(String selectCommand, String fromCommand, String whereCommand, String orderByCommand)
        {
            var result = selectCommand;

            if (!PagingDisabled)
            {
                result = @";
                           WITH CTE AS
                            ( " +
                                selectCommand + ", ROW_NUMBER() OVER (" + orderByCommand + ") as RowNumber " +
                                fromCommand + " " +
                                whereCommand +
                         @" )
                          SELECT *, (SELECT COUNT(*) FROM CTE) AS TotalRecords 
                          FROM CTE
                          WHERE RowNumber Between (@startRowIndex + 1) AND (@startRowIndex + @maximumRows)
                          ORDER BY RowNumber ASC";
            }

            return result;
        }

        #endregion
    }
}