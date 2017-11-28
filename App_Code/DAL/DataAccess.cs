using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DataAccess
/// </summary>
abstract public class BaseDataAccess
{
    //private string _ConnectionString;
    private SqlConnection _Conn = new SqlConnection();
    private SqlCommand _cmd;

    public BaseDataAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected abstract string ConnectionString
    {
        get;
    }

    protected SqlConnection OpenConnection(string ConnectionString)
    {
        if (_Conn.State != ConnectionState.Open)
        {
            _Conn.ConnectionString = ConnectionString;
            _Conn.Open();
        }

        return _Conn;
    }

    protected SqlCommand CreateCommand(string sqlQuery)
    {
        _cmd = new SqlCommand();
        _cmd.CommandText = sqlQuery;
        _cmd.CommandType = CommandType.Text;
        _cmd.CommandTimeout = 120;
        return _cmd;
    }

    protected SqlCommand CreateCommand(string StoreProcedure, bool IsStoreProcedure)
    {
        _cmd = new SqlCommand();
        _cmd.CommandText = StoreProcedure;
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.CommandTimeout = 120;
        return _cmd;
    }

    protected SqlCommand CreateCommand(SqlParameter[] Parameters, string StoreProcedure)
    {
        _cmd = new SqlCommand();
        _cmd.CommandText = StoreProcedure;
        _cmd.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter param in Parameters)
        {
            _cmd.Parameters.Add(param);
        }
        _cmd.CommandTimeout = 120;
        return _cmd;
    }


    protected SqlDataReader ExecuteReader(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = OpenConnection(this.ConnectionString);
            return cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int ExecuteNonQuery(SqlCommand cmd)
    {
        int result;
        try
        {
            cmd.Connection = OpenConnection(this.ConnectionString);
            result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result;
    }

    protected object ExecuteScalar(SqlCommand cmd)
    {
        object obj;
        try
        {
            cmd.Connection = OpenConnection(this.ConnectionString);
            obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return obj;
    }

    protected void Close(SqlCommand cmd)
    {
        if (cmd != null)
        {
            cmd.Dispose();
            cmd.Connection.Close();
        }
    }

    protected void Close(SqlDataReader dr)
    {
        if (dr != null)
        {
            dr.Close();
            dr.Dispose();
        }
    }

}
