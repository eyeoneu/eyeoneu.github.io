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

public partial class m_To_Submit : System.Web.UI.Page
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

    #region 직급 선택 변경 이벤트 (이용현 추가 2015.07.27)
    protected void ddlWorkGroup2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        if (ddl.SelectedValue.ToString().Equals("640") || ddl.SelectedValue.ToString().Equals("641")) //계약직(월) / 계약직(행사)
        {
            this.divTOSelect.Visible = true;

            this.ddlRES_CON_TIME.Enabled = true;
            this.txtRES_CON_PAY.Enabled = true;

        }
        else
        {
            this.divTOSelect.Visible = false;

            this.ddlRES_CON_TIME.Enabled = false;
            this.txtRES_CON_PAY.Enabled = false;
        }


        // 선택항목 초기화
        this.ddlRES_CON_TIME.SelectedValue = "";
        this.txtRES_CON_PAY.Text = "";
    } 
    #endregion

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

            SqlCommand Cmd = new SqlCommand("EPM_TO_LIST_SUBMIT_MOBILE", Con);
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
            Cmd.Parameters.AddWithValue("@TO_TIME", this.ddlRES_CON_TIME.SelectedValue.ToString());
            Cmd.Parameters.AddWithValue("@TO_PAY", this.txtRES_CON_PAY.Text.ToString());

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
    protected void ddlGB_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

		/* 순회 및 격고의 종류가 늘어남에 소스 변경 2016-07-14 박병진*/

        if(ddl.SelectedValue.Contains("순회("))
            Response.Redirect("/TO/m_To_Submit2.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString() + "&Type=" + ddl.SelectedValue);

        if (ddl.SelectedValue.Contains("격고("))
            Response.Redirect("/TO/m_To_Submit2.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString() + "&Type=" + ddl.SelectedValue);
    }
}