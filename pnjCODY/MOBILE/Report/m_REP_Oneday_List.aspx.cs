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

public partial class MOBILE_Report_m_REP_Oneday_List : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

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
            SetControl();

            SetList();
        }
    }

    // 컨트롤 세팅
    private void SetControl()
    {
        //지난월요일 ~ 오늘
        DateTime dtime = DateTime.Now;
        string lastMonday = "";        

        ////지난주 월요일 구하기
        //for (int i = 7; i <= 14; i++)
        //{
        //    if (DayOfWeek.Monday == dtime.AddDays(-i).DayOfWeek)
        //    {
        //        lastMonday = dtime.AddDays(-i).ToString("yyyy-MM-dd"); break;
        //    }
        //}
        //this.txtFROMDATE.Text = lastMonday;
        this.txtFROMDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.txtTODATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_List();

            this.gvReportList.DataSource = ds;
            this.gvReportList.DataBind();
        }
    }

    #region 목록 조회
    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_REP_DLY_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_GB", RES_GB);
        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@FROM", this.txtFROMDATE.Text.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TO", this.txtTODATE.Text.ToString());


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
        return ds;
    }
    #endregion

    // 검색 버튼 클릭 시
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetList();
    }


    // 그리드뷰 DataBound 시
    protected void gvReportList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["REP_DLY_ID"].ToString() + "', '"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";

        }
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("m_REP_Oneday_Report.aspx?REP_DLY_ID=" + this.hdREP_DLY_ID.Value.ToString() + "&RES_ID=" + this.hdRES_ID.Value.ToString());
    }
}