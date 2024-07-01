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

public partial class Attendance_m_ATT_Leave_Request : System.Web.UI.Page
{
    string exeGB = "I";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            SetPage();
        }
    }


    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
		SetgvResList();
    
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

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
			Resource resource = new Resource();

			DataSet ds = new DataSet();

			ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(Session["sRES_ID"].ToString()), "REQ", "Table");

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
			}
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

            Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Closed_Mng.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
	
	// 배정/계약 정보 목록 바인딩
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
    
    // 배정/계약 정보 선택 시
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
}