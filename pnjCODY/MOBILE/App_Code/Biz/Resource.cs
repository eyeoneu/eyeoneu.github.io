using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// ======================================
/// 작성일: 2011.11.22
/// 작성자: 정창화
/// 내용  : 사원 정보
/// ======================================
public class Resource
{
    SqlDbAgent _agent;
    string query;

    public Resource()
    {
        _agent = new SqlDbAgent("SQL_CODY");
    }

    /// <summary>
    /// 사원 목록
    /// </summary>
    public DataSet EPM_RES_LIST_MOBILE(string strNAME, string strWORKGROUP1, string strRES_RBS_CD, string strRES_RBS_AREA_CD)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@NAME", strNAME),
            new SqlParameter("@WORKGROUP1", strWORKGROUP1),
            new SqlParameter("@RES_RBS_CD", strRES_RBS_CD),
            new SqlParameter("@RES_RBS_AREA_CD", strRES_RBS_AREA_CD)
        };

        return _agent.GetDataSet("EPM_RES_LIST_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 목록 (생년월일 추가)
    /// </summary>
    public DataSet EPM_RES_LIST_MOBILE(string strNAME, string strWORKGROUP1, string strRES_RBS_CD, string strRES_RBS_AREA_CD, string strBIRTH)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@NAME", strNAME),
            new SqlParameter("@WORKGROUP1", strWORKGROUP1),
            new SqlParameter("@RES_RBS_CD", strRES_RBS_CD),
            new SqlParameter("@RES_RBS_AREA_CD", strRES_RBS_AREA_CD),
            new SqlParameter("@BIRTH", strBIRTH)
        };

        return _agent.GetDataSet("EPM_RES_LIST_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 매장별 사원 목록
    /// </summary>
    public DataSet EPM_RES_LIST_STORE_MOBILE(int intRES_ASS_StoreID)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RES_ASS_StoreID", intRES_ASS_StoreID),
            new SqlParameter("@DATE", DateTime.Today.ToShortDateString())
        };

        return _agent.GetDataSet("EPM_RES_LIST_STORE_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 사원 정보 : 목록
    /// </summary>
    public SqlDataReader EPM_RES_DETAIL_SELECT_MOBILE(int intRES_ID)
    {
        query = "EPM_RES_DETAIL_SELECT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@MODE", ""),
            new SqlParameter("@RES_ID", intRES_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 단일 사원의 여러행 정보 : BAS(기본정보), ADD(주소), EMP(고용정보), CAR(경력사항), EDU(학력정보), LIC(자격정보), FAM(가족관계), PHO(사진)
    /// </summary>
    public DataSet EPM_RES_DETAIL_SELECT_MOBILE(int intRES_ID, string strMODE, string strTable)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@RES_ID", intRES_ID)
        };

        return _agent.GetDataSet("EPM_RES_DETAIL_SELECT_MOBILE", strTable, null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 사원 정보 : BAS(기본정보), ADD(주소), EMP(고용정보), CAR(경력사항), EDU(학력정보), LIC(자격정보), FAM(가족관계), PHO(사진)
    /// </summary>
    public SqlDataReader EPM_RES_DETAIL_SELECT_MOBILE(int intRES_ID, string strMODE)
    {
        query = "EPM_RES_DETAIL_SELECT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@RES_ID", intRES_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 사원 - CAR(경력사항), EDU(학력정보), LIC(자격정보), FAM(가족) 단일 정보
    /// </summary>
    public SqlDataReader EPM_RES_DETAIL_VALUE_SELECT_MOBILE(int intRES_ID, int intLT_ID, string strMODE)
    {
        query = "EPM_RES_DETAIL_VALUE_SELECT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@MODE", strMODE),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@LT_ID", intLT_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 단일 사원 - 사원 등록 정보 분류별 저장 여부
    /// </summary>
    public SqlDataReader EPM_RES_DETAIL_IS_SAVED_MOBILE(int intRES_ID)
    {
        query = "EPM_RES_DETAIL_IS_SAVED_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 기본정보 삽입 처리  (삽입 시 RES_ID 값 반환)
    /// </summary>
    public string EPM_RES_DETAIL_INSERT_BASIC_MOBILE(string strRES_NAME, string strRES_PNUMBER, string strRES_TEL, string strRES_CP, string strRES_BIRTHDAY, string strRES_MARRY, string strRES_DISABLED, string strRES_Bank, string strRES_BankNumber, string strRES_RBS_CD, string strRES_RBS_AREA_CD, string strPass)
    {
        query = "EPM_RES_DETAIL_INSERT_BASIC_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_PNUMBER", strRES_PNUMBER),
            new SqlParameter("@RES_NAME", strRES_NAME),
            new SqlParameter("@RES_TEL", strRES_TEL),
            new SqlParameter("@RES_CP", strRES_CP),
            new SqlParameter("@RES_BIRTHDAY", strRES_BIRTHDAY),
            new SqlParameter("@RES_MARRY", strRES_MARRY),
            new SqlParameter("@RES_DISABLED", strRES_DISABLED),
            new SqlParameter("@RES_BANK", strRES_Bank),
            new SqlParameter("@RES_BANKNUMBER", strRES_BankNumber),
            new SqlParameter("@RES_RBS_CD", strRES_RBS_CD),
            new SqlParameter("@RES_RBS_AREA_CD", strRES_RBS_AREA_CD),
            new SqlParameter("@RES_PASS", strPass)
        };

        DbParamHelper.Add(ref paramArray, "@RES_ID", SqlDbType.Int);

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);

        return DbParamHelper.GetValue(paramArray, "@RES_ID").ToString();
    }


    /// <summary>
    /// 사원 정보 - 기본정보 저장 시 이전에 저장된 사원 정보의 입사일자 반환
    /// </summary>
    public SqlDataReader EPM_RES_CHECK_SAVED_INFO_MOBILE(int intRES_ID, string strRES_PNUMBER)
    {
        query = "EPM_RES_CHECK_SAVED_INFO_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_PNUMBER", strRES_PNUMBER)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 기본정보 저장 시 이전에 저장된 사원 정보의 입사일자 반환
    /// </summary>
    public SqlDataReader EPM_RES_CHECK_JOIN_RESTRICTION(string strRES_PNUMBER)
    {
        query = "EPM_RES_CHECK_JOIN_RESTRICTION";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_PNUMBER", strRES_PNUMBER)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 상태확인
    /// </summary>
    public SqlDataReader EPM_RES_CHECK_JOIN_RESTRICTION_V2(string strRES_PNUMBER)
    {
        query = "EPM_RES_CHECK_JOIN_RESTRICTION_V2";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_PNUMBER", strRES_PNUMBER)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 기본정보 저장 시 이전에 저장된 사원 정보로 업데이트
    /// </summary>
    public void EPM_RES_SAVED_INFO_UPDATE_MOBILE(int intRES_ID, int intPRE_RES_ID)
    {
        query = "EPM_RES_SAVED_INFO_UPDATE_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@PRE_RES_ID", intPRE_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 정보 - 기본정보 수정 처리
    /// </summary>
    public void EPM_RES_DETAIL_SUBMIT_BASIC_MOBILE(string strEXE_GB, int intRES_ID, string strRES_NAME, string strRES_PNUMBER, string strRES_TEL, string strRES_CP, string strRES_BIRTHDAY, string strRES_MARRY, string strRES_DISABLED, string strRES_Bank, string strRES_BankNumber)
    {
        query = "EPM_RES_DETAIL_SUBMIT_BASIC_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_PNUMBER", strRES_PNUMBER),
            new SqlParameter("@RES_NAME", strRES_NAME),
            new SqlParameter("@RES_TEL", strRES_TEL),
            new SqlParameter("@RES_CP", strRES_CP),
            new SqlParameter("@RES_BIRTHDAY", strRES_BIRTHDAY),
            new SqlParameter("@RES_MARRY", strRES_MARRY),
            new SqlParameter("@RES_DISABLED", strRES_DISABLED),
            new SqlParameter("@RES_Bank", strRES_Bank),
            new SqlParameter("@RES_BankNumber", strRES_BankNumber)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 삭제 처리
    /// </summary>
    public void EPM_RES_DETAIL_SUBMIT_BASIC_MOBILE(int intRES_ID)
    {
        query = "EPM_RES_DETAIL_SUBMIT_BASIC_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", "D"),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_PNUMBER", null),
            new SqlParameter("@RES_NAME", null),
            new SqlParameter("@RES_TEL", null),
            new SqlParameter("@RES_CP", null),
            new SqlParameter("@RES_BIRTHDAY", null),
            new SqlParameter("@RES_MARRY", null),
            new SqlParameter("@RES_DISABLED", null),
            new SqlParameter("@RES_Bank", null),
            new SqlParameter("@RES_BankNumber", null)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 정보 - 주소정보 수정 처리
    /// </summary>
    public void EPM_RES_DETAIL_UPDATE_ADDRESS_MOBILE(int intRES_ID, string strRES_POST, string strRES_ADD1, string strRES_ADD2)
    {
        query = "EPM_RES_DETAIL_UPDATE_ADDRESS_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_POST", strRES_POST),
            new SqlParameter("@RES_ADD1", strRES_ADD1),
            new SqlParameter("@RES_ADD2", strRES_ADD2),
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 고용정보 수정 처리
    /// </summary>
    public void EPM_RES_DETAIL_UPDATE_EPMLOYMENT_MOBILE(int intRES_ID, string strRES_WORKTYPE, string strRES_RBS_CD, string strRES_RBS_AREA_CD, string strRES_WORKGROUP1, string strRES_WORKGROUP2, string strRES_WORKGROUP3)
    {
        query = "EPM_RES_DETAIL_UPDATE_EPMLOYMENT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_WORKTYPE", strRES_WORKTYPE),
            new SqlParameter("@RES_RBS_CD", strRES_RBS_CD),
            new SqlParameter("@RES_RBS_AREA_CD", strRES_RBS_AREA_CD),
            new SqlParameter("@RES_WORKGROUP1", strRES_WORKGROUP1),
            new SqlParameter("@RES_WORKGROUP2", strRES_WORKGROUP2),
            new SqlParameter("@RES_WORKGROUP3", strRES_WORKGROUP3),
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 정보 - 사진정보 수정 처리
    /// </summary>
    public void EPM_RES_DETAIL_UPDATE_PICTURE_MOBILE(int intRES_ID, string RES_PICTURE)
    {
        query = "EPM_RES_DETAIL_UPDATE_PICTURE_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_PICTURE", RES_PICTURE)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 정보 - 경력정보 처리
    /// </summary>
    public void EPM_RES_CAREER_SUBMIT(string strEXE_GB, int intRES_ID, int intRES_CAR_ID, string strRES_CAR_COMPANY, string strRES_CAR_START, string strRES_CAR_FINISH, string strRES_CAR_MAINJOB, string strRES_CAR_RETIRE_CODE)
    {
        query = "EPM_RES_CAREER_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_CAR_ID", intRES_CAR_ID),
            new SqlParameter("@RES_CAR_COMPANY", strRES_CAR_COMPANY),
            new SqlParameter("@RES_CAR_START", strRES_CAR_START),
            new SqlParameter("@RES_CAR_FINISH", strRES_CAR_FINISH),
            new SqlParameter("@RES_CAR_MAINJOB", strRES_CAR_MAINJOB),
            new SqlParameter("@RES_CAR_RETIRE_CODE", strRES_CAR_RETIRE_CODE)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 가족관계 처리 (연락처 주가)
    /// </summary>
    public void EPM_RES_FAMILY_SUBMIT(string strEXE_GB, int intRES_ID, int intRES_FAM_ID, string strRES_FAM_Name, string strRES_FAM_Relation, string strRES_FAM_Pnumber, string strRES_FAM_Work, string strRES_FAM_Support, string strRES_FAM_Together, string strRES_FAM_Health, string strRES_FAM_CP)
    {
        query = "EPM_RES_FAMILY_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_FAM_ID", intRES_FAM_ID),
            new SqlParameter("@RES_FAM_Name", strRES_FAM_Name),
            new SqlParameter("@RES_FAM_Relation", strRES_FAM_Relation),
            new SqlParameter("@RES_FAM_Pnumber", strRES_FAM_Pnumber),
            new SqlParameter("@RES_FAM_Work", strRES_FAM_Work),
            new SqlParameter("@RES_FAM_Support", strRES_FAM_Support),
            new SqlParameter("@RES_FAM_Together", strRES_FAM_Together),
            new SqlParameter("@RES_FAM_Health", strRES_FAM_Health),
            new SqlParameter("@RES_FAM_CP", strRES_FAM_CP)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 자격정보 처리
    /// </summary>
    public void EPM_RES_LICENSE_SUBMIT(string strEXE_GB, int intRES_ID, int intRES_LIC_ID, string strRES_LIC_Name, string strRES_LIC_Type, string strRES_LIC_Number, string strRES_LIC_Date, string strRES_LIC_Memo)
    {
        query = "EPM_RES_LICENSE_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_LIC_ID", intRES_LIC_ID),
            new SqlParameter("@RES_LIC_Name", strRES_LIC_Name),
            new SqlParameter("@RES_LIC_Type", strRES_LIC_Type),
            new SqlParameter("@RES_LIC_Number", strRES_LIC_Number),
            new SqlParameter("@RES_LIC_Date", strRES_LIC_Date),
            new SqlParameter("@RES_LIC_Memo", strRES_LIC_Memo)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 정보 - 학력정보 처리
    /// </summary>
    public void EPM_RES_EDUCATION_SUBMIT(string strEXE_GB, int intRES_ID, int intRES_EDU_ID, string strRES_EDU_School, string strRES_EDU_Graduation, string strRES_EDU_GraduationDate, string strRES_EDU_Major, string strRES_EDU_Area)
    {
        query = "EPM_RES_EDUCATION_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_EDU_ID", intRES_EDU_ID),
            new SqlParameter("@RES_EDU_School", strRES_EDU_School),
            new SqlParameter("@RES_EDU_Graduation", strRES_EDU_Graduation),
            new SqlParameter("@RES_EDU_GraduationDate", strRES_EDU_GraduationDate),
            new SqlParameter("@RES_EDU_Major", strRES_EDU_Major),            
            new SqlParameter("@RES_EDU_Area", strRES_EDU_Area)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 정보 - 입사/퇴사 요청 처리
    /// </summary>
    public void EPM_RES_DETAIL_UPDATE_WORKSTATE_MOBILE(int intRES_ID, string strRES_WORKSTATE, string strRES_DATE, string strRES_CAR_RETIRE_CODE, int intEXECUTE_RES_ID)
    {
        query = "EPM_RES_DETAIL_UPDATE_WORKSTATE_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_WORKSTATE", strRES_WORKSTATE),
            new SqlParameter("@RES_DATE", strRES_DATE),
            new SqlParameter("@RES_CAR_RETIRE_CODE", strRES_CAR_RETIRE_CODE),
            new SqlParameter("@EXECUTE_RES_ID", intEXECUTE_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 코디배정 (삽입)
    /// </summary>
    public void EPM_RES_ASSIGNMENT_SUBMIT(string strEXE_GB, int intRES_ID, string strRES_WORKTYPE, string strRES_WORKGROUP1, string strRES_WORKGROUP2, string strRES_WORKGROUP3, string strRES_ASS_VENDER_CD, string strRES_ASS_VEN_AREA_CD, string strRES_ASS_VEN_OFFICE_CD, int intRES_ASS_SUPPORTER, string strRES_ASS_RBS_CD, string strRES_ASS_AREA_CD, int intRES_ASS_STOREID, string strRES_ASS_SALES, string strRES_ASS_STARTDATE, string strRES_ASS_FINISHDATE, string strRES_ASS_STATE, string strRES_TO_ID, int intEXECUTE_RES_ID)
    {
        query = "EPM_RES_ASSIGNMENT_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ASS_ID", null),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_WORKTYPE", strRES_WORKTYPE),
            new SqlParameter("@RES_WORKGROUP1", strRES_WORKGROUP1),
            new SqlParameter("@RES_WORKGROUP2", strRES_WORKGROUP2),
            new SqlParameter("@RES_WORKGROUP3", strRES_WORKGROUP3),
            new SqlParameter("@RES_ASS_VENDER_CD", strRES_ASS_VENDER_CD),
            new SqlParameter("@RES_ASS_VEN_AREA_CD", strRES_ASS_VEN_AREA_CD),
            new SqlParameter("@RES_ASS_VEN_OFFICE_CD", strRES_ASS_VEN_OFFICE_CD),
            new SqlParameter("@RES_ASS_SUPPORTER", intRES_ASS_SUPPORTER),
            new SqlParameter("@RES_ASS_RBS_CD", strRES_ASS_RBS_CD),
            new SqlParameter("@RES_ASS_AREA_CD", strRES_ASS_AREA_CD),
            new SqlParameter("@RES_ASS_STOREID", intRES_ASS_STOREID),
            new SqlParameter("@RES_ASS_SALES", strRES_ASS_SALES),
            new SqlParameter("@RES_ASS_STARTDATE", strRES_ASS_STARTDATE),
            new SqlParameter("@RES_ASS_FINISHDATE", strRES_ASS_FINISHDATE),
            new SqlParameter("@RES_ASS_STATE", strRES_ASS_STATE),
            new SqlParameter("@RES_TO_NUM", strRES_TO_ID),
            new SqlParameter("@EXECUTE_RES_ID", intEXECUTE_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 관리 - 코디배정 (수정)
    /// </summary>
    public void EPM_RES_ASSIGNMENT_SUBMIT(string strEXE_GB, int intRES_ASS_ID, string strRES_ASS_VENDER_CD, string strRES_ASS_VEN_AREA_CD, string strRES_ASS_VEN_OFFICE_CD, int intRES_ASS_STOREID, string strRES_ASS_SALES, string strRES_ASS_STARTDATE, string strRES_ASS_FINISHDATE, string strRES_TO_ID, int intEXECUTE_RES_ID)
    {
        query = "EPM_RES_ASSIGNMENT_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ASS_ID", intRES_ASS_ID),
            new SqlParameter("@RES_ID", null),
            new SqlParameter("@RES_WORKTYPE", null),
            new SqlParameter("@RES_WORKGROUP1", null),
            new SqlParameter("@RES_WORKGROUP2", null),
            new SqlParameter("@RES_WORKGROUP3", null),
            new SqlParameter("@RES_ASS_VENDER_CD", strRES_ASS_VENDER_CD),
            new SqlParameter("@RES_ASS_VEN_AREA_CD", strRES_ASS_VEN_AREA_CD),
            new SqlParameter("@RES_ASS_VEN_OFFICE_CD", strRES_ASS_VEN_OFFICE_CD),
            new SqlParameter("@RES_ASS_SUPPORTER", null),
            new SqlParameter("@RES_ASS_RBS_CD", null),
            new SqlParameter("@RES_ASS_AREA_CD", null),
            new SqlParameter("@RES_ASS_STOREID", intRES_ASS_STOREID),
            new SqlParameter("@RES_ASS_SALES", strRES_ASS_SALES),
            new SqlParameter("@RES_ASS_STARTDATE", strRES_ASS_STARTDATE),
            new SqlParameter("@RES_ASS_FINISHDATE", strRES_ASS_FINISHDATE),
            new SqlParameter("@RES_ASS_STATE", "001"),
            new SqlParameter("@RES_TO_NUM", strRES_TO_ID),
            new SqlParameter("@EXECUTE_RES_ID", intEXECUTE_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 코디배정 (삭제)
    /// </summary>
    public void EPM_RES_ASSIGNMENT_SUBMIT(string strEXE_GB, int intRES_ASS_ID)
    {
        query = "EPM_RES_ASSIGNMENT_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_ASS_ID", intRES_ASS_ID),
            new SqlParameter("@RES_ID", null),
            new SqlParameter("@RES_WORKTYPE", null),
            new SqlParameter("@RES_WORKGROUP1", null),
            new SqlParameter("@RES_WORKGROUP2", null),
            new SqlParameter("@RES_WORKGROUP3", null),
            new SqlParameter("@RES_ASS_VENDER_CD", null),
            new SqlParameter("@RES_ASS_VEN_AREA_CD", null),
            new SqlParameter("@RES_ASS_VEN_OFFICE_CD", null),
            new SqlParameter("@RES_ASS_SUPPORTER", null),
            new SqlParameter("@RES_ASS_RBS_CD", null),
            new SqlParameter("@RES_ASS_AREA_CD", null),
            new SqlParameter("@RES_ASS_STOREID", null), // 수정 필요
            new SqlParameter("@RES_ASS_SALES", null),
            new SqlParameter("@RES_ASS_STARTDATE", null),
            new SqlParameter("@RES_ASS_FINISHDATE", null),
            new SqlParameter("@RES_ASS_STATE", null)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 단일 사용자의 코디배정 목록
    /// </summary>
    public DataSet EPM_RES_ASSIGNMENT_SELECT_MOBILE(int intRES_ID)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_ASS_ID", null)
        };

        return _agent.GetDataSet("EPM_RES_ASSIGNMENT_SELECT_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 사용자의 단일 코디배정 정보
    /// </summary>
    public SqlDataReader EPM_RES_ASSIGNMENT_SELECT_MOBILE(int intRES_ID, int intRES_ASS_ID)
    {
        query = "EPM_RES_ASSIGNMENT_SELECT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_ASS_ID", intRES_ASS_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 단일 사용자의 아르바이트 계약 목록
    /// </summary>
    public DataSet EPM_RES_CONTRACT_SELECT_MOBILE(int intRES_ID)
    {
        SqlParameter[] parameterArr = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_CON_ID", null)
        };

        return _agent.GetDataSet("EPM_RES_CONTRACT_SELECT_MOBILE", "Table", null, parameterArr, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 사용자의 단일 아르바이트 계약 정보
    /// </summary>
    public SqlDataReader EPM_RES_CONTRACT_SELECT_MOBILE(int intRES_ID, int intRES_CON_ID)
    {
        query = "EPM_RES_CONTRACT_SELECT_MOBILE";

        SqlParameter[] paramArray = {
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_CON_ID", intRES_CON_ID)
        };

        return _agent.ExecuteReader(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 아르바이트 계약 (삽입)
    /// </summary>
    public void EPM_RES_CONTRACT_SUBMIT(string strEXE_GB, int intRES_ID, string strRES_WORKTYPE, string strRES_WORKGROUP1, string strRES_WORKGROUP2, string strRES_WORKGROUP3, string strRES_CON_VENDER_CD, string strRES_CON_VEN_AREA_CD, string strRES_CON_VEN_OFFICE_CD, int intRES_CON_SUPPORTER, string strRES_CON_RBS_CD, string strRES_CON_AREA_CD, int intRES_CON_STOREID, string strRES_CON_SALES, string strRES_CON_STARTDATE, string strRES_CON_FINISHDATE, string strRES_CON_STATE, string strRES_CON_TYPE, string strRES_CON_TIME, string strRES_CON_PAY, string strRES_CON_IS_TO_LINK, string strRES_TO_NUM, string strRES_CON_GB, int intEXECUTE_RES_ID)
    {
        query = "EPM_RES_CONTRACT_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_CON_ID", null),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_WORKTYPE", strRES_WORKTYPE),
            new SqlParameter("@RES_WORKGROUP1", strRES_WORKGROUP1),
            new SqlParameter("@RES_WORKGROUP2", strRES_WORKGROUP2),
            new SqlParameter("@RES_WORKGROUP3", strRES_WORKGROUP3),
            new SqlParameter("@RES_CON_VENDER_CD", strRES_CON_VENDER_CD),
            new SqlParameter("@RES_CON_VEN_AREA_CD", strRES_CON_VEN_AREA_CD),
            new SqlParameter("@RES_CON_VEN_OFFICE_CD", strRES_CON_VEN_OFFICE_CD),
            new SqlParameter("@RES_CON_SUPPORTER", intRES_CON_SUPPORTER),
            new SqlParameter("@RES_CON_RBS_CD", strRES_CON_RBS_CD),
            new SqlParameter("@RES_CON_AREA_CD", strRES_CON_AREA_CD),
            new SqlParameter("@RES_CON_STOREID", intRES_CON_STOREID),
            new SqlParameter("@RES_CON_SALES", strRES_CON_SALES),
            new SqlParameter("@RES_CON_STARTDATE", strRES_CON_STARTDATE),
            new SqlParameter("@RES_CON_FINISHDATE", strRES_CON_FINISHDATE),
            new SqlParameter("@RES_CON_STATE", strRES_CON_STATE),
            new SqlParameter("@RES_CON_TYPE", strRES_CON_TYPE),
            new SqlParameter("@RES_CON_TIME", strRES_CON_TIME),
            new SqlParameter("@RES_CON_PAY", strRES_CON_PAY),
            new SqlParameter("@RES_CON_IS_TO_LINK", strRES_CON_IS_TO_LINK),
            new SqlParameter("@RES_TO_NUM", string.IsNullOrEmpty(strRES_TO_NUM) ? DBNull.Value : (object)strRES_TO_NUM),
            new SqlParameter("@RES_CON_GB", string.IsNullOrEmpty(strRES_CON_GB) ? DBNull.Value : (object)strRES_CON_GB),
            new SqlParameter("@EXECUTE_RES_ID", intEXECUTE_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }


    /// <summary>
    /// 사원 관리 - 아르바이트 계약 (수정)
    /// </summary>
    public void EPM_RES_CONTRACT_SUBMIT(string strEXE_GB, int intRES_CON_ID, int intRES_ID, string strRES_CON_VENDER_CD, string strRES_CON_VEN_AREA_CD, string strRES_CON_VEN_OFFICE_CD, int intRES_CON_STOREID, string strRES_CON_SALES, string strRES_CON_STARTDATE, string strRES_CON_FINISHDATE, string strRES_CON_TYPE, string strRES_CON_TIME, string strRES_CON_PAY, string strRES_CON_IS_TO_LINK, string strRES_TO_NUM, string strRES_CON_GB, int intEXECUTE_RES_ID)
    {
        query = "EPM_RES_CONTRACT_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_CON_ID", intRES_CON_ID),
            new SqlParameter("@RES_ID", intRES_ID),
            new SqlParameter("@RES_WORKTYPE", null),
            new SqlParameter("@RES_WORKGROUP1", null),
            new SqlParameter("@RES_WORKGROUP2", null),
            new SqlParameter("@RES_WORKGROUP3", null),
            new SqlParameter("@RES_CON_VENDER_CD", strRES_CON_VENDER_CD),
            new SqlParameter("@RES_CON_VEN_AREA_CD", strRES_CON_VEN_AREA_CD),
            new SqlParameter("@RES_CON_VEN_OFFICE_CD", strRES_CON_VEN_OFFICE_CD),
            new SqlParameter("@RES_CON_SUPPORTER", null),
            new SqlParameter("@RES_CON_RBS_CD", null),
            new SqlParameter("@RES_CON_AREA_CD", null),
            new SqlParameter("@RES_CON_STOREID", intRES_CON_STOREID),
            new SqlParameter("@RES_CON_SALES", strRES_CON_SALES),
            new SqlParameter("@RES_CON_STARTDATE", strRES_CON_STARTDATE),
            new SqlParameter("@RES_CON_FINISHDATE", strRES_CON_FINISHDATE),
            new SqlParameter("@RES_CON_STATE", "001"),
            new SqlParameter("@RES_CON_TYPE", strRES_CON_TYPE),
            new SqlParameter("@RES_CON_TIME", strRES_CON_TIME),
            new SqlParameter("@RES_CON_PAY", strRES_CON_PAY),
            new SqlParameter("@RES_CON_IS_TO_LINK", strRES_CON_IS_TO_LINK),
            new SqlParameter("@RES_TO_NUM", string.IsNullOrEmpty(strRES_TO_NUM) ? DBNull.Value : (object)strRES_TO_NUM),
            new SqlParameter("@RES_CON_GB", string.IsNullOrEmpty(strRES_CON_GB) ? DBNull.Value : (object)strRES_CON_GB),
            new SqlParameter("@EXECUTE_RES_ID", intEXECUTE_RES_ID)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }

    /// <summary>
    /// 사원 관리 - 아르바이트 계약 (삭제)
    /// </summary>
    public void EPM_RES_CONTRACT_SUBMIT(string strEXE_GB, int intRES_CON_ID)
    {
        query = "EPM_RES_CONTRACT_SUBMIT";

        SqlParameter[] paramArray = {
            new SqlParameter("@EXE_GB", strEXE_GB),
            new SqlParameter("@RES_CON_ID", intRES_CON_ID),
            new SqlParameter("@RES_ID", null),
            new SqlParameter("@RES_WORKTYPE", null),
            new SqlParameter("@RES_WORKGROUP1", null),
            new SqlParameter("@RES_WORKGROUP2", null),
            new SqlParameter("@RES_WORKGROUP3", null),
            new SqlParameter("@RES_CON_VENDER_CD", null),
            new SqlParameter("@RES_CON_VEN_AREA_CD", null),
            new SqlParameter("@RES_CON_VEN_OFFICE_CD", null),
            new SqlParameter("@RES_CON_SUPPORTER", null),
            new SqlParameter("@RES_CON_RBS_CD", null),
            new SqlParameter("@RES_CON_AREA_CD", null),
            new SqlParameter("@RES_CON_STOREID", null), // 수정 필요
            new SqlParameter("@RES_CON_SALES", null),
            new SqlParameter("@RES_CON_STARTDATE", null),
            new SqlParameter("@RES_CON_FINISHDATE", null),
            new SqlParameter("@RES_CON_STATE", null),
            new SqlParameter("@RES_CON_TIME", null),
            new SqlParameter("@RES_CON_PAY", null)
        };

        _agent.ExecuteNonQuery(query, paramArray, CommandType.StoredProcedure);
    }
}