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

public partial class Business_m_BUS_Expend_Mng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();

            //승인상태 선택
            SetddStatusType();

            //승인항목 선택
            SetddReqType();

            SetList();
        }
    }

    // 컨트롤 세팅
    private void SetControl()
    {
        this.txtFROMDATE.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.txtTODATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    #region 항목 DDL 바인딩
    protected void SetddStatusType()
    {
        DataSet ds = null;
        ds = Select_CodeList("12");

        //ListItem firstItem = new ListItem("-선택-", "");
        //ddlReqType.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_Name"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlStatus.Items.Add(tempItem);
        }
    }

    protected void SetddReqType()
    {
        DataSet ds = null;
        ds = Select_CodeList("10");

        //ListItem firstItem = new ListItem("-선택-", "");
        //ddlReqType.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name;
            if (dr["COD_Remarks"].ToString() == "")
                code_name = dr["COD_Name"].ToString();
            else
                code_name = dr["COD_Remarks"].ToString() + "_" + dr["COD_Name"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlReqType.Items.Add(tempItem);
        }
    }
    #endregion

    #region EPM 코드리스트 조회
    private DataSet Select_CodeList(string Code_Category)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CODE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@CODE_CATEGORY", Code_Category);

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
    #endregion
        
    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_List();

            this.gvReqList.DataSource = ds;
            this.gvReqList.DataBind();
        }
    }

    #region 목록 조회
    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_BUS_EXP_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@FROM", this.txtFROMDATE.Text.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TO", this.txtTODATE.Text.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@REQ_STATUS", ddlStatus.SelectedValue.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@REQ_TYPE", ddlReqType.SelectedValue.ToString());

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
    #endregion
    // 그리드뷰 DataBound 시
    protected void gvReqList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["EXP_REQ_ID"].ToString() + "');";
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
        Response.Redirect("/Business/m_BUS_Expend_Request.aspx?EXP_REQ_ID=" + this.hdEXP_REQ_ID.Value.ToString());
    }
}