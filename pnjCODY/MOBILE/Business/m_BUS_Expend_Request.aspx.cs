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

public partial class MOBILE_Business_M_BUS_Expend_Request : System.Web.UI.Page
{
    string onlyNumberInsert;
    string UserGroup;
    string resID;
    string exeGB = "I";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dvResList.Visible = false;
            this.dvResInfo.Visible = false;
            //lblTitle.Text = "지출 신청서 > 신규 작성";

            //승인항목 선택
            SetddReqType();

            if (!string.IsNullOrEmpty(Request.QueryString["EXP_REQ_ID"]))
            {
                this.hdEXP_REQ_ID.Value = Request.QueryString["EXP_REQ_ID"];
                ViewState["REQ_ID"] = Request.QueryString["EXP_REQ_ID"];
                exeGB = "M";

                //신청서 내용
                SetReqData();
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["EXP_REQ_ID"]))
        {
            exeGB = "M";
        }
        
    }

    #region 항목 DDL 바인딩
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

        if (this.Page.Request["EXP_REQ_ID"] == null)
        {
            ddlReqType.Items.Remove(ddlReqType.Items.FindByValue("002"));
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

    protected void SetReqData()
    {
        DataSet dsData = null;

        dsData = Select_ExpendRequestData();
        
        if (dsData.Tables[0].Rows.Count != 0)
        {
            //txtSuporterRBS.Text = dsData.Tables[0].Rows[0]["RES_RBS_Name"].ToString();
            //txtSupporterArea.Text = dsData.Tables[0].Rows[0]["RES_AREA_Name"].ToString();
        }

        //승인항목
        for (int i = 0; i < ddlReqType.Items.Count; i++)
        {
            if (ddlReqType.Items[i].Value == dsData.Tables[0].Rows[0]["EXP_REQ_Type"].ToString().Trim())
            {
                ddlReqType.Items[i].Selected = true;

                ddlReqType_SelectedIndexChanged(new Object(), new EventArgs());

                break;
            }
        }
        
        //신청대상목록
        if (dsData.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
            {
                this.hdREQ_LIST.Value += dsData.Tables[1].Rows[i]["RES_NAME"].ToString() + "|"
                                       + dsData.Tables[1].Rows[i]["EXP_TAR_Amount"].ToString() + "|"
                                       + dsData.Tables[1].Rows[i]["EXP_TAR_Tax"].ToString() + "|"
                                       + dsData.Tables[1].Rows[i]["EXP_TAR_Memo"].ToString() + "|"
                                       + dsData.Tables[1].Rows[i]["EXP_TAR_RES_ID"].ToString() + "|"
                                       + dsData.Tables[1].Rows[i]["EXP_TAR_RES_GB"].ToString() + "|"
                                       + dsData.Tables[1].Rows[i]["EXP_TAR_RES_ASSCON_ID"].ToString() + ";";
            }

            //그리드 셋팅
            SetgvReqList();
        }

        //기타입력사항
        if (dsData.Tables[2].Rows.Count > 0)
        {
            for (int i = 0; i < dsData.Tables[2].Rows.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (hdnLine1.Value == dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString())
                        {
                            txtText1.Text = dsData.Tables[2].Rows[i]["EXP_REM_Contents"].ToString();
                            hdnLine1.Value = dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString();
                        }
                        break;
                    case 1:
                        if (hdnLine2.Value == dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString())
                        {
                            txtText2.Text = dsData.Tables[2].Rows[i]["EXP_REM_Contents"].ToString();
                            hdnLine2.Value = dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString();
                        }
                        break;
                    case 2:
                        if (hdnLine3.Value == dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString())
                        {
                            txtText3.Text = dsData.Tables[2].Rows[i]["EXP_REM_Contents"].ToString();
                            hdnLine3.Value = dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString();
                        }
                        break;
                    case 3:
                        if (hdnLine4.Value == dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString())
                        {
                            txtText4.Text = dsData.Tables[2].Rows[i]["EXP_REM_Contents"].ToString();
                            hdnLine4.Value = dsData.Tables[2].Rows[i]["EXP_REM_Name_CODE"].ToString();
                        }
                        break;
                }
            }
        }
        //승인날짜
        lblApproveDate.Text = string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["EXP_REQ_ApproveDate"].ToString()) ? "" : dsData.Tables[0].Rows[0]["EXP_REQ_ApproveDate"].ToString().Substring(0, 10);
        lblApproveNumber.Text = string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["EXP_REQ_ApproveNumber"].ToString()) ? "" : dsData.Tables[0].Rows[0]["EXP_REQ_ApproveNumber"].ToString();
        
        //상태에 따른 버튼 처리
        if (dsData.Tables[0].Rows[0]["EXP_REQ_ApproveCode"].ToString() == "100")//발의상태에서만 수정가능
        {
            //lblTitle.Text = "지출 신청서 > 내용 수정";
            this.dvResSearch.Visible = true;
            //this.dvResList.Visible = true;
            //this.dvResInfo.Visible = true;

            btnSave.Text = "수정";
            btnCancel.OnClientClick = "javascript:return window.location='m_BUS_Expend_Mng.aspx';";
        }
        else
        {
            //lblTitle.Text = "지출 신청서 > 내용 보기";
            this.dvResSearch.Visible = false;
            this.dvResList.Visible = false;
            this.dvResInfo.Visible = false;
            this.btnSave.Visible = false;
            this.gvReqList.Columns[7].HeaderStyle.Width = 0;//삭제 감추기

            btnSave.Enabled = false;
            btnCancel.OnClientClick = "javascript:return window.location='m_BUS_Expend_Mng.aspx';";
        }
        
    }

    protected DataSet Select_ExpendRequestData()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_BUS_EXP_SELECT_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@REQ_ID", ViewState["REQ_ID"]);

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
    // 검색 버튼 클릭
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetgvResList();
        this.dvResSearch.Visible = true;
        this.dvResList.Visible = true;
        this.dvResInfo.Visible = true;
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
            adp.SelectCommand.Parameters.AddWithValue("@WORKGROUP1", "012");

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

    protected void gvReqList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string RowIndex = (e.Row.RowIndex + 1).ToString();

        if (e.Row.RowIndex != -1)
        {
            //e.Row.Cells[0].Text = "<input id=\"txt1_" + RowIndex + "\" type=\"text\" value=\"" + ((DataRowView)e.Row.DataItem)["이름"].ToString() + "\" Class='textbox1' style='width:80%;' />";

            //e.Row.Cells[1].Text = "<input id=\"txt2_" + RowIndex + "\" type=\"text\" value=\"" + ((DataRowView)e.Row.DataItem)["승인금액"].ToString() + "\" Class='textbox1' style='width:80%;' />";

            //e.Row.Cells[2].Text = "<input id=\"txt3_" + RowIndex + "\" type=\"text\" value=\"" + ((DataRowView)e.Row.DataItem)["부가세"].ToString() + "\" Class='textbox1' style='width:80%;' />";

            //e.Row.Cells[3].Text = "<input id=\"txt4_" + RowIndex + "\" type=\"text\" value=\"" + ((DataRowView)e.Row.DataItem)["비고"].ToString() + "\" Class='textbox1' style='width:80%;' />";

            e.Row.Cells[7].Text = "<a style=\"cursor:hand;\" "
                                    + "OnClick=\"javascript:fncDelitem('" + e.Row.RowIndex.ToString() + "');\">X</a>";
        }
    }

    /// <summary>
    /// 지출 신청서 대상 선택 시
    /// </summary>
    protected void btnSelectRes_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ");

        if (rd.Read())
        {
            this.lblREQ_RES_ID.Text = rd["RES_Number"].ToString();
            this.lblRES_Name.Text = rd["RES_Name"].ToString();
            this.hdRES_NAME.Value = rd["RES_Name"].ToString();
            this.lblRES_RBS_NAME.Text = rd["RES_RBS_NAME"].ToString();
            this.lblRES_WorkGroup1_NAME.Text = rd["RES_WorkGroup1_NAME"].ToString();
            this.lblRES_RBS_AREA_NAMEE.Text = rd["RES_RBS_AREA_NAME"].ToString();
            this.lblRES_JoinDate.Text = DateTime.Parse(rd["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");
        }


        this.dvResList.Visible = false;
    }

    /// <summary>
    /// 입력 추가시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SetgvReqList();

        expAddInputClear();
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SetgvReqList();
    }

    protected void SetgvReqList()
    {
        DataTable grid_dt = new DataTable();
        DataRow dr = null;
        //for (int ci = 0; ci < gvReqList.Columns.Count; ci++)
        //{
        //    grid_dt.Columns.Add(new DataColumn(gvReqList.Columns[ci].HeaderText));
        //}
        grid_dt.Columns.Add(new DataColumn("이름", typeof(string)));
        grid_dt.Columns.Add(new DataColumn("승인금액", typeof(string)));
        grid_dt.Columns.Add(new DataColumn("부가세", typeof(string)));
        grid_dt.Columns.Add(new DataColumn("비고", typeof(string)));
        grid_dt.Columns.Add(new DataColumn("EXP_TAR_RES_ID", typeof(string)));
        grid_dt.Columns.Add(new DataColumn("GB", typeof(string)));
        grid_dt.Columns.Add(new DataColumn("ASSCON_ID", typeof(string)));


        string[] arrItem = null;
        string[] arrValue = null;
        string arrValue1, arrValue2, arrValue3, arrValue4, arrValue5, arrValue6, arrValue7 = null;

        arrItem = this.hdREQ_LIST.Value.ToString().Split(';');

        for (int i = 0; i < arrItem.Length - 1; i++)
        {
            dr = grid_dt.NewRow();

            arrValue = arrItem[i].Split('|');

            arrValue1 = arrValue[0].ToString();//이름
            arrValue2 = arrValue[1].ToString();//승인금액
            arrValue3 = arrValue[2].ToString();//부가세
            arrValue4 = arrValue[3].ToString();//비고
            arrValue5 = arrValue[4].ToString();//EXP_TAR_RES_ID
            arrValue6 = arrValue[5].ToString();//GB
            arrValue7 = arrValue[6].ToString();//ASSCON_ID

            dr["이름"] = arrValue1;
            dr["승인금액"] = arrValue2;
            dr["부가세"] = arrValue3;
            dr["비고"] = arrValue4;
            dr["EXP_TAR_RES_ID"] = arrValue5;
            dr["GB"] = arrValue6;
            dr["ASSCON_ID"] = arrValue7;

            grid_dt.Rows.Add(dr);

        }
        //grid_dt.AcceptChanges();

        this.gvReqList.DataSource = grid_dt;
        this.gvReqList.DataBind();
    }

    /// <summary>
    /// 신청사원 입력 초기화
    /// </summary>
    protected void expAddInputClear()
    {
        this.txtRES_Name.Text = "";
        this.hdRES_NAME.Value = "";
        this.hdRES_ID.Value = "";

        lblREQ_RES_ID.Text = "";
        lblRES_Name.Text = "";
        lblRES_RBS_AREA_NAMEE.Text = "";
        lblRES_WorkGroup1_NAME.Text = "";
        lblRES_RBS_NAME.Text = "";
        lblRES_JoinDate.Text = "";

        this.txtEXP_TAR_Amount.Text = "";
        this.txtEXP_TAR_Tax.Text = "";
        this.txtEXP_TAR_Memo.Text = "";

    }

    protected void ddlReqType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet _ds = null;

        _ds = Select_ExpendRequestOthersList();

        int rowCnt = 0;

        othersInputClear();

        if (_ds.Tables[0].Rows.Count > 0)
        {
            rowCnt = _ds.Tables[0].Rows.Count;

            ViewState["remarkCnt"] = rowCnt.ToString();

            for (int i = 0; i < rowCnt; i++)
            {
                switch (i)
                {
                    case 0:
                        seq1.Text = "1";
                        txtLine1.Text = _ds.Tables[0].Rows[i]["COD_Name"].ToString();
                        hdnLine1.Value = _ds.Tables[0].Rows[i]["COD_CD"].ToString();
                        break;
                    case 1:
                        seq2.Text = "2";
                        txtLine2.Text = _ds.Tables[0].Rows[i]["COD_Name"].ToString();
                        hdnLine2.Value = _ds.Tables[0].Rows[i]["COD_CD"].ToString();
                        break;
                    case 2:
                        seq3.Text = "3";
                        txtLine3.Text = _ds.Tables[0].Rows[i]["COD_Name"].ToString();
                        hdnLine3.Value = _ds.Tables[0].Rows[i]["COD_CD"].ToString();
                        break;
                    case 3:
                        seq4.Text = "4";
                        txtLine4.Text = _ds.Tables[0].Rows[i]["COD_Name"].ToString();
                        hdnLine4.Value = _ds.Tables[0].Rows[i]["COD_CD"].ToString();
                        break;
                }
            }

            for (int j = 3; j > _ds.Tables[0].Rows.Count - 1; j--)
            {
                switch (j)
                {
                    case 0:
                        txtText1.Visible = false;
                        break;
                    case 1:
                        txtText2.Visible = false;
                        break;
                    case 2:
                        txtText3.Visible = false;
                        break;
                    case 3:
                        txtText4.Visible = false;
                        break;
                }
            }
        }
    }

    protected void othersInputClear()
    {
        this.divRemark.Visible = true;

        seq1.Text = "";
        seq2.Text = "";
        seq3.Text = "";
        seq4.Text = "";
        seq5.Text = "";

        txtLine1.Text = "";
        txtLine2.Text = "";
        txtLine3.Text = "";
        txtLine4.Text = "";
        txtLine5.Text = "";

        hdnLine1.Value = "";
        hdnLine2.Value = "";
        hdnLine3.Value = "";
        hdnLine4.Value = "";
        hdnLine5.Value = "";

        txtText1.Text = "";
        txtText2.Text = "";
        txtText3.Text = "";
        txtText4.Text = "";
        txtText5.Text = "";

        txtText1.Visible = true;
        txtText2.Visible = true;
        txtText3.Visible = true;
        txtText4.Visible = true;
        txtText5.Visible = false;
    }

    #region EPM 코드리스트 조회
    private DataSet Select_ExpendRequestOthersList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CODE_OTHERINPUTLIST", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@COD_Remarks", ddlReqType.SelectedItem.Text.Trim());

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


    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            // 지출신청 마스터
            SqlCommand Cmd = new SqlCommand("EPM_BUS_EXP_SUBMIT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@EXE_GB", exeGB);  // I:삽입 , M:수정
            if (exeGB == "M")
                Cmd.Parameters.AddWithValue("@REQ_ID", int.Parse((string)ViewState["REQ_ID"]));

            Cmd.Parameters.AddWithValue("@REQ_TYPE", ddlReqType.SelectedItem.Value);
            Cmd.Parameters.AddWithValue("@REQ_WRITER_ID", int.Parse((string)Session["sRES_ID"]));

            if (exeGB == "I")
                ViewState["REQ_ID"] = Cmd.ExecuteScalar();
            else
                Cmd.ExecuteNonQuery();

            int reqId = 0;

            if (exeGB == "I")
            {
                reqId = Convert.ToInt32(ViewState["REQ_ID"]);
            }
            else if (exeGB == "M")
            {
                reqId = int.Parse((string)ViewState["REQ_ID"]);
            }

            // 지출신청 대상 (기존 아이템 삭제후 다시 저장)
            SqlCommand CmdTargetDel = new SqlCommand("EPM_BUS_EXP_TARGET_SUBMIT_MOBILE", Con);
            CmdTargetDel.CommandType = CommandType.StoredProcedure;

            CmdTargetDel.Transaction = trans;
            CmdTargetDel.Parameters.Clear();

            CmdTargetDel.Parameters.AddWithValue("@EXE_GB", "A");
            CmdTargetDel.Parameters.AddWithValue("@EXP_REQ_ID", reqId);
            CmdTargetDel.ExecuteNonQuery();

            string[] arrItem = null;
            string[] arrValue = null;
            string arrValue1, arrValue2, arrValue3, arrValue4, arrValue5, arrValue6, arrValue7 = null;

            arrItem = this.hdREQ_LIST.Value.ToString().Split(';');

            for (int i = 0; i < arrItem.Length - 1; i++)
            {
                arrValue = arrItem[i].Split('|');

                arrValue1 = arrValue[0].ToString();//이름
                arrValue2 = arrValue[1].ToString();//승인금액
                arrValue3 = arrValue[2].ToString();//부가세
                arrValue4 = arrValue[3].ToString();//비고
                arrValue5 = arrValue[4].ToString();//EXP_TAR_RES_ID
                arrValue6 = arrValue[5].ToString();//GB
                arrValue7 = arrValue[6].ToString();//ASSCON_ID
                if (string.IsNullOrEmpty(arrValue7))
                    arrValue7 = "0";

                SqlCommand CmdTarget = new SqlCommand("EPM_BUS_EXP_TARGET_SUBMIT_MOBILE", Con);
                CmdTarget.CommandType = CommandType.StoredProcedure;

                CmdTarget.Transaction = trans;

                CmdTarget.Parameters.Clear();

                CmdTarget.Parameters.AddWithValue("@EXE_GB", "I");
                CmdTarget.Parameters.AddWithValue("@EXP_REQ_ID", reqId);
                CmdTarget.Parameters.AddWithValue("@EXP_TAR_RES_ID", arrValue5);
                CmdTarget.Parameters.AddWithValue("@EXP_TAR_RES_GB", arrValue6);
                CmdTarget.Parameters.AddWithValue("@EXP_TAR_RES_ASSCON_ID", int.Parse(arrValue7));
                CmdTarget.Parameters.AddWithValue("@EXP_TAR_Amount", float.Parse(arrValue2));
                CmdTarget.Parameters.AddWithValue("@EXP_TAR_Tax", float.Parse(arrValue3));
                CmdTarget.Parameters.AddWithValue("@EXP_TAR_Memo", arrValue4);

                try
                {
                    CmdTarget.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    bError = true;
                    trans.Rollback();
                    Response.Write(ex.Message);
                    Response.End();
                }
            }

            // 기타사항 입력(기존 아이템 삭제후 다시 저장)
            SqlCommand CmdRemarkDel = new SqlCommand("EPM_BUS_EXP_REMARK_SUBMIT_MOBILE", Con);
            CmdRemarkDel.CommandType = CommandType.StoredProcedure;
            CmdRemarkDel.Transaction = trans;
            CmdRemarkDel.Parameters.Clear();
            CmdRemarkDel.Parameters.AddWithValue("@EXE_GB", "A");
            CmdRemarkDel.Parameters.AddWithValue("@EXP_REQ_ID", reqId);
            CmdRemarkDel.ExecuteNonQuery();


            SqlCommand CmdRemark = new SqlCommand("EPM_BUS_EXP_REMARK_SUBMIT_MOBILE", Con);
            CmdRemark.CommandType = CommandType.StoredProcedure;
            CmdRemark.Transaction = trans;

            int rowCnt = int.Parse((string)ViewState["remarkCnt"]);

            for (int i = 1; i <= rowCnt; i++)
            {
                CmdRemark.Parameters.Clear();

                string _code = "";
                string _text = "";

                switch (i)
                {
                    case 1:
                        _code = hdnLine1.Value.ToString();
                        _text = txtText1.Text.ToString();
                        break;
                    case 2:
                        _code = hdnLine2.Value.ToString();
                        _text = txtText2.Text.ToString();
                        break;
                    case 3:
                        _code = hdnLine3.Value.ToString();
                        _text = txtText3.Text.ToString();
                        break;
                    case 4:
                        _code = hdnLine4.Value.ToString();
                        _text = txtText4.Text.ToString();
                        break;
                }

                CmdRemark.Parameters.AddWithValue("@EXE_GB", "I");
                CmdRemark.Parameters.AddWithValue("@EXP_REQ_ID", reqId);
                CmdRemark.Parameters.AddWithValue("@EXP_REM_Name_CODE", _code);
                CmdRemark.Parameters.AddWithValue("@EXP_REM_Contents", _text);
                                
                try
                {
                    CmdRemark.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    bError = true;
                    trans.Rollback();
                    Response.Write(ex.Message);
                    Response.End();
                }
            }

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_BUS_Expend_Mng.aspx");
        }
    }
}
