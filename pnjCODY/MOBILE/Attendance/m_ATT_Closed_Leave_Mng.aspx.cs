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

public partial class Attendance_m_ATT_Closed_Leave_Mng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
            SetList();
        }
    }

    // 컨트롤 세팅
    private void SetControl()
    {
        this.txtFROMDATE.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.txtTODATE.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();
            DataSet ds = attendance.EPM_ATT_REQ_LEAVE_LIST_MOBILE(int.Parse(Session["sRES_ID"].ToString()), this.txtFROMDATE.Text.ToString(), this.txtTODATE.Text.ToString());

            this.gvReqList.DataSource = ds;
            this.gvReqList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvReqList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["ATT_REQ_ID"].ToString() + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";

            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    // 검색 버튼 클릭 시
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetList();
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Attendance/m_ATT_Closed_Leave_Request.aspx?ATT_REQ_ID=" + this.hdATT_REQ_ID.Value.ToString());
    }
}