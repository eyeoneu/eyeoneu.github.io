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

public partial class Resource_m_RES_MyList : System.Web.UI.Page
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
			//Page.Response.Write(Session["sRES_RBS_CD"].ToString() + " "+ Session["sRES_RBS_AREA_CD"].ToString());
			//Page.Response.End();
			
            //Resource resource = new Resource();
            //DataSet ds = resource.EPM_RES_LIST_MOBILE("", "999", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());
			
			DataSet ds = GetList();
			
			if(ds != null)
			{
				if(ds.Tables.Count> 0 && ds.Tables[0].Rows.Count > 0)
				{
					this.gvResList.DataSource = ds;
					this.gvResList.DataBind();				
				}
			}
			
			//Resource resource = new Resource();
            //DataSet ds = resource.EPM_RES_LIST_MOBILE("", "999", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());

            //this.gvResList.DataSource = ds;
            //this.gvResList.DataBind();
            
            //Page.Response.Write(Session["sRES_ID"] + " " + Session["sRES_RBS_CD"].ToString() + " " + Session["sRES_RBS_AREA_CD"].ToString());
            
        }
    }
    
    private DataSet GetList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_LIST_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

		adp.SelectCommand.Parameters.AddWithValue("@NAME", "");
        adp.SelectCommand.Parameters.AddWithValue("@WORKGROUP1", "999");
        adp.SelectCommand.Parameters.AddWithValue("@RES_RBS_CD", Session["sRES_RBS_CD"].ToString());
        adp.SelectCommand.Parameters.AddWithValue("@RES_RBS_AREA_CD", Session["sRES_RBS_AREA_CD"].ToString());
		

        DataSet ds = new DataSet();

        try
        {
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
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