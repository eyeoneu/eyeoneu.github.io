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

public partial class Attendance_m_ATT_Parttime_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetPage();
        }
    }

    // 페이지 세팅
    private void SetPage()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();
            DataSet ds = attendance.EPM_ATT_BY_DAY_SELECT_MOBILE(DateTime.Now.ToString("yyyy-MM-dd"), "A", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());


            DataRow[] drTOT_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL");
            DataRow[] drATT_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Type = 'A'");

            DataRow[] drA1_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '001'");
            DataRow[] drA2_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '002'");
            DataRow[] drA3_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '003'");
            DataRow[] drA4_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '004'");
            DataRow[] drA5_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '005'");
            DataRow[] drA6_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '006'");

            DataRow[] drB1_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '101'");
            DataRow[] drB2_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '102'");
            DataRow[] drB3_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '103'");
            DataRow[] drB4_CNT = ds.Tables[0].Select("RES_CON_ID IS NOT NULL AND ATT_DAY_Code = '104'");


            this.lblTOT_CNT.Text = drTOT_CNT.Length.ToString();
            this.lblATT_CNT.Text = drATT_CNT.Length.ToString();

            this.lblA1_CNT.Text = drA1_CNT.Length.ToString();
            this.lblA2_CNT.Text = drA2_CNT.Length.ToString();
            this.lblA3_CNT.Text = drA3_CNT.Length.ToString();
            this.lblA4_CNT.Text = drA4_CNT.Length.ToString();
            this.lblA5_CNT.Text = drA5_CNT.Length.ToString();
            this.lblA6_CNT.Text = drA6_CNT.Length.ToString();

            this.lblB1_CNT.Text = drB1_CNT.Length.ToString();
            this.lblB2_CNT.Text = drB2_CNT.Length.ToString();
            this.lblB3_CNT.Text = drB3_CNT.Length.ToString();
            this.lblB4_CNT.Text = drB4_CNT.Length.ToString();
        }
    }
}