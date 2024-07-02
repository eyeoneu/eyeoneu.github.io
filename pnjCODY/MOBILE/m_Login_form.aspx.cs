using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


public partial class m_Login_form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 로그인 버튼 클릭시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnLogin_Click(object sender, EventArgs e)
    {
        Security sectr = new Security(this.Page);
        SimpleSecureTransfer sst = new SimpleSecureTransfer();
        string _txtMemId = (hdMemID_Encrypt.Value.Length > 0) ? sectr.SafeSqlLiteralRtnStr(sst.Decrypt(hdMemID_Encrypt.Value)) : "";
        //string _TxtPassword = (hdPass_Encrypt.Value.Length > 0) ? sectr.SafeSqlLiteralRtnStr(sst.Encrypt(hdPass_Encrypt.Value)) : "";
        string _TxtPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Value, "MD5");
        //Response.Write(txtPassword.Text + "|"+ _TxtPassword);
        //로그인 처리
        if (Authenticate(_txtMemId, _TxtPassword))
        {
            //로그인 성공시 요청페이지로 이동
            FormsAuthentication.RedirectFromLoginPage(_txtMemId, false);
        }
    }

    /// <summary>
    /// 로그인 처리
    /// </summary>
    /// <param name="strEmpId">아이디</param>
    /// <param name="strPassword">비밀번호</param>
    /// <returns>로그인 성공 여부 true/false</returns>
    private bool Authenticate(string strRES_Number, string strRES_Pwd)
    {
        bool bExist = false;

        //// 개발용
        //bExist = true;
        //Session["sRES_ID"] = "3";                   //U/D 를 위한 사원ID
        //Session["sRES_Number"] = "S110300001";		//사번
        //Session["sRES_Name"] = "서포터";		        //사용자 이름
        //Session["sRES_RBS_CD"] = "2010";             //부문 코드
        //Session["sRES_RBS_AREA_CD"] = "5058";        //부서 코드
        //Session["sRES_RBS_NAME"] = "지원1부문";         //부문 이름
        //Session["sRES_RBS_AREA_NAME"] = "강북유통1";    //부서 이름
        //Session["sRES_WorkGroup1"] = "008";		       //직종: 서포터직
        //Session["sRES_WorkGroup2"] = "9998";           //직책: 대리
        //Session["sRES_WorkState"] = "002";                    //퇴사여부

        //Response.Redirect("/m_Default.aspx");

        Login login = new Login();

        string retVal = login.EPM_RES_LOGIN_IS_LOGIN(strRES_Number, strRES_Pwd);

        //로그인 성공
        if (retVal == "1")
        {
            bExist = true;

            //사용자 정보 조회
            SqlDataReader rd = login.EPM_RES_LOGIN_INFO_SELECT(strRES_Number);

            if (rd.Read())
            {
                Session["sRES_ID"] = rd["RES_ID"].ToString();                       //U/D 를 위한 사원ID
                Session["sRES_Number"] = rd["RES_Number"].ToString();		        //사번
                Session["sRES_Name"] = rd["RES_Name"].ToString();		            //사용자 이름
                Session["sRES_RBS_CD"] = rd["RES_RBS_CD"].ToString();               //부문 코드
                Session["sRES_RBS_AREA_CD"] = rd["RES_RBS_AREA_CD"].ToString();     //부서 코드
                Session["sRES_RBS_NAME"] = rd["RES_RBS_NAME"].ToString();           //부문 이름
                Session["sRES_RBS_AREA_NAME"] = rd["RES_RBS_AREA_NAME"].ToString(); //부서 이름
                Session["sRES_WorkGroup1"] = rd["RES_WorkGroup1"].ToString();		//직종 코드
                Session["sRES_WorkGroup2"] = rd["RES_WorkGroup2"].ToString();       //직책 코드
                Session["sRES_WorkState"] = rd["RES_WorkState_Code"].ToString();       //퇴사여부: 공지사항(근로자) 기능을 위해 2019-09-02 추가
            }

            rd.Close();

            Response.Redirect("/m_Default.aspx");
        }
        //비밀번호 불일치 : 보안상의 이유로 로그인 못하는 이유를 정확히 알려주지 않음.
        else if (retVal == "2")
        {
            Common.scriptAlert(this.Page, "일치하는 회원정보가 없습니다. 아이디와 비밀번호를 확인 후 다시 로그인해 주십시요.", "/m_Default.aspx");
        }
        //아이디 불일치 : 보안상의 이유로 로그인 못하는 이유를 정확히 알려주지 않음.
        else if (retVal == "3")
        {
            Common.scriptAlert(this.Page, "일치하는 회원정보가 없습니다. 아이디와 비밀번호를 확인 후 다시 로그인해 주십시요.", "/m_Default.aspx");
        }

        //Response.Write(retVal);
        //Response.End();

        return bExist;
    }
}
