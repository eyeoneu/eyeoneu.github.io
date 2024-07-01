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


public partial class m_EXP_Employment_List : System.Web.UI.Page
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
        Response.Redirect("/Document/m_EXP_Employment.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&EMP_ID=" + this.htEMP_ID.Value.ToString());
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            txtDate.Text = DateTime.Today.ToShortDateString();

            DataTable dt = Select_List();

            this.gvMkList.DataSource = dt;
            this.gvMkList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "', '"
                                        + ((DataRowView)e.Row.DataItem)["EMP_ID"].ToString()
                                       + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";


            if (!string.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["EMP_GB_NAME"].ToString()))
            {
                e.Row.Cells[0].Text = ((DataRowView)e.Row.DataItem)["EMP_GB_NAME"].ToString();

                //if (((DataRowView)e.Row.DataItem)["EMP_GB"].ToString().Equals("W"))
                //    e.Row.Cells[0].Text = "재직증명서";
                //else if(((DataRowView)e.Row.DataItem)["EMP_GB"].ToString().Equals("E"))
                //    e.Row.Cells[0].Text = "소득증명서";
                //else
                //    e.Row.Cells[0].Text = "이직확인서";
            }

            e.Row.Cells[1].Text = ((DataRowView)e.Row.DataItem)["STATUS"].ToString();
        }
    }

    private DataTable Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_EMPLOYMENT_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@EMP_DATE", txtDate.Text);

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

        DataTable dt = Select_List();

        this.gvMkList.DataSource = dt;
        this.gvMkList.DataBind();
    }
}