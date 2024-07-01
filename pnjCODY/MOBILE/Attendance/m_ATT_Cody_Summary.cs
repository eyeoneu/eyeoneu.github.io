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

public partial class Attendance_m_ATT_Parttime_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
            SetList();
        }
    }

    // 기본 정보 세팅
    private void SetControl()
    {
        if (this.Page.Request["DATE"] != null)
        {
            this.lblDate.Text = DateTime.Parse(this.Page.Request["DATE"].ToString()).ToString("yyyy년 MM월 dd일");
            this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.Page.Request["DATE"].ToString()));
            this.hdDate.Value = DateTime.Parse(this.Page.Request["DATE"].ToString()).ToString("yyyy-MM-dd");
        }
        else
        {
            this.lblDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
            this.lblWeek.Text = GetStrDayOfWeek(DateTime.Now);
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    // 한글 요일 변환
    public static string GetStrDayOfWeek(DateTime a_odtDateTime)
    {
        switch (a_odtDateTime.DayOfWeek)
        {
            case DayOfWeek.Monday:
                return "(월)";
            case DayOfWeek.Tuesday:
                return "(화)";
            case DayOfWeek.Wednesday:
                return "(수)";
            case DayOfWeek.Thursday:
                return "(목)";
            case DayOfWeek.Friday:
                return "(금)";
            case DayOfWeek.Saturday:
                return "(토)";
            case DayOfWeek.Sunday:
                return "(일)";
        }
        return "";
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();
            DataSet ds = attendance.EPM_ATT_BY_DAY_SELECT_MOBILE(this.hdDate.Value.ToString(), "A", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());

            this.gvARAttList.DataSource = ds;
            this.gvARAttList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvARAttList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            // 매월 2일 까지 수정
            //if (this.lblDate.Text == DateTime.Now.ToString("yyyy년 MM월 dd일"))

            if (DateTime.Now.ToString("yyyy년 MM월") == lblDate.Text.Substring(0, 9) || DateTime.Now.Day < 3)
            {
                e.Row.Attributes["onClick"] = "fncDetail('"
                                            + ((DataRowView)e.Row.DataItem)["ATT_DAY_ID"].ToString() + "','"
                                            + this.hdDate.Value.ToString()
                                            + "');";
                e.Row.Attributes["style"] = "cursor: pointer;";
            }

            if (((DataRowView)e.Row.DataItem)["ATT_DAY_Icon"].ToString() != "0" && ((DataRowView)e.Row.DataItem)["CON_TYPE"].ToString() == "D")
                e.Row.CssClass = "mepm_menu_active_bg";
            else if (((DataRowView)e.Row.DataItem)["ATT_DAY_Icon"].ToString() != ((DataRowView)e.Row.DataItem)["CON_TIME"].ToString() && ((DataRowView)e.Row.DataItem)["CON_TYPE"].ToString() == "M")
                e.Row.CssClass = "mepm_menu_active_bg";
            else if (((DataRowView)e.Row.DataItem)["ATT_DAY_Code"].ToString() != "001" && ((DataRowView)e.Row.DataItem)["CON_TYPE"].ToString() == "M")
                e.Row.CssClass = "mepm_menu_active_bg";

            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Attendance/m_ATT_Parttime_Modify.aspx?ATT_DAY_ID=" + this.hdATT_DAY_ID.Value.ToString() + "&DATE=" + this.hdDate.Value.ToString());
    }

    /// <summary>
    /// 날짜 변경 버튼 클릭 시
    /// </summary>
    protected void btnChange_Click(object sender, EventArgs e)
    {
        this.txtDate.Text = "YYYYMMDD";

        this.lblDate.Visible = false;
        this.lblWeek.Visible = false;
        this.btnChange.Visible = false;

        this.txtDate.Visible = true;
        this.btnSubmit.Visible = true;
    }

    /// <summary>
    /// 날짜 확인 버튼 클릭 시
    /// </summary>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.lblDate.Visible = true;
        this.lblWeek.Visible = true;
        this.btnChange.Visible = true;

        this.txtDate.Visible = false;
        this.btnSubmit.Visible = false;

        if (this.txtDate.Text != "YYYYMMDD")
        {
            this.hdDate.Value = this.txtDate.Text.ToString();
            this.lblDate.Text = DateTime.Parse(this.txtDate.Text.ToString()).ToString("yyyy년 MM월 dd일");
            this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.txtDate.Text.ToString()));

            SetList();
        }
    }

    /// <summary>
    /// 이전 날짜 버튼 클릭 시
    /// </summary>
    protected void btnPreDate_Click(object sender, EventArgs e)
    {
        this.hdDate.Value = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(-1).ToString("yyyy-MM-dd");
        this.lblDate.Text = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(-1).ToString("yyyy년 MM월 dd일");
        this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.lblDate.Text.ToString()));

        SetList();
    }

    /// <summary>
    /// 다음 날짜 버튼 클릭 시
    /// </summary>
    protected void btnNextDate_Click(object sender, EventArgs e)
    {
        this.hdDate.Value = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        this.lblDate.Text = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(1).ToString("yyyy년 MM월 dd일");
        this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.lblDate.Text.ToString()));

        SetList();
    }
}