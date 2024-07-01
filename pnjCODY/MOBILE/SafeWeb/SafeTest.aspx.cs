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

public partial class SafeWeb_SafeTest : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// btnSend_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        SimpleSecureTransfer sst = new SimpleSecureTransfer();
        string decryptedString1 = sst.Decrypt(hdUserID_Encrypt.Value);
        string decryptedString2 = sst.Decrypt(hdPassword_Encrypt.Value);
        lbResult.Text =
            string.Format(@"암호화된 데이터가 수신되었습니다.<br>
                                복호화된 아이디 : {0}<br>
                                복호화된 패스워드 : {1}", decryptedString1, decryptedString2);
    }
}
