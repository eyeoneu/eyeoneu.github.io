using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
/// <summary>
/// SqlDbAgent의 요약 설명입니다.
/// </summary>
public class SqlDbAgent
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


    /// <summary>
    /// Default 생성자
    /// </summary>
    public SqlDbAgent()
    {
        string logOnOffSetting = ConfigurationManager.AppSettings.Get("LogOnOff_DataAccess");

        if ((!string.IsNullOrEmpty(logOnOffSetting)) && (logOnOffSetting.ToLower() == "on"))
        {
            _isLoggingOn = true;
        }
    }

    /// <summary>
    /// Config의 이름을 이용하여 Connection string을 읽어 내어 객체를 생성
    /// </summary>
    /// <param name="connStringName"></param>
    public SqlDbAgent(string connStringName)
    {
        string logOnOffSetting = ConfigurationManager.AppSettings.Get("LogOnOff_DataAccess");

        if ((!string.IsNullOrEmpty(logOnOffSetting)) && (logOnOffSetting.ToLower() == "on"))
        {
            _isLoggingOn = true;
        }

        _connectionName = connStringName;

        _connectionString = ConfigurationManager.AppSettings.Get(connStringName);
    }

    #region GetDataSet
		    
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
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, SqlParameter[] paramArray, CommandType cmdType, DataTableMapping[] dataTableMappings)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::GetDataSet(string strQuery, string strAlias, DataSet dsDataSet, SqlParameter[] paramArray, CommandType cmdType, DataTableMapping[] dataTableMappings)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strQuery, _connectionString);

        sqlAdapter.SelectCommand.CommandType = cmdType;
        sqlAdapter.SelectCommand.CommandTimeout = _commandTimeOut;

        try
        {
            if (dsDataSet == null)
            {
                dsDataSet = new DataSet();
            }
            if (paramArray != null)
            {
                foreach (SqlParameter param in paramArray)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(param);
                }
            }

            DateTime dtStart = DateTime.Now;

            if (strAlias != null)
            {
                sqlAdapter.Fill(dsDataSet, strAlias);
            }
            else
            {
                if (dataTableMappings != null)
                {
                    sqlAdapter.TableMappings.AddRange(dataTableMappings);
                }
                sqlAdapter.Fill(dsDataSet);
            }
            sqlAdapter.SelectCommand.Parameters.Clear();

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
            if ((sqlAdapter.SelectCommand != null) && (sqlAdapter.SelectCommand.Connection != null))
            {
                if (sqlAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    sqlAdapter.SelectCommand.Connection.Close();
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
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet,
       SqlParameter[] paramArray, CommandType cmdType)
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
    public DataSet GetDataSet(string strQuery, string strAlias, DataSet dsDataSet,
       SqlParameter[] paramArray)
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
    public DataSet GetDataSet(string strQuery, DataTableMapping[] dataTableMappings)
    {
        return GetDataSet(strQuery, null, null, null, CommandType.Text,
           dataTableMappings);
    }

    /// <summary>
    /// Query 구문과 함께, 데이터테이블 명과  데이터를 담을 데이터셋을 파라메터로 넘겨,
    /// 데이터셋에 데이터를 채운다
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <returns>Query 조회 결과 DataSet</returns>
    public DataSet GetDataSet(string strQuery)
    {
        return GetDataSet(strQuery, null, null, null, CommandType.Text, null);
    }

    #endregion

    #region [ GetDataTable ]
    /// <summary>
    /// 질의 문자열을 이용하여 DataTable을 얻는다.
    /// </summary>
    /// <param name="query">질의 문자열</param>
    /// <param name="parameters">Parameters</param>
    /// <returns></returns>
    public DataTable GetDataTable(string query, List<SqlParameter> parameters)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::GetDataTable(string query, List<SqlParameter> parameters)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + query + System.Environment.NewLine;


        SqlConnection sqlConnection = new SqlConnection(_connectionString);

        DataTable dtResult = new DataTable();

        SqlDataAdapter adapter = new SqlDataAdapter();

        try
        {

            adapter.SelectCommand = new SqlCommand(query, sqlConnection);

            adapter.SelectCommand.CommandType = CommandType.Text;

            adapter.SelectCommand.CommandTimeout = _commandTimeOut;


            //
            // SQL Param seting
            //

            _logContents += "[Params] : ";

            foreach (SqlParameter param in parameters)
            {
                _logContents += param.ParameterName + " = " + param.Value.ToString() + "; ";

                adapter.SelectCommand.Parameters.Add(param);
            }

            _logContents += System.Environment.NewLine;

            DateTime dtStart = DateTime.Now;

            adapter.SelectCommand.Connection.Open();

            adapter.Fill(dtResult);

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
            if ((adapter.SelectCommand != null) && (adapter.SelectCommand.Connection != null))
            {
                if (adapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    adapter.SelectCommand.Connection.Close();
                }
            }

            DataAccessLogWrite();
        }

        return dtResult;

    }

    /// <summary>
    /// 질의 문자열을 이용하여 DataTable을 얻는다.
    /// </summary>
    /// <param name="query">질의 문자열</param>
    /// <returns></returns>
    public DataTable GetDataTable(string query)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::GetDataTable(string query)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + query + System.Environment.NewLine;

        SqlConnection sqlConnection = new SqlConnection(_connectionString);

        DataTable dtResult = new DataTable();

        SqlDataAdapter adapter = new SqlDataAdapter();

        try
        {

            adapter.SelectCommand = new SqlCommand(query, sqlConnection);

            adapter.SelectCommand.CommandType = CommandType.Text;

            adapter.SelectCommand.CommandTimeout = _commandTimeOut;

            DateTime dtStart = DateTime.Now;

            adapter.SelectCommand.Connection.Open();

            adapter.Fill(dtResult);

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
            if ((adapter.SelectCommand != null) && (adapter.SelectCommand.Connection != null))
            {
                if (adapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    adapter.SelectCommand.Connection.Close();
                }
            }

            DataAccessLogWrite();
        }

        return dtResult;

    }

    #endregion

    #region SqlDataReader

    /// <summary>
    /// DB을 연결한 상태에서 Stream 으로 데이터 액세스
    /// .NET 의 ExecuteReader 를 랩퍼한 메서드
    /// 반드시 최종 사용후 SqlDataReader 를 close 시켜야 한다.
    /// </summary>
    /// <param name="strQuery">조회 조건 Query문</param>
    /// <param name="paramArray">조회 조건에 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>SqlDataReader 객체</returns>
    public SqlDataReader ExecuteReader(string strQuery, SqlParameter[] paramArray, CommandType cmdType)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::ExecuteReader(string strQuery, SqlParameter[] paramArray, CommandType cmdType)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;
        
        SqlDataReader reader = null;

        SqlConnection sqlConnection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(strQuery, sqlConnection);

        try
        {
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = _commandTimeOut;

            if (paramArray != null)
            {
                foreach (SqlParameter param in paramArray)
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
            /*
            if ((cmd != null) && (cmd.Connection != null))
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            */
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
    public SqlDataReader ExecuteReader(string strQuery, SqlParameter[] paramArray)
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
    public SqlDataReader ExecuteReader(string strQuery)
    {
        return ExecuteReader(strQuery, null, CommandType.Text);
    }

    #endregion

    #region Execute Scalar

    /// <summary>
    /// .NET 의 ExecuteScalar 의 랩퍼한 메서드.
    /// 단일 객체값만 조회
    /// </summary>
    /// <param name="strQuery">조회 Query문</param>
    /// <param name="paramArray">조회 조건에 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>조회된 객체</returns>
    public object ExecuteScalar(string strQuery, SqlParameter[] paramArray, CommandType cmdType)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::ExecuteScalar(string strQuery, SqlParameter[] paramArray, CommandType cmdType)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        object oRc = 0;

        SqlConnection sqlConnection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(strQuery, sqlConnection);

        try
        {
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = _commandTimeOut;

            if (paramArray != null)
            {
                foreach (SqlParameter param in paramArray)
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
    public object ExecuteScalar(string strQuery, SqlParameter[] paramArray)
    {
        return ExecuteScalar(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// .NET 의 ExecuteScalar 의 랩퍼한 메서드.
    /// 단일 객체값만 조회
    /// </summary>
    /// <param name="strQuery">조회 Query문</param>
    /// <returns>조회된 객체</returns>
    public object ExecuteScalar(string strQuery)
    {
        return ExecuteScalar(strQuery, null, CommandType.Text);
    }

    #endregion

    #region ExecuteQuery

    /// <summary>
    /// .NET 의 ExecuteNonQuery 의 랩퍼한 메서드.
    /// 보통 Insert / update /delete 등의 Query 문을 수행하며 이에 따른 실행된 레코드 갯수를 되돌린다
    /// </summary>
    /// <param name="strQuery">실행 Query문</param>
    /// <param name="paramArray">조건에 사용하는 SqlParameter[]</param>
    /// <param name="cmdType">Text, TableDirect, StoredProcedure 중에 선택</param>
    /// <returns>실행된 레코드 갯수</returns>
    public int ExecuteNonQuery(string strQuery, SqlParameter[] paramArray, CommandType cmdType)
    {
        _logTitle = "Agent Info - " + this.GetType().ToString() + "::ExecuteNonQuery(string strQuery, SqlParameter[] paramArray, CommandType cmdType)";
        _logContents = "[Connection info] : " + _connectionName + System.Environment.NewLine;
        _logContents += "[Query] : " + strQuery + System.Environment.NewLine;

        int affectedRows = 0;

        SqlConnection sqlConnection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(strQuery, sqlConnection);
        
        try
        {
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = _commandTimeOut;

            if (paramArray != null)
            {
                foreach (SqlParameter param in paramArray)
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
    public int ExecuteNonQuery(string strQuery, SqlParameter[] paramArray)
    {
        return ExecuteNonQuery(strQuery, paramArray, CommandType.Text);
    }

    /// <summary>
    /// .NET 의 ExecuteNonQuery 의 랩퍼한 메서드.
    /// 보통 Insert / update /delete 등의 Query 문을 수행하며 이에 따른 실행된 레코드 갯수를 되돌린다
    /// </summary>
    /// <param name="strQuery">실행 Query문</param>
    /// <returns>실행된 레코드 갯수</returns>
    public int ExecuteNonQuery(string strQuery)
    {
        return ExecuteNonQuery(strQuery, null, CommandType.Text);
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
