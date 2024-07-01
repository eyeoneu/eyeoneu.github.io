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
using System.Drawing;


public partial class Resource_m_RES_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Page.Request["RES_ID"] == "")
                Response.Redirect("/m_Default.aspx");
            else
                SetPage();
        }
    }

    
    // 사원 등록 정보의 저장 여부에 따른 분류별 버튼 타입 설정
    private void SetPage()
    {
        Resource resource = new Resource();

        
        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        if (rd.Read())
        {
            this.lblRES_Name.Text = rd["RES_Name"].ToString();
            this.lblRES_PersonNumber.Text = rd["RES_PersonNumber"].ToString();
            this.lblRES_Number.Text = rd["RES_Number"].ToString();
            this.lblRES_WorkState.Text = rd["RES_WorkState"].ToString();
        }

        rd.Close();

        rd = resource.EPM_RES_DETAIL_IS_SAVED_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        if (rd.Read())
        {
            if (rd["IS_ADD_SAVED"].ToString() == "1")
                this.btnADD.Attributes["class"] = "button orange mepm_btn";
            if (rd["IS_EMP_SAVED"].ToString() == "1")
                this.btnEMP.Attributes["class"] = "button orange mepm_btn";
            if (rd["IS_CAR_SAVED"].ToString() == "1")
                this.btnCAR.Attributes["class"] = "button orange mepm_btn";
            if (rd["IS_EDU_SAVED"].ToString() == "1")
                this.btnEDU.Attributes["class"] = "button orange mepm_btn";
            if (rd["IS_LIC_SAVED"].ToString() == "1")
                this.btnLIC.Attributes["class"] = "button orange mepm_btn";
            if (rd["IS_FAM_SAVED"].ToString() == "1")
                this.btnFAM.Attributes["class"] = "button orange mepm_btn";
            if (rd["IS_PHO_SAVED"].ToString() == "1")
                this.btnPHO.Attributes["class"] = "button orange mepm_btn";
        }

        rd.Close();
    }

    // 이전단계 버튼 클릭 시: 상태별 페이지로 이동
    protected void btnPageMove_Click(object sender, EventArgs e)
    {
            if (this.Page.Request["RES_ID"] == null)
                Response.Redirect("/m_Default.aspx");
            else
                Response.Redirect("/Resource/m_RES_Mng.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
    }
}
