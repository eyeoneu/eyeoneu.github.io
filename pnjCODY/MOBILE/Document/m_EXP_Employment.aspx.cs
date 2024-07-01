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

public partial class m_EXP_Employment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetddlPresentation();
            SetPage();
            SetList();
        }
    }

    #region 제출처 DDL 바인딩
    private void SetddlPresentation()
    {
        DataSet ds = null;
        ds = Select_CodeList("17");

        ListItem firstItem = new ListItem("-선택-", "");
        ddlPresentation.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["COD_CD"].ToString().Length > 0)
            {
                string CodeName = dr["COD_Name"].ToString();
                string Code = dr["COD_CD"].ToString();
                ListItem tempItem = new ListItem(CodeName, Code);
                ddlPresentation.Items.Add(tempItem);
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

    // 발송구분 선택 시
    protected void ddlSend_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSend.SelectedValue == "우편")
            lblTitle.Text = "주소 :";
        else if (ddlSend.SelectedValue == "팩스")
            lblTitle.Text = "팩스번호 :";
        else if (ddlSend.SelectedValue == "직접수령")
            lblTitle.Text = "방문예정일 :";
        else
            lblTitle.Text = "";
    }


    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["EMP_ID"].ToString() != "0")
        {
            DataTable dt = Select_Detail();

            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Date"].ToString()))
                    txtDate.Text = Convert.ToDateTime(dt.Rows[0]["EMP_Date"].ToString()).ToShortDateString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_GB"].ToString()))
                    hdType.Value = dt.Rows[0]["EMP_GB"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Presentation"].ToString()))
                    ddlPresentation.SelectedValue = dt.Rows[0]["EMP_Presentation"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Send"].ToString()))
                    ddlSend.SelectedValue = dt.Rows[0]["EMP_Send"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Description"].ToString()))
                    txtDescription.Text = dt.Rows[0]["EMP_Description"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Rejecting_Text"].ToString()))
                    txtDeny.Text = dt.Rows[0]["EMP_Rejecting_Text"].ToString();

                if (string.IsNullOrEmpty(dt.Rows[0]["EMP_Confirm_Date"].ToString()) && string.IsNullOrEmpty(dt.Rows[0]["EMP_Rejecting_Date"].ToString()))
                {
                    trStatus.Visible = true;
                    txtStatus.Text = "요청";
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Confirm_Date"].ToString()))
                {
                    trStatus.Visible = true;
                    txtStatus.Text = "확인";
                    gvEmploymentList.Enabled = false;
                    txtDescription.ReadOnly = true;
                    btnSave.Visible = false;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["EMP_Rejecting_Date"].ToString()))
                {
                    trStatus.Visible = true;
                    txtStatus.Text = "반려";
                    gvEmploymentList.Enabled = false;
                    txtDescription.ReadOnly = true;
                    trDeny.Visible = true;
                    btnSave.Visible = false;
                }
            }
        }
        else if (this.Page.Request["RES_ID"] != null && this.Page.Request["EMP_ID"].ToString() == "0")
        {
            txtDate.Text = DateTime.Today.ToShortDateString();
        }
    }

    private DataTable Select_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_EMPLOYMENT_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@EMP_ID", this.Page.Request["EMP_ID"].ToString());

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

    private DataTable GetDT()
        {
            DataTable dtdataTable = new DataTable();

            DataColumn col = new DataColumn("SEQ", typeof(Int32));

            col.AutoIncrementSeed = 1;
            col.AutoIncrement = true;
            col.AutoIncrementStep = 1;
            col.Caption = "Value ID";
            dtdataTable.Columns.Add(col);

            DataColumn[] Keys = new DataColumn[2];
            Keys[0] = col;
            dtdataTable.PrimaryKey = Keys;

            col = new DataColumn("CONTENTS", typeof(string));
            dtdataTable.Columns.Add(col);

            //col = new DataColumn("Value", typeof(string));
            //col.MaxLength = 50;
            //dtdataTable.Columns.Add(col);

            //col = new DataColumn("DateTime", typeof(Int32));
            //dtdataTable.Columns.Add(col);

            ArrayList ar = new ArrayList();
            ar.Add(1);
            ar.Add("재직증명서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(2);
            ar.Add("근무기간확인서(계약직)");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(3);
            ar.Add("퇴직증명서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(4);
            ar.Add("경력증명서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(5);
            ar.Add("근로계약서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(6);
            ar.Add("원천징수영수증");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(7);
            ar.Add("원천징수부");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(8);
            ar.Add("갑근세납세필증명서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(9);
            ar.Add("급여명세표");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(10);
            ar.Add("퇴직금예상확인서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(11);
            ar.Add("일용근로소득지급명세서");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            ar.Clear();
            ar.Add(12);
            ar.Add("일용근로소득영수증");
            dtdataTable.LoadDataRow(ar.ToArray(), true);

            return dtdataTable;            
        } 

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            DataTable dt = GetDT();

            this.gvEmploymentList.DataSource = dt;
            this.gvEmploymentList.DataBind();     
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvEmploymentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            CheckBox chk;
            HiddenField hdfield;
            String[] arrType = this.hdType.Value.Split(';');

            for (int i = 0; i < arrType.Length; i++)
            {
                if (e.Row.Cells[1].Text == arrType[i].ToString())
                {
                    chk = (CheckBox)(e.Row.Cells[0].FindControl("cbCheck"));
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffcc00");
                    chk.Checked = true;
                }             
            }
        }
    }

    protected void gvEmploymentList_RowCreated1(object sender, GridViewRowEventArgs e)
    {

    }

    protected void cbCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        CheckBox chk;

        foreach (GridViewRow rowItem in gvEmploymentList.Rows)
        {
            if (chkAll.Checked == true)
            {
                chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));
                rowItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffcc00");
                chk.Checked = true;
            }
            else
            {
                chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));
                rowItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                chk.Checked = false;
            }
        }
    }
    
    protected void cbCheck_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk;

        foreach (GridViewRow rowItem in gvEmploymentList.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));
            if (chk.Checked == true)
                rowItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffcc00");
            else
                rowItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
        }
    }

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        HiddenField hdfield;
    
        int cnt = 0;
        string Temp = "";
        string Contents = "";

        foreach (GridViewRow rowItem in gvEmploymentList.Rows) // 체크된 신청항목을 가져온다
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));

            if (chk.Checked == true)
            {
                hdfield = (HiddenField)(rowItem.Cells[0].FindControl("hdType"));
                Temp += hdfield.Value.ToString() + ";";
                cnt++;
            }
        }

        // 마지막에 붙은 ; 하나 제거
        if (Temp.Length != 0)
            Temp = Temp.Substring(0, Temp.Length - 1);
        else
            Contents = null;

        // 중복 제거된 계정명 정보를 Contents에 담아둔다 (저장시 사용)
        Contents = Temp;

        if (cnt == 0)
        {
            Common.scriptAlert(this.Page, "신청항목을 선택하세요.");
        }

        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand CmdRemark = new SqlCommand("EPM_EMPLOYMENT_INSERT", Con);
            CmdRemark.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            CmdRemark.Transaction = trans;

            CmdRemark.Parameters.AddWithValue("@EMP_ID", this.Page.Request["EMP_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@RES_ID", int.Parse(Session["sRES_ID"].ToString()));
            CmdRemark.Parameters.AddWithValue("@RES_RBS", Session["sRES_RBS_CD"].ToString());
            CmdRemark.Parameters.AddWithValue("@RES_ASSAREA", Session["sRES_RBS_AREA_CD"].ToString());
            CmdRemark.Parameters.AddWithValue("@EMP_DATE", txtDate.Text);
            CmdRemark.Parameters.AddWithValue("@EMP_GB", Contents.ToString());
            CmdRemark.Parameters.AddWithValue("@EMP_PRESENTATION", ddlPresentation.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@EMP_SEND", ddlSend.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@EMP_DESCRIPTION", txtDescription.Text);

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_EXP_Employment_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
         
    }    
}