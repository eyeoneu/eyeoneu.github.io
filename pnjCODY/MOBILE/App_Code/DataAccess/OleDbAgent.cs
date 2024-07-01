using System;
using System.EnterpriseServices;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Data.OleDb;
using System.Configuration;

/// <summary>
/// Summary description for OleDbDbAgent.
/// </summary>
[Transaction(TransactionOption.Supported)]
[JustInTimeActivation(true)]
[ClassInterface(ClassInterfaceType.AutoDual)]
//  [ObjectPooling(true,5,100)]
[ConstructionEnabled(true, Default = @"None")]
[Guid("DF449795-F50D-44ac-91F2-699ED9BB45CD")]
#if DEBUG
	[EventTrackingEnabled(true)]
#endif
public class OleDbDbAgent
{
    string _connectionString = "";      // Connection string field

    string _connectionName = "";

    int _commandTimeOut = 30;

    string _logTitle = "";

    string _logContents = "";

    bool _isLoggingOn = false;


    /// <summary>
    /// Connection string�� �Ҵ� �ϰų� �д´�.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }

        set { _connectionString = value; }
    }

    public OleDbDbAgent()
    {
        string logOnOffSetting = ConfigurationManager.AppSettings.Get("LogOnOff_DataAccess");

        if ((!string.IsNullOrEmpty(logOnOffSetting)) && (logOnOffSetting.ToLower() == "on"))
        {
            _isLoggingOn = true;
        }
    }

    public OleDbDbAgent(string connStringName)
    {
        string logOnOffSetting = ConfigurationManager.AppSettings.Get("LogOnOff_DataAccess");

        if ((!string.IsNullOrEmpty(logOnOffSetting)) && (logOnOffSetting.ToLower() == "on"))
        {
            _isLoggingOn = true;
        }

        _connectionName = connStringName;

        _connectionString = ConfigurationManager.AppSettings.Get(connStringName);
    }

    #region DataSet

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <param name="paramArray">��ȸ ���ǿ��� ����ϴ� SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure �߿� ����</param>
    /// <param name="dataTableMappings">���� ���̺��� Fill�ϴ� ���, �� DataTable �̸� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray, CommandType cmdType, DataTableMapping[] dataTableMappings)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, SqlParameter[] paramArray, CommandType cmdType, DataTableMapping[] dataTableMappings)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        OleDbDataAdapter oracleAdapter = new OleDbDataAdapter(strQuery, _connectionString);

        oracleAdapter.SelectCommand.CommandType = cmdType;
        oracleAdapter.SelectCommand.CommandTimeout = _commandTimeOut;

        try
        {
            
            if (dsDataSet == null)
            {
                dsDataSet = new DataSet();
            }
            if (paramArray != null)
            {
                foreach (OleDbParameter param in paramArray)
                {
                    oracleAdapter.SelectCommand.Parameters.Add(param);
                }
            }

            DateTime dtStart = DateTime.Now;

            if (strAlias != null)
            {
                oracleAdapter.Fill(dsDataSet, strAlias);
            }
            else
            {
                if (dataTableMappings != null)
                {
                    oracleAdapter.TableMappings.AddRange(dataTableMappings);
                }
                oracleAdapter.Fill(dsDataSet);
            }
            oracleAdapter.SelectCommand.Parameters.Clear();

            DateTime dtEnd = DateTime.Now;

            _logContents += "[Result] : Success" + System.Environment.NewLine;

            TimeSpan timeSpan = dtEnd - dtStart;

            _logContents += "[Spending time] : " + timeSpan.Milliseconds + "M/Sec" + System.Environment.NewLine;
        }
        catch (Exception ex)
        {
            _logContents += "[Result] : Fail" + System.Environment.NewLine;

            _logContents += "[Ex Message] : " + ex.Message + System.Environment.NewLine;
            _logContents += "[Excute Query] : " + strQuery + System.Environment.NewLine;

            throw ex;
        }
        finally
        {
            if ((oracleAdapter.SelectCommand != null) && (oracleAdapter.SelectCommand.Connection != null))
            {
                if (oracleAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    oracleAdapter.SelectCommand.Connection.Close();
                }
            }

            DataAccessLogWrite();
        }

        return dsDataSet;
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���.
    /// ��ȸ ���� Query���� Text�̿ܿ� StoredProcedure �� ��������
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <param name="paramArray">��ȸ ���ǿ��� ����ϴ� SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure �߿� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray, CommandType cmdType)
    {
        return GetDataSet(strQuery, strAlias, dsDataSet, paramArray, cmdType, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <param name="paramArray">��ȸ ���ǿ��� ����ϴ� SqlParameter[]</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray)
    {
        return GetDataSet(strQuery, strAlias, dsDataSet, paramArray, CommandType.Text, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet)
    {
        return GetDataSet(strQuery, strAlias, dsDataSet, null, CommandType.Text, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias)
    {
        return GetDataSet(strQuery, strAlias, null, null, CommandType.Text, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="dataTableMappings">���� ���̺��� Fill�ϴ� ���, �� DataTable �̸� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, DataTableMapping[] dataTableMappings)
    {
        return GetDataSet(strQuery, null, null, null, CommandType.Text, dataTableMappings);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery)
    {
        return GetDataSet(strQuery, null, null, null, CommandType.Text, null);
    }

    // 2003. 4.14 �߰� ���� : startRecord, maxRecords ����
    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="startRecord">���ڵ� ���� ��ġ</param>
    /// <param name="maxRecords">���ڵ� ������ ��ġ</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <param name="paramArray">��ȸ ���ǿ��� ����ϴ� SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure �߿� ����</param>
    /// <param name="dataTableMappings">���� ���̺��� Fill�ϴ� ���, �� DataTable �̸� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray, CommandType cmdType, DataTableMapping[] dataTableMappings)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, SqlParameter[] paramArray, CommandType cmdType, DataTableMapping[] dataTableMappings)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        OleDbDataAdapter oracleAdapter = new OleDbDataAdapter(strQuery, _connectionString);

        oracleAdapter.SelectCommand.CommandType = cmdType;
        oracleAdapter.SelectCommand.CommandTimeout = _commandTimeOut;

        try
        {
            if (dsDataSet == null)
            {
                dsDataSet = new DataSet();
            }
            if (paramArray != null)
            {
                foreach (OleDbParameter param in paramArray)
                {
                    oracleAdapter.SelectCommand.Parameters.Add(param);
                }
            }
            
            DateTime dtStart = DateTime.Now;

            if (strAlias != null)
            {
                oracleAdapter.Fill(dsDataSet, startRecord, maxRecords, strAlias);
            }
            else
            {
                if (dataTableMappings != null)
                {
                    oracleAdapter.TableMappings.AddRange(dataTableMappings);
                }
                oracleAdapter.Fill(dsDataSet);
            }
            oracleAdapter.SelectCommand.Parameters.Clear();

            DateTime dtEnd = DateTime.Now;

            _logContents += "[Result] : Success" + System.Environment.NewLine;

            TimeSpan timeSpan = dtEnd - dtStart;

            _logContents += "[Spending time] : " + timeSpan.Milliseconds + "M/Sec" + System.Environment.NewLine;
        }
        catch (Exception ex)
        {
            _logContents += "[Result] : Fail" + System.Environment.NewLine;

            _logContents += "[Ex Message] : " + ex.Message + System.Environment.NewLine;

            throw ex;
        }
        finally
        {
            if ((oracleAdapter.SelectCommand != null) && (oracleAdapter.SelectCommand.Connection != null))
            {
                if (oracleAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    oracleAdapter.SelectCommand.Connection.Close();
                }
            }

            DataAccessLogWrite();
        }

        return dsDataSet;
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���.
    /// ��ȸ ���� Query���� Text�̿ܿ� StoredProcedure �� ��������
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="startRecord">���ڵ� ���� ��ġ</param>
    /// <param name="maxRecords">���ڵ� ������ ��ġ</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <param name="paramArray">��ȸ ���ǿ��� ����ϴ� SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure �߿� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray, CommandType cmdType)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, dsDataSet, paramArray, cmdType, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="startRecord">���ڵ� ���� ��ġ</param>
    /// <param name="maxRecords">���ڵ� ������ ��ġ</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <param name="paramArray">��ȸ ���ǿ��� ����ϴ� SqlParameter[]</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, dsDataSet, paramArray, CommandType.Text, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="startRecord">���ڵ� ���� ��ġ</param>
    /// <param name="maxRecords">���ڵ� ������ ��ġ</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <param name="dsDataSet">������ �̹� ����ϴ� DataSet�� ä�ﶧ ������</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, dsDataSet, null, CommandType.Text, null);
    }

    /// <summary>
    /// Query ������ �Բ�, ���������̺� ���  �����͸� ���� �����ͼ��� �Ķ���ͷ� �Ѱ�,
    /// �����ͼ¿� �����͸� ä���
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="startRecord">���ڵ� ���� ��ġ</param>
    /// <param name="maxRecords">���ڵ� ������ ��ġ</param>
    /// <param name="strAlias">DataSet ���� DataTable �̸� ����, UI���� Merge�ϴµ� �ַ� ����</param>
    /// <returns>Query ��ȸ ��� DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, null, null, CommandType.Text, null);
    }
    // 2003.4.14 �߰� ��

    #endregion


    #region ExecuteReader
    /// <summary>
    /// DB�� ������ ���¿��� Stream ���� ������ �׼���
    /// .NET �� ExecuteReader �� ������ �޼���
    /// �ݵ�� ���� ����� SqlDataReader �� close ���Ѿ� �Ѵ�.
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="paramArray">��ȸ ���ǿ� ����ϴ� SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure �߿� ����</param>
    /// <returns>SqlDataReader ��ü</returns>
    [AutoComplete]
    public OleDbDataReader ExecuteReader(string strQuery, OleDbParameter[] paramArray, CommandType cmdType)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::ExecuteReader(string strQuery, SqlParameter[] paramArray, CommandType cmdType)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        OleDbDataReader reader = null;

        OleDbConnection sqlConnection = new OleDbConnection(_connectionString);
        OleDbCommand cmd = new OleDbCommand(strQuery, sqlConnection);

        try
        {
            
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = _commandTimeOut;

            if (paramArray != null)
            {
                foreach (OleDbParameter param in paramArray)
                {
                    cmd.Parameters.Add(param);
                }
            }

            DateTime dtStart = DateTime.Now;

            sqlConnection.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
        }
        catch (Exception ex)
        {
            _logContents += "[Result] : Fail" + System.Environment.NewLine;
            _logContents += "[Ex Message] : " + ex.Message + System.Environment.NewLine;

            throw ex;
        }
        finally
        {
            DataAccessLogWrite();
        }

        return reader;
    }

    /// <summary>
    /// DB�� ������ ���¿��� Stream ���� ������ �׼���
    /// .NET �� ExecuteReader �� ������ �޼���
    /// �ݵ�� ���� ����� SqlDataReader �� close ���Ѿ� �Ѵ�.
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <param name="paramArray">��ȸ ���ǿ� ����ϴ� SqlParameter[]</param>
    /// <returns>SqlDataReader ��ü</returns>
    [AutoComplete]
    public OleDbDataReader ExecuteReader(string strQuery, OleDbParameter[] paramArray)
    {
        return ExecuteReader(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// DB�� ������ ���¿��� Stream ���� ������ �׼���
    /// .NET �� ExecuteReader �� ������ �޼���
    /// �ݵ�� ���� ����� SqlDataReader �� close ���Ѿ� �Ѵ�.
    /// </summary>
    /// <param name="strQuery">��ȸ ���� Query��</param>
    /// <returns>SqlDataReader ��ü</returns>
    [AutoComplete]
    public OleDbDataReader ExecuteReader(string strQuery)
    {
        return ExecuteReader(strQuery, null, CommandType.Text);
    }

    #endregion

    //**************************************************************************************************
    // ���� Insert / update /delete ���� Query ���� �����ϸ� �̿� ���� ����� ���ڵ� ������ �ǵ�����.
    //**************************************************************************************************
    #region ExecuteQuery
    [AutoComplete]
    public int ExecuteNonQuery(string strQuery, OleDbParameter[] paramArray, CommandType cmdType)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::ExecuteNonQuery(string strQuery, SqlParameter[] paramArray, CommandType cmdType)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        int affectedRows = 0;

        OleDbConnection sqlConnection = new OleDbConnection(_connectionString);
        OleDbCommand cmd = new OleDbCommand(strQuery, sqlConnection);

        try
        {
            
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = _commandTimeOut;

            if (paramArray != null)
            {
                foreach (OleDbParameter param in paramArray)
                {
                    cmd.Parameters.Add(param);
                }
            }

            DateTime dtStart = DateTime.Now;

            sqlConnection.Open();
            affectedRows = cmd.ExecuteNonQuery();

            DateTime dtEnd = DateTime.Now;
            _logContents += "[Result] : Affected rows = " + affectedRows.ToString() + System.Environment.NewLine;

            TimeSpan timeSpan = dtEnd - dtStart;
            _logContents += "[Spending time] : " + timeSpan.Milliseconds + "M/Sec" + System.Environment.NewLine;

            sqlConnection.Close();
            cmd.Parameters.Clear();
        }
        catch (Exception ex)
        {
            _logContents += "[Result] : Fail" + System.Environment.NewLine;
            _logContents += "[Ex Message] : " + ex.Message + System.Environment.NewLine;

            throw ex;
        }
        finally
        {
            if ((cmd != null) && (cmd.Connection != null))
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            DataAccessLogWrite();
        }

        return affectedRows;
    }

    /// <summary>
    /// .NET �� ExecuteNonQuery �� ������ �޼���.
    /// ���� Insert / update /delete ���� Query ���� �����ϸ� �̿� ���� ����� ���ڵ� ������ �ǵ�����
    /// </summary>
    /// <param name="strQuery">���� Query��</param>
    /// <param name="paramArray">���ǿ� ����ϴ� SqlParameter[]</param>
    /// <returns>����� ���ڵ� ����</returns>
    [AutoComplete]
    public int ExecuteNonQuery(string strQuery, OleDbParameter[] paramArray)
    {
        return ExecuteNonQuery(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// .NET �� ExecuteNonQuery �� ������ �޼���.
    /// ���� Insert / update /delete ���� Query ���� �����ϸ� �̿� ���� ����� ���ڵ� ������ �ǵ�����
    /// </summary>
    /// <param name="strQuery">���� Query��</param>
    /// <returns>����� ���ڵ� ����</returns>
    [AutoComplete]
    public int ExecuteNonQuery(string strQuery)
    {
        return ExecuteNonQuery(strQuery, null, CommandType.Text);
    }

    #endregion

    #region ExecuteScalar
    /// <summary>
    /// .NET �� ExecuteScalar �� ������ �޼���.
    /// ���� ��ü���� ��ȸ
    /// </summary>
    /// <param name="strQuery">��ȸ Query��</param>
    /// <param name="paramArray">��ȸ ���ǿ� ����ϴ� SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure �߿� ����</param>
    /// <returns>��ȸ�� ��ü</returns>
    [AutoComplete]
    public object ExecuteScalar(string strQuery, OleDbParameter[] paramArray, CommandType cmdType)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::ExecuteScalar(string strQuery, SqlParameter[] paramArray, CommandType cmdType)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        object oRc = 0;

        OleDbConnection sqlConnection = new OleDbConnection(_connectionString);
        OleDbCommand cmd = new OleDbCommand(strQuery, sqlConnection);

        try
        {
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = _commandTimeOut;

            if (paramArray != null)
            {
                foreach (OleDbParameter param in paramArray)
                {
                    cmd.Parameters.Add(param);
                }
            }

            sqlConnection.Open();
            oRc = cmd.ExecuteScalar();
            sqlConnection.Close();
            cmd.Parameters.Clear();
        }
        catch (Exception ex)
        {
            _logContents += "[Result] : Fail" + System.Environment.NewLine;
            _logContents += "[Ex Message] : " + ex.Message + System.Environment.NewLine;

            throw ex;
        }
        finally
        {
            if ((cmd != null) && (cmd.Connection != null))
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            DataAccessLogWrite();
        }

        return oRc;
    }

    /// <summary>
    /// .NET �� ExecuteScalar �� ������ �޼���.
    /// ���� ��ü���� ��ȸ
    /// </summary>
    /// <param name="strQuery">��ȸ Query��</param>
    /// <param name="paramArray">��ȸ ���ǿ� ����ϴ� SqlParameter[]</param>
    /// <returns>��ȸ�� ��ü</returns>
    [AutoComplete]
    public object ExecuteScalar(string strQuery, OleDbParameter[] paramArray)
    {
        return ExecuteScalar(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// .NET �� ExecuteScalar �� ������ �޼���.
    /// ���� ��ü���� ��ȸ
    /// </summary>
    /// <param name="strQuery">��ȸ Query��</param>
    /// <returns>��ȸ�� ��ü</returns>
    [AutoComplete]
    public object ExecuteScalar(string strQuery)
    {
        return ExecuteScalar(strQuery, null, CommandType.Text);
    }
    #endregion


    private void DataAccessLogWrite()
    {
        if (_isLoggingOn)
        {
            AppLogger.LogWrite(AppLogger.LoggingCategory.DataBase, _logTitle, _logContents);
        }
    }
}
