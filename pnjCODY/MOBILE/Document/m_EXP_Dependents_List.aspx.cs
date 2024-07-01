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

public partial class m_EXP_Dependents_List : System.Web.UI.Page
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
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = Select_List();

            this.gvFamilyList.DataSource = ds.Tables[0];
            this.gvFamilyList.DataBind();
        }
    }

    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_DEPENDENTS_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());

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

        return ds;
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

            if (!string.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["RES_FAM_Relation"].ToString()))
                e.Row.Cells[2].Text = getRelationName(((DataRowView)e.Row.DataItem)["RES_FAM_Relation"].ToString());
         
            if (!string.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["STATUS"].ToString()))
            {
                if (((DataRowView)e.Row.DataItem)["STATUS"].ToString().Equals("Y"))
                    e.Row.Cells[4].Text = "확인";
                else if (((DataRowView)e.Row.DataItem)["STATUS"].ToString().Equals("D"))
                    e.Row.Cells[4].Text = "반려";
                else if (((DataRowView)e.Row.DataItem)["STATUS"].ToString().Equals("N"))
                    e.Row.Cells[4].Text = "요청";             
                else
                    e.Row.Cells[4].Text = "-";
            }

        }
    }   

    private string getRelationName(string strCode)
    {
        string retStr = string.Empty;

        Code code = new Code();
        DataSet ds = code.DZICUBE_CODE("H1");

        DataRow[] dr = ds.Tables[0].Select(string.Format("CTD_CD='{0}'", strCode));

        if (dr.Length > 0)
            retStr = dr[0]["CTD_NM"].ToString();

        return retStr;
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Document/m_EXP_Dependents.aspx?RES_ID=" + this.hdRES_ID.Value.ToString() + "&RES_FAM_ID=" + this.hdRES_FAM_ID.Value.ToString());
    }
}