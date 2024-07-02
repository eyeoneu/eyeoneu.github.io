using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// ======================================
/// 작성일: 2011.11.22
/// 작성자: 정창화
/// 내용  : 코드
/// ======================================
public class Code
{
    SqlDbAgent _agent;
    string query;

    public Code()
    {
        _agent = new SqlDbAgent("SQL_CODY");
    }

    /// <summary>
    /// 커스텀 코드
    /// </summary>
    public DataSet EPM_CODE(string strCODE_CATEGORY)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@CODE_CATEGORY", strCODE_CATEGORY)
        };

        return _agent.GetDataSet("EPM_CODE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 부문/부서 (RBS) 코드
    /// </summary>
    public DataSet EPM_RES_RBS_LIST(string strGB, string strSECT_CD)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@SECT_CD", strSECT_CD)
        };

        return _agent.GetDataSet("EPM_RES_RBS_LIST", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 지원사 L2, L3 코드 (L2=소속, L3=근무부서)
    /// </summary>
    public DataSet EPM_VEN_AREA_LIST(string strGB, string strVEN_CD, string strVEN_AREA)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@VEN_CD", strVEN_CD),
            new SqlParameter("@VEN_AREA", strVEN_AREA)
        };

        return _agent.GetDataSet("EPM_VEN_AREA_LIST", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 우편번호
    /// </summary>
    public DataSet EPM_POST_SEARCH(string strDong)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@SEARCHTEXT", strDong)
        };

        return _agent.GetDataSet("EPM_POST_SEARCH", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장코드 L1
    /// </summary>
    public DataSet EPM_CUSTOMER_STORE()
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", ""),
            new SqlParameter("@CUSTOMER", ""),
            new SqlParameter("@WORKGROUP1", "")
        };

        return _agent.GetDataSet("EPM_CUSTOMER_STORE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장코드 L1
    /// </summary>
    public DataSet EPM_CUSTOMER_STORE(string strTYPE)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", ""),
            new SqlParameter("@CUSTOMER", ""),
            new SqlParameter("@WORKGROUP1", ""),
            new SqlParameter("@TYPE", strTYPE)
        };

        return _agent.GetDataSet("EPM_CUSTOMER_STORE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장코드 L2
    /// </summary>
    public DataSet EPM_CUSTOMER_STORE(string strGB, int intCUSTOMER)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@CUSTOMER", intCUSTOMER),
            new SqlParameter("@WORKGROUP1", "")
        };

        return _agent.GetDataSet("EPM_CUSTOMER_STORE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장코드 L2 : 부문값이 지원1 또는 지원2 일 경우
    /// </summary>
    public DataSet EPM_CUSTOMER_STORE(string strGB, int intCUSTOMER, string strWORKGROUP1)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@CUSTOMER", intCUSTOMER),
            new SqlParameter("@WORKGROUP1", strWORKGROUP1)
        };

        return _agent.GetDataSet("EPM_CUSTOMER_STORE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장코드 L2 : 일일보고에서 거래처 및 매장을 선택할 경우 (공통작업 포함)
    /// </summary>
    public DataSet EPM_CUSTOMER_STORE(string strGB, int intCUSTOMER, string strWORKGROUP1, string strTYPE)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@CUSTOMER", intCUSTOMER),
            new SqlParameter("@WORKGROUP1", strWORKGROUP1),
            new SqlParameter("@TYPE", strTYPE)
        };

        return _agent.GetDataSet("EPM_CUSTOMER_STORE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장코드 : 매장방문관리에서 매장을 선택할 경우
    /// </summary>
    public DataSet EPM_VISIT_STORE_DROPDOWN_LIST(int intRESID, string strDATE, string strGB)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RES_ID", intRESID),
            new SqlParameter("@DATE", strDATE),
            new SqlParameter("@GB", strGB)
        };

        return _agent.GetDataSet("EPM_VISIT_STORE_DROPDOWN_LIST", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// DZICUBE 코드
    /// </summary>
    public DataSet DZICUBE_CODE(string strCTRL_CD)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@CTRL_CD", strCTRL_CD)
        };

        return _agent.GetDataSet("DZICUBE_CODE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }
    
    /// <summary>
    /// DZICUBE_CODE_BY_WORKTYPE 코드
    /// </summary>
    public DataSet DZICUBE_CODE_BY_WORKTYPE(string strWORKTYPE)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RES_WORKTYPE", strWORKTYPE)
        };

        return _agent.GetDataSet("DZICUBE_CODE_BY_WORKTYPE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// DZICUBE 코드
    /// </summary>
    public DataSet DZICUBE_EXC_CODE(string strMODE)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@MODE", strMODE)
        };

        return _agent.GetDataSet("DZICUBE_EXC_CODE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }


    /// <summary>
    /// DZICUBE 코드
    /// </summary>
    public DataSet DZICUBE_CODE_BY_WORKGROUP1(string strWORKGROUP1)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RMK_DC", strWORKGROUP1)
        };

        return _agent.GetDataSet("DZICUBE_CODE_BY_WORKGROUP1", "Table", null, parameterArr, CommandType.StoredProcedure);
    }
    
    /// <summary>
    /// DZICUBE 코드
    /// </summary>
    public DataSet DZICUBE_CODE_BY_WORKGROUP1_EMPLOYMENT(string strWORKGROUP1, string strWORKTYPE)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RMK_DC", strWORKGROUP1),
            new SqlParameter("@WORK_TYPE", strWORKTYPE)
        };

        return _agent.GetDataSet("DZICUBE_CODE_BY_WORKGROUP1", "Table", null, parameterArr, CommandType.StoredProcedure);
    }
}