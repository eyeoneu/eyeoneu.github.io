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

public partial class m_REP_Vacancy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetddlCause();
            SetddlJob();
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["VAC_ID"].ToString() != "0")
        {
            DataTable dt = Select_VAC_Detail();

            if (dt.Rows.Count > 0)
            {
                lblToName.Text = dt.Rows[0]["TO_Name"].ToString();
                lblStartDate.Text = dt.Rows[0]["VAC_StartDate"].ToString();
                ddlCause.SelectedValue = dt.Rows[0]["VAC_Cause"].ToString();
                ddlInstead.SelectedValue = dt.Rows[0]["VAC_Instead"].ToString();
                ddlJob.SelectedValue = dt.Rows[0]["VAC_Job"].ToString();
                txtInterview.Text = dt.Rows[0]["VAC_Interview"].ToString();
                txtRemark.Text = dt.Rows[0]["VAC_Remarks"].ToString();
            }
        }
    }

    #region 결원사유 DDL 바인딩
    private void SetddlCause()
    {
        DataSet ds = null;
        ds = Select_CodeList("18");

        ListItem firstItem = new ListItem("-선택-", "");
        ddlCause.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["COD_CD"].ToString().Length > 0)
            {
                string CodeName = dr["COD_Name"].ToString();
                string Code = dr["COD_CD"].ToString();
                ListItem tempItem = new ListItem(CodeName, Code);
                ddlCause.Items.Add(tempItem);
            }

        }
    }
    #endregion

    #region 구인방법 DDL 바인딩
    private void SetddlJob()
    {
        DataSet ds = null;
        ds = Select_CodeList("19");

        ListItem firstItem = new ListItem("-선택-", "");
        ddlJob.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["COD_CD"].ToString().Length > 0)
            {
                string CodeName = dr["COD_Name"].ToString();
                string Code = dr["COD_CD"].ToString();
                ListItem tempItem = new ListItem(CodeName, Code);
                ddlJob.Items.Add(tempItem);
            }

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

    // 검색 버튼 클릭
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetgvResList();
        this.dvResList.Visible = true;
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_SEARCH", Con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            adp.SelectCommand.Parameters.AddWithValue("@NAME", this.txtRES_Name.Text.Trim());
            adp.SelectCommand.Parameters.AddWithValue("@WORKGROUP1", "004");

            DataSet ds = new DataSet();

            try
            {
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            this.gvResList.DataSource = ds;
            this.gvResList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncSelectRes('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["GB"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["ASSCON_ID"].ToString() + "');";

            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    protected void ddlInstead_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstead.SelectedValue == "Y")
        {
            dvResSearch.Visible = true;
            dvResSelected.Visible = true;
        }
        else
        {
            dvResSearch.Visible = false;
            dvResSelected.Visible = false;
            txtRES_Name.Text = "";
            txtResName.Text = "";
            hdnResName.Value = "";
            hdnAssConID.Value = "";
            hdnAssConStore.Value = "";
            hdnAssConGB.Value = "";
            hdnAssConRES.Value = "";
            hdnAssConVender.Value = "";
            hdnWorkGroup2.Value = "";
        }
    }

    /// <summary>
    /// 신청대상 선택 시
    /// </summary>
    protected void btnSelectRes_Click(object sender, EventArgs e)
    {
        DataSet dsList = null;
        dsList = SelectAC_List();
        ViewState["ds"] = dsList;

        if (dsList.Tables[0].Rows.Count != 0)
        {
            this.txtResName.Text = dsList.Tables[0].Rows[0]["RES_NAME"].ToString() + " (" + dsList.Tables[0].Rows[0]["RES_Number"].ToString() + ")";
            this.hdnResName.Value = dsList.Tables[0].Rows[0]["RES_NAME"].ToString();
            this.hdnAssConID.Value = dsList.Tables[1].Rows[0]["ASSCON_ID"].ToString();
            this.hdnAssConStore.Value = dsList.Tables[1].Rows[0]["RES_STORE"].ToString();
            this.hdnAssConGB.Value = dsList.Tables[1].Rows[0]["GB"].ToString();
            this.hdnAssConRES.Value = dsList.Tables[1].Rows[0]["RES_ID"].ToString();
            this.hdnAssConVender.Value = dsList.Tables[1].Rows[0]["ASSCON_VENDER"].ToString();
            this.hdnWorkGroup2.Value = dsList.Tables[0].Rows[0]["RES_WorkGroup2"].ToString();
        }

        this.dvResList.Visible = false;       
    }

    #region 사원정보 조회
    private DataSet SelectAC_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_DETAIL_SELECT_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@MODE", "VAC");
        adp.SelectCommand.Parameters.AddWithValue("@RES_ID", int.Parse(this.hdnAssConRES.Value.ToString()));

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

    private DataTable Select_VAC_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_TO_VACANCY_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@VAC_ID", this.Page.Request["VAC_ID"].ToString());

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

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand CmdRemark = new SqlCommand("EPM_TO_VACANCY_SUBMIT_MOBILE", Con);
            CmdRemark.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            CmdRemark.Transaction = trans;

            CmdRemark.Parameters.AddWithValue("@VAC_ID", this.Page.Request["VAC_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@VAC_Cause", ddlCause.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@VAC_Instead", ddlInstead.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@VAC_Job", ddlJob.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@VAC_Interview", txtInterview.Text);
            CmdRemark.Parameters.AddWithValue("@VAC_Remarks", txtRemark.Text);
            CmdRemark.Parameters.AddWithValue("@VAC_Instead_ID", hdnAssConID.Value);
            CmdRemark.Parameters.AddWithValue("@VAC_Instead_GB", hdnAssConGB.Value);

            CmdRemark.ExecuteNonQuery();

            trans.Commit();
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (Con != null)
            {
                Con.Close();
                Con = null;
            }
        }

        if (!bError)
        {
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_REP_Vacancy_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }  
}