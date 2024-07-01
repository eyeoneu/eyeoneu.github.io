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
using System.IO;

public partial class m_Etc_Change : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {                   
            txtDate.Text = DateTime.Today.ToShortDateString();
        }
    }


    protected void ddlEtcType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        if (ddl.SelectedValue.ToString().Equals("계좌번호변경"))
        {
            this.bank.Visible = true;
			this.dvResSearch.Visible = true;
			this.lbTitle.Text = "계좌사진첨부";
        }
        else if (ddl.SelectedValue.ToString().Equals("개명신청"))
        {
            this.bank.Visible = true;
			this.dvResSearch.Visible = true;
			this.lbTitle.Text = "초본사진첨부";
        }
        else
        {
            this.bank.Visible = false;
            this.dvResSearch.Visible = true; // 사원정보 선택 정보 반영 (2016-01-28, 정창화)
		}
    }

	// 검색 버튼 클릭
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.hdnResName.Value = "";
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
										+ ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "');";

			e.Row.Attributes["style"] = "cursor: pointer;";
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
			this.txtRES_Name.Text = dsList.Tables[0].Rows[0]["RES_NAME"].ToString() + "(" + dsList.Tables[0].Rows[0]["RES_NUMBER"].ToString() + ")";
			this.hdnResName.Value = dsList.Tables[0].Rows[0]["RES_ID"].ToString();
			//this.txtEtcContent.Text = dsList.Tables[0].Rows[0]["RES_NAME"].ToString() + "(" + dsList.Tables[0].Rows[0]["RES_NUMBER"].ToString() + ")";
		}

		this.dvResList.Visible = false;
	}

	#region 사원정보 조회
	private DataSet SelectAC_List()
	{
		SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
		SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_DETAIL_SELECT_MOBILE", Con);
		adp.SelectCommand.CommandType = CommandType.StoredProcedure;

		adp.SelectCommand.Parameters.AddWithValue("@MODE", "REQ");
		adp.SelectCommand.Parameters.AddWithValue("@RES_ID", int.Parse(this.hdnResName.Value.ToString()));

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

            Cmd.Parameters.AddWithValue("@HIS_GB", "E");
            Cmd.Parameters.AddWithValue("@HIS_TYPE", this.ddlEtcType.SelectedValue.ToString());
            Cmd.Parameters.AddWithValue("@HIS_Text", this.txtEtcContent.Text.ToString());
            Cmd.Parameters.AddWithValue("@HIS_Reason", this.txtEtcReason.Text.ToString());
            Cmd.Parameters.AddWithValue("@SPT_RES_ID", this.Page.Request["RES_ID"].ToString());
            Cmd.Parameters.AddWithValue("@HIS_DueProcess", this.txtDueDate.Text.ToString());
			Cmd.Parameters.AddWithValue("@AC_RES_ID", this.hdnResName.Value.ToString());

			if (this.ddlEtcType.SelectedValue.ToString() == "계좌번호변경" || this.ddlEtcType.SelectedValue.ToString() == "개명신청" )
            {
                ViewState["HIS_ID"] = Cmd.ExecuteScalar();

                if (Request.Files != null)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile postedFile = Request.Files[i] as HttpPostedFile;

                        if (!string.IsNullOrEmpty(postedFile.FileName))
                        {
                            Stream fs = postedFile.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                            //Response.Write(ViewState["HIS_ID"] + " || " + Path.GetFileName(postedFile.FileName) + " || " + Path.GetExtension(postedFile.FileName) + " || " + bytes);

                            SqlCommand cmd2 = new SqlCommand("EPM_TO_HISTORY_FILE_SUBMIT_MOBILE", Con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Transaction = trans;

                            cmd2.Parameters.AddWithValue("@HIS_ID", ViewState["HIS_ID"]);
                            cmd2.Parameters.AddWithValue("@HIS_GB", "E");
                            cmd2.Parameters.AddWithValue("@FILE_NAME", Path.GetFileName(postedFile.FileName));
                            cmd2.Parameters.AddWithValue("@FILE_EXT", Path.GetExtension(postedFile.FileName));
                            cmd2.Parameters.AddWithValue("@FILE_INFO", bytes);

                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }
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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_Etc_Change_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }

}