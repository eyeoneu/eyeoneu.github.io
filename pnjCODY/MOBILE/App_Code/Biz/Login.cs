using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// ======================================
/// 작성일: 2011.11.22
/// 작성자: 정창화
/// 내용  : 로그인 관련
/// ======================================
public class Login
{
    SqlDbAgent _agent;
    string query;

    public Login()
    {
        _agent = new SqlDbAgent("SQL_CODY");
    }

    /// <summary>
    /// 로그인 조회
    /// </summary>
    /// <param name="strEmpId">아이디</param>
    /// <param name="strPassword">패스워드</param>
    /// <param name="strResult">1:로그인 성공 // 사용하지 않음 // 2:패스워드 불일치, 3:탈퇴회원, etc:계정없음 //</param>
    public string EPM_RES_LOGIN_IS_LOGIN(string strRES_Number, string strRES_Pwd)
    {
        query = "EPM_RES_LOGIN_IS_LOGIN";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_NUMBER", strRES_Number),
            new SqlParameter("@RES_PWD", strRES_Pwd)
        };

        DbParamHelper.Add(ref paramArray, "@retVal", SqlDbType.Int);

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);

        return DbParamHelper.GetValue(paramArray, "@retVal").ToString();
    }


    //// 회원 로그인 정보
    public SqlDataReader EPM_RES_LOGIN_INFO_SELECT(string strRES_NUMBER)
    {
        query = "EPM_RES_LOGIN_INFO_SELECT";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_NUMBER", strRES_NUMBER)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

}
