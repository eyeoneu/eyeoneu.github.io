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


public partial class Resource_m_RES_PreInfoProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Mng.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
    }

    protected void btnPreInfoSave_Click(object sender, EventArgs e)
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["PRE_RES_ID"] != null)
        {
            Resource resource = new Resource();

            resource.EPM_RES_SAVED_INFO_UPDATE_MOBILE
                                                (int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                int.Parse(this.Page.Request["PRE_RES_ID"].ToString()));

            Common.scriptAlert(this.Page, "등록정보를 모두 가져오는데 성공하였습니다.", "/Resource/m_RES_Register.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        }
        else
        {
            Common.scriptAlert(this.Page, "올바른 경로가 아닙니다.", "/m_Default.aspx");
        }
    }
}
