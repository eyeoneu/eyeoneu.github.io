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

public partial class Resource_m_RES_Assignment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetPage();
            SetList();
        }
    }

    // 사원 기본 정보 세팅
    private void SetPage()
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        if (rd.Read())
        {
            this.lblRES_Name.Text = rd["RES_Name"].ToString();
            this.lblRES_PersonNumber.Text = rd["RES_PersonNumber"].ToString();
            this.lblRES_Number.Text = rd["RES_Number"].ToString();
            this.lblRES_WorkState.Text = rd["RES_WorkState"].ToString();
        }

        rd.Close();
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();
            DataSet ds = resource.EPM_RES_ASSIGNMENT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

            this.gvAssignmentList.DataSource = ds;
            this.gvAssignmentList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvAssignmentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["RES_ASS_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";

            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Assignment_New.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&RES_ASS_ID=" + this.hdRES_ASS_ID.Value.ToString());
    }
}