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

public partial class Resource_m_RES_Career_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 신규 추가 등록 일 경우
        if (this.Page.Request["RES_CAR_ID"] == null)
        {
            this.btnDel.Visible = false;
            this.txtRES_CAR_START.CssClass = "i_f_out2";
            this.txtRES_CAR_FINISH.CssClass = "i_f_out2";
        }
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            SetControl();
            SetPage();
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        Code code = new Code();
        DataSet ds = code.EPM_CODE("13");

        this.ddlRES_CAR_RETIRE.DataSource = ds;
        this.ddlRES_CAR_RETIRE.DataBind();
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_CAR_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_VALUE_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["RES_CAR_ID"].ToString()), "CAR");

            if (rd.Read())
            {
                this.txtRES_CAR_COMPANY.Text = rd["RES_CAR_COMPANY"].ToString();
                this.txtRES_CAR_START.Text = rd["RES_CAR_START"].ToString();
                this.txtRES_CAR_FINISH.Text = rd["RES_CAR_FINISH"].ToString();
                this.txtRES_CAR_MAINJOB.Text = rd["RES_CAR_MAINJOB"].ToString();
                this.ddlRES_CAR_RETIRE.SelectedValue = rd["RES_CAR_RETIRE_CODE"].ToString();
            }

            rd.Close();
        }
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();
            string strEXE_GB = "I";
            int intRES_CAR_ID = 0;

            // 신규 추가 등록이 아닐 경우
            if (this.Page.Request["RES_CAR_ID"] != null)
            {
                strEXE_GB = "M";
                intRES_CAR_ID = int.Parse(this.Page.Request["RES_CAR_ID"].ToString());
            }

            try
            {

                resource.EPM_RES_CAREER_SUBMIT
                                            (strEXE_GB,
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            intRES_CAR_ID,
                                            this.txtRES_CAR_COMPANY.Text.ToString(),
                                            this.txtRES_CAR_START.Text.ToString(),
                                            this.txtRES_CAR_FINISH.Text.ToString(),
                                            this.txtRES_CAR_MAINJOB.Text.ToString(),
                                            this.ddlRES_CAR_RETIRE.SelectedValue.ToString()
                                            );

                Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Career.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
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
                if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_CAR_ID"] != null)
                {
                    Resource resource = new Resource();
                    resource.EPM_RES_CAREER_SUBMIT
                                                ("D",
                                                int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                int.Parse(this.Page.Request["RES_CAR_ID"].ToString()),
                                                "",
                                                "",
                                                "",
                                                "",
                                                ""
                                            );
                }

                Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_Career.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
    }
}