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

public partial class Attendance_m_ATT_Closed_Request : System.Web.UI.Page
{
    string exeGB = "I";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            ExpendReq.Visible = false;
            SetControl();
            SetPage();
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        // 승인항목
        Code code = new Code();
        DataSet dsREQ_TYPE = code.EPM_CODE("9");

        this.ddlREQ_TYPE.DataSource = dsREQ_TYPE;
        this.ddlREQ_TYPE.DataBind();       
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["ATT_REQ_ID"] != null)
        {
            this.btnSave.Text = "수정";

            Attendance attendance = new Attendance();

            SqlDataReader rd = attendance.EPM_ATT_REQ_SELECT(int.Parse(this.Page.Request["ATT_REQ_ID"].ToString()));

            if (rd.Read())
            {
                this.ddlREQ_TYPE.SelectedValue = rd["ATT_REQ_Type"].ToString();

                this.hdRES_ID.Value = rd["REQ_RES_ID"].ToString();
                this.lblREQ_RES_ID.Text = rd["REQ_RES_NUMBER"].ToString();
                this.lblRES_Name.Text = rd["REQ_RES_NAME"].ToString();

                this.lblRES_RBS_NAME.Text = rd["REQ_RES_RBS"].ToString();
                this.lblRES_WorkGroup1_NAME.Text = rd["REQ_RES_WORKGROUP1"].ToString();
                this.lblRES_RBS_AREA_NAMEE.Text = rd["REQ_RES_AREA"].ToString();
                this.lblRES_JoinDate.Text = DateTime.Parse(rd["REQ_RES_JOIN"].ToString()).ToString("yyyy-MM-dd");

                this.lblRES_Vender.Text = rd["REQ_RES_VENDER"].ToString();
                this.lblRES_Store.Text = rd["REQ_RES_STORE"].ToString();

                this.txtATT_REQ_StartDate.CssClass = "i_f_out";
                this.txtATT_REQ_FinishDate.CssClass = "i_f_out";
                this.txtATT_REQ_StartDate.Text = rd["ATT_REQ_StartDate"].ToString();
                if(rd["ATT_REQ_FinishDate"].ToString() != "")
                    this.txtATT_REQ_FinishDate.Text = rd["ATT_REQ_FinishDate"].ToString();
                else
                    this.txtATT_REQ_FinishDate.CssClass = "i_f_out2";

                this.lblATT_REQ_Duration.Text = rd["ATT_REQ_Duration"].ToString();
                this.hdATT_REQ_Duration.Value = rd["ATT_REQ_Duration"].ToString();

                this.txtATT_REQ_Tel.Text = rd["ATT_REQ_Tel"].ToString();
                this.txtATT_REQ_Reason.Text = rd["ATT_REQ_Reason"].ToString();
                this.txtATT_REQ_Attatch.Text = rd["ATT_REQ_Attatch"].ToString();
                this.txtATT_REQ_Delay.Text = rd["ATT_REQ_Delay"].ToString();

                // 팀장 승인 이후 단계 일 경우
                if (rd["ATT_REQ_Approve1"].ToString() != "")
                {
                    this.ddlREQ_TYPE.Enabled = false;
                    this.dvResSearch.Visible = false;
                    this.btnSave.Enabled = false;
                    this.txtATT_REQ_StartDate.Enabled = false;
                    this.txtATT_REQ_FinishDate.Enabled = false;
                    this.txtATT_REQ_Tel.Enabled = false;
                    this.txtATT_REQ_Reason.Enabled = false;
                    this.txtATT_REQ_Attatch.Enabled = false;
                    this.txtATT_REQ_Delay.Enabled = false;
                }
            }

            rd.Close();
        }
    }

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
            Resource resource = new Resource();
            DataSet ds = resource.EPM_RES_LIST_MOBILE(this.txtRES_Name.Text.ToString(), "005", "", "");

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
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    /// <summary>
    /// 휴무 신청서 대상 선택 시
    /// </summary>
    protected void btnSelectRes_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        DataSet ds = new DataSet();

        ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ", "Table");

        if (ds.Tables[1].Rows.Count > 1)
        {
            this.dvResSubList.Visible = true;

            gvResSubList.DataSource = ds.Tables[1];
            gvResSubList.DataBind();
        }
        else
        {
            this.lblREQ_RES_ID.Text = ds.Tables[0].Rows[0]["RES_NUMBER"].ToString();
            this.lblRES_Name.Text = ds.Tables[0].Rows[0]["RES_Name"].ToString();
            this.lblRES_RBS_NAME.Text = ds.Tables[0].Rows[0]["RES_RBS_NAME"].ToString();
            this.lblRES_WorkGroup1_NAME.Text = ds.Tables[0].Rows[0]["RES_WorkGroup1_NAME"].ToString();
            this.lblRES_RBS_AREA_NAMEE.Text = ds.Tables[0].Rows[0]["RES_RBS_AREA_NAME"].ToString();
            this.lblRES_JoinDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");

            this.txtATT_REQ_Tel.Text = ds.Tables[0].Rows[0]["RES_CP"].ToString();

            /* 기본적으로 배정정를 넣을 수 있도록 배정으로 정렬된 데이터의 첫뻔체 값을 넣는다 2016-10-28 박병진*/
            this.lblRES_Vender.Text = ds.Tables[1].Rows[0]["RES_Vender"].ToString();
            this.lblRES_Store.Text = ds.Tables[1].Rows[0]["RES_Store"].ToString();
            this.hdnGB.Value = ds.Tables[1].Rows[0]["GB"].ToString();
            this.hdnASSCONID.Value = ds.Tables[1].Rows[0]["ASSCON_ID"].ToString();


            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    this.lblRES_Vender.Text = dr["RES_Vender"].ToString();
            //    this.lblRES_Store.Text = dr["RES_Store"].ToString();
            //    this.hdnGB.Value = dr["GB"].ToString();
            //    this.hdnASSCONID.Value = dr["ASSCON_ID"].ToString();
            //}
        }


        //SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ");

        //if (rd.Read())
        //{
        //    this.lblREQ_RES_ID.Text = rd["RES_ID"].ToString();
        //    this.lblRES_Name.Text = rd["RES_Name"].ToString();
        //    this.lblRES_RBS_NAME.Text = rd["RES_RBS_NAME"].ToString();
        //    this.lblRES_WorkGroup1_NAME.Text = rd["RES_WorkGroup1_NAME"].ToString();
        //    this.lblRES_RBS_AREA_NAMEE.Text = rd["RES_RBS_AREA_NAME"].ToString();
        //    this.lblRES_JoinDate.Text = DateTime.Parse(rd["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");
        //}

        this.dvResList.Visible = false;
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Attendance attendance = new Attendance();


        try
        {
            if (this.txtATT_REQ_FinishDate.Text.ToString() == "YYYYMMDD")
                this.txtATT_REQ_FinishDate.Text = "";

            // 삽입
            if (this.Page.Request["ATT_REQ_ID"] == null)
            {
                attendance.EPM_ATT_REQ_SUBMIT
                                                        ("",
                                                        int.Parse(Session["sRES_ID"].ToString()),
                                                        this.ddlREQ_TYPE.SelectedValue.ToString(),
                                                        int.Parse(this.hdRES_ID.Value.ToString()),
                                                        this.txtATT_REQ_StartDate.Text.ToString(),
                                                        this.txtATT_REQ_FinishDate.Text.ToString(),
                                                        this.hdATT_REQ_Duration.Value.ToString(),
                                                        this.txtATT_REQ_Tel.Text.ToString(),
                                                        this.txtATT_REQ_Reason.Text.ToString(),
                                                        this.txtATT_REQ_Attatch.Text.ToString(),
                                                        this.txtATT_REQ_Delay.Text.ToString(),
                                                        this.hdnGB.Value.ToString(),
                                                        this.hdnASSCONID.Value.ToString()
                                                        );
            }
            // 수정
            else
            {
                attendance.EPM_ATT_REQ_SUBMIT
                                                        ("",
                                                        int.Parse(this.Page.Request["ATT_REQ_ID"].ToString()),
                                                        int.Parse(Session["sRES_ID"].ToString()),
                                                        this.ddlREQ_TYPE.SelectedValue.ToString(),
                                                        int.Parse(this.hdRES_ID.Value.ToString()),
                                                        this.txtATT_REQ_StartDate.Text.ToString(),
                                                        this.txtATT_REQ_FinishDate.Text.ToString(),
                                                        this.hdATT_REQ_Duration.Value.ToString(),
                                                        this.txtATT_REQ_Tel.Text.ToString(),
                                                        this.txtATT_REQ_Reason.Text.ToString(),
                                                        this.txtATT_REQ_Attatch.Text.ToString(),
                                                        this.txtATT_REQ_Delay.Text.ToString(),
                                                        this.hdnGB.Value.ToString(),
                                                        this.hdnASSCONID.Value.ToString()
                                                        );
            }

            if (ddlREQ_TYPE.SelectedValue == "010" && this.Page.Request["ATT_REQ_ID"] == null)
            {
                Expend_Req_Save();
            }

            Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Closed_Mng.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    protected void gvResSubList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncSelectResSub('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "', '"
                                        + ((DataRowView)e.Row.DataItem)["GB"].ToString() + "', '"
                                        + ((DataRowView)e.Row.DataItem)["ASSCON_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    protected void btnSelectResSub_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        DataSet ds = new DataSet();

        ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ", "Table");


        this.lblREQ_RES_ID.Text = ds.Tables[0].Rows[0]["RES_NUMBER"].ToString();
        this.lblRES_Name.Text = ds.Tables[0].Rows[0]["RES_Name"].ToString();
        this.lblRES_RBS_NAME.Text = ds.Tables[0].Rows[0]["RES_RBS_NAME"].ToString();
        this.lblRES_WorkGroup1_NAME.Text = ds.Tables[0].Rows[0]["RES_WorkGroup1_NAME"].ToString();
        this.lblRES_RBS_AREA_NAMEE.Text = ds.Tables[0].Rows[0]["RES_RBS_AREA_NAME"].ToString();
        this.lblRES_JoinDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");

        this.txtATT_REQ_Tel.Text = ds.Tables[0].Rows[0]["RES_CP"].ToString();

        foreach (DataRow dr in ds.Tables[1].Select("GB = '" + hdnGB.Value + "' AND ASSCON_ID = '" + hdnASSCONID.Value + "'"))
        {
            this.lblRES_Vender.Text = dr["RES_Vender"].ToString();
            this.lblRES_Store.Text = dr["RES_Store"].ToString();
        }

        this.dvResSubList.Visible = false;
    }

    // 경조비 저장
    protected void Expend_Req_Save()
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

            Cmd.Parameters.AddWithValue("@REQ_TYPE", "002");
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

            SqlCommand CmdTarget = new SqlCommand("EPM_BUS_EXP_TARGET_SUBMIT_MOBILE", Con);
            CmdTarget.CommandType = CommandType.StoredProcedure;

            CmdTarget.Transaction = trans;

            CmdTarget.Parameters.Clear();

            CmdTarget.Parameters.AddWithValue("@EXE_GB", "I");
            CmdTarget.Parameters.AddWithValue("@EXP_REQ_ID", reqId);
            CmdTarget.Parameters.AddWithValue("@EXP_TAR_RES_ID", int.Parse(this.hdRES_ID.Value));
            CmdTarget.Parameters.AddWithValue("@EXP_TAR_RES_GB", hdnGB.Value);
            CmdTarget.Parameters.AddWithValue("@EXP_TAR_RES_ASSCON_ID",  this.hdnASSCONID.Value);
            CmdTarget.Parameters.AddWithValue("@EXP_TAR_Amount", txtEXP_TAR_Amount.Text);
            CmdTarget.Parameters.AddWithValue("@EXP_TAR_Tax", "0");
            CmdTarget.Parameters.AddWithValue("@EXP_TAR_Memo", txtEXP_TAR_Memo.Text);

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

            int rowCnt = 3; //int.Parse((string)ViewState["remarkCnt"]);

            for (int i = 1; i <= rowCnt; i++)
            {
                CmdRemark.Parameters.Clear();

                string _code = "";
                string _text = "";

                switch (i)
                {
                    case 1:
                        _code = "004"; // 경조사유 코드
                        _text = txtATT_REQ_Reason.Text.ToString();
                        break;
                    case 2:
                           _code = "005"; // 경조기간 코드
                           _text = this.txtATT_REQ_StartDate.Text.ToString() + " ~ " + this.txtATT_REQ_FinishDate.Text.ToString();
                        break;
                    case 3:
                        _code = "029"; // 지급대상 코드
                        _text = txtEXP_TAR_Res.Text.ToString();
                        break;
                    //case 4:
                    //    _code = hdnLine4.Value.ToString();
                    //    _text = txtText4.Text.ToString();
                    //    break;
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
    }


    protected void ddlREQ_TYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlREQ_TYPE.SelectedValue == "012")
        {
            txtATT_REQ_FinishDate.Text = "YYYYMMDD";
            txtATT_REQ_FinishDate.Enabled = false;
        }
        else
        {
            txtATT_REQ_FinishDate.Enabled = true;
        }
        // 경조비 신청
        if (ddlREQ_TYPE.SelectedValue == "010" && this.Page.Request["ATT_REQ_ID"] == null)            
        {
            ExpendReq.Visible = true;
        }
        else
        {
            ExpendReq.Visible = false;
        }
    }
}