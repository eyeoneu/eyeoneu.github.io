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

public partial class m_To_Change : System.Web.UI.Page
{
    int RBS_CD;
    int AREA_CD;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
            SetddlTO();
            txtDate.Text = DateTime.Today.ToShortDateString();
        }
    }   

    #region TO DDL 바인딩
    protected void SetddlTO()
    {
        ddlTO.Items.Clear();

        ddlTOType.Items.Clear();
        ddlTOAfter.Items.Clear();

        ddlTOType.Enabled = false;
        ddlTOAfter.Enabled = false;

        DataSet ds = null;
        ds = Select_TOCodeList("T", this.Page.Request["RES_ID"].ToString(), "");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["TO_Name"].ToString();
            string code = dr["TO_ID"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlTO.Items.Add(tempItem);
        }
    }
    #endregion

    #region TO변경항목 DDL 바인딩
    protected void SetddlTOType(object sender, EventArgs e)
    {
        ddlTOType.Items.Clear();
        ddlTOAfter.Items.Clear();

        ddlTOType.Enabled = true;
        ddlTOAfter.Enabled = false;

        if (ddlTO.SelectedValue == "0") // 구분: 공백값 선택시
        {
            ddlTOType.Enabled = false;
        }
        else
        {
            ListItem firstItem = new ListItem("-선택-", "");
            ddlTOType.Items.Add(firstItem);
            ddlTOType.Items.Add(new ListItem("TO삭제", "TO삭제"));
            //ddlTOType.Items.Add(new ListItem("TO이동", "TO이동"));
            //ddlTOType.Items.Add(new ListItem("TO증원", "TO증원"));
            ddlTOType.Items.Add(new ListItem("TO직급변경", "TO직급변경"));
        }
    }
    #endregion

    #region TO변경전, 변경후 DDL 바인딩
    protected void SetddlTOBefore(object sender, EventArgs e)
    {
        if (ddlTOType.SelectedValue == "") // 구분: 공백값 선택시
        {
            ddlTOAfter.Items.Clear();
            ddlTOAfter.Enabled = false;
            this.divAfter.Visible = true;
        }
        if (ddlTOType.SelectedValue == "TO삭제")
        {
            this.divAfter.Visible = false;
        }
        //if (ddlTOType.SelectedValue == "TO이동")
        //{
        //    this.divAfter.Visible = true;
        //    ddlTOAfter.Items.Clear();

        //    DataSet ds = null;
        //    ds = Select_TOCodeList("T", this.Page.Request["RES_ID"].ToString(), "");

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        string code_name = dr["TO_Name"].ToString();
        //        string code = dr["TO_ID"].ToString();

        //        ListItem tempItem = new ListItem(code_name, code);
        //        ddlTOAfter.Items.Add(tempItem);
        //    }
        //    ddlTOAfter.Enabled = true;
        //}
        if (ddlTOType.SelectedValue == "TO직급변경")
        {
            ddlTOAfter.Items.Clear();

            this.divAfter.Visible = true;

            DataSet wds = null;
            wds = Select_TOCodeList("W", this.Page.Request["RES_ID"].ToString(), ddlTO.SelectedValue.ToString());

            if (wds.Tables[0].Rows.Count != 0)
            {
                this.hdnWorkGroup2.Value = wds.Tables[0].Rows[0]["TO_WorkGroup2"].ToString();
            }

            DataSet ds = null;
            ds = Select_DZCodeList("G4");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string CodeName = dr["CTD_NM"].ToString();
                string Code = dr["CTD_CD"].ToString();

                ListItem tempItem = new ListItem(CodeName, Code);
                ddlTOAfter.Items.Add(tempItem);
            }
            ddlTOAfter.Enabled = true;
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

            Cmd.Parameters.AddWithValue("@TO_ID", this.ddlTO.SelectedValue.ToString());
            Cmd.Parameters.AddWithValue("@HIS_GB", "T");
            Cmd.Parameters.AddWithValue("@HIS_TYPE", this.ddlTOType.SelectedValue.ToString());
            Cmd.Parameters.AddWithValue("@HIS_Before", this.ddlTO.SelectedValue.ToString());
            if (this.ddlTOType.SelectedValue.ToString() == "TO삭제")
            {
                Cmd.Parameters.AddWithValue("@HIS_After", null);
            }
            else
                Cmd.Parameters.AddWithValue("@HIS_After", this.ddlTOAfter.SelectedValue.ToString());
            Cmd.Parameters.AddWithValue("@HIS_Reason", this.txtTOReason.Text.ToString());
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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_To_Change_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }
}