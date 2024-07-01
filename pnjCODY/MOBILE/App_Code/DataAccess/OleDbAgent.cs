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
    /// Connection string을 할당 하거나 읽는다.
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
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <param name="paramArray">조회 조건에서 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <param name="dataTableMappings">다중 테이블을 Fill하는 경우, 각 DataTable 이름 지정</param>
    /// <returns>Query 조회 결과 DataSet</returns>
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
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다.
    /// 조회 조건 Query문은 Text이외에 StoredProcedure 도 지정가능
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <param name="paramArray">조회 조건에서 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray, CommandType cmdType)
    {
        return GetDataSet(strQuery, strAlias, dsDataSet, paramArray, cmdType, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <param name="paramArray">조회 조건에서 사용하는 SqlParameter[]</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray)
    {
        return GetDataSet(strQuery, strAlias, dsDataSet, paramArray, CommandType.Text, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet)
    {
        return GetDataSet(strQuery, strAlias, dsDataSet, null, CommandType.Text, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, string strAlias)
    {
        return GetDataSet(strQuery, strAlias, null, null, CommandType.Text, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="dataTableMappings">다중 테이블을 Fill하는 경우, 각 DataTable 이름 지정</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, DataTableMapping[] dataTableMappings)
    {
        return GetDataSet(strQuery, null, null, null, CommandType.Text, dataTableMappings);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery)
    {
        return GetDataSet(strQuery, null, null, null, CommandType.Text, null);
    }

    // 2003. 4.14 추가 시작 : startRecord, maxRecords 지원
    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="startRecord">레코드 시작 위치</param>
    /// <param name="maxRecords">레코드 마지막 위치</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <param name="paramArray">조회 조건에서 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <param name="dataTableMappings">다중 테이블을 Fill하는 경우, 각 DataTable 이름 지정</param>
    /// <returns>Query 조회 결과 DataSet</returns>
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
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다.
    /// 조회 조건 Query문은 Text이외에 StoredProcedure 도 지정가능
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="startRecord">레코드 시작 위치</param>
    /// <param name="maxRecords">레코드 마지막 위치</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <param name="paramArray">조회 조건에서 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray, CommandType cmdType)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, dsDataSet, paramArray, cmdType, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="startRecord">레코드 시작 위치</param>
    /// <param name="maxRecords">레코드 마지막 위치</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <param name="paramArray">조회 조건에서 사용하는 SqlParameter[]</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet, OleDbParameter[] paramArray)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, dsDataSet, paramArray, CommandType.Text, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="startRecord">레코드 시작 위치</param>
    /// <param name="maxRecords">레코드 마지막 위치</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <param name="dsDataSet">기존에 이미 사용하는 DataSet에 채울때 설정함</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias, DataSet dsDataSet)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, dsDataSet, null, CommandType.Text, null);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="startRecord">레코드 시작 위치</param>
    /// <param name="maxRecords">레코드 마지막 위치</param>
    /// <param name="strAlias">DataSet 내에 DataTable 이름 지정, UI에서 Merge하는데 주로 사용됨</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    [AutoComplete]
    public DataSet GetDataSet(string strQuery, int startRecord, int maxRecords, string strAlias)
    {
        return GetDataSet(strQuery, startRecord, maxRecords, strAlias, null, null, CommandType.Text, null);
    }
    // 2003.4.14 추가 끝

    #endregion


    #region ExecuteReader
    /// <summary>
    /// DB을 연결한 상태에서 Stream 으로 데이터 액세스
    /// .NET 의 ExecuteReader 를 랩퍼한 메서드
    /// 반드시 최종 사용후 SqlDataReader 를 close 시켜야 한다.
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="paramArray">조회 조건에 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>SqlDataReader 객체</returns>
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
    /// DB을 연결한 상태에서 Stream 으로 데이터 액세스
    /// .NET 의 ExecuteReader 를 랩퍼한 메서드
    /// 반드시 최종 사용후 SqlDataReader 를 close 시켜야 한다.
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="paramArray">조회 조건에 사용하는 SqlParameter[]</param>
    /// <returns>SqlDataReader 객체</returns>
    [AutoComplete]
    public OleDbDataReader ExecuteReader(string strQuery, OleDbParameter[] paramArray)
    {
        return ExecuteReader(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// DB을 연결한 상태에서 Stream 으로 데이터 액세스
    /// .NET 의 ExecuteReader 를 랩퍼한 메서드
    /// 반드시 최종 사용후 SqlDataReader 를 close 시켜야 한다.
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <returns>SqlDataReader 객체</returns>
    [AutoComplete]
    public OleDbDataReader ExecuteReader(string strQuery)
    {
        return ExecuteReader(strQuery, null, CommandType.Text);
    }

    #endregion

    //**************************************************************************************************
    // 보통 Insert / update /delete 등의 Query 문을 수행하며 이에 따른 실행된 레코드 갯수를 되돌린다.
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
    /// .NET 의 ExecuteNonQuery 의 랩퍼한 메서드.
    /// 보통 Insert / update /delete 등의 Query 문을 수행하며 이에 따른 실행된 레코드 갯수를 되돌린다
    /// </summary>
    /// <param name="strQuery">실행 Query문</param>
    /// <param name="paramArray">조건에 사용하는 SqlParameter[]</param>
    /// <returns>실행된 레코드 갯수</returns>
    [AutoComplete]
    public int ExecuteNonQuery(string strQuery, OleDbParameter[] paramArray)
    {
        return ExecuteNonQuery(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// .NET 의 ExecuteNonQuery 의 랩퍼한 메서드.
    /// 보통 Insert / update /delete 등의 Query 문을 수행하며 이에 따른 실행된 레코드 갯수를 되돌린다
    /// </summary>
    /// <param name="strQuery">실행 Query문</param>
    /// <returns>실행된 레코드 갯수</returns>
    [AutoComplete]
    public int ExecuteNonQuery(string strQuery)
    {
        return ExecuteNonQuery(strQuery, null, CommandType.Text);
    }

    #endregion

    #region ExecuteScalar
    /// <summary>
    /// .NET 의 ExecuteScalar 의 랩퍼한 메서드.
    /// 단일 객체값만 조회
    /// </summary>
    /// <param name="strQuery">조회 Query문</param>
    /// <param name="paramArray">조회 조건에 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>조회된 객체</returns>
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
    /// .NET 의 ExecuteScalar 의 랩퍼한 메서드.
    /// 단일 객체값만 조회
    /// </summary>
    /// <param name="strQuery">조회 Query문</param>
    /// <param name="paramArray">조회 조건에 사용하는 SqlParameter[]</param>
    /// <returns>조회된 객체</returns>
    [AutoComplete]
    public object ExecuteScalar(string strQuery, OleDbParameter[] paramArray)
    {
        return ExecuteScalar(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// .NET 의 ExecuteScalar 의 랩퍼한 메서드.
    /// 단일 객체값만 조회
    /// </summary>
    /// <param name="strQuery">조회 Query문</param>
    /// <returns>조회된 객체</returns>
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
