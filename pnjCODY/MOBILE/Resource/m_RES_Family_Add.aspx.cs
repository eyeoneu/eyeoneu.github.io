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

public partial class Resource_m_RES_Family_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 신규 추가 등록 일 경우
        if (this.Page.Request["RES_FAM_ID"] == null)
            this.btnDel.Visible = false;

        if (!IsPostBack)
        {
            SetControl();
            SetPage();
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        Code code = new Code();
        DataSet ds = code.DZICUBE_CODE("H1");

        this.ddlRES_FAM_Relation.DataSource = ds;
        this.ddlRES_FAM_Relation.DataBind();
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_FAM_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_VALUE_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["RES_FAM_ID"].ToString()), "FAM");

            if (rd.Read())
            {
                this.txtRES_FAM_Name.Text = rd["RES_FAM_Name"].ToString();
                this.ddlRES_FAM_Relation.SelectedValue = rd["RES_FAM_Relation"].ToString();
                this.txtRES_FAM_Pnumber1.Text = rd["RES_FAM_PersonNumber"].ToString().Substring(0, 6);
                this.txtRES_FAM_Pnumber2.Text = rd["RES_FAM_PersonNumber"].ToString().Substring(7, 7); ;
                this.txtRES_FAM_Work.Text = rd["RES_FAM_Work"].ToString();
                this.txtRES_FAM_CP.Text = rd["RES_FAM_CP"].ToString();
                this.rdoRES_FAM_Support.SelectedValue = rd["RES_FAM_Support"].ToString();
                this.rdoRES_FAM_Together.SelectedValue = rd["RES_FAM_Together"].ToString();
                this.rdoRES_FAM_Health.SelectedValue = rd["RES_FAM_Health"].ToString();
            }

            rd.Close();

            DataTable dt = Select_LogList();

            this.gvLogList.DataSource = dt;
            this.gvLogList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = ((DataRowView)e.Row.DataItem)["EXECUTE_DATE"].ToString();
            e.Row.Cells[1].Text = ((DataRowView)e.Row.DataItem)["EXECUTE_HISTORY"].ToString();
            e.Row.Cells[2].Text = ((DataRowView)e.Row.DataItem)["EXECUTE_RES_NAME"].ToString();

            if (((DataRowView)e.Row.DataItem)["EXECUTE_HISTORY"].ToString() == "반려") //반려된 이력을 클릭하면 레이어로 사유를 보여준다
            {
                e.Row.Cells[1].Text = "<ul><li title=\"상세보기\" onclick=\"toggle_display('div_com" + ((DataRowView)e.Row.DataItem)["LOG_ID"].ToString()
                + "');\" class=\"ListStyle\">" + ((DataRowView)e.Row.DataItem)["EXECUTE_HISTORY"].ToString()
                + "</li><li id=\"div_com" + ((DataRowView)e.Row.DataItem)["LOG_ID"].ToString() + "\" style=\"display: none;\">"
                + "<ul onclick=\"toggle_display('div_com" + ((DataRowView)e.Row.DataItem)["LOG_ID"].ToString()
                + "');\" title=\"닫기\" class=\"ModalPopup\">"
                + "<li>" + "<span style=\"font-size:1.1em; font-weight:bold;\">반려내용 상세보기</span>"
                + "<BR />" + "처리일시 : &nbsp;" + ((DataRowView)e.Row.DataItem)["EXECUTE_DATE"].ToString()
                + "<BR />" + "반려사유 : &nbsp;" + ((DataRowView)e.Row.DataItem)["EXECUTE_DENY"].ToString()
                + "<BR />" + "승인자 : &nbsp;" + ((DataRowView)e.Row.DataItem)["EXECUTE_RES_NAME"].ToString()
                + "</li>"
                + "</ul></li></ul>";
            }
        }
    }

    // 피부양 이력 조회
    private DataTable Select_LogList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_DEPENDENTS_DETAIL_LOG", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@RES_ID", int.Parse(this.Page.Request["RES_ID"].ToString()));
        adp.SelectCommand.Parameters.AddWithValue("@RES_FAM_ID", int.Parse(this.Page.Request["RES_FAM_ID"].ToString()));

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
        return ds.Tables[0];
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();
            string strEXE_GB = "I";
            int intRES_FAM_ID = 0;

            // 신규 추가 등록이 아닐 경우
            if (this.Page.Request["RES_FAM_ID"] != null)
            {
                strEXE_GB = "M";
                intRES_FAM_ID = int.Parse(this.Page.Request["RES_FAM_ID"].ToString());
            }

            try
            {

                resource.EPM_RES_FAMILY_SUBMIT
                                            (strEXE_GB,
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            intRES_FAM_ID,
                                            this.txtRES_FAM_Name.Text.ToString(),
                                            this.ddlRES_FAM_Relation.SelectedValue.ToString(),
                                            this.txtRES_FAM_Pnumber1.Text.ToString() + "-" + this.txtRES_FAM_Pnumber2.Text.ToString(),
                                            this.txtRES_FAM_Work.Text.ToString(),
                                            this.rdoRES_FAM_Support.SelectedValue.ToString(),
                                            this.rdoRES_FAM_Together.SelectedValue.ToString(),
                                            this.rdoRES_FAM_Health.SelectedValue.ToString(),
                                            this.txtRES_FAM_CP.Text.ToString()
                                            );

                Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Family.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        else
        {
            Common.scriptAlert(this.Page, "잘못된 접근 입니다.");
            Response.Redirect("/m_Default.aspx");
        }
    }

    // 삭제 버튼 클릭 시
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_FAM_ID"] != null)
            {
                Resource resource = new Resource();
                resource.EPM_RES_FAMILY_SUBMIT
                                            ("D",
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            int.Parse(this.Page.Request["RES_FAM_ID"].ToString()),
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            ""
                                        );
            }

            Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_Family.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}