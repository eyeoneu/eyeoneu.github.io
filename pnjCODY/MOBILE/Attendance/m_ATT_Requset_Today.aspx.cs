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

public partial class Attendance_m_ATT_Requset_Today : System.Web.UI.Page
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

            SqlDataReader rd = attendance.EPM_ATT_DAY_REQ_TODAY("ALL", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString(), DateTime.Today.ToString("yyyyMM"));

            if (rd.Read())
            {
                this.lblALL_REQ_CNT.Text = rd["ALL_REQ"].ToString();
                this.lblALL_CNF_CNT.Text = rd["ALL_CNF"].ToString();
                this.lblALL_RTN_CNT.Text = rd["ALL_RTN"].ToString();

                this.lblCODY_REQ_CNT.Text = rd["ASS_REQ"].ToString();
                this.lblCODY_CNF_CNT.Text = rd["ASS_CNF"].ToString();
                this.lblCODY_RTN_CNT.Text = rd["ASS_RTN"].ToString();

                this.lblAR_REQ_CNT.Text = rd["CON_REQ"].ToString();
                this.lblAR_CNF_CNT.Text = rd["CON_CNF"].ToString();
                this.lblAR_RTN_CNT.Text = rd["CON_RTN"].ToString();
            }
        }
    }

    // 이동
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        if (this.hdMODE.Value == "C")
            Response.Redirect("/Attendance/m_ATT_Cody_Request_List.aspx");
        else if (this.hdMODE.Value == "A")
            Response.Redirect("/Attendance/m_ATT_Parttime_Request_List.aspx");  //Response.Redirect("/Attendance/m_ATT_Parttime_Summary.aspx");
    }
}