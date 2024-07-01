using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// ======================================
/// 작성일: 2011.11.30
/// 작성자: 정창화
/// 내용  : 근태 현황 정보
/// ======================================
public class Attendance
{
    SqlDbAgent _agent;
    string query;

    public Attendance()
    {
        _agent = new SqlDbAgent("SQL_CODY");
    }

	/// <summary>
    /// 연차신청서 기간 중복건 체크
    /// </summary>
    public SqlDataReader EPM_ATT_REQ_CHECK(int intRES_ID, string strStartDate, string strFinishDate)
    {
        query = "EPM_ATT_REQ_CHECK";

        SqlParameter[] paramArray = {
            new SqlParameter("@REQ_RES_ID", intRES_ID),
            new SqlParameter("@REQ_STARTDATE", strStartDate),
            new SqlParameter("@REQ_FINISHDATE", strFinishDate)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 오늘의 근태 현황
    /// </summary>
    public DataSet EPM_ATT_TODAY_SELECT_MOBILE(string strRES_RBS_CD, string strRES_RBS_AREA_CD)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RES_RBS_CD", strRES_RBS_CD),
            new SqlParameter("@RES_RBS_AREA_CD", strRES_RBS_AREA_CD)
        };

        return _agent.GetDataSet("EPM_ATT_TODAY_SELECT_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 일자별 직종별 근태 목록
    /// </summary>
    public DataSet EPM_ATT_BY_DAY_SELECT_MOBILE(string strDATE, string strMODE, string strRES_RBS_CD, string strRES_RBS_AREA_CD)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@DATE", strDATE),
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@RES_RBS_CD", strRES_RBS_CD),
            new SqlParameter("@RES_RBS_AREA_CD", strRES_RBS_AREA_CD)
        };

        return _agent.GetDataSet("EPM_ATT_BY_DAY_SELECT_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 근태 수정요청 현황
    /// </summary>
    public SqlDataReader EPM_ATT_DAY_REQ_TODAY(string strGB, string strRBS, string strAREA, string strDATE)
    {
        query = "EPM_ATT_DAY_REQ_TODAY";

        SqlParameter[] paramArray = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@RBS", strRBS),
            new SqlParameter("@AREA", strAREA),
            new SqlParameter("@ATTMONTH", strDATE)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 근태 수정요청 현황 목록
    /// </summary>
    public DataSet EPM_ATT_DAY_REQ_TODAY_LIST(string strGB, string strRBS, string strAREA, string strDATE)
    {
        query = "EPM_ATT_DAY_REQ_TODAY";

        SqlParameter[] parameterArr = {
            new SqlParameter("@GB", strGB),
            new SqlParameter("@RBS", strRBS),
            new SqlParameter("@AREA", strAREA),
            new SqlParameter("@ATTMONTH", strDATE)
        };

        return _agent.GetDataSet("EPM_ATT_DAY_REQ_TODAY", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 근태정보
    /// </summary>
    public SqlDataReader EPM_ATT_BY_DAY_ITEM_SELECT_MOBILE(string strDATE, string strMODE, int intATT_DAY_ID)
    {
        query = "EPM_ATT_BY_DAY_ITEM_SELECT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@DATE", strDATE),
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@ATT_DAY_ID", intATT_DAY_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 근태정보 수정 처리
    /// </summary>
    public void EPM_ATT_BY_DAY_ITEM_UPDATE_MOBILE(string strMODE, string strATT_DAY_Code, string strATT_DAY_Icon, int intATT_DAY_ID)
    {
        query = "EPM_ATT_BY_DAY_ITEM_UPDATE_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@ATT_DAY_Code", strATT_DAY_Code),
            new SqlParameter("@ATT_DAY_Icon", strATT_DAY_Icon),
            new SqlParameter("@ATT_DAY_ID", intATT_DAY_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 근태정보 수정요청 처리
    /// </summary>
    public void EPM_ATT_BY_DAY_ITEM_REQUEST_MOBILE(string strMODE, int intATT_DAY_ID, string strATT_DAY_ICON, string strATT_DAY_CODE, string strADR_REQUEST_TEXT, string strADR_RBS, string strADR_ASSAREA, int intADR_RES_ID)
    {
        query = "EPM_ATT_BY_DAY_ITEM_REQUEST_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@ATT_DAY_ID", intATT_DAY_ID),
            new SqlParameter("@ATT_DAY_ICON", strATT_DAY_ICON),
            new SqlParameter("@ATT_DAY_CODE", strATT_DAY_CODE),
            new SqlParameter("@ADR_REQUEST_TEXT", strADR_REQUEST_TEXT),
            new SqlParameter("@ADR_RBS", strADR_RBS),
            new SqlParameter("@ADR_ASSAREA", strADR_ASSAREA),
            new SqlParameter("@ADR_RES_ID", intADR_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 휴무신청서 삽입
    /// </summary>
    public void EPM_ATT_REQ_SUBMIT(string strEXE_GB, int intREQ_WRITER_ID, string strREQ_TYPE, int intREQ_RES_ID, string strREQ_STARTDATE, string strREQ_FINISHDATE, string strREQ_DURATION, string strREQ_TEL, string strREQ_REASON, string strREQ_ATTATCH, string strREQ_DELAY, string strGB, string strASSCON_ID)
    {
        query = "EPM_ATT_REQ_SUBMIT";

        SqlParameter[] paramArray = {

            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@ATT_REQ_ID", null),
            new SqlParameter("@REQ_WRITER_ID", intREQ_WRITER_ID),
            new SqlParameter("@REQ_TYPE", strREQ_TYPE),
            new SqlParameter("@REQ_RES_ID", intREQ_RES_ID),
            new SqlParameter("@REQ_RES_ASSCON_GB", strGB),
            new SqlParameter("@REQ_RES_ASSCON_ID", strASSCON_ID),
            new SqlParameter("@REQ_STARTDATE", strREQ_STARTDATE),
            new SqlParameter("@REQ_FINISHDATE", strREQ_FINISHDATE),
            new SqlParameter("@REQ_DURATION", strREQ_DURATION),
            new SqlParameter("@REQ_APPROVENUMBER", null),
            new SqlParameter("@REQ_APPROVE1", null),
            new SqlParameter("@REQ_APPROVE1DATE", null),
            new SqlParameter("@REQ_APPROVE2", null),
            new SqlParameter("@REQ_APPROVE2DATE", null),
            new SqlParameter("@REQ_APPROVESTATE", null),
            new SqlParameter("@REQ_TEL", strREQ_TEL),
            new SqlParameter("@REQ_REASON", strREQ_REASON),
            new SqlParameter("@REQ_ATTATCH", strREQ_ATTATCH),
            new SqlParameter("@REQ_DELAY", strREQ_DELAY)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 휴무신청서 수정
    /// </summary>
    public void EPM_ATT_REQ_SUBMIT(string strEXE_GB, int intATT_REQ_ID, int intREQ_WRITER_ID, string strREQ_TYPE, int intREQ_RES_ID, string strREQ_STARTDATE, string strREQ_FINISHDATE, string strREQ_DURATION, string strREQ_TEL, string strREQ_REASON, string strREQ_ATTATCH, string strREQ_DELAY, string strGB, string strASSCON_ID)
    {
        query = "EPM_ATT_REQ_SUBMIT";

        SqlParameter[] paramArray = {

            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@ATT_REQ_ID", intATT_REQ_ID),
            new SqlParameter("@REQ_WRITER_ID", intREQ_WRITER_ID),
            new SqlParameter("@REQ_TYPE", strREQ_TYPE),
            new SqlParameter("@REQ_RES_ID", intREQ_RES_ID),
            new SqlParameter("@REQ_RES_ASSCON_GB", strGB),
            new SqlParameter("@REQ_RES_ASSCON_ID", strASSCON_ID),
            new SqlParameter("@REQ_STARTDATE", strREQ_STARTDATE),
            new SqlParameter("@REQ_FINISHDATE", strREQ_FINISHDATE),
            new SqlParameter("@REQ_DURATION", strREQ_DURATION),
            new SqlParameter("@REQ_APPROVENUMBER", null),
            new SqlParameter("@REQ_APPROVE1", null),
            new SqlParameter("@REQ_APPROVE1DATE", null),
            new SqlParameter("@REQ_APPROVE2", null),
            new SqlParameter("@REQ_APPROVE2DATE", null),
            new SqlParameter("@REQ_APPROVESTATE", null),
            new SqlParameter("@REQ_TEL", strREQ_TEL),
            new SqlParameter("@REQ_REASON", strREQ_REASON),
            new SqlParameter("@REQ_ATTATCH", strREQ_ATTATCH),
            new SqlParameter("@REQ_DELAY", strREQ_DELAY)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }
    
    /// <summary>
    /// 휴무신청서 승인
    /// </summary>
    public void EPM_ATT_REQ_SUBMIT(string strEXE_GB, int intATT_REQ_ID, int intREQ_WRITER_ID, string strREQ_TYPE, int intREQ_RES_ID, string strREQ_STARTDATE, string strREQ_FINISHDATE, string strREQ_DURATION, string strREQ_TEL, string strREQ_REASON, string strREQ_ATTATCH, string strREQ_DELAY, string strGB, string strASSCON_ID, string strREQ_APPROVESTATE, string strREQ_REJECTREASON, string strREQ_PhotoPath)
    {
        query = "EPM_ATT_REQ_SUBMIT";

        SqlParameter[] paramArray = {

            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@ATT_REQ_ID", intATT_REQ_ID),
            new SqlParameter("@REQ_WRITER_ID", intREQ_WRITER_ID),
            new SqlParameter("@REQ_TYPE", strREQ_TYPE),
            new SqlParameter("@REQ_RES_ID", intREQ_RES_ID),
            new SqlParameter("@REQ_RES_ASSCON_GB", strGB),
            new SqlParameter("@REQ_RES_ASSCON_ID", strASSCON_ID),
            new SqlParameter("@REQ_STARTDATE", strREQ_STARTDATE),
            new SqlParameter("@REQ_FINISHDATE", strREQ_FINISHDATE),
            new SqlParameter("@REQ_DURATION", strREQ_DURATION),
            new SqlParameter("@REQ_APPROVENUMBER", null),
            new SqlParameter("@REQ_APPROVE1", intREQ_WRITER_ID),
            new SqlParameter("@REQ_APPROVE1DATE", null),
            new SqlParameter("@REQ_APPROVE2", intREQ_WRITER_ID),
            new SqlParameter("@REQ_APPROVE2DATE", null),
            new SqlParameter("@REQ_APPROVESTATE", strREQ_APPROVESTATE),
            new SqlParameter("@REQ_REJECTREASON", strREQ_REJECTREASON),
            new SqlParameter("@REQ_TEL", strREQ_TEL),
            new SqlParameter("@REQ_REASON", strREQ_REASON),
            new SqlParameter("@REQ_ATTATCH", strREQ_ATTATCH),
            new SqlParameter("@REQ_DELAY", strREQ_DELAY),
            new SqlParameter("@REQ_PhotoPath", strREQ_PhotoPath)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 휴무신청서 목록
    /// </summary>
    public DataSet EPM_ATT_REQ_LIST_MOBILE(int intATT_REQ_Writer_ID, string strFROM, string strTO)
    {
        SqlParameter[] parameterArr = {

            new SqlParameter("@ATT_REQ_Writer_ID", intATT_REQ_Writer_ID),
            new SqlParameter("@FROM", strFROM),
            new SqlParameter("@TO", strTO),
        };

        return _agent.GetDataSet("EPM_ATT_REQ_LIST_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 연차신청서 목록
    /// </summary>
    public DataSet EPM_ATT_REQ_LEAVE_LIST_MOBILE(int intATT_REQ_Writer_ID, string strFROM, string strTO)
    {
        SqlParameter[] parameterArr = {

            new SqlParameter("@ATT_REQ_Writer_ID", intATT_REQ_Writer_ID),
            new SqlParameter("@FROM", strFROM),
            new SqlParameter("@TO", strTO),
        };

        return _agent.GetDataSet("EPM_ATT_REQ_LEAVE_LIST_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 휴무신청서 단일 정보
    /// </summary>
    public SqlDataReader EPM_ATT_REQ_SELECT(int intREQ_ID)
    {
        query = "EPM_ATT_REQ_SELECT";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", null),
            new SqlParameter("@REQ_ID", intREQ_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }
}