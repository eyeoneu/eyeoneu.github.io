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

public partial class Attendance_m_ATT_Cody_Request_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
            SetPage();
            SetList();
        }
    }

    // 기본 정보 세팅
    private void SetControl()
    {
        this.txtAttMonth.Text = DateTime.Today.ToString("yyyy-MM");
        this.hdDate.Value = DateTime.Today.ToString("yyyyMM");
    }

    // 코디/알바 출근 현황 정보
    private void SetPage()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();

            SqlDataReader rd = attendance.EPM_ATT_DAY_REQ_TODAY("ALL", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString(), this.hdDate.Value.ToString());

            if (rd.Read())
            {
                //this.lblALL_REQ_CNT.Text = rd["ALL_REQ"].ToString();
                //this.lblALL_CNF_CNT.Text = rd["ALL_CNF"].ToString();
                //this.lblALL_RTN_CNT.Text = rd["ALL_RTN"].ToString();

                this.lblCODY_REQ_CNT.Text = rd["ASS_REQ"].ToString();
                this.lblCODY_CNF_CNT.Text = rd["ASS_CNF"].ToString();
                this.lblCODY_RTN_CNT.Text = rd["ASS_RTN"].ToString();

                //this.lblAR_REQ_CNT.Text = rd["CON_REQ"].ToString();
                //this.lblAR_CNF_CNT.Text = rd["CON_CNF"].ToString();
                //this.lblAR_RTN_CNT.Text = rd["CON_RTN"].ToString();
            }
        }
    }


    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();
            DataSet ds = attendance.EPM_ATT_DAY_REQ_TODAY_LIST("ASS", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString(), this.hdDate.Value.ToString());

            this.gvAttReqList.DataSource = ds;
            this.gvAttReqList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvAttReqList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            string result = ((DataRowView)e.Row.DataItem)["ADR_Edit_Text"].ToString();
            string[] part = result.Split('→');

            e.Row.Cells[2].Text = part[1].ToString();

            if (((DataRowView)e.Row.DataItem)["ADR_Status"].ToString() == "승인반려" || ((DataRowView)e.Row.DataItem)["ADR_Status"].ToString() == "결재반려") //반려된 이력을 클릭하면 레이어로 사유를 보여준다
            {
                e.Row.Cells[3].Text = "<ul><li title=\"상세보기\"onclick=\"toggle_display('div_com" + ((DataRowView)e.Row.DataItem)["ADR_ID"].ToString()
                + "');\" class=\"ListStyle\">" + ((DataRowView)e.Row.DataItem)["ADR_Status"].ToString()
                + "</li><li id=\"div_com" + ((DataRowView)e.Row.DataItem)["ADR_ID"].ToString() + "\" style=\"display: none;\">"
                + "<ul onclick=\"toggle_display('div_com" + ((DataRowView)e.Row.DataItem)["ADR_ID"].ToString()
                + "');\" title=\"닫기\" class=\"ModalPopup\">"
                + "<li>" + "<span style=\"font-size:1.1em; font-weight:bold;\">반려내용 상세보기</span>"
                + "이름 : &nbsp;" + ((DataRowView)e.Row.DataItem)["RES_Name"].ToString()
                + "<BR />" + "수정일자 : &nbsp;" + ((DataRowView)e.Row.DataItem)["ATT_DATE"].ToString()
                + "<BR />" + "수정내용 : &nbsp;" + ((DataRowView)e.Row.DataItem)["ADR_Edit_Text"].ToString()
                + "<BR />" + "반려사유 : &nbsp;" + ((DataRowView)e.Row.DataItem)["ADR_Return_Text"].ToString()
                + "</li>"
                + "</ul></li></ul>";
            }

        }
    }
    // 검색 버튼 클릭 시
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.txtAttMonth.Text != "YYYY-MM")
        {
            this.hdDate.Value = DateTime.Parse(this.txtAttMonth.Text.ToString()).ToString("yyyyMM");

            SetPage();
            SetList();
        }
    }

    /// <summary>
    /// 이전 날짜 버튼 클릭 시
    /// </summary>
    protected void btnPreDate_Click(object sender, EventArgs e)
    {
        this.hdDate.Value = DateTime.Parse(this.txtAttMonth.Text.ToString()).AddMonths(-1).ToString("yyyyMM");
        this.txtAttMonth.Text = DateTime.Parse(this.txtAttMonth.Text.ToString()).AddMonths(-1).ToString("yyyy-MM");

        SetPage();
        SetList();
    }

    /// <summary>
    /// 다음 날짜 버튼 클릭 시
    /// </summary>
    protected void btnNextDate_Click(object sender, EventArgs e)
    {
        this.hdDate.Value = DateTime.Parse(this.txtAttMonth.Text.ToString()).AddMonths(1).ToString("yyyyMM");
        this.txtAttMonth.Text = DateTime.Parse(this.txtAttMonth.Text.ToString()).AddMonths(1).ToString("yyyy-MM");

        SetPage();
        SetList();
    }
}