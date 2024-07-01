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
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;

public partial class m_BUS_Join_Confirm_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Page.Request["APP_ID"].ToString()))
            ViewState["APP_ID"] = this.Page.Request["APP_ID"].ToString();

        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            SetPage();
        }
    }


    // 드롭다운 리스트 바인딩
    private void SetControl()
    {     
		Code code = new Code();
		        
		// 퇴사 사유
        DataSet dsReason = code.EPM_CODE("13");
        this.ddlRES_CAR_RETIRE.DataSource = dsReason;
        this.ddlRES_CAR_RETIRE.DataBind();
        
        // 지원사
        DataSet dsVender = code.EPM_CODE("5");
        this.ddlVender.DataSource = dsVender;
        this.ddlVender.DataBind();
        
        // 거래처
        SetddlCustomer();
        
        // 직종
        DataSet dsRES_WorkGroup1 = code.DZICUBE_CODE("JC");
        this.ddlRES_WorkGroup1.DataSource = dsRES_WorkGroup1;
        this.ddlRES_WorkGroup1.DataBind();
    }

    private void SetddlCustomer()
    {
        DataSet ds = null;
        ds = Select_StoreList("", "");

        //ListItem firstItem = new ListItem("-선택-", "");
        //ddlCustomer.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["COD_NM"].ToString();
            string Code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(CodeName, Code);
            ddlCustomer.Items.Add(tempItem);
        }
    }

	// 거래처 선택 시
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
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

	// 매장 목록 조회
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

	// 직급 변경 시
    protected void ddlRES_WorkGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlRES_WorkGroup2.Items.Clear();

        Code code = new Code();
        DataSet ds = code.DZICUBE_CODE_BY_WORKGROUP1(this.ddlRES_WorkGroup1.SelectedValue.ToString());

        this.ddlRES_WorkGroup2.DataSource = ds;
        this.ddlRES_WorkGroup2.DataBind();
    }


	// 과거근무이력 확인 버튼 클릭 시
    protected void btnCheck_Click(object sender, EventArgs e)
    { 
        this.gvResList.DataSource = Select_WorkHistoryList();
        this.gvResList.DataBind();
        this.dvResList.Visible = true;
        this.lblStore.Text = "";
    }

    private DataSet Select_WorkHistoryList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_WORK_HISTORY_SELECT_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@RES_Name", this.txtRES_Name.Text);
        adp.SelectCommand.Parameters.AddWithValue("@RES_PersonNumber", this.txtRES_PersonNumber1.Text + "-" + this.txtRES_PersonNumber2.Text);

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
            e.Row.Attributes["onClick"] = "fncSelectRes('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["Vender"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["Customer"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["Store"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["RES_WorkGroup1"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["RES_WorkGroup2"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["RES_JoinDate"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["RES_RetireDate"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

	// 과거근무이력 선택 시
    protected void btnSelectHistory_Click(object sender, EventArgs e)
    {
		this.lblStore.Text = "("+ this.hdCustomer.Value + " " + this.hdStore.Value + ")";
        this.dvResList.Visible = false;
    }


    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
		SetControl();
		
        if (this.Page.Request["APP_ID"].ToString() != "0")
        {
            DataSet ds = Select_APP_Detail();

            if (ds.Tables[0].Rows.Count > 0)
            {
				Code code = new Code();
				
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Name"].ToString()))
                    txtRES_Name.Text = ds.Tables[0].Rows[0]["APP_Name"].ToString();
				
				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_PersonNumber1"].ToString()))
                    txtRES_PersonNumber1.Text = ds.Tables[0].Rows[0]["APP_PersonNumber1"].ToString();
				
				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_PersonNumber2"].ToString()))
                    txtRES_PersonNumber2.Text = ds.Tables[0].Rows[0]["APP_PersonNumber2"].ToString();
				
				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_History_Store_DP"].ToString()))
                    lblStore.Text = ds.Tables[0].Rows[0]["APP_History_Store_DP"].ToString();
				
				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_History_Reason"].ToString()))
                    ddlRES_CAR_RETIRE.SelectedValue = ds.Tables[0].Rows[0]["APP_History_Reason"].ToString();
                
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Vender"].ToString()))
                    ddlVender.SelectedValue = ds.Tables[0].Rows[0]["APP_Vender"].ToString();
                
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Customer"].ToString()))
                {
                    ddlCustomer.SelectedValue = ds.Tables[0].Rows[0]["APP_Customer"].ToString();
					ddlStore.Items.Clear();
					ddlStore.Enabled = true;
					
					foreach (DataRow dr in Select_StoreList("CUS", ddlCustomer.SelectedValue).Tables[0].Rows)
					{
						string code_name = dr["COD_NM"].ToString();
						string code_cd = dr["COD_CD"].ToString();

						ListItem tempItem = new ListItem(code_name, code_cd);
						ddlStore.Items.Add(tempItem);
					}
                }
                
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_StoreID"].ToString()))
                    ddlStore.SelectedValue = ds.Tables[0].Rows[0]["APP_StoreID"].ToString();
                
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_WorkGroup1"].ToString()))
                {
                    ddlRES_WorkGroup1.SelectedValue = ds.Tables[0].Rows[0]["APP_WorkGroup1"].ToString();
					this.ddlRES_WorkGroup2.Items.Clear();
					this.ddlRES_WorkGroup2.DataSource = code.DZICUBE_CODE_BY_WORKGROUP1(this.ddlRES_WorkGroup1.SelectedValue.ToString());;
					this.ddlRES_WorkGroup2.DataBind();
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_WorkGroup2"].ToString()))
                    ddlRES_WorkGroup2.SelectedValue = ds.Tables[0].Rows[0]["APP_WorkGroup2"].ToString();
				
				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Duedate_Join"].ToString()))
				{
                    txtDuedate_Join.Text = ds.Tables[0].Rows[0]["APP_Duedate_Join"].ToString();
					txtDuedate_Join.Attributes["class"] = "i_f_out";
                }
                    
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Duedate_Retire"].ToString()))
                {
                    txtDuedate_Retire.Text = ds.Tables[0].Rows[0]["APP_Duedate_Retire"].ToString();                           
					txtDuedate_Retire.Attributes["class"] = "i_f_out";
                }      

				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Remarks"].ToString()))
                    txtRemarks.Text = ds.Tables[0].Rows[0]["APP_Remarks"].ToString();
                    
                // 과거 근무 이력 기본정보 수정 제한    
				txtRES_Name.Enabled = false;
				txtRES_PersonNumber1.Enabled = false;
				txtRES_PersonNumber2.Enabled = false;
				this.btnCheck.Visible = false; 

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Status"].ToString()))
                {
					// 승인 상태일 경우 서포터 수정 기능 제한
		            if (ds.Tables[0].Rows[0]["APP_STATUS"].ToString().Trim().Equals("A"))
                    {
                        this.btnSave.Visible = false;
                        
                        ddlRES_CAR_RETIRE.Enabled = false;
                        ddlVender.Enabled = false;
                        ddlCustomer.Enabled = false;
                        ddlStore.Enabled = false;
                        ddlRES_WorkGroup1.Enabled = false;
                        ddlRES_WorkGroup2.Enabled = false;
                        txtDuedate_Join.Enabled = false;
                        txtDuedate_Retire.Enabled = false;
                        txtRemarks.Enabled = false;
                    }
                }
            }
        }
    }

    private DataSet Select_APP_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_APPROVAL_JOIN_CONFIRM_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@APP_ID", this.Page.Request["APP_ID"].ToString());

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

            SqlCommand Cmd = new SqlCommand("EPM_APPROVAL_JOIN_CONFIRM_INSERT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

			Cmd.Parameters.AddWithValue("@APP_ID", this.Page.Request["APP_ID"].ToString());
			Cmd.Parameters.AddWithValue("@APP_SUPPORTER_ID", Session["sRES_ID"].ToString());
			Cmd.Parameters.AddWithValue("@APP_PersonNumber", this.txtRES_PersonNumber1.Text + "-" + this.txtRES_PersonNumber2.Text);
			Cmd.Parameters.AddWithValue("@APP_Name", this.txtRES_Name.Text);
			Cmd.Parameters.AddWithValue("@APP_Vender", this.ddlVender.SelectedValue);
			Cmd.Parameters.AddWithValue("@APP_Customer", this.ddlCustomer.SelectedValue);
			Cmd.Parameters.AddWithValue("@APP_StoreID", this.ddlStore.SelectedValue);
			Cmd.Parameters.AddWithValue("@APP_WorkGroup1", this.ddlRES_WorkGroup1.SelectedValue);
			Cmd.Parameters.AddWithValue("@APP_WorkGroup2", this.ddlRES_WorkGroup2.SelectedValue);
			Cmd.Parameters.AddWithValue("@APP_Duedate_Join", this.txtDuedate_Join.Text);
			Cmd.Parameters.AddWithValue("@APP_Duedate_Retire", string.IsNullOrEmpty(txtDuedate_Retire.Text) ? DBNull.Value : (object) txtDuedate_Retire.Text);
			Cmd.Parameters.AddWithValue("@APP_Remarks", this.txtRemarks.Text);
			Cmd.Parameters.AddWithValue("@APP_History_Res_ID", this.hdRES_ID.Value);
			Cmd.Parameters.AddWithValue("@APP_History_Vender", this.hdVender.Value);
			Cmd.Parameters.AddWithValue("@APP_History_Customer", this.hdCustomer.Value);
			Cmd.Parameters.AddWithValue("@APP_History_Store", this.hdStore.Value);
			Cmd.Parameters.AddWithValue("@APP_History_RES_WorkGroup1", this.hdRES_WorkGroup1.Value);
			Cmd.Parameters.AddWithValue("@APP_History_RES_WorkGroup2", this.hdRES_WorkGroup2.Value);
			Cmd.Parameters.AddWithValue("@APP_History_RES_JoinDate", this.hdRES_JoinDate.Value);
			Cmd.Parameters.AddWithValue("@APP_History_RES_RetireDate", this.hdRES_RetireDate.Value);
			Cmd.Parameters.AddWithValue("@APP_History_Reason", this.ddlRES_CAR_RETIRE.SelectedValue);

            if (this.Page.Request["APP_ID"].ToString() == "0")
                ViewState["APP_ID"] = Cmd.ExecuteScalar();
            else
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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_BUS_Join_Confirm_List.aspx");
        }
    }
}