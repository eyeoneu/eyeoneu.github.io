using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;

/// <summary>
/// Common의 요약 설명입니다.
/// </summary>
public class Common
{
    public static DateTime realNameDate = DateTime.Parse("2009-04-14");

	public Common()
	{
		//
		// TODO: 생성자 논리를 여기에 추가합니다.
		//
	}

    /// <summary>
    /// 원하는 길이만큼 자르기
    /// </summary>
    /// <param name="getString">원본 문자열</param>
    /// <param name="cutLength">자를 길이</param>
    /// <returns>길이 조절된 문자열</returns>
    public static string getCuttingString2(string getString, int cutLength)
    {
        string retString = getString;

        if (getString.Length > cutLength)
        {
            retString = getString.Substring(0, cutLength) + "...";
        }

        return retString;
    }

    public static string getCuttingString(string getString, int cutLength)
    {
        string rValue;
        double nLength;

        nLength = 0.00;
        rValue = "";

        string tmpStr;
        byte tmpLen;

        if (getString == "" || getString == null)
            rValue = "";
        else
        {
            System.Text.ASCIIEncoding ae = new System.Text.ASCIIEncoding();

            byte[] byteArray = ae.GetBytes(getString);

            for (int i = 0; i < byteArray.Length; i++)
            {
                //Console.WriteLine(byteArray[i]);

                //tmpLen = Convert.ToInt32(byteArray[i]);
                tmpLen = byteArray[i];
                tmpStr = getString.Substring(i, 1);

                //한글
                if (tmpLen == 63)
                {
                    nLength = nLength + 1.4;
                    rValue = rValue + tmpStr;
                }
                //영문소문자
                else if (tmpLen >= 97 && tmpLen <= 122)
                {
                    nLength = nLength + 0.75;
                    rValue = rValue + tmpStr;
                }
                //영문대문자
                else if (tmpLen >= 65 && tmpLen <= 90)
                {
                    nLength = nLength + 1;
                    rValue = rValue + tmpStr;
                }
                //그외 키 값(특수문자, 기호)
                else
                {
                    nLength = nLength + 0.6;
                    rValue = rValue + tmpStr;
                }

                if (nLength > cutLength)
                {
                    rValue = rValue + "...";
                    break;
                }
            }

        }

        return rValue;
    }



    /// <summary>
    /// 날짜형식 출력
    /// </summary>
    /// <param name="getDateString">날짜 문자열</param>
    /// <param name="dateKind">작업 구분. 1:2007-08-01, 2:2007/08/01</param>
    /// <returns>변경된 문자열</returns>
    public static string getDateTime(DateTime getDateString, string dateKind)
    {
        string retString;

        switch (dateKind)
        {
            case "1":
                retString = string.Format("{0:yyyy-MM-dd}", getDateString);
                break;
            case "2":
                retString = string.Format("{0:yyyy/MM/dd}", getDateString);
                break;
            default:
                retString = getDateString.ToString();
                break;
        }

        return retString;
    }

    /// <summary>
    /// 자바스크립트
    /// </summary>
    /// <param name="response"></param>
    /// <param name="strFunction"></param>
    public static void AlertUserFunction(HttpResponse response, string strFunction)
    {
        string script = "<script type='text/javascript'>" + strFunction + "</script>";
        response.Write(script);
    }

    /// <summary>
    /// 자바스크립트 alert 띄우기
    /// </summary>
    /// <param name="response">Response 개체</param>
    /// <param name="message">알림 메시지</param>
    public static void Alert(HttpResponse response, string message)
    {
        string script = "<script type='text/javascript'>alert('" + message + "');</script>";
        response.Write(script);

    }

    /// <summary>
    /// 자바스크립트 alert 띄운 후 원하는 페이지로 이동
    /// </summary>
    /// <param name="response">Response 개체</param>
    /// <param name="message">알림 메시지</param>
    /// <param name="url">이동할 페이지 URL</param>
    public static void Alert(HttpResponse response, string message, string url)
    {
        string script = "<script type='text/javascript'>alert('" + message + "');location.href='" + url + "'</script>";
        response.Write(script);
    }



    /// <summary>
    /// 자바스크립트 알림창 후 페이지 닫기
    /// </summary>
    /// <param name="page">페이지 개체</param>
    /// <param name="message">알림 내용</param>
    public static void scriptAlertClose(HttpResponse reponse, string message)
    {
        string script = "<script type='text/javascript'>alert('" + message + "');this.close();</script>";
        reponse.Write(script);

    }

    /// <summary>
    /// 자바스크립트 알림창
    /// </summary>
    /// <param name="page">페이지 개체</param>
    /// <param name="message">알림 내용</param>
    public static void scriptAlert(Page page, string message)
    {
        // 정창화 수정 (2011-11-24)
        //string script = "<script type='text/javascript'>alert('" + message + "');</script>";
        //page.RegisterClientScriptBlock("script", script);

        page.ClientScript.RegisterClientScriptBlock(typeof(Page), "script", "alert('" + message + "');", true);
    }

    /// <summary>
    /// 자바스크립트 알림창 후 페이지 이동
    /// </summary>
    /// <param name="page">페이지 개체</param>
    /// <param name="message">알림 내용</param>
    /// <param name="url">이동할 페이지 URL</param>
    public static void scriptAlert(Page page, string message, string url)
    {
        // 정창화 수정 (2011-11-24)
        //string script = "<script type='text/javascript'>alert('" + message + "');location.href='" + url + "'</script>";
        //page.RegisterClientScriptBlock("script", script);

        page.ClientScript.RegisterClientScriptBlock(typeof(Page), "script", "alert('" + message + "');location.href='" + url + "';", true);
    }



    /// <summary>
    /// DB 인서트 전 문자열 Replace
    /// </summary>
    /// <param name="strVal">바꿀 문자열</param>
    /// <returns>바뀐 문자열</returns>
    public static string ReplaceInsertDB(string strVal)
    {
        string strTmp = strVal;
        strTmp = strTmp.Replace("&", "&amp;");
        strTmp = strTmp.Replace("<", "&lt;");
        strTmp = strTmp.Replace(">", "&gt;");
        strTmp = strTmp.Replace("'", "''");

        return strTmp;
    }

    /// <summary>
    /// 화면 출력 문자열 Replace
    /// </summary>
    /// <param name="strVal">바꿀 문자열</param>
    /// <returns>바뀐 문자열</returns>
    public static string ReplaceView(string strVal)
    {
        string strTmp = strVal;
        strTmp = strTmp.Replace("&amp;", "&");
        strTmp = strTmp.Replace("&lt;", "<");
        strTmp = strTmp.Replace("&gt;", ">");
        strTmp = strTmp.Replace("''", "'");

        return strTmp;
    }

    /// <summary>
    /// 댓글 개수 출력
    /// </summary>
    /// <param name="commentCnt">댓글 수</param>
    /// <returns>리턴 문자열</returns>
    public static string getCommentCnt(int commentCnt)
    {
        if (commentCnt > 0)
        {
            return "[" + commentCnt.ToString() + "]";
        }
        else
            return "";
    }

    /// <summary>
    /// 제목 길이 조절 (계층 게시판일 경우)
    /// </summary>
    /// <param name="strTitle">제목</param>
    /// <param name="depth">답글 단계</param>
    /// <returns>조절된 제목 문자</returns>
    public static string getTitle(string strTitle, int depth)
    {
        int cutCnt = 22;

        if (depth > 0)
        {
            if (depth == 1)
                cutCnt = 16;
            else
                cutCnt = cutCnt - (depth * 2) - 2;
            //cutCnt -= depth * 2;
        }

        //제목 지정한 수치만큼 Cut
        return getCuttingString(strTitle, cutCnt);
    }

    /// <summary>
    /// 첨부파일이 있을 경우 리스트에 첨부파일 이미지 출
    /// </summary>
    /// <param name="fileCnt">첨부파일 개수</param>
    /// <returns>첨부파일 이미지 혹은 ""</returns>
    public static string getFileImg(int fileCnt)
    {
        if (fileCnt > 0)
        {
            return "<img src='../images/ico_p02.gif'>";
        }
        else
            return "";
    }

    /// <summary>
    /// RadioButtonList 에 Item 추가
    /// </summary>
    /// <param name="rbtn">RadioButtonList 개체</param>
    /// <param name="strValue">값</param>
    /// <param name="strText">출력 문자열</param>
    public static void addListItem_rdio(RadioButtonList rbtn, string strValue, string strText)
    {
        ListItem li = new ListItem();
        li.Value = strValue;
        li.Text = strText;

        rbtn.Items.Add(li);
    }

    /// <summary>
    /// ListBox 에 item 추가
    /// </summary>
    /// <param name="lbx">ListBox 개체</param>
    /// <param name="strValue">값</param>
    /// <param name="strText">출력 문자열</param>
    public static void addListItem_lbx(ListBox lbx, string strValue, string strText)
    {
        ListItem li = new ListItem();
        li.Value = strValue;
        li.Text = strText;

        lbx.Items.Add(li);
    }

    /// <summary>
    /// DropDownList 에 item 추가
    /// </summary>
    /// <param name="ddl">DropDownList 개체</param>
    /// <param name="strValue">값</param>
    /// <param name="strText">출력 문자열</param>
    public static void addListItem_ddl(DropDownList ddl, string strValue, string strText)
    {
        ListItem li = new ListItem();
        li.Value = strValue;
        li.Text = strText;

        ddl.Items.Add(li);
    }


    ///// <summary>
    ///// 첨부파일 업로드
    ///// </summary>
    ///// <param name="file1">업로드할 파일 개체</param>
    ///// <param name="uploadDir">업로드할 디렉터리</param>
    ///// <returns>업로드된 파일명</returns>
    //public static string procFileUpload(FileUpload file1, string uploadDir)
    //{
    //    DirectoryInfo dirInfo = new DirectoryInfo(uploadDir);
    //    if (!dirInfo.Exists)
    //    {
    //        dirInfo.Create();
    //    }

    //    string fileName = file1.FileName;                   //업로드할 파일명
    //    string fFullName = uploadDir + fileName;            //업로드할 디렉터리 + 파일명 Full 경로

    //    FileInfo fInfo = new FileInfo(fFullName);
    //    string newFileName = "";

    //    //업로드할 파일명이 이미 존재할 경우 파일명 변경
    //    if (fInfo.Exists)
    //    {
    //        int fIndex = 0;
    //        string fExtension = fInfo.Extension;                    //파일 확장자
    //        string fRealName = fileName.Replace(fExtension, "");    //확장자를 제외한 파일명

    //        do
    //        {
    //            fIndex++;
    //            newFileName = fRealName + "_" + fIndex.ToString() + fExtension;     //변경된 파일명
    //            fInfo = new FileInfo(uploadDir + newFileName);
    //        } while (fInfo.Exists);


    //        fFullName = uploadDir + newFileName;
    //        file1.PostedFile.SaveAs(fFullName);

    //        return newFileName;
    //    }
    //    else
    //    {
    //        file1.PostedFile.SaveAs(fFullName);

    //        return fileName;
    //    }
    //}



    ///// <summary>
    ///// 첨부파일 삭제(물리적 파일)
    ///// </summary>
    ///// <param name="fileName">파일명</param>
    ///// <param name="uploadDir">업로드 디렉터리</param>
    //public static void procFileDelete(string fileName, string uploadDir)
    //{
    //    try
    //    {
    //        FileInfo fInfo = new FileInfo(uploadDir + fileName);

    //        if (fInfo.Exists)
    //            fInfo.Delete();
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.WriteLine(ex.Message);
    //    }
    //}


    ///// <summary>
    ///// 첨부파일 변경
    ///// </summary>
    ///// <param name="fileName">파일명</param>
    ///// <param name="uploadDir">업로드 디렉터리</param>
    ///// <param name="moveDir">변경 디렉터리</param>/// 
    //public static string procFileMove(string fileName, string uploadDir, string moveDir)
    //{
    //    DirectoryInfo dirInfo = new DirectoryInfo(moveDir);
    //    if (!dirInfo.Exists)
    //    {
    //        dirInfo.Create();
    //    }

    //    FileInfo fInfo = new FileInfo(uploadDir + fileName);
    //    FileInfo fMove = new FileInfo(moveDir + fileName);

    //    if (fInfo.Exists)
    //    {
    //        if (fMove.Exists)
    //        {
    //            int fIndex = 0;
    //            string fExtension = fMove.Extension;                    //파일 확장자
    //            string fRealName = fileName.Replace(fExtension, "");    //확장자를 제외한 파일명

    //            do
    //            {
    //                fIndex++;
    //                fileName = fRealName + "_" + fIndex.ToString() + fExtension;     //변경된 파일명
    //                fMove = new FileInfo(moveDir + fileName);
    //            } while (fMove.Exists);
    //        }

    //        fInfo.CopyTo(moveDir + fileName, false);
    //        fInfo.Delete();
    //    }
    //    return fileName;
    //}

    ///// <summary>
    ///// 첨부파일 파일명 중복 체크 - 2008.01.09 이순영
    ///// </summary>
    ///// <param name="file1">업로드할 파일명</param>
    ///// <param name="uploadDir">업로드할 디렉터리</param>
    ///// <returns>업로드될 파일명</returns>
    //public static string GetCheckFileName(string fileName, string uploadDir)
    //{
    //    DirectoryInfo dirInfo = new DirectoryInfo(uploadDir);
    //    if (!dirInfo.Exists)
    //    {
    //        dirInfo.Create();
    //    }

    //    string fFullName = uploadDir + fileName;            //업로드할 디렉터리 + 파일명 Full 경로

    //    FileInfo fInfo = new FileInfo(fFullName);
    //    string newFileName = "";
                
    //    //업로드할 파일명이 이미 존재할 경우 파일명 변경
    //    if (fInfo.Exists)
    //    {
    //        int fIndex = 0;
    //        string fExtension = fInfo.Extension;                    //파일 확장자
    //        string fRealName = fileName.Replace(fExtension, "");    //확장자를 제외한 파일명

    //        do
    //        {
    //            fIndex++;
    //            newFileName = fRealName + "_" + fIndex.ToString() + fExtension;     //변경된 파일명
    //            fInfo = new FileInfo(uploadDir + newFileName);
    //        } while (fInfo.Exists);

    //        fFullName = uploadDir + newFileName;

    //        return newFileName;
    //    }
    //    else
    //    {
    //        return fileName;
    //    }
    //}

    /// <summary>
    /// Null값일경우 "" 값 반환
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetMemtyValueToNull(string value)
    {
        if (value == null)
        {
            value = "";
        }

        return value;
    }

    public static string GetMemtyValueToNull(string value, string setValue)
    {
        if (value == null)
        {
            value = setValue;
        }

        return value;
        
    }

    // 값이 null 이거나 빈값일경우 "0"을 리턴함
    public static string GetZeroValueToNull(string value)
    {
        if (string.IsNullOrEmpty(value))
            value = "0";

        return value;
    }

    public static void setQuickEmp(object lblKOR_NM, string strEMP_CD, string strSendId, string strEMAL_ADD, string strSendName)
    {
        //
    }

    public static string getQuickEmpString(string strRecEMP_CD, string strReceiveId, string strRecEMAL_ADD, string strReceiveEmpName)
    {
        //
        return strRecEMP_CD;
    }

    public static string getMemoImportantType(object ctl)
    {
        string strRtnMsg = "";
        switch (Int16.Parse((string)ctl))
        {
            case 1: strRtnMsg = "<img src='/img/sub/03.gif' width='10' height='9' alt='낮음'>"; break;
            case 2: strRtnMsg = "<img src='/img/sub/02.gif' width='10' height='9' alt='보통'>"; break;
            case 3: strRtnMsg = "<img src='/img/sub/01.gif' width='10' height='9' alt='높음'>"; break;
        }
        return strRtnMsg;
    }

    public static void setTableCell(TableRow tr, int nWidth, HorizontalAlign Align, object ctl)
    {
        TableCell td = new TableCell();
        if( nWidth != 0 ) td.Width = nWidth;
        td.CssClass = "bbsNameTxt";
        td.HorizontalAlign = Align;
        if (ctl.GetType().Equals(typeof(String)))
        {
            td.Text = ctl.ToString();
        }
        else
        {
            td.Controls.Add((Control)ctl);
        }
        tr.Cells.Add(td);
    }


    /// <summary>
    /// 메일 발송
    /// </summary>
    /// <param name="strSendName">보내는 사람 이름</param>
    /// <param name="strSendEMail">보내는 사람 메일</param>
    /// <param name="strReceiveName">받는 사람 이름</param>
    /// <param name="strReceiveEMail">받는 사람 메일</param>
    /// <param name="strTitle">메일 제목</param>
    /// <param name="strContent">메일 내용</param>
    public static void sendEMail(string strSendName, string strSendEMail, string strReceiveName, string strReceiveEMail, string strTitle, string strContent)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(strSendEMail, strSendName, System.Text.Encoding.Default);
        mail.To.Add(new MailAddress(strReceiveEMail, strReceiveName, System.Text.Encoding.Default));

        mail.Subject = strTitle;
        mail.Body = strContent;
        mail.SubjectEncoding = System.Text.Encoding.Default;
        mail.BodyEncoding = System.Text.Encoding.Default;
        mail.IsBodyHtml = true;

        //SmtpClient smtp = new SmtpClient("203.109.5.241");
        SmtpClient smtp = new SmtpClient("localhost");
        try
        {
            smtp.Send(mail);
        }
        catch
        {

        }
    }

    /// <summary>
    /// 이미지 테이블에 맞게 보여주기
    /// </summary>
    /// <param name="ImgPath">이미지 경로</param>
    /// <param name="ImgUrl">이미지 웹 경로</param>
    /// <param name="MaxImgWidth">이미지 최대 크기</param>
    public static string dspImage(System.Web.HttpServerUtility Server, string ImgPath, int MaxImgWidth)
    {
        string photo = string.Empty;

        try
        {
            if (!System.IO.File.Exists(Server.MapPath(ImgPath)))
                return "<font color=red>Error : 해당 이미지를 찾을 수 없습니다.</font>";
            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));

            if (int.Parse(img.Width.ToString()) > MaxImgWidth)
                photo = "<img src='" + ImgPath + "' width='" + MaxImgWidth.ToString() + "' border='0'/>";
            else
                photo = "<img src='" + ImgPath + "' width='" + img.Width.ToString() + "' height='" + img.Height.ToString() + "' />";
        }
        catch
        {
            photo = "<font color=red>Error : 이미지 파일 경로 오류!!!</font>";
        }

        return photo;
    }
}
