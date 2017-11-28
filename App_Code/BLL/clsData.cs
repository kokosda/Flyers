# region Import Lib.
using System;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
#endregion

/// <summary>
/// Summary description for clsData.
/// </summary>
public class clsData : IDisposable
{
    //Declaring Delegate and Event Corresponding to thats
    # region member variables of class
    public delegate void ErrorProvider(Exception e);
    public event ErrorProvider onError;


    private bool m_writeLog;
    private bool m_handleError = false;
    public String strSql;
    public SqlConnection objCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString);
    #endregion

    #region Public property of class
    public bool writeLogEvent
    {
        get
        {
            return m_writeLog;
        }
        set
        {
            m_writeLog = value;
        }
    }


    /// <summary>
    /// Through an exception when ever error raised. Don't rais Event for error handling
    /// </summary>

    public bool HandleError
    {
        get
        {
            return m_handleError;
        }
        set
        {
            m_handleError = value;
        }
    }
    #endregion

    #region Public function

    public bool OpenConnection()
    {
        try
        {
            if (objCon.State != System.Data.ConnectionState.Open)
                objCon.Open();
            return true;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return false;
        }

    }



    public int returnNextValue(string strTableName, string strFieldName)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand("SELECT ISNULL(MAX(" + strFieldName + ")+1,1) from " + strTableName + "", objCon);
            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }
    public bool ExecuteSql()
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strSql, objCon);
            if (cm.ExecuteNonQuery() != -1)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return false;
        }
        finally
        {
            objCon.Close();

        }
    }

    public bool ExecuteSql(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandTimeout = 120;

            foreach (object i in ht.Keys)
            {
                if (ht[i].ToString() == "")
                {
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), System.DBNull.Value);
                }
                else
                {
                   // cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
                }
            }

            if (cm.ExecuteNonQuery() != -1)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return false;
        }
        finally
        {
            objCon.Close();

        }
    }
    public string  ExecuteSql(string strProcedureName, Hashtable ht,string  outParam,SqlDbType   outParam_Type,int size)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                if (ht[i].ToString() == "")
                {
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), System.DBNull.Value);
                }
                else
                {
                    //cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
                }
            }
            
            SqlParameter paramout = new SqlParameter();
            paramout.ParameterName = outParam;
            paramout.SqlDbType  = outParam_Type;
            paramout.Direction = ParameterDirection.Output;
            if (outParam_Type == SqlDbType.BigInt || outParam_Type == SqlDbType.Bit || outParam_Type == SqlDbType.DateTime || outParam_Type == SqlDbType.Decimal || outParam_Type == SqlDbType.Image || outParam_Type == SqlDbType.Int || outParam_Type == SqlDbType.SmallDateTime || outParam_Type == SqlDbType.Money || outParam_Type == SqlDbType.SmallInt || outParam_Type == SqlDbType.TinyInt)
            {
            }
            else
            {
                paramout.Size = size;
            }
            
            cm.Parameters.Add(paramout); 
            cm.ExecuteNonQuery() ;
            return paramout.Value.ToString()  ;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return "Error";
        }
        finally
        {
            objCon.Close();

        }
    }


    private string ReplaceQuote(string strValue)
    {
        return strValue.Trim().Replace("'", "");
    }

    public bool ExecuteSql(string strProcedureName)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;
            if (cm.ExecuteNonQuery() != -1)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return false;
        }
        finally
        {
            objCon.Close();

        }
    }


    public int getReturnValue(string strProcedureName)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;


            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }



    public string GetFieldValuesInString(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                if (ht[i].ToString() == "")
                {
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), System.DBNull.Value);
                }
                else
                {
                    if (Convert.ToString(i) == "Status")
                    {
                        cm.Parameters.Add("@Status", SqlDbType.NVarChar, 4000);
                        cm.Parameters["@Status"].Direction = ParameterDirection.Output;
                    }
                    else
                        cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
                }
            }
            cm.ExecuteNonQuery();

            string VarUnique = "";
            VarUnique = Convert.ToString(cm.Parameters["@Status"].Value);

            return VarUnique;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return "";
        }
        finally
        {
            objCon.Close();
        }
    }

    
    public int getReturnValue(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
            }

            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }

    public string GetReturnValueFromSQlProcedure(string strProcedureName, Hashtable ht, SqlDbType ReturnParameterDBType, string ReturnParameter)
    {
        string ReturnValue = "";
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
            }
          
            SqlParameter parm3 = new SqlParameter("@" + ReturnParameter, ReturnParameterDBType);
            parm3.Direction = ParameterDirection.ReturnValue;
            objcom.Parameters.Add(parm3);

            ReturnValue = objcom.ExecuteReader(CommandBehavior.SingleResult).ToString();

              ReturnValue = objcom.Parameters["@" + ReturnParameter].Value.ToString();

        }
        catch (Exception e)
        {
            CatchErr(e);
         

        }
        finally
        {
            objCon.Close();

        }
        return ReturnValue;
    }


    /// <summary>
    /// Return Datatable as result of procedure return set of records
    /// Pass Hashtable as args in Procedures : 
    ///		Key as Parametre Name
    /// Don't send @ sign with parameter name
    /// </summary>
    /// <param name="strProcedureName">name of the procedure</param>
    /// <param name="ht">hash table</param>
    /// <returns>datatable</returns>
    public DataTable GetDataTable(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                String str = string.Format("@{0}", i.ToString()) + "|" + ht[i].ToString();

                //  This condition is used for use of 'uspSearchEntity' in which we pass a string variable in quote, so here we remove the function 'ReplaceQuote()'

                if (strProcedureName == "uspSearchEntity")
                    objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
                else
                    objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
            }

            SqlDataAdapter adp = new SqlDataAdapter(objcom);

            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }

    
    public DataTable GetData(string strsql)
    {
        try
        {
            OpenConnection();
            SqlCommand objCommand = new SqlCommand(strsql,objCon);
            objCommand.CommandTimeout = 0;
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataTable dt = new DataTable();
            objAdapter.Fill(dt);
            return dt;

        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }
   
    public DataTable GetDataTable(string strProcedureName)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;


            SqlDataAdapter adp = new SqlDataAdapter(objcom);

            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }

    public DataSet GetDataSet(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;
            objcom.CommandTimeout = 120;

            foreach (object i in ht.Keys)
            {
                objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
            }


            SqlDataAdapter adp = new SqlDataAdapter(objcom);


            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }

    public DataSet GetDataSetWithoutReplacingQute(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;
            objcom.CommandTimeout = 120;

            foreach (object i in ht.Keys)
            {
                objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
            }


            SqlDataAdapter adp = new SqlDataAdapter(objcom);


            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }

    public DataSet GetDataSetWithQuote(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
            }


            SqlDataAdapter adp = new SqlDataAdapter(objcom);


            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }

    public DataSet GetDataSetPaging(string strProcedureName, Hashtable ht, Int32 CurrentIdx, Int32 PageSz, string TblName)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand(strProcedureName, objCon);

            objcom.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                objcom.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
            }

            SqlDataAdapter adp = new SqlDataAdapter(objcom);

            DataSet ds = new DataSet();
            adp.Fill(ds,CurrentIdx,PageSz ,TblName);
            return ds;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }

    public int returnCountInt(string strTableName, string strFieldName, int intFieldValue)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand("select count(" + strFieldName + ") from " + strTableName + " where " + strFieldName + "=" + intFieldValue, objCon);
            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }

    public int returnCountInt(string strTableName)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand("select count(*) from " + strTableName, objCon);
            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }

    /// <summary>
    /// Counts the record for select query. Please type main FROM clause in Capital Letters
    /// </summary>
    /// <param name="strSql">Sql Select Query</param>
    /// <returns>Record Conut</returns>

    public int CountRecord(string strSql)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand();
            OpenConnection();
            objcom.Connection = objCon;
            objcom.CommandText = strSql.Remove(7, strSql.IndexOf("FROM") - 8).Insert(7, "Count(*) ");
            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }
    public int MaxKey(string strSql)
    {
        try
        {
            OpenConnection();
            SqlCommand objcom = new SqlCommand();
            OpenConnection();
            objcom.Connection = objCon;
            objcom.CommandText = strSql;
            return (int)objcom.ExecuteScalar();
        }
        catch (Exception e)
        {
            CatchErr(e);
            return -1;

        }
        finally
        {
            objCon.Close();

        }
    }

    public DataTable GetDataTable()
    {
        try
        {
            OpenConnection();
            SqlDataAdapter adp = new SqlDataAdapter(strSql, objCon);

            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        catch (Exception e)
        {
            CatchErr(e);
            return null;
        }
        finally
        {
            objCon.Close();

        }
    }
    /// <summary>

    //A To Return Record Count And Query
    public string ReturnRecCountNQuery(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                if (ht[i].ToString() == "")
                {
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), System.DBNull.Value);
                }
                else
                {
                    if (Convert.ToString(i) == "StrSql")
                    {
                        cm.Parameters.Add("@StrSql", SqlDbType.VarChar , 4000);
                        cm.Parameters["@StrSql"].Direction = ParameterDirection.Output;

                    }
                    else
                        if (strProcedureName == "uspSearchEntity")
                            cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
                        else
                            cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
                }
            }


            cm.ExecuteNonQuery();

            SqlDataAdapter adp = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            string StrSql = "";
            StrSql = dt.Rows.Count + "|" + Convert.ToString(cm.Parameters["@StrSql"].Value);

            return StrSql;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return "";
        }
        finally
        {
            objCon.Close();
        }
    }

    //A To Check unique names
    public byte ExecuteSqlChk(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                if (ht[i].ToString() == "")
                {
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), System.DBNull.Value);
                }
                else
                {

                    if (Convert.ToString(i) == "Status")
                    {
                        cm.Parameters.Add("@Status", SqlDbType.Bit);
                        cm.Parameters["@Status"].Direction = ParameterDirection.Output;
                    }
                    else
                        cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
                }
            }
            cm.ExecuteNonQuery();
            byte VarUnique = 0;
            VarUnique = Convert.ToByte(cm.Parameters["@Status"].Value);

            return VarUnique;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return 2;
        }
        finally
        {
            objCon.Close();
        }
    }

    //A To Return Record Count And Query
    public bool ExecuteDeleteSql(string strProcedureName, Hashtable ht)
    {
        try
        {
            OpenConnection();
            SqlCommand cm = new SqlCommand(strProcedureName, objCon);
            cm.CommandType = CommandType.StoredProcedure;

            foreach (object i in ht.Keys)
            {
                if (ht[i].ToString() == "")
                {
                    cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), System.DBNull.Value);
                }
                else
                {

                    if (strProcedureName == "uspDeleteEntity")
                        cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ht[i].ToString());
                    else
                        cm.Parameters.AddWithValue(string.Format("@{0}", i.ToString()), ReplaceQuote(ht[i].ToString()));
                }
            }

            if (cm.ExecuteNonQuery() != -1)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            CatchErr(e);
            return false;
        }
        finally
        {
            objCon.Close();
        }
    }


    #endregion

    #region Error Handlar & Dispose



    /// <summary>
    /// Call to distroy connection object and release memory.
    /// </summary>
    public void Dispose()
    {
        if (objCon != null)
        {
            objCon.Close();
            objCon.Dispose();
        }
        GC.SuppressFinalize(this);
    }
    ~clsData()
    {
        if (objCon != null)
        {
            objCon.Close();
            objCon.Dispose();
        }
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Rais Event Whenever Error Occured in Class
    /// </summary>
    /// <param name="e"></param>
    private void CatchErr(Exception e)
    {

        // don't handle error raise Exception
        if (m_handleError == false)
        {
            throw e;
        }

        //Handle Error & Raise Event 
        try
        {
            onError(e);

        }
        catch
        {
            // return if no event handlar intialize
            return;
        }

    }
    #endregion
}
