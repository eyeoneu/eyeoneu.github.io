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

public partial class Resource_m_RES_Lic_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {    
        // 신규 추가 등록 일 경우
        if (this.Page.Request["RES_LIC_ID"] == null)                     
        {                
            this.btnDel.Visible = false;
            this.txtRES_LIC_Date.CssClass = "i_f_out2";
        }

        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_LIC_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_VALUE_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["RES_LIC_ID"].ToString()), "LIC");

            if (rd.Read())
            {
                this.txtRES_LIC_Name.Text = rd["RES_LIC_Name"].ToString();
                this.txtRES_LIC_Type.Text = rd["RES_LIC_Type"].ToString();
                this.txtRES_LIC_Number.Text = rd["RES_LIC_Number"].ToString();
                this.txtRES_LIC_Date.Text = rd["RES_LIC_Date"].ToString();
                this.txtRES_LIC_Memo.Text = rd["RES_LIC_Memo"].ToString();
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
            int intRES_LIC_ID = 0;

            // 신규 추가 등록이 아닐 경우
            if (this.Page.Request["RES_LIC_ID"] != null)
            {
                strEXE_GB = "M";
                intRES_LIC_ID = int.Parse(this.Page.Request["RES_LIC_ID"].ToString());
            }

            try
            {

                resource.EPM_RES_LICENSE_SUBMIT
                                            (strEXE_GB,
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            intRES_LIC_ID,
                                            this.txtRES_LIC_Name.Text.ToString(),
                                            this.txtRES_LIC_Type.Text.ToString(),
                                            this.txtRES_LIC_Number.Text.ToString(),
                                            this.txtRES_LIC_Date.Text.ToString(),
                                            this.txtRES_LIC_Memo.Text.ToString()
                                            );

                Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Lic.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
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
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_LIC_ID"] != null)
            {
                Resource resource = new Resource();
                resource.EPM_RES_LICENSE_SUBMIT
                                            ("D",
                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                            int.Parse(this.Page.Request["RES_LIC_ID"].ToString()),
                                            "",
                                            "",
                                            "",
                                            "",
                                            ""
                                        );
            }

            Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_Lic.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}