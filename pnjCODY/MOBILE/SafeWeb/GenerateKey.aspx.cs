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
using System.Security.Cryptography;

public partial class SafeWeb_GenerateKey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        //RSA초기화
        CspParameters cspParam = new CspParameters();
        cspParam.Flags = CspProviderFlags.UseMachineKeyStore;
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(Convert.ToInt32(ddlKeySize.SelectedValue), cspParam);

        //공개키 생성
        tbPublicKey.Text = RSA.ToXmlString(false);

        //비밀키 생성
        tbPrivateKey.Text = RSA.ToXmlString(true);
    }
}
