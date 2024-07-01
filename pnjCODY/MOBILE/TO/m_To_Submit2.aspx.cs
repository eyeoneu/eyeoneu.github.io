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

public partial class m_To_Submit2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Page.Request["Type"] != null)
                ddlGB.SelectedValue = this.Page.Request["Type"].ToString();
   
            txtDate.Text = DateTime.Today.ToShortDateString();
            SetddlVender();
            SetddlCustomer();
            SetddlAssAreaTO();
            SetddlWorkgroup2();

            DataTable dtList = new DataTable();

            if (ViewState["DT_TO"] == null)
            {
                dtList.Columns.Add("TO_VENDER", typeof(string));
                dtList.Columns.Add("TO_VENAREA", typeof(string));
                dtList.Columns.Add("TO_VENOFFICE", typeof(string));
                dtList.Columns.Add("TO_CUSTOMER", typeof(int));
                dtList.Columns.Add("TO_CUSTOMER_NAME", typeof(string));
                dtList.Columns.Add("TO_STORE", typeof(int));
                dtList.Columns.Add("TO_STORE_NAME", typeof(string));
                dtList.Columns.Add("TO_WORKGROUP2", typeof(string));
                dtList.Columns.Add("TO_GB", typeof(string));
                dtList.Columns.Add("TO_MONDAY", typeof(string));
                dtList.Columns.Add("TO_TUESDAY", typeof(string));
                dtList.Columns.Add("TO_WEDNESDAY", typeof(string));
                dtList.Columns.Add("TO_THURSDAY", typeof(string));
                dtList.Columns.Add("TO_FRIDAY", typeof(string));
                dtList.Columns.Add("TO_SATURDAY", typeof(string));
                dtList.Columns.Add("TO_SUNDAY", typeof(string));
                dtList.Columns.Add("TO_REMARK", typeof(string));
                dtList.Columns.Add("IS_REPRESENT", typeof(bool));

                ViewState["DT_TO"] = dtList;
            }

            gvList.DataSource = dtList;
            gvList.DataBind();
        }
    }

    #region 지원사 소속/근무부서 리스트 조회
    private DataSet Select_VENList(string gb, string venCD, string venArea)
    {
        SqlConnection  Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_VEN_AREA_LIST", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@GB", gb);
        adp.SelectCommand.Parameters.AddWithValue("@VEN_CD", venCD);
        adp.SelectCommand.Parameters.AddWithValue("@VEN_AREA", venArea);

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

    #region 지원사 소속 DDL 바인딩
    protected void SetddlVenArea(object sender, EventArgs e)
    {
        ddlVenArea.Items.Clear();
        ddlVenOffice.Items.Clear();

        ddlVenArea.Enabled = true;
        ddlVenOffice.Enabled = false;

        DataSet ds = null;
        ds = Select_VENList("L2", ddlVender.SelectedValue, "");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_NM"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlVenArea.Items.Add(tempItem);
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

    #region EPM 지원사리스트 조회
    private DataSet Select_VenderList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CODE_FOR_VENDER", Con);
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

    #region 근무부서 DDL 바인딩
    protected void SetddlVenOffice(object sender, EventArgs e)
    {
        ddlVenOffice.Items.Clear();

        ddlVenOffice.Enabled = true;

        DataSet ds = null;
        ds = Select_VENList("L3", ddlVender.SelectedValue, ddlVenArea.SelectedValue);

        //ListItem firstItem = new ListItem("-선택-", "");
        //ddlVenOffice.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_NM"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlVenOffice.Items.Add(tempItem);
        }
    }
    #endregion

    #region 매장 DDL 바인딩(TO)
    protected void SetddlStoreTO(object sender, EventArgs e)
    {
        ddlStore.Items.Clear();

        ddlStore.Enabled = true;

        DataSet ds = null;
        ds = Select_StoreList("CUS", ddlCustomer.SelectedValue);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_NM"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlStore.Items.Add(tempItem);
        }

    }
    #endregion

    #region 지역 DDL 바인딩
    protected void SetddlAssAreaTO()
    {
        ddlAssArea.Items.Clear();

        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "REQ");

        if (rd.Read())
        {
     
        DataSet ds = null;
        ds = Select_RBSList("AREA", rd["RES_RBS_CD"].ToString()); //서포터의 부문

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["RES_RBS_NAME"].ToString();
            string code = dr["RES_RBS_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlAssArea.Items.Add(tempItem);
        }
        ddlAssArea.SelectedValue = rd["RES_RBS_AREA_CD"].ToString();; //서포터의 부서

        }
    }
    #endregion 

    #region 직급 DDL 바인딩
    protected void SetddlWorkgroup2()
    {
        ddlWorkgroup2.Items.Clear();

        DataSet ds = null;
        ds = Select_WORKGROUP2_DZCodeList();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["CTD_NM"].ToString();
            string Code = dr["CTD_CD"].ToString();

            ListItem tempItem = new ListItem(CodeName, Code);
            ddlWorkgroup2.Items.Add(tempItem);
        }
    }
    #endregion 

    #region 거래처 DDL
    private void SetddlCustomer()
    {
        DataSet ds = null;
        ds = Select_StoreList("", "");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["COD_NM"].ToString();
            string Code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(CodeName, Code);
            ddlCustomer.Items.Add(tempItem);
        }
    }
    #endregion

    #region 지원사 DDL
    private void SetddlVender()
    {
        DataSet ds = null;
        ds = Select_VenderList();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["COD_NAME"].ToString();
            string Code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(CodeName, Code);
            ddlVender.Items.Add(tempItem);
        }
    }
    #endregion

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        DataTable dtList = ViewState["DT_TO"] as DataTable;

		/* 순회 및 격고의 종류가 늘어남에 소스 변경 2016-07-14 박병진*/
        if (dtList.Rows.Count <= 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('하나 이상의 매장은 필수로 등록해야 합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else if (dtList.Rows.Count < 2 && ddlGB.SelectedValue.Contains("격고("))
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('근무형태가 격고 일 경우 2개의 매장을 필수 등록하여야 합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else if (dtList.Rows.Count < 3 && ddlGB.SelectedValue.Contains("순회("))
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('근무형태가 순회 일 경우 3개 이상의 매장을 필수 등록하여야 합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else
        {
            try
            {
                Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
                Con.Open();

                SqlCommand Cmd = new SqlCommand("EPM_TO_ROUND_LIST_SUBMIT_MOBILE", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                trans = Con.BeginTransaction();
                Cmd.Transaction = trans;

                Cmd.Parameters.AddWithValue("@TO_Vender", this.ddlVender.SelectedValue);
                Cmd.Parameters.AddWithValue("@TO_VenArea", this.ddlVenArea.SelectedValue);
                Cmd.Parameters.AddWithValue("@TO_Area", this.ddlVenOffice.SelectedValue);
                Cmd.Parameters.AddWithValue("@TO_Customer", this.ddlCustomer.SelectedValue);
                Cmd.Parameters.AddWithValue("@TO_Store", this.ddlStore.SelectedValue);
                Cmd.Parameters.AddWithValue("@TO_WorkGroup2", this.ddlWorkgroup2.SelectedValue);
                Cmd.Parameters.AddWithValue("@TO_GB", this.ddlGB.SelectedValue);
                Cmd.Parameters.AddWithValue("@HIS_Reason", this.txtTOSubmitReason.Text.ToString());
                Cmd.Parameters.AddWithValue("@SPT_RES_ID", this.Page.Request["RES_ID"].ToString());
                Cmd.Parameters.AddWithValue("@HIS_DueProcess", this.txtDueDate.Text.ToString());


                Cmd.Parameters.AddWithValue("@TO_ROUND_VALUES", dtList);


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
                Common.scriptAlert(this.Page, "저장되었습니다.", "m_To_Submit_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DataTable dtList = ViewState["DT_TO"] as DataTable;

        DataRow dr = dtList.NewRow();

        dr["TO_VENDER"] = this.ddlVender.SelectedValue;
        dr["TO_VENAREA"] = this.ddlVenArea.SelectedValue;
        dr["TO_VENOFFICE"] =this.ddlVenOffice.SelectedValue;
        dr["TO_CUSTOMER"] = ddlCustomer.SelectedValue;
        dr["TO_CUSTOMER_NAME"] = ddlCustomer.SelectedItem.Text;
        dr["TO_STORE"] = ddlStore.SelectedValue;
        dr["TO_STORE_NAME"] = ddlStore.SelectedItem.Text;
        dr["TO_WORKGROUP2"] =this.ddlWorkgroup2.SelectedValue;
        dr["TO_GB"] = this.ddlGB.SelectedValue;
        dr["TO_MONDAY"] = ddlMonday.SelectedValue;
        dr["TO_TUESDAY"] = ddlTuesday.SelectedValue;
        dr["TO_WEDNESDAY"] = ddlWednesday.SelectedValue;
        dr["TO_THURSDAY"] = ddlThursday.SelectedValue;
        dr["TO_FRIDAY"] = ddlFriday.SelectedValue;
        dr["TO_SATURDAY"] = ddlSaturday.SelectedValue;
        dr["TO_SUNDAY"] = ddlSunday.SelectedValue;
        dr["TO_REMARK"] = txtRemark.Text;

        if (dtList.Rows.Count <= 0)
            dr["IS_REPRESENT"] = true;
        else
            dr["IS_REPRESENT"] = false;

		/* 순회 및 격고의 종류가 늘어남에 소스 변경 2016-07-14 박병진*/
        if (ddlGB.SelectedValue.Contains("(격고") && dtList.Rows.Count >= 2)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('격고는 2개 이상의 매장 등록이 불가능합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else if (ddlGB.SelectedValue.Contains("(순회") && dtList.Rows.Count >= 30)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('순회는 최대 30개의 매장 등록만 가능합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else
        {
            DataRow[] findRows = dtList.Select(string.Format("TO_STORE = {0}", ddlStore.SelectedValue));

            if (findRows.Length <= 0)
            {
                dtList.Rows.Add(dr);
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('같은 매장이 등록되어 있습니다. 리스트에서 삭제하고 다시 시도하십시오.');</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
            }

            ViewState["DT_TO"] = dtList;

            gvList.DataSource = dtList;
            gvList.DataBind();

            ddlCustomer.SelectedValue = "";
            ddlStore.SelectedValue = "";
            txtRemark.Text = "";
            ddlMonday.SelectedValue = "";
            ddlTuesday.SelectedValue = "";
            ddlWednesday.SelectedValue = "";
            ddlThursday.SelectedValue = "";
            ddlFriday.SelectedValue = "";
            ddlSaturday.SelectedValue = "";
            ddlSunday.SelectedValue = "";
        }
    }


    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView row = (DataRowView)e.Row.DataItem;

        string morning = string.Empty;
        string afternoon = string.Empty;
        string wholeday = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                e.Row.Attributes["onClick"] = string.Format("fncDelete('{0}', '{1}', '{2}');", row["TO_STORE"].ToString(), row["TO_CUSTOMER_NAME"].ToString(), row["TO_STORE_NAME"].ToString());

                e.Row.Cells[0].Text = row["TO_CUSTOMER_NAME"].ToString();
                e.Row.Cells[1].Text = row["TO_STORE_NAME"].ToString();


                if (row["TO_MONDAY"].ToString().Equals("오전"))
                    morning += "월";
                if (row["TO_MONDAY"].ToString().Equals("오후"))
                    afternoon += "월";
                if (row["TO_MONDAY"].ToString().Equals("종일"))
                    wholeday += "월";

                if (row["TO_TUESDAY"].ToString().Equals("오전"))
                    morning += "화";
                if (row["TO_TUESDAY"].ToString().Equals("오후"))
                    afternoon += "화";
                if (row["TO_TUESDAY"].ToString().Equals("종일"))
                    wholeday += "화";

                if (row["TO_WEDNESDAY"].ToString().Equals("오전"))
                    morning += "수";
                if (row["TO_WEDNESDAY"].ToString().Equals("오후"))
                    afternoon += "수";
                if (row["TO_WEDNESDAY"].ToString().Equals("종일"))
                    wholeday += "수";

                if (row["TO_THURSDAY"].ToString().Equals("오전"))
                    morning += "목";
                if (row["TO_THURSDAY"].ToString().Equals("오후"))
                    afternoon += "목";
                if (row["TO_THURSDAY"].ToString().Equals("종일"))
                    wholeday += "목";

                if (row["TO_FRIDAY"].ToString().Equals("오전"))
                    morning += "금";
                if (row["TO_FRIDAY"].ToString().Equals("오후"))
                    afternoon += "금";
                if (row["TO_FRIDAY"].ToString().Equals("종일"))
                    wholeday += "금";

                if (row["TO_SATURDAY"].ToString().Equals("오전"))
                    morning += "토";
                if (row["TO_SATURDAY"].ToString().Equals("오후"))
                    afternoon += "토";
                if (row["TO_SATURDAY"].ToString().Equals("종일"))
                    wholeday += "토";

                if (row["TO_SUNDAY"].ToString().Equals("오전"))
                    morning += "일";
                if (row["TO_SUNDAY"].ToString().Equals("오후"))
                    afternoon += "일";
                if (row["TO_SUNDAY"].ToString().Equals("종일"))
                    wholeday += "일";

                e.Row.Cells[2].Text = GetCommaString(morning);
                e.Row.Cells[3].Text = GetCommaString(afternoon);
                e.Row.Cells[4].Text = GetCommaString(wholeday);

                e.Row.Attributes["style"] = "cursor: pointer;";
            }
        }
    }

    private string GetCommaString(string inputString)
    {
        for (int i = 0; i < inputString.Length; i++)
        {
            if (i % 2 != 0)
                inputString = inputString.Insert(i, ",");
        }

        return inputString;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DataTable dtList = ViewState["DT_TO"] as DataTable;
        string storeId = hdStoreId.Value;

        for (int i = 0; i < dtList.Rows.Count; i++)
        {
            if (storeId.Equals(dtList.Rows[i]["TO_STORE"].ToString()))
                dtList.Rows.RemoveAt(i);
        }

        dtList.AcceptChanges();
        ViewState["DT_TO"] = dtList;

        gvList.DataSource = dtList;
        gvList.DataBind();

    }

    protected void ddlGB_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

		/* 순회 및 격고의 종류가 늘어남에 소스 변경 2016-07-14 박병진*/
        if (!ddl.SelectedValue.Contains("순회(") && !ddl.SelectedValue.Contains("격고("))
            Response.Redirect("/TO/m_To_Submit.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString() + "&Type=" + ddl.SelectedValue);
    }

    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMonday.SelectedValue = "";
        ddlTuesday.SelectedValue = "";
        ddlWednesday.SelectedValue = "";
        ddlThursday.SelectedValue = "";
        ddlFriday.SelectedValue = "";
        ddlSaturday.SelectedValue = "";
        ddlSunday.SelectedValue = "";
    }

    protected void btnVisible_Click(object sender, EventArgs e)
    {
        if (pnlBasicInfo.Visible == true)
        {
            pnlBasicInfo.Visible = false;
            btnVisible.Text = "펼치기";
        }
        else
        {
            pnlBasicInfo.Visible = true;
            btnVisible.Text = "숨기기";
        }
    }
}