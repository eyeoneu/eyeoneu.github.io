using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

/// <summary>
/// DbParam 를 다루기 위한 Helper 클래스
/// </summary>
public class DbParamHelper
{
    /// <summary>
    /// DbParamHelper 기본 생성자
    /// </summary>
    public DbParamHelper()
    {
    }

    #region Add for SqlParameter
    /// <summary>
    /// ref로써 SqlParameter[]를 받아서 새로운 SqlParameter 를 생성 Array에 추가한다.
    /// 기존값: SqlParameter의 출력 속성은 Out
    /// </summary>
    /// <param name="paramArray">기존에 SqlParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="paramValue">새롭게 생성하고자 하는 SqlParamter 에 사용할 값</param>
    public static void Add(ref SqlParameter[] paramArray, string parameterName, object paramValue)
    {
        Add(ref paramArray, parameterName, paramValue, ParameterDirection.Output);
    }

    /// <summary>
    /// ref로써 SqlParameter[]를 받아서 새로운 SqlParameter 를 생성 Array에 추가한다.
    /// </summary>
    /// <param name="paramArray">기존에 SqlParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="paramValue">새롭게 생성하고자 하는 SqlParamter 에 사용할 값</param>
    /// <param name="direction">SqlParameter의 출력 속성지정</param>
    public static void Add(ref SqlParameter[] paramArray, string parameterName, object paramValue, ParameterDirection direction)
    {
        SqlParameter parameter = new SqlParameter(parameterName, paramValue);
        parameter.Direction = direction;

        Add(ref paramArray, parameter);
    }

    /// <summary>
    /// ref로써 SqlParameter[]를 받아서 새로운 SqlParameter 를 생성 Array에 추가한다.
    /// SqlParameter 의 타입속성을 추가적으로 지정할수 있다.
    /// 기본값: SqlParameter의 출력 속성은 Out 
    /// </summary>
    /// <param name="paramArray">기존에 SqlParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="dbType">추가하고자 하는 SqlParameter의 DB타입지정</param>
    public static void Add(ref SqlParameter[] paramArray, string parameterName, SqlDbType dbType)
    {
        Add(ref paramArray, parameterName, dbType, ParameterDirection.Output);
    }

    /// <summary>
    /// ref로써 SqlParameter[]를 받아서 새로운 SqlParameter 를 생성 Array에 추가한다.
    /// SqlParameter 의 타입속성을 추가적으로 지정할수 있다.
    /// </summary>
    /// <param name="paramArray">기존에 SqlParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParameter 에 사용할 이름</param>
    /// <param name="dbType">추가하고자 하는 SqlParamter의 DB타입지정</param>
    /// <param name="direction">SqlParameter의 출력 속성지정</param>
    public static void Add(ref SqlParameter[] paramArray, string parameterName, SqlDbType dbType, ParameterDirection direction)
    {
        SqlParameter parameter = new SqlParameter(parameterName, dbType);
        parameter.Direction = direction;

        Add(ref paramArray, parameter);
    }
    
    /// <summary>
    /// ref로써 SqlParameter[]를 받아서 새로운 SqlParameter[] 를 추가한다.
    /// </summary>
    /// <param name="paramArray">기존 SqlParameter[]</param>
    /// <param name="newParameters">추가하고자 하는 SqlParameter[]</param>
    public static void Add(ref SqlParameter[] paramArray, params SqlParameter[] newParameters)
    {
        SqlParameter[] newArray = Array.CreateInstance(typeof(SqlParameter), paramArray.Length + newParameters.Length) as SqlParameter[];
        paramArray.CopyTo(newArray, 0);
        newParameters.CopyTo(newArray, paramArray.Length);

        paramArray = newArray;
    }
    
    #endregion

    #region Add for OleDbParameter

    /// <summary>
    /// ref로써 OleDbParameter[]를 받아서 새로운 OleDbParameter 를 생성 Array에 추가한다.
    /// 기존값: OleDbParameter의 출력 속성은 Out
    /// </summary>
    /// <param name="paramArray">기존에 OleDbParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="paramValue">새롭게 생성하고자 하는 SqlParamter 에 사용할 값</param>
    public static void Add(ref OleDbParameter[] paramArray, string parameterName, object paramValue)
    {
        Add(ref paramArray, parameterName, paramValue, ParameterDirection.Output);
    }

    /// <summary>
    /// ref로써 OleDbParameter[]를 받아서 새로운 OleDbParameter 를 생성 Array에 추가한다.
    /// </summary>
    /// <param name="paramArray">기존에 OleDbParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="paramValue">새롭게 생성하고자 하는 SqlParamter 에 사용할 값</param>
    /// <param name="direction">OleDbParameter의 출력 속성지정</param>
    public static void Add(ref OleDbParameter[] paramArray, string parameterName, object paramValue, ParameterDirection direction)
    {
        OleDbParameter parameter = new OleDbParameter(parameterName, paramValue);
        parameter.Direction = direction;

        Add(ref paramArray, parameter);
    }

    /// <summary>
    /// ref로써 OleDbParameter[]를 받아서 새로운 OleDbParameter 를 생성 Array에 추가한다.
    /// OleDbParameter 의 타입속성을 추가적으로 지정할수 있다.
    /// 기본값: OleDbParameter의 출력 속성은 Out 
    /// </summary>
    /// <param name="paramArray">기존에 OleDbParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="dbType">추가하고자 하는 OleDbParameter의 DB타입지정</param>
    public static void Add(ref OleDbParameter[] paramArray, string parameterName, OleDbType dbType)
    {
        Add(ref paramArray, parameterName, dbType, ParameterDirection.Output);
    }


    /// <summary>
    /// ref로써 OleDbParameter[]를 받아서 새로운 OleDbParameter 를 생성 Array에 추가한다.
    /// OleDbParameter 의 타입속성을 추가적으로 지정할수 있다.
    /// </summary>
    /// <param name="paramArray">기존에 OleDbParameter[]</param>
    /// <param name="parameterName">새롭게 생성하고자 하는 SqlParamter 에 사용할 이름</param>
    /// <param name="dbType">추가하고자 하는 OleDbParameter의 DB타입지정</param>
    /// <param name="direction">OleDbParameter의 출력 속성지정</param>
    public static void Add(ref OleDbParameter[] paramArray, string parameterName, OleDbType dbType, ParameterDirection direction)
    {
        OleDbParameter parameter = new OleDbParameter(parameterName, dbType);
        parameter.Direction = direction;

        Add(ref paramArray, parameter);
    }

    /// <summary>
    /// ref로써 OleDbParameter[]를 받아서 새로운 OleDbParameter[] 를 추가한다.
    /// </summary>
    /// <param name="paramArray">기존 OleDbParameter[]</param>
    /// <param name="newParameters">추가하고자 하는 OleDbParameter[]</param>
    public static void Add(ref OleDbParameter[] paramArray, params OleDbParameter[] newParameters)
    {
        OleDbParameter[] newArray = Array.CreateInstance(typeof(OleDbParameter), paramArray.Length + newParameters.Length) as OleDbParameter[];
        paramArray.CopyTo(newArray, 0);
        newParameters.CopyTo(newArray, paramArray.Length);

        paramArray = newArray;
    }
    #endregion

    /// <summary>
    /// SqlParameter 로 부터 해당 파라미터의 값을 얻어냄
    /// 찾지 못할경우 IndexOutOfRangeException 발생
    /// </summary>
    /// <param name="paramArray">SqlParameter Array</param>
    /// <param name="parameterName">찾고자 하는 SqlParameter 키값</param>
    /// <returns>찾은 결과 값(Value) </returns>
    public static object GetValue(SqlParameter[] paramArray, string parameterName)
    {
        foreach (SqlParameter param in paramArray)
        {
            if (param.ParameterName == parameterName)
            {
                return param.Value;
            }
        }

        throw new IndexOutOfRangeException(parameterName + "라는 이름을 가진 SqlParameter가 존재하지 않습니다.");
    }

    /// <summary>
    /// OleDbParameter 로 부터 해당 파라미터의 값을 얻어냄
    /// 찾지 못할경우 IndexOutOfRangeException 발생
    /// </summary>
    /// <param name="paramArray">OleDbParameter Array</param>
    /// <param name="parameterName">찾고자 하는 OleDbParameter 키값</param>
    /// <returns>찾은 결과 값(Value) </returns>
    public static object GetValue(OleDbParameter[] paramArray, string parameterName)
    {
        foreach (OleDbParameter param in paramArray)
        {
            if (param.ParameterName == parameterName)
            {
                return param.Value;
            }
        }

        throw new IndexOutOfRangeException(parameterName + "라는 이름을 가진 OleDbParameter가 존재하지 않습니다.");
    }

    /// <summary>
    /// SqlParameter에 해당 파라미터의 값을 설정함
    /// 찾지 못할 경우 IndexOutOfRangeException 발생
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="paramArray">SqlParameter Array</param>
    /// <param name="parameterName">찾고자 하는 SqlParameter 키값</param>
    /// <param name="value">설정하고 하는 SqlParameter 값</param>
    public static void SetValue<T>(SqlParameter[] paramArray, string parameterName, T value)
    {
        foreach (SqlParameter param in paramArray)
        {
            if (param.ParameterName == parameterName)
            {
                param.Value = value;
                return;
            }
        }

        throw new IndexOutOfRangeException(parameterName + "라는 이름을 가진 SqlParameter가 존재하지 않습니다.");
    }

    public static void SetValue<T>(OleDbParameter[] paramArray, string parameterName, T value)
    {
        foreach (OleDbParameter param in paramArray)
        {
            if (param.ParameterName == parameterName)
            {
                param.Value = value;
                return;
            }
        }

        throw new IndexOutOfRangeException(parameterName + "라는 이름을 가진 SqlParameter가 존재하지 않습니다.");
    }
}
