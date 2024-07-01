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

public partial class m_NotSupport : System.Web.UI.Page
{
    /// <summary>
    /// 로그아웃 클릭 시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();

        Response.Redirect("/m_Login_form.aspx");
    }
}
