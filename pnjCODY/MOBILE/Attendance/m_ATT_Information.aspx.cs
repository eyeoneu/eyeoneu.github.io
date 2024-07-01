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

public partial class Attendance_m_ATT_Information : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetSupporterInfo();
            SetPage();
        }
    }

    // 로그인 된 서포터 정보 세팅
    private void SetSupporterInfo()
    {
        if (Session["sRES_ID"] != null)
        {
            this.lblRES_Name.Text = Session["sRES_Name"].ToString() + " (" + Session["sRES_Number"].ToString() + ")";
            this.lblRES_RBS_NAME.Text = Session["sRES_RBS_NAME"].ToString();
            this.lblRES_RBS_AREA_NAME.Text = Session["sRES_RBS_AREA_NAME"].ToString();
        }
    }

    // 코디/알바 출근 현황 정보
    private void SetPage()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();

            DataSet ds = attendance.EPM_ATT_TODAY_SELECT_MOBILE(Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());

            DataRow[] drCODY_ATT_CNT = ds.Tables[0].Select("RES_ASS_ID IS NOT NULL AND ATT_DAY_Type = 'A'");
            DataRow[] drCODY_ABS_CNT = ds.Tables[0].Select("RES_ASS_ID IS NOT NULL AND ATT_DAY_Type = 'B'");
            DataRow[] drCODY_HOL_CNT = ds.Tables[0].Select("RES_ASS_ID IS NOT NULL AND ATT_DAY_Type = 'C'");

            DataRow[] drAR_ATT_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Type = 'A'");
            DataRow[] drAR_ABS_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Type = 'B'");
            DataRow[] drAR_HOL_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Type = 'C'");

            this.lblCODY_ATT_CNT.Text = drCODY_ATT_CNT.Length.ToString();
            this.lblCODY_ABS_CNT.Text = drCODY_ABS_CNT.Length.ToString();
            this.lblCODY_HOL_CNT.Text = drCODY_HOL_CNT.Length.ToString();

            this.lblAR_ATT_CNT.Text = drAR_ATT_CNT.Length.ToString();
            this.lblAR_ABS_CNT.Text = drAR_ABS_CNT.Length.ToString();
            //this.lblAR_HOL_CNT.Text = drAR_HOL_CNT.Length.ToString();
        }
    }

    // 이동
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        if (this.hdMODE.Value == "C")
            Response.Redirect("/Attendance/m_ATT_Cody_Summary.aspx");
        else if (this.hdMODE.Value == "A")
            Response.Redirect("/Attendance/m_ATT_Parttime_List.aspx");  //Response.Redirect("/Attendance/m_ATT_Parttime_Summary.aspx");
    }
}