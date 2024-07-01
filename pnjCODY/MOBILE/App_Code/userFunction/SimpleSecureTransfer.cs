using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

/// <summary>
/// SimpleSecureTransfer의 요약 설명입니다.
/// </summary>
public class SimpleSecureTransfer
{
    private RSACryptoServiceProvider RSA;

    /// <summary>
    /// 비밀키를 로드합니다.
    /// </summary>
    public SimpleSecureTransfer()
    {
        //System.Security.Principal.WindowsImpersonationContext impersonationContext;
        //impersonationContext = ((System.Security.Principal.WindowsIdentity)User.Identity).Impersonate();

        //PrivateKey Setting
        CspParameters csp = new CspParameters();
        csp.Flags = CspProviderFlags.UseMachineKeyStore;
        RSA = new RSACryptoServiceProvider(csp);

        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PrivateKeyPath"]));
        RSA.FromXmlString(xdoc.OuterXml);

        //impersonationContext.Undo();
        //GC.Collect();
    }

    /// <summary>
    /// 복호화
    /// </summary>
    /// <param name="sValue">암호화된 토큰문자열</param>
    /// <returns>복호화된 문자열</returns>
    public string Decrypt(string sValue)
    {
        //try
       // {
            StringBuilder sb = new StringBuilder();
            string[] arStr = sValue.Split('|');
            foreach (string orgString in arStr)
            {
                string decryptString = RSADecrypt(orgString);
                sb.Append(decryptString);
            }
            return HttpUtility.UrlDecode(sb.ToString());
       // }
        //catch
        //{
            //return null;
        //}
    }
        
    /// <summary>
    /// RSA복호화
    /// </summary>
    /// <param name="sValue">암호화된 문자열</param>
    /// <returns>복호화된 문자열</returns>
    private string RSADecrypt(string sValue)
    {
        byte[] encrypted = Convert.FromBase64String(sValue);
        byte[] decrypted = RSA.Decrypt(encrypted, false);
        return Encoding.UTF8.GetString(decrypted);
    }

    ~SimpleSecureTransfer()
    {
        RSA.Clear();
    }
}
