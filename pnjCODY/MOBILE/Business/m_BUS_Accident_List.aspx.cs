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


public partial class m_BUS_Accident_List : System.Web.UI.Page
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
        Response.Redirect("/Business/m_BUS_Accident_Report.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&ACC_ID=" + this.htACC_ID.Value.ToString());
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            this.txtStartDate.Text = DateTime.Today.ToShortDateString().ToString().Substring(0, 8) + "01";
            this.txtFinishDate.Text = DateTime.Today.ToShortDateString();

            DataTable dt = Select_Market_List();

            this.gvMkList.DataSource = dt;
            this.gvMkList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "', '"
                                        + ((DataRowView)e.Row.DataItem)["ACC_ID"].ToString()
                                       + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    private DataTable Select_Market_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ACCIDENT_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@START_DATE", txtStartDate.Text);
        ad.SelectCommand.Parameters.AddWithValue("@FINISH_DATE", txtFinishDate.Text);

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DataTable dt = Select_Market_List();

        this.gvMkList.DataSource = dt;
        this.gvMkList.DataBind();
    }
}