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


public partial class m_AC_Change_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtFinishDate.Text = DateTime.Now.ToString("yyyyMMdd"); 
            SetgvResList();
        }
    }

    /// <summary>
    /// 사원 관리 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Business/m_BUS_Connection_View.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&HIS_ID=" + this.hdHIS_ID.Value.ToString());
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            DataTable dt = Select_Trn_List();

            this.gvResList.DataSource = dt;
            this.gvResList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["HIS_SPT_RES_ID"].ToString()
                                        + "', '"
                                        + ((DataRowView)e.Row.DataItem)["HIS_ID"].ToString()
                                       + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    // 검색 버튼 클릭시
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetgvResList();
    }

    private DataTable Select_Trn_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_TO_HISTORY_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@GB", "A");
        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@FINISH_DATE", txtFinishDate.Text.ToString());

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
}