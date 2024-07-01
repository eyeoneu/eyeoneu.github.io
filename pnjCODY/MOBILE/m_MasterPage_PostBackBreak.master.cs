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

public partial class m_MasterPage_PostBackBreak : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //세션이 없을 경우
        if (Request.Path.ToString() != "/m_Login_form.aspx" && Session["sRES_ID"] == null)
        {
            Common.AlertUserFunction(HttpContext.Current.Response, "top.location.href='/m_Login_form.aspx';");
        }
    }

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

    public interface IEnterPostBack
    {

    }
}
