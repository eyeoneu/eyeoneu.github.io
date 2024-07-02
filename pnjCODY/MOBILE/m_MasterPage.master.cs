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

public partial class m_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //세션이 없을 경우
        if (Request.Path.ToString() != "/m_Login_form.aspx" && Session["sRES_ID"] == null)
        {
            Common.AlertUserFunction(HttpContext.Current.Response, "top.location.href='/m_Login_form.aspx';");
        }
        else
        {
			// 일반 사용자가 접근이 허용된 페이지 외에 다른 페이지의 경로로 강제 접근하였을 경우, 화면을 출력하지 않고 로그인 페이지로 이동되도록 처리
			if (Request.Path.ToString() != "/m_Login_form.aspx")
			{
				if (Session["sRES_RBS_CD"].ToString() == "1111")//관리팀
				{

				}
				else if (Session["sRES_WorkGroup2"].ToString() == "220")//팀장 
				{

				}
				else if (Session["sRES_WorkGroup1"].ToString() == "008" || Session["sRES_WorkGroup1"].ToString() == "005") //서포터, 매니저
				{

				}
				else
				{
					if (Request.Path.ToString() != "/Notice/m_NOT_WORKER_Write.aspx" && Request.Path.ToString() != "/Notice/m_NOT_WORKER_List.aspx" && Request.Path.ToString() != "/m_Default.aspx" && Request.Path.ToString() != "/Pay/m_Pay_Cheak.aspx" && Request.Path.ToString() != "/Document/m_EXP_Employment_List.aspx" && Request.Path.ToString() != "/Document/m_EXP_Dependents_List.aspx" && Request.Path.ToString() != "/Attendance/m_ATT_Closed_Leave_Request.aspx" && Request.Path.ToString() != "/Attendance/m_ATT_Closed_Leave_Mng.aspx")
					{
						Common.AlertUserFunction(HttpContext.Current.Response, "top.location.href='/m_Login_form.aspx';");
					}
				}
			}	
        }
        
        
        //Response.Write(Request.Path.ToString());
    }

    /// <summary>
    /// 로그아웃 클릭 시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();

        Response.Redirect("/m_Login_form.aspx");
    }
}
