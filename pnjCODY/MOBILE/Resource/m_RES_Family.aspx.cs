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


public partial class Resource_m_RES_Family : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetList();
        }
    }
    
    // 그리드뷰 DataBound
    private void SetList()
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();
            DataSet ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "FAM", "Table");

            this.gvFamilyList.DataSource = ds;
            this.gvFamilyList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvFamilyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["RES_FAM_ID"].ToString()
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
        Response.Redirect("/Resource/m_RES_Family_Add.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&RES_FAM_ID=" + this.hdRES_FAM_ID.Value.ToString());
    }
}