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

public partial class m_To_Round_Change : System.Web.UI.Page
{
    int RBS_CD;
    int AREA_CD;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
            SetddlTO();
            SetddlCustomer();
            txtDate.Text = DateTime.Today.ToShortDateString();
        }
    }   

    #region TO DDL 바인딩
    protected void SetddlTO()
    {
        ddlTO.Items.Clear();

        DataSet ds = null;
        ds = Select_TOCodeList("R", this.Page.Request["RES_ID"].ToString(), "");

        bool isRequested = false;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (!string.IsNullOrEmpty(dr["IS_REQUESTED"].ToString()))   // 변경 요청 된 TO는 포함 안함
            {
                if (int.Parse(dr["IS_REQUESTED"].ToString()) > 0)
                    isRequested = true;
                else
                    isRequested = false;
            }
            else
                isRequested = false;

            string code_name = dr["TO_Name"].ToString();
            string code = dr["TO_Num"].ToString();

            if (!isRequested)
            {
                ListItem tempItem = new ListItem(code_name, code);
                ddlTO.Items.Add(tempItem);
            }
        }
    }
    #endregion
    
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
    private DataSet Select_DZCodeList(string ctrlcd)
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

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtList = ViewState["DT_STORES"] as DataTable;

        DataRow[] drNotDelete = dtList.Select(string.Format("TO_Status2 <> 'D' AND TO_Status2 <> 'X'"));




        if(dtList.Rows.Count <= 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('하나이상의 매장이 등록되어야 합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else if (dtList.Rows[0]["TO_GB"].ToString().Contains("격고(") && drNotDelete.Length != 2)
        {
        
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('격고는 2개의 매장이 등록 되어야 합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else if (dtList.Rows[0]["TO_GB"].ToString().Contains("순회(") && drNotDelete.Length < 3)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('순회는 최소 3개 이상의 매장을 등록해야 합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else
        {
            DataRow[] drChanged = dtList.Select(string.Format("TO_Status2 <> 'I' AND TO_Status2 <> 'X'"));

            if (drChanged.Length <= 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('변경된 사항이 없습니다.');</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
            }
            else
            {
                DataTable dt = dtList.Clone();

                foreach (DataRow d in drChanged)
                    dt.Rows.Add(d.ItemArray);

                bool bError = false;
                SqlConnection Con = null;
                SqlTransaction trans = null;

                try
                {
                    Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
                    Con.Open();

                    SqlCommand Cmd = new SqlCommand("EPM_TO_ROUND_HISTORY_SUBMIT_MOBILE", Con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    trans = Con.BeginTransaction();
                    Cmd.Transaction = trans;

                    Cmd.Parameters.AddWithValue("@TO_Num", this.ddlTO.SelectedValue.ToString());
                    Cmd.Parameters.AddWithValue("@HIS_GB", "R");
                    Cmd.Parameters.AddWithValue("@HIS_TYPE", "순회/격고매장변경");
                    Cmd.Parameters.AddWithValue("@HIS_Reason", this.txtTOReason.Text.ToString());
                    Cmd.Parameters.AddWithValue("@SPT_RES_ID", this.Page.Request["RES_ID"].ToString());
                    Cmd.Parameters.AddWithValue("@HIS_DueProcess", this.txtDueDate.Text.ToString());
                    Cmd.Parameters.AddWithValue("@ROUND_EDIT_VALUES", dt);

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
                    Common.scriptAlert(this.Page, "저장되었습니다.", "m_To_Round_Change_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
                }
            }
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
                e.Row.Attributes["onClick"] = string.Format("fncChange('{0}');", row["TO_Round_UID"].ToString());

                if (bool.Parse(row["IS_Represent"].ToString()) == true)
                    e.Row.Cells[0].Text = "* " +row["TO_CUSTOMER_NAME"].ToString();
                else
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

                if (!string.IsNullOrEmpty(row["TO_Status"].ToString()))
                {
                    if (row["TO_Status2"].ToString().Equals("D") || row["TO_Status2"].ToString().Equals("X"))
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    if (row["TO_Status"].ToString().Equals("N") && !row["TO_Status2"].ToString().Equals("X"))
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                    if (row["TO_Status"].ToString().Equals("I") && row["TO_Status2"].ToString().Equals("E"))
                        e.Row.ForeColor = System.Drawing.Color.Green;
                }
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

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DataTable dtList = ViewState["DT_STORES"] as DataTable;

        DataRow dr = dtList.NewRow();

        if (dtList.Rows.Count <= 0)
        {
            DataTable dtToInfo = ViewState["DT_TOiNFO"] as DataTable;

            dr["TO_Num"] = dtToInfo.Rows[0]["TO_Num"].ToString();
            dr["TO_Round_UID"] = Guid.NewGuid();
            dr["TO_VENDER"] = dtToInfo.Rows[0]["TO_Vender"].ToString();
            dr["TO_VENAREA"] = dtToInfo.Rows[0]["TO_VenArea"].ToString();
            dr["TO_VENOFFICE"] = dtToInfo.Rows[0]["TO_VenOffice"].ToString();
            dr["TO_CUSTOMER"] = ddlCustomer.SelectedValue;
            dr["TO_CUSTOMER_NAME"] = ddlCustomer.SelectedItem.Text;
            dr["TO_STORE"] = ddlStore.SelectedValue;
            dr["TO_STORE_NAME"] = ddlStore.SelectedItem.Text;
            dr["TO_WORKGROUP2"] = dtToInfo.Rows[0]["TO_WorkGroup2"].ToString();
            dr["TO_GB"] = dtToInfo.Rows[0]["TO_GB"].ToString();
            dr["TO_RBS"] = dtToInfo.Rows[0]["TO_RBS"].ToString();
            dr["TO_AssArea"] = dtToInfo.Rows[0]["TO_AssArea"].ToString();
            dr["TO_MONDAY"] = ddlMonday.SelectedValue;
            dr["TO_TUESDAY"] = ddlTuesday.SelectedValue;
            dr["TO_WEDNESDAY"] = ddlWednesday.SelectedValue;
            dr["TO_THURSDAY"] = ddlThursday.SelectedValue;
            dr["TO_FRIDAY"] = ddlFriday.SelectedValue;
            dr["TO_SATURDAY"] = ddlSaturday.SelectedValue;
            dr["TO_SUNDAY"] = ddlSunday.SelectedValue;
            dr["TO_REMARK"] = txtRemark.Text;
            dr["TO_Status"] = "N";
            dr["TO_Status2"] = "N";
            dr["IS_REPRESENT"] = true;

            ddlCustomer.Enabled = true;
            ddlStore.Enabled = true;
        }
        else
        {
            dr["TO_Num"] = dtList.Rows[0]["TO_Num"].ToString();
            dr["TO_Round_UID"] = Guid.NewGuid();
            dr["TO_VENDER"] = dtList.Rows[0]["TO_Vender"].ToString();
            dr["TO_VENAREA"] = dtList.Rows[0]["TO_VenArea"].ToString();
            dr["TO_VENOFFICE"] = dtList.Rows[0]["TO_VenOffice"].ToString();
            dr["TO_CUSTOMER"] = ddlCustomer.SelectedValue;
            dr["TO_CUSTOMER_NAME"] = ddlCustomer.SelectedItem.Text;
            dr["TO_STORE"] = ddlStore.SelectedValue;
            dr["TO_STORE_NAME"] = ddlStore.SelectedItem.Text;
            dr["TO_WORKGROUP2"] = dtList.Rows[0]["TO_WorkGroup2"].ToString();
            dr["TO_GB"] = dtList.Rows[0]["TO_GB"].ToString();
            dr["TO_RBS"] = dtList.Rows[0]["TO_RBS"].ToString();
            dr["TO_AssArea"] = dtList.Rows[0]["TO_AssArea"].ToString();
            dr["TO_MONDAY"] = ddlMonday.SelectedValue;
            dr["TO_TUESDAY"] = ddlTuesday.SelectedValue;
            dr["TO_WEDNESDAY"] = ddlWednesday.SelectedValue;
            dr["TO_THURSDAY"] = ddlThursday.SelectedValue;
            dr["TO_FRIDAY"] = ddlFriday.SelectedValue;
            dr["TO_SATURDAY"] = ddlSaturday.SelectedValue;
            dr["TO_SUNDAY"] = ddlSunday.SelectedValue;
            dr["TO_REMARK"] = txtRemark.Text;
            dr["TO_Status"] = "N";
            dr["TO_Status2"] = "N";
            dr["IS_REPRESENT"] = false;
        }
        DataRow[] drNotDelete = dtList.Select(string.Format("TO_Status2 <> 'D' AND TO_Status2 <> 'X'"));

        if (dtList.Rows.Count > 0 && dtList.Rows[0]["TO_GB"].ToString().Contains("격고(") && drNotDelete.Length >= 2)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('격고는 2개 이상의 매장 등록이 불가능합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else if (dtList.Rows.Count > 0 && dtList.Rows[0]["TO_GB"].ToString().Contains("순회(") && drNotDelete.Length >= 30)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('순회는 최대30개의 매장 등록만 가능합니다.');</script>");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
        }
        else
        {
            DataRow[] findRows = dtList.Select(string.Format("TO_STORE = {0}", ddlStore.SelectedValue));

            if (findRows.Length <= 0)
                dtList.Rows.Add(dr);
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('같은 매장이 등록되어 있습니다. 리스트에서 삭제하고 다시 시도하십시오.');</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
            }

            ViewState["DT_STORES"] = dtList;

            gvList.DataSource = dtList;
            gvList.DataBind();

            SetStoreInfoEmpty();

            DataRow[] drRepresent = dtList.Select(string.Format("TO_Status2 <> 'D' AND TO_Status2 <> 'X'"));
            int index = 0;
            ddlRepresent.Items.Clear();
            ddlRepresent.Items.Add(new ListItem("-대표 매장 선택-", ""));
            for (int i = 0; i < drRepresent.Length; i++)
            {
                ddlRepresent.Items.Add(new ListItem(drRepresent[i]["TO_STORE_NAME"].ToString(), drRepresent[i]["TO_STORE"].ToString()));

                if (bool.Parse(drRepresent[i]["IS_Represent"].ToString()) == true)
                    index = i + 1;
            }

            ddlRepresent.SelectedIndex = index;
        }
    }

    #region 매장 DDL 바인딩(TO)
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStoreBind();
    }
    #endregion

    private void ddlStoreBind()
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

    protected void ddlTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        if (!string.IsNullOrEmpty(ddl.SelectedValue.Trim()))
        {
            pnlStores.Visible = true;

            SetStoreInfoEmpty();

            DataTable dtList = SelectRoundStoreList(ddl.SelectedValue).Tables[0];
            DataTable dtToType = SelectRoundStoreList(ddl.SelectedValue).Tables[1];

            if (dtToType.Rows.Count > 0)
                lblRoundType.Text = dtToType.Rows[0]["TO_GB"].ToString();

            ViewState["DT_STORES"] = dtList;

            gvList.DataSource = dtList;
            gvList.DataBind();

            ViewState["DT_TOiNFO"] = SelectRoundStoreList(ddl.SelectedValue.ToString()).Tables[2];

            ddlWork.Visible = false;
            btnNew.Visible = false;
            btnOk.Visible = false;
            btnInsert.Visible = true;

            int index = 0;
            ddlRepresent.Items.Clear();
            ddlRepresent.Items.Add(new ListItem("-대표 매장 선택-", ""));
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                ddlRepresent.Items.Add(new ListItem(dtList.Rows[i]["TO_STORE_NAME"].ToString(), dtList.Rows[i]["TO_STORE"].ToString()));

                if (bool.Parse(dtList.Rows[i]["IS_REPRESENT"].ToString()) == true)
                    index = i + 1;
            }

            ddlRepresent.SelectedIndex = index;

            // 순회/격고 매장 정보가 없을 경우(신규 등록): TO_List에 등록된 매장 정보로 대표 매장을 무조건 등록하게 한다.
            DataTable dtToInfo = SelectRoundStoreList(ddl.SelectedValue).Tables[2];
            DataTable dtToRoundCnt = SelectRoundStoreList(ddl.SelectedValue).Tables[3];
            if (int.Parse(dtToRoundCnt.Rows[0]["TO_ROUND_CNT"].ToString()) == 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script>alert('저장된 순회/격고 매장 정보가 없습니다. 대표매장 정보를 먼저 입력해주세요.');</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());

                // 신규 등록
                SetStoreDdlEnable(true);
                SetStoreInfoEmpty();

                ddlWork.Visible = false;
                btnNew.Visible = false;
                btnOk.Visible = false;
                btnInsert.Visible = true;

                ddlCustomer.SelectedValue = dtToInfo.Rows[0]["TO_Customer"].ToString();
                ddlStoreBind();
                ddlStore.SelectedValue = dtToInfo.Rows[0]["TO_Store"].ToString();

                ddlCustomer.Enabled = false;
                ddlStore.Enabled = false;
            }
        }
        else
        {
            pnlStores.Visible = false;
        }
    }

    private void SetStoreInfoEmpty()
    {
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

    private DataSet SelectRoundStoreList(string toNum)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_ROUND_STORE_LIST", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@TO_NUM", toNum);

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

    protected void btnChange_Click(object sender, EventArgs e)
    {
        DataTable dtList = ViewState["DT_STORES"] as DataTable;

        DataRow[] dr = dtList.Select(string.Format("TO_Round_UID = '{0}'", hdRoundUid.Value));

        if(dr.Length > 0)
        {
            ddlCustomer.SelectedValue = dr[0]["TO_Customer"].ToString();
            ddlStoreBind();
            ddlStore.SelectedValue = dr[0]["TO_Store"].ToString();
            txtRemark.Text = dr[0]["TO_Remark"].ToString();

            ddlMonday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Monday"].ToString().Trim()) ? "" : dr[0]["TO_Monday"].ToString();
            ddlTuesday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Tuesday"].ToString().Trim()) ? "" : dr[0]["TO_Tuesday"].ToString();
            ddlWednesday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Wednesday"].ToString().Trim()) ? "" : dr[0]["TO_Wednesday"].ToString();
            ddlThursday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Thursday"].ToString().Trim()) ? "" : dr[0]["TO_Thursday"].ToString();
            ddlFriday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Friday"].ToString().Trim()) ? "" : dr[0]["TO_Friday"].ToString();
            ddlSaturday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Saturday"].ToString().Trim()) ? "" : dr[0]["TO_Saturday"].ToString();
            ddlSunday.SelectedValue = string.IsNullOrEmpty(dr[0]["TO_Sunday"].ToString().Trim()) ? "" : dr[0]["TO_Sunday"].ToString();

            ddlWork.Visible = true;
            btnNew.Visible = true;
            btnOk.Visible = true;
            btnInsert.Visible = false;

            ddlWork.SelectedValue = "";

            SetStoreDdlEnable(false);
        }
    }

    protected void ddlWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        if(ddl.SelectedValue.Equals("E"))
            SetStoreDdlEnable(true);
        else
            SetStoreDdlEnable(false);
    }

    private void SetStoreDdlEnable(bool isEnable)
    {

        ddlCustomer.Enabled = isEnable;
        ddlStore.Enabled = isEnable;

        // 대표매장일 경우: 거래처 및 매장 선택박스 비활성화
        DataTable dtList = ViewState["DT_STORES"] as DataTable;
        foreach (DataRow dr in dtList.Rows)
        {
            if (dr["TO_Round_UID"].ToString().Equals(hdRoundUid.Value))
            {
                if (bool.Parse(dr["IS_Represent"].ToString()) == true)
                {
                    ddlCustomer.Enabled = false;
                    ddlStore.Enabled = false;
                }
            }
        }

        txtRemark.Enabled = isEnable;

        ddlMonday.Enabled = isEnable;
        ddlTuesday.Enabled = isEnable;
        ddlWednesday.Enabled = isEnable;
        ddlThursday.Enabled = isEnable;
        ddlFriday.Enabled = isEnable;
        ddlSaturday.Enabled = isEnable;
        ddlSunday.Enabled = isEnable;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        DataTable dtList = ViewState["DT_STORES"] as DataTable;

        if(ddlWork.SelectedValue.Equals("D"))
        {
            foreach (DataRow dr in dtList.Rows)
            {
                if (dr["TO_Round_UID"].ToString().Equals(hdRoundUid.Value))
                {
                    if (bool.Parse(dr["IS_Represent"].ToString()) == true)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script>alert('대표매장은 삭제하실 수 없습니다.');</script>");
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
                    }
                    else
                    {
                        dr.BeginEdit();

                        if ((dr["TO_Status"].ToString().Equals("I") && dr["TO_Status2"].ToString().Equals("I"))         // 기존 항목 삭제
                            || (dr["TO_Status"].ToString().Equals("I") && dr["TO_Status2"].ToString().Equals("E")))     // 기존 항목 수정 후 삭제
                            dr["TO_Status2"] = "D";
                        else
                            dr["TO_Status2"] = "X";

                        dr.EndEdit();
                    }
                }
            }
        }
        else if (ddlWork.SelectedValue.Equals("E"))
        {
            foreach (DataRow dr in dtList.Rows)
            {
                if (dr["TO_Round_UID"].ToString().Equals(hdRoundUid.Value))
                {
                    DataRow[] findRows = dtList.Select(string.Format("TO_STORE = {0} AND TO_Round_UID <> '{1}'", ddlStore.SelectedValue, hdRoundUid.Value));

                    if (findRows.Length <= 0)
                    {
                        dr.BeginEdit();

                        dr["TO_CUSTOMER"] = ddlCustomer.SelectedValue;
                        dr["TO_CUSTOMER_NAME"] = ddlCustomer.SelectedItem.Text;
                        dr["TO_STORE"] = ddlStore.SelectedValue;
                        dr["TO_STORE_NAME"] = ddlStore.SelectedItem.Text;
                        dr["TO_MONDAY"] = ddlMonday.SelectedValue;
                        dr["TO_TUESDAY"] = ddlTuesday.SelectedValue;
                        dr["TO_WEDNESDAY"] = ddlWednesday.SelectedValue;
                        dr["TO_THURSDAY"] = ddlThursday.SelectedValue;
                        dr["TO_FRIDAY"] = ddlFriday.SelectedValue;
                        dr["TO_SATURDAY"] = ddlSaturday.SelectedValue;
                        dr["TO_SUNDAY"] = ddlSunday.SelectedValue;
                        dr["TO_REMARK"] = txtRemark.Text;

                        dr["TO_Status2"] = "E";

                        dr.EndEdit();
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script>alert('같은 매장이 등록되어 있습니다. 리스트에서 삭제하고 다시 시도하십시오.');</script>");
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
                    }
                }
            }
        }

        dtList.AcceptChanges();
        ViewState["DT_STORES"] = dtList;

        gvList.DataSource = dtList;
        gvList.DataBind();

        DataRow[] drRepresent = dtList.Select(string.Format("TO_Status2 <> 'D' AND TO_Status2 <> 'X'"));

        int index = 0;

        ddlRepresent.Items.Clear();
        ddlRepresent.Items.Add(new ListItem("-대표 매장 선택-", ""));
        for (int i = 0; i < drRepresent.Length; i++)
        {
            ddlRepresent.Items.Add(new ListItem(drRepresent[i]["TO_STORE_NAME"].ToString(), drRepresent[i]["TO_STORE"].ToString()));

            if (bool.Parse(drRepresent[i]["IS_Represent"].ToString()) == true)
                index = i + 1;
        }

        ddlRepresent.SelectedIndex = index;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        SetStoreDdlEnable(true);
        SetStoreInfoEmpty();

        ddlWork.Visible = false;
        btnNew.Visible = false;
        btnOk.Visible = false;
        btnInsert.Visible = true;
    }

    protected void ddlRepresent_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        DataTable dtList = ViewState["DT_STORES"] as DataTable;

        foreach (DataRow dr in dtList.Rows)
        {
            if (bool.Parse(dr["IS_Represent"].ToString()) == true)
            {
                if (!dr["TO_Status2"].ToString().Equals("D") && !dr["TO_Status2"].ToString().Equals("X"))
                {
                    dr.BeginEdit();
                    dr["IS_Represent"] = false;
                    dr["TO_Status2"] = "E";
                    dr.EndEdit();
                }
            }
        }

        foreach (DataRow dr in dtList.Rows)
        {
            if (dr["TO_Store"].ToString().Equals(ddl.SelectedValue))
            {
                dr.BeginEdit();
                dr["IS_Represent"] = true;
                dr["TO_Status2"] = "E";
                dr.EndEdit();
            }
        }
        
        dtList.AcceptChanges();

        ViewState["DT_STORES"] = dtList;
        gvList.DataSource = dtList;
        gvList.DataBind();
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
