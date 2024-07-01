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

public partial class Resource_m_RES_Edu_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 신규 추가 등록 일 경우
        if (this.Page.Request["RES_EDU_ID"] == null)
            this.btnDel.Visible = false;

        if (!IsPostBack)
        {
            SetControl();
            SetPage();
        }
    }

    // 페이지 컨트롤 바인딩
    private void SetControl()
    {
        ListItem tempItem = new ListItem("-선택-", "");
        this.ddlRES_EDU_GraduationDate.Items.Add(tempItem);
    
        for (int i = 0; i >= -75; i--)
        {
            string date = DateTime.Now.AddYears(i).ToString("yyyy");
            this.ddlRES_EDU_GraduationDate.Items.Add(date);
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_EDU_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_VALUE_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["RES_EDU_ID"].ToString()), "EDU");

            if (rd.Read())
            {
                this.txtRES_EDU_School.Text = rd["RES_EDU_School"].ToString();
                this.ddlRES_EDU_GraduationDate.Text = rd["RES_EDU_GraduationDate"].ToString();
                this.ddlRES_EDU_Graduation.Text = rd["RES_EDU_Graduation"].ToString();
                this.txtRES_EDU_Major.Text = rd["RES_EDU_Major"].ToString();
                this.txtRES_EDU_Area.Text = rd["RES_EDU_Area"].ToString();             
                
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
            int intRES_EDU_ID = 0;

            // 신규 추가 등록이 아닐 경우
            if (this.Page.Request["RES_EDU_ID"] != null)
            {
                strEXE_GB = "M";
                intRES_EDU_ID = int.Parse(this.Page.Request["RES_EDU_ID"].ToString());
            }

            try
            {

                resource.EPM_RES_EDUCATION_SUBMIT
                                            (strEXE_GB,
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            intRES_EDU_ID,
                                            this.txtRES_EDU_School.Text.ToString(),
                                            this.ddlRES_EDU_Graduation.Text.ToString(),
                                            this.ddlRES_EDU_GraduationDate.Text.ToString(),   
                                            this.txtRES_EDU_Major.Text.ToString(),
                                            this.txtRES_EDU_Area.Text.ToString()                                           
                                                                                     
                                            );

                if (strEXE_GB == "I")
                    Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_EDU.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
                else
                    Common.scriptAlert(this.Page, "저장되었습니다.");
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
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_EDU_ID"] != null)
            {
                Resource resource = new Resource();
                resource.EPM_RES_EDUCATION_SUBMIT
                                            ("D",
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            int.Parse(this.Page.Request["RES_EDU_ID"].ToString()),
                                            "",
                                            "",
                                            "",
                                            "",
                                            ""
                                        );
            }

            Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_EDU.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

}