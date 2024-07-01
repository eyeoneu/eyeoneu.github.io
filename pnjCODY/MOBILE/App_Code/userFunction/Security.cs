using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// 정창화
/// 2011.11.26
/// <summary>
/// 보안 관련 스크립트
/// </summary>
public class Security
{
    Page m_Page;

    protected string[] chkChar = {"end", "--", ";", "/*", "%", "=", "char", "alter", "begin", "cast", "create", 
                            "create", "declare", "exec", "fetch", "kill", "open", "drop", "delete", "insert", 
                            "select", "table", "update", "union", "null", "carrige return", "new line", 
                            "\'", "\"", " or "};

	public Security(Page szPage)
	{
        m_Page = szPage;
	}

    ///// <summary>
    ///// 세션 체크 및 허용권한 레벨 확인
    ///// </summary>
    ///// <param name="AuthLevel">접근 허용 레벨</param>
    //public void AuthenticateManager (int AuthLevel)
    //{
    //    //전체 운영자, 커뮤니티 운영자만 로드..
    //    if (m_Page.Session["sMemID"] == null || m_Page.Session["sMemCls"] == null)
    //    {
    //        m_Page.Response.Redirect("/ErrorInfo.aspx?ErrCode=100");
    //        m_Page.Response.End();
    //    }

    //    if (GetMemberClass() > AuthLevel)
    //    {
    //        m_Page.Response.Redirect("/ErrorInfo.aspx?ErrCode=101");
    //        m_Page.Response.End();
    //    }
    //}

    ///// <summary>
    ///// 로그인 한 회원의 등급.
    ///// </summary>
    //public int GetMemberClass()
    //{
    //    string strMemCls = "5";

    //    if(m_Page.Session["sMemID"] != null)
    //    {
    //        tbMember member = new tbMember();
    //        SqlDataReader rd = member.SelectMemberInfo(m_Page.Session["sMemID"].ToString());

    //        if (rd.Read())
    //            strMemCls = rd["MemCls"].ToString();

    //        rd.Close();
    //    }
    //    return int.Parse(strMemCls);
    //}

    /// <summary>
    /// 캐쉬 출력 방지(뒤로가기)
    /// </summary>
    public void NoCache()
    {
        m_Page.Response.Cache.SetCacheability(HttpCacheability.Public);
        m_Page.Response.Cache.SetExpires(DateTime.Now.AddSeconds(0));
        m_Page.Response.Cache.VaryByParams["*"] = true;
    }

    /// <summary>
    /// 입력글 XSS 해킹 방지
    /// 사용자 입력 필드는 모두 검사함.
    /// </summary>
    public string XSSContent(string content)
    {
        content = HttpUtility.HtmlEncode(content);

        content = content.Replace("script", "x-script");
        content = content.Replace("object", "x-object");
        content = content.Replace("applet", "x-applet");
        content = content.Replace("iframe", "x-iframe");
        content = content.Replace("embed", "x-embed");

        return content;
    }

    /// <summary>
    /// SQL 인젝션 방지
    /// </summary>
    public bool SafeSqlLiteralRtnBool(string szData)
    {
        if (szData == null) return false;

        string strTemp = " " + szData.ToLower();

        foreach (string Item in chkChar)
        {
            int idx = strTemp.IndexOf(Item);
            if (idx > 0) return false;
        }

        return true;
    }

    /// <summary>
    /// SQL 인젝션 방지
    /// </summary>
    public string SafeSqlLiteralRtnStr(string szData)
    {
        if (szData == null)
            return string.Empty;
        else
        {
            string strTemp = " " + szData.ToLower();

            foreach (string Item in chkChar)
            {
                int idx = strTemp.IndexOf(Item);
                if (idx > 0) return "유효하지 않은 글입니다.";
            }
        }

        //m_Page.ClientScript.RegisterClientScriptBlock(m_Page.GetType(), "Err", "alert('허용하지 않은 키워드를 입력하였습니다.');history.go(-1);", true);

        return szData;
    }

    /// <summary>
    /// 파일 확장자 확인
    /// </summary>
    public bool UploadFileNameChk(string fName)
    {
        int[] nRtn = new int[20];
        fName = " " + fName;
        nRtn[1] = fName.IndexOf(".aspx");
        nRtn[2] = fName.IndexOf(".config");
        nRtn[3] = fName.IndexOf(".html");
        nRtn[4] = fName.IndexOf(".htm");
        nRtn[5] = fName.IndexOf(".asp");
        nRtn[6] = fName.IndexOf(".csp");
        nRtn[7] = fName.IndexOf(".asa");
        nRtn[8] = fName.IndexOf(".cs");
        nRtn[9] = fName.IndexOf(".exe");
        nRtn[10] = fName.IndexOf(".cgi");
        nRtn[11] = fName.IndexOf(".js");
        nRtn[12] = fName.IndexOf(".com");
        nRtn[13] = fName.IndexOf(".sh");
        nRtn[14] = fName.IndexOf(".bat");
        nRtn[15] = fName.IndexOf(".dll");
        nRtn[16] = fName.IndexOf(".cab");

        foreach (int nItem in nRtn)
            if (nItem > 0) return false;

        return true;
    }

    /// <summary>
    /// 파일명 확인
    /// </summary>
    public bool DownFileNameChk(string fName)
    {
        int[] nRtn = new int[10];
        fName = " " + fName;
        nRtn[0] = fName.IndexOf("../");
        nRtn[1] = fName.IndexOf("..\\");
        nRtn[2] = fName.IndexOf(" ");
        nRtn[3] = fName.IndexOf(".aspx");
        nRtn[4] = fName.IndexOf(".config");
        nRtn[5] = fName.IndexOf(".html");
        nRtn[6] = fName.IndexOf(".htm");
        nRtn[7] = fName.IndexOf(".asp");
        nRtn[8] = fName.IndexOf(".csp");
        nRtn[9] = fName.IndexOf(".asa");

        foreach (int nItem in nRtn)
            if (nItem > 0) return false;

        return true;
    }

    /// <summary>
    /// 암호화 키
    /// </summary>
    public byte[] GetHashKey(string hashKey)
    {
        // Initialise
        UTF8Encoding encoder = new UTF8Encoding();

        // Get the salt
        string salt = "HUB Movie Player";
        byte[] saltBytes = encoder.GetBytes(salt);

        // Setup the hasher
        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(hashKey, saltBytes);

        // Return the key
        return rfc.GetBytes(16);
    }

    /// <summary>
    /// 암호화
    /// </summary>
    public string Encrypt(byte[] key, string dataToEncrypt)
    {
        //// Initialise
        //AesManaged encryptor = new AesManaged();

        //// Set the key
        //encryptor.Key = key;
        //encryptor.IV = key;

        //// create a memory stream
        //using (MemoryStream encryptionStream = new MemoryStream())
        //{
        //    // Create the crypto stream
        //    using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //    {
        //        // Encrypt
        //        byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
        //        encrypt.Write(utfD1, 0, utfD1.Length);
        //        encrypt.FlushFinalBlock();
        //        encrypt.Close();

        //        // Return the encrypted data
        //        return Convert.ToBase64String(encryptionStream.ToArray());
        //    }
        //}
        return dataToEncrypt;
    }

    /// <summary>
    /// 복호화
    /// </summary>
    public string Decrypt(byte[] key, string encryptedString)
    {
        //// Initialise
       
        //System.Security.Cryptography.
        //AesManaged decryptor = new AesManaged();
        //byte[] encryptedData = Convert.FromBase64String(encryptedString);

        //// Set the key
        //decryptor.Key = key;
        //decryptor.IV = key;

        //// create a memory stream
        //using (MemoryStream decryptionStream = new MemoryStream())
        //{
        //    // Create the crypto stream
        //    using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //    {
        //        // Encrypt
        //        decrypt.Write(encryptedData, 0, encryptedData.Length);
        //        decrypt.Flush();
        //        decrypt.Close();

        //        // Return the unencrypted data
        //        byte[] decryptedData = decryptionStream.ToArray();
        //        return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
        //    }
        //}
        return encryptedString;
    }
}
