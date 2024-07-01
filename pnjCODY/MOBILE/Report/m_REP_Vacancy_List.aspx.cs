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


public partial class m_REP_Vacancy_List : System.Web.UI.Page
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
        Response.Redirect("/Report/m_REP_Vacancy.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&VAC_ID=" + this.htVAC_ID.Value.ToString());
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            DataTable dt = Select_VAC_List();

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
                                        + Session["sRES_ID"].ToString()
                                        + "', '"
                                        + ((DataRowView)e.Row.DataItem)["VAC_ID"].ToString()
                                       + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    private DataTable Select_VAC_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_TO_VACANCY_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@TO_RBS", Session["sRES_RBS_CD"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TO_AssArea", Session["sRES_RBS_AREA_CD"].ToString());

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