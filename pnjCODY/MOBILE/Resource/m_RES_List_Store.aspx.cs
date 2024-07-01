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

public partial class Resource_m_RES_List_Store : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetgvResList();
        }
    }

    /// <summary>
    /// 사원 관리 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Mng.aspx?RES_ID=" + this.hdRES_ID.Value.ToString());
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            Resource resource = new Resource();
            DataSet ds = resource.EPM_RES_LIST_STORE_MOBILE(int.Parse(this.Page.Request["RES_ASS_StoreID"].ToString()));

            this.gvResList.DataSource = ds.Tables[0];
            this.gvResList.DataBind();

            this.STORE_NAME.Text = ds.Tables[1].Rows[0]["STORE_NAME"].ToString();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }
}