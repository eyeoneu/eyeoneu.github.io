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

public partial class m_REP_Daily_Report_Main : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //권한체크
        if (Session["sRES_RBS_CD"].ToString() == "1111")//관리팀
        {
            RES_GB = "A";
        }
        else if (Session["sRES_WorkGroup2"].ToString() == "220")//팀장 
        {
            RES_GB = "T";
        }
        else if (Session["sRES_WorkGroup1"].ToString() == "008" || Session["sRES_WorkGroup1"].ToString() == "005")//서포터, 매니저
        {
            RES_GB = "S";
            //this.btnWrite.Enabled = false;
        }

        if (!IsPostBack)
        {
            SetSupporterInfo();
        }
    }

    // 로그인 된 서포터 정보 세팅
    private void SetSupporterInfo()
    {
        if (Session["sRES_ID"] != null)
        {
            this.lblDate.Text = DateTime.Today.ToString("yyyy년 MM월 dd일");
            this.lblRES_Name.Text = Session["sRES_Name"].ToString() + " (" + Session["sRES_Number"].ToString() + ")";

            DataTable dt = Select_Daily_Report_Is_Saved();
            DataTable dtOneDay = Select_List();

            int dailyDriveCnt = 0;

            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["IS_ATT_SAVED"].ToString()))
                {
                    if (dt.Rows[0]["IS_ATT_SAVED"].ToString().Trim().Equals("1"))
                    {
                        this.btnAttendance.Attributes["class"] = "button orange mepm_btn";
                        this.lblAttendance.Text = "출근(1)";
                    }
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["IS_LEAVE_SAVED"].ToString()))
                {
                    if (dt.Rows[0]["IS_LEAVE_SAVED"].ToString().Trim().Equals("1"))
                    {
                        this.btnLeave.Attributes["class"] = "button orange mepm_btn";
                        this.lblLeave.Text = "퇴근(1)";
                    }
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["CNT_TRN_SAVED"].ToString()))
                {
                    if (int.Parse(dt.Rows[0]["CNT_TRN_SAVED"].ToString()) > 0)
                    {
                        lblTrn.Text = "교통비정산서(" + dt.Rows[0]["CNT_TRN_SAVED"].ToString() + ")";
                        this.btnDrvieCost.Attributes["class"] = "button orange mepm_btn";
                    }
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["CNT_MKT_SAVED"].ToString()))
                {
                    if (int.Parse(dt.Rows[0]["CNT_MKT_SAVED"].ToString()) > 0)
                    {
                        lblMarket.Text = "시장조사(" + dt.Rows[0]["CNT_MKT_SAVED"].ToString() + ")";
                        this.btnMarketResearch.Attributes["class"] = "button orange mepm_btn";
                    }
                }

                // 결원매장
                if (!string.IsNullOrEmpty(dt.Rows[0]["CNT_VAC_SAVED"].ToString()))
                {
                    if (int.Parse(dt.Rows[0]["CNT_VAC_SAVED"].ToString()) > 0)
                    {
                        lblVacancy.Text = "결원매장(" + dt.Rows[0]["CNT_VAC_SAVED"].ToString() + ")";
                        this.btnVacancy.Attributes["class"] = "button orange mepm_btn";
                    }
                }
                

                if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_PRICE_SAVED"].ToString()))
                    dailyDriveCnt++;

                if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_TOLLCASH_SAVED"].ToString()) || !string.IsNullOrEmpty(dt.Rows[0]["DRV_TOLLCARD_SAVED"].ToString()))
                    dailyDriveCnt++;
            }
            else
            {
                this.btnAttendance.Attributes["class"] = "button skyblue mepm_btn";
                this.btnLeave.Attributes["class"] = "button skyblue mepm_btn";
                this.lblAttendance.Text = "출근";
                this.lblLeave.Text = "퇴근";
            }

            if (dtOneDay.Rows.Count > 0)
            {
                this.lblDailyReport.Text = "일일업무보고(" + dtOneDay.Rows.Count.ToString() + ")";
                this.btnDailyReport.Attributes["class"] = "button orange mepm_btn_big";
                //dailyDriveCnt++;
            }
            else
                this.btnDailyReport.Attributes["class"] = "button skyblue mepm_btn_big";

            if (dailyDriveCnt > 0)
            {
                this.btnDailyDrive.Attributes["class"] = "button orange mepm_btn";
                lblDrv.Text = "자차운영일지(" + dailyDriveCnt.ToString() + ")";
            }
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


    private DataTable Select_Daily_Report_Is_Saved()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_DAILY_REPORT_IS_SAVED_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TO_RBS", Session["sRES_RBS_CD"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TO_AssArea", Session["sRES_RBS_AREA_CD"].ToString());

        DataSet ds = new DataSet();

        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();

        return ds.Tables[0];
    }

    #region 목록 조회
    private DataTable Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_REP_DLY_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_GB", RES_GB);
        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@FROM", DateTime.Today.ToShortDateString());
        ad.SelectCommand.Parameters.AddWithValue("@TO", DateTime.Today.ToShortDateString());


        DataSet ds = new DataSet();

        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds.Tables[0];
    }
    #endregion

}