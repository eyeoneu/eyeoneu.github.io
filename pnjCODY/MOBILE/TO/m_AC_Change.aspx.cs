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

public partial class m_AC_Change : System.Web.UI.Page
{
    int RBS_CD;
    int AREA_CD;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
            txtDate.Text = DateTime.Today.ToShortDateString();
        }
    }       

    #region TO 코드리스트 조회
    private DataSet Select_TOCodeList(string gb, string resID, string toID)
    {
        Resource resource = new Resource();
        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "REQ");
        if (rd.Read())
        {
            RBS_CD = int.Parse(rd["RES_RBS_CD"].ToString());
            AREA_CD = int.Parse(rd["RES_RBS_AREA_CD"].ToString());
        }
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_DROPDOWN_LIST", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@GB", gb);
        adp.SelectCommand.Parameters.AddWithValue("@TO_SPT_RES_ID", resID);
        adp.SelectCommand.Parameters.AddWithValue("@TO_RBS", RBS_CD);
        adp.SelectCommand.Parameters.AddWithValue("@TO_AssArea", AREA_CD);
        adp.SelectCommand.Parameters.AddWithValue("@TO_ID", toID);

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

    #region DZ 코드리스트 조회
    private DataSet Select_WORKGROUP2_DZCodeList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_WORKGROUP2_DZ_CODE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

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

    #region 부서 리스트 조회
    private DataSet Select_RBSList(string gb, string rbsID)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_RBS_LIST", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@GB", gb);
        adp.SelectCommand.Parameters.AddWithValue("@SECT_CD", rbsID);

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

    #region 매장 리스트 조회
    private DataSet Select_StoreList(string gb, string Customer)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CUSTOMER_STORE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@GB", gb);
        adp.SelectCommand.Parameters.AddWithValue("@CUSTOMER", Customer);

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

    #region 매장 DDL 바인딩(RES)
    protected void SetddlStore(object sender, EventArgs e)
    {
        ddlResAfter2.Items.Clear();
        ddlResAfter2.Enabled = true;

        DataSet ds = null;
        ds = Select_StoreList("CUS", ddlResAfter3.SelectedValue);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_NM"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlResAfter2.Items.Add(tempItem);
        }

    }
    #endregion

    #region RBS 부서 DDL 바인딩
    protected void SetddlAssArea(object sender, EventArgs e)
    {
        ddlResAfter2.Items.Clear();
        ddlResAfter2.Enabled = true;

        DataSet ds = null;
        ds = Select_RBSList("AREA", ddlResAfter4.SelectedValue);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["RES_RBS_NAME"].ToString();
            string code = dr["RES_RBS_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlResAfter2.Items.Add(tempItem);
        }
    }
    #endregion 

    #region 사원정보 변경전, 변경후 DDL 바인딩
    protected void SetddlResBefore(object sender, EventArgs e)
    {
        divMsg.Visible = false;

        ddlResBefore.Items.Clear();
        ddlResBefore.Enabled = true;
        if (ddlResType.SelectedValue == "") // 구분: 공백값 선택시
        {
            this.divBefore.Visible = true;
            this.divAfter.Visible = true;

            ddlResBefore.Items.Clear();
            ddlResAfter.Items.Clear();
            ddlResAfter.Width = new Unit("100%");
            ddlResAfter.Visible = true;
            ddlResAfter2.Visible = false;
            ddlResAfter3.Visible = false;
            ddlResAfter4.Visible = false;

            ddlResBefore.Enabled = false;
            ddlResAfter.Enabled = false;

            divConInfo.Visible = false;
        }
        if (ddlResType.SelectedValue == "매장이동")
        {
            this.divBefore.Visible = true;
            this.divAfter.Visible = true;

            ddlResBefore.Items.Clear();
            ddlResAfter.Items.Clear();
            ddlResAfter2.Items.Clear();
            ddlResAfter3.Visible = false;
            ddlResAfter4.Visible = false;

            DataSet ds = null;
            ds = Select_StoreList("", "");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string CodeName = dr["COD_NM"].ToString();
                string Code = dr["COD_CD"].ToString();

                ListItem tempItem = new ListItem(CodeName, Code);
                ddlResAfter3.Items.Add(tempItem);
            }

            ddlResBefore.Visible = false;
            txtResBefore2.Text = hdnAssConStore.Value;
            txtResBefore2.Visible = true;

            ddlResAfter.Visible = false;
            ddlResAfter3.Visible = true;
            ddlResAfter2.Visible = true;
            ddlResAfter2.Enabled = false;

            divConInfo.Visible = false;
        }
        else if (ddlResType.SelectedValue == "")
            ddlResAfter2.Visible = false;

        if (ddlResType.SelectedValue == "지원사변경")
        {
            ddlResBefore.Items.Clear();
            ddlResAfter.Items.Clear();
            ddlResBefore.Visible = true;
            txtResBefore2.Visible = false;
            ddlResAfter.Visible = true;
            ddlResAfter2.Visible = false;
            ddlResAfter3.Visible = false;
            ddlResAfter4.Visible = false;

            DataSet cds = null;
            cds = Select_CodeList("5");
            foreach (DataRow dr in cds.Tables[0].Rows)
            {
                string CodeName = dr["COD_NAME"].ToString();
                string Code = dr["COD_CD"].ToString();

                ListItem tempItem = new ListItem(CodeName, Code);
                ddlResBefore.Items.Add(tempItem);
                ddlResAfter.Items.Add(tempItem);
            }
            ddlResBefore.SelectedValue = hdnAssConVender.Value;
            ddlResBefore.Enabled = false;
            ddlResAfter.Enabled = true;

            divConInfo.Visible = false;
        }

        if (ddlResType.SelectedValue == "직급변경")
        {
            ddlResBefore.Items.Clear();
            ddlResAfter.Items.Clear();
            ddlResBefore.Visible = true;
            txtResBefore2.Visible = false;
            ddlResAfter.Width = new Unit("100%");
            ddlResAfter.Visible = true;
            ddlResAfter2.Visible = false;
            ddlResAfter3.Visible = false;
            ddlResAfter4.Visible = false;

            this.divBefore.Visible = true;
            this.divAfter.Visible = true;

            DataSet ds = null;
            ds = Select_WORKGROUP2_DZCodeList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string CodeName = dr["CTD_NM"].ToString();
                string Code = dr["CTD_CD"].ToString();

                ListItem tempItem = new ListItem(CodeName, Code);
                ddlResBefore.Items.Add(tempItem);
                ddlResAfter.Items.Add(tempItem);
            }
            ddlResBefore.SelectedValue = hdnWorkGroup2.Value;
            ddlResBefore.Enabled = false;
            ddlResAfter.Enabled = true;

            divConInfo.Visible = false;
        }

        if (ddlResType.SelectedValue == "서포터지역변경")
        {
            ddlResBefore.Items.Clear();
            ddlResAfter.Items.Clear();
            ddlResAfter2.Items.Clear();
            ddlResAfter4.Items.Clear();
            ddlResBefore.Visible = true;
            txtResBefore2.Visible = false;
            ddlResAfter4.Visible = true;
            ddlResAfter.Visible = false;
            ddlResAfter3.Visible = false;

            DataSet ds = null;
            ds = Select_TOCodeList("D", this.Page.Request["RES_ID"].ToString(), "");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string code_name = dr["RES_RBS_Name"].ToString();
                string code = dr["RES_RBS_CD"].ToString();

                ListItem tempItem = new ListItem(code_name, code);
                ddlResBefore.Items.Add(tempItem);
            }

            DataSet rds = null;
            rds = Select_RBSList("DEPT", "");

            foreach (DataRow dr in rds.Tables[0].Rows)
            {
                string code_name = dr["RES_RBS_NAME"].ToString();
                string code = dr["RES_RBS_CD"].ToString();

                ListItem tempItem = new ListItem(code_name, code);
                ddlResAfter4.Items.Add(tempItem);
            }      
          
            ddlResBefore.Enabled = false;
            ddlResAfter.Width = 148;
            ddlResAfter.Enabled = true;
            ddlResAfter2.Visible = true;
            ddlResAfter2.Enabled = false;

            divConInfo.Visible = false;
        }

        if (ddlResType.SelectedValue == "월계약정보변경")
        {
            if (this.txtDueDate.Text == "" || this.txtDueDate.Text == "YYYYMMDD")
            {
                divMsg.Visible = true;
                lblMsg.Text = "월계약정보변경을 입력할 경우 변경일자를 먼저 입력해주세요.";

                ddlResType.SelectedValue = "";
            }
            else
            {
                divConInfo.Visible = true;

                divBefore.Visible = false;
                divAfter.Visible = false;

                setDdlToList();
            }
        }
        else if (ddlResType.SelectedValue == "")
            ddlResAfter2.Visible = false;
    }
    #endregion

    // 일근무시간변경 선택 시 사원별 TO목록 반영
    private void setDdlToList()
    {
        // TO 리스트
        DataSet ds = Select_TOCodeList(this.hdnAssConRES.Value.ToString());

        this.ddlToList.DataSource = ds;
        this.ddlToList.DataBind();
    }

    #region 사원별 TO 목록 조회
    private DataSet Select_TOCodeList(string resID)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_LIST_BY_RES_ID", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@RES_ID", resID);
        adp.SelectCommand.Parameters.AddWithValue("@HIS_DueProcess", this.txtDueDate.Text.ToString());

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

    protected void ddlToList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        DataSet ds = Select_TOCodeDetail(ddl.SelectedValue.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblConInfoBefore.Text = "일근무시간(" + ds.Tables[0].Rows[0]["TO_TIME"].ToString().Trim() + "시간), 월급여(" + ds.Tables[0].Rows[0]["TO_PAY"].ToString() + "원)";
        }
        else
        {
            lblConInfoBefore.Text = "일근무시간(0시간), 월급여(0원)";
        }
    }

    #region TO 상세 조회
    private DataSet Select_TOCodeDetail(string ToNum)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_DETAIL_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@TO_NUM", ToNum);

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
            //adp.SelectCommand.Parameters.AddWithValue("@WORKGROUP1", "004");

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

    #region 사원정보 조회
    private DataSet SelectAC_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_DETAIL_SELECT_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@MODE", "REQ");
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

    #region 사원정보 변경항목 DDL 바인딩
    protected void SetddlResType()
    {
        this.divBefore.Visible = true;
        this.divAfter.Visible = true;

        ddlResType.Items.Clear();
        ddlResBefore.Items.Clear();
        ddlResAfter.Items.Clear();
        ddlResAfter.Width = new Unit("100%");

        ddlResType.Enabled = true;
        ddlResBefore.Enabled = false;
        ddlResAfter.Enabled = false;

        if (hdnAssConRES.Value == "") // 구분: 공백값 선택시
        {
            ddlResType.Enabled = false;
        }
        else
        {
            ListItem firstItem = new ListItem("-선택-", "");
            ddlResType.Items.Add(firstItem);
            ddlResType.Items.Add(new ListItem("매장이동", "매장이동"));
            ddlResType.Items.Add(new ListItem("서포터지역변경", "서포터지역변경"));
            ddlResType.Items.Add(new ListItem("지원사변경", "지원사변경"));
            ddlResType.Items.Add(new ListItem("직급변경", "직급변경"));
            ddlResType.Items.Add(new ListItem("월계약정보변경", "월계약정보변경"));
        }
    }
    #endregion


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
            this.txtResName.Text = dsList.Tables[0].Rows[0]["RES_NAME"].ToString();
            this.hdnResName.Value = dsList.Tables[0].Rows[0]["RES_NAME"].ToString();
            this.hdnWorkGroup2.Value = dsList.Tables[0].Rows[0]["RES_WorkGroup2"].ToString();
            
            if (dsList.Tables[1].Rows.Count != 0)
            {
				this.hdnAssConID.Value = dsList.Tables[1].Rows[0]["ASSCON_ID"].ToString();
				this.hdnAssConStore.Value = dsList.Tables[1].Rows[0]["RES_STORE"].ToString();
				this.hdnAssConGB.Value = dsList.Tables[1].Rows[0]["GB"].ToString();
				this.hdnAssConRES.Value = dsList.Tables[1].Rows[0]["RES_ID"].ToString();
				this.hdnAssConVender.Value = dsList.Tables[1].Rows[0]["ASSCON_VENDER"].ToString();
            }
        }

        this.dvResList.Visible = false;
        SetddlResType();
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

            SqlCommand Cmd = new SqlCommand("EPM_TO_HISTORY_SUBMIT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@AC_ID", this.hdnAssConID.Value.ToString());
            Cmd.Parameters.AddWithValue("@HIS_GB", "A");
            Cmd.Parameters.AddWithValue("@HIS_TYPE", this.ddlResType.SelectedValue.ToString());

            if (this.ddlResType.SelectedValue.ToString() == "월계약정보변경")
            {
                Cmd.Parameters.AddWithValue("@HIS_Before", this.lblConInfoBefore.Text.ToString());
                Cmd.Parameters.AddWithValue("@HIS_After", "일근무시간(" + this.ddlRES_CON_TIME.SelectedValue.ToString() + "시간), 월급여(" + this.txtRES_CON_PAY.Text.ToString() + "원)");
                Cmd.Parameters.AddWithValue("@TO_NUM", this.ddlToList.SelectedValue.ToString());
            }
            else
            {
                if (this.ddlResType.SelectedValue.ToString() == "매장이동")
                {
                    Cmd.Parameters.AddWithValue("@HIS_Before", this.txtResBefore2.Text.ToString());
                }
                else
                    Cmd.Parameters.AddWithValue("@HIS_Before", this.ddlResBefore.SelectedItem.Text.ToString());

                if (this.ddlResType.SelectedValue.ToString() == "매장이동")
                {
                    Cmd.Parameters.AddWithValue("@HIS_After", this.ddlResAfter3.SelectedItem.Text.ToString() + " " + this.ddlResAfter2.SelectedItem.Text.ToString());
                }
                if (this.ddlResType.SelectedValue.ToString() == "서포터지역변경")
                {
                    Cmd.Parameters.AddWithValue("@HIS_After", this.ddlResAfter4.SelectedItem.Text.ToString() + " " + this.ddlResAfter2.SelectedItem.Text.ToString());
                }
                if (this.ddlResType.SelectedValue.ToString() == "지원사변경" || this.ddlResType.SelectedValue.ToString() == "직급변경")
                {
                    Cmd.Parameters.AddWithValue("@HIS_After", this.ddlResAfter.SelectedItem.Text.ToString());
                }
            }

            Cmd.Parameters.AddWithValue("@AC_GB", this.hdnAssConGB.Value.ToString());
            Cmd.Parameters.AddWithValue("@AC_RES_ID", this.hdnAssConRES.Value.ToString());
            Cmd.Parameters.AddWithValue("@HIS_Reason", this.txtResReason.Text.ToString());
            Cmd.Parameters.AddWithValue("@SPT_RES_ID", this.Page.Request["RES_ID"].ToString());
            Cmd.Parameters.AddWithValue("@HIS_DueProcess", this.txtDueDate.Text.ToString());

            Cmd.ExecuteNonQuery();

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_AC_Change_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }
}