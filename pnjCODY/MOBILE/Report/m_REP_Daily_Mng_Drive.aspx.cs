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

public partial class m_REP_Daily_Mng_Drive : System.Web.UI.Page
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
            SetPage();
        }
    }

    // 코디/알바 출근 현황 정보
    private void SetPage()
    {
        if (Session["sRES_ID"] != null)
        {
            double start = 0;
            double finish = 0;
            double tollCash = 0;
            double tollCard = 0;
            int minus = 0;

            DataTable dt = Select_Att_Is_Saved();

            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["START_MILE"].ToString()))
                    start = double.Parse(dt.Rows[0]["START_MILE"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[0]["FINISH_MILE"].ToString()))
                    finish = double.Parse(dt.Rows[0]["FINISH_MILE"].ToString());

                minus = (int)(finish - start);

                this.lblAtt.Text = string.Format("{0:N0} / {1:N0} ({2})", start, finish, minus);

                if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_SUM"].ToString()))
                    lblOil.Text = string.Format("{0:N0}", double.Parse(dt.Rows[0]["DRV_SUM"].ToString()));

                if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_TOLLCASH"].ToString()))
                    tollCash = double.Parse(dt.Rows[0]["DRV_TOLLCASH"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_TOLLCORP"].ToString()))
                    tollCard = double.Parse(dt.Rows[0]["DRV_TOLLCORP"].ToString());

                lblToll.Text = string.Format("{0:N0}", tollCash + tollCard);

                //if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_VISIT"].ToString()))
                //    txtStoreCnt.Text = dt.Rows[0]["DRV_VISIT"].ToString();

                //if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_PATH"].ToString()))
                //    txtRoute.Text = dt.Rows[0]["DRV_PATH"].ToString();
            }

            DataTable dtOneDay = Select_List();

            if (dtOneDay.Rows.Count > 0)
            {
                txtStoreCnt.Text = dtOneDay.Rows.Count.ToString();

                string temp = string.Empty;

                for (int i = 0; i < dtOneDay.Rows.Count; i++)
                {
                    if (i != dtOneDay.Rows.Count - 1)
                        temp += dtOneDay.Rows[i]["STORE_NAME"].ToString() + " -> ";
                    else
                        temp += dtOneDay.Rows[i]["STORE_NAME"].ToString();
                }

                txtRoute.Text = temp;
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


    private DataTable Select_Att_Is_Saved()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ATT_IS_SAVED_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());

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

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    bool bError = false;
    //    SqlConnection Con = null;
    //    SqlTransaction trans = null;

    //    try
    //    {
    //        Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
    //        Con.Open();

    //        SqlCommand CmdRemark = new SqlCommand("EPM_DAILY_STORE_ROUTE_INSERT", Con);
    //        CmdRemark.CommandType = CommandType.StoredProcedure;
    //        trans = Con.BeginTransaction();
    //        CmdRemark.Transaction = trans;

    //        CmdRemark.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());
    //        CmdRemark.Parameters.AddWithValue("@STORE_CNT", txtStoreCnt.Text);
    //        CmdRemark.Parameters.AddWithValue("@STORE_ROUTE", txtRoute.Text);

    //        CmdRemark.ExecuteNonQuery();

    //        trans.Commit();
    //    }
    //    catch (Exception ex)
    //    {
    //        bError = true;
    //        trans.Rollback();
    //        Response.Write(ex.Message);
    //    }

    //    finally
    //    {
    //        if (Con != null)
    //        {
    //            Con.Close();
    //            Con = null;
    //        }
    //    }

    //    if (!bError)
    //    {
    //        Common.scriptAlert(this.Page, "저장되었습니다.", "m_REP_Daily_Report_Main.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    //    }
    //}
}