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
using System.IO;

public partial class MOBILE_Notice_m_NOT_Comment_Write : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";
    // 입력 구분
    string exeGB = "I";
    // 덧글 개수
    public int rowCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

        ViewState["NOT_COMMENT_ID"] = "";
        ViewState["GB_COMMENT"] = "I";
        ViewState["RBS"] = Session["sRES_RBS_NAME"];

        //권한체크
        if (Session["sRES_RBS_CD"] == "1111")//관리팀
        {
            RES_GB = "A";
        }
        else if (Session["sRES_WorkGroup2"].ToString() == "220")//팀장 
        {
            RES_GB = "T";
        }
        else if (Session["sRES_WorkGroup1"].ToString() == "008")//서포터
        {
            RES_GB = "S";
            //this.btnSave.Enabled = false;
        }

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ETC_NOT_ID"]))
            {
                this.hdETC_NOT_ID.Value = Request.QueryString["ETC_NOT_ID"];
                ViewState["ETC_NOT_ID"] = Request.QueryString["ETC_NOT_ID"];
                exeGB = "M";

                this.lblRES_Name.Text = Session["sRES_Name"].ToString();

                //덧글 바인딩
                SetList();
            }
            else
            {
                this.lblRES_Name.Text = Session["sRES_Name"].ToString();
            }      
        }

        if (!string.IsNullOrEmpty(Request.QueryString["ETC_NOT_ID"]))
        {
            exeGB = "M";
        }

    }    

    protected DataSet Select_NoticeData()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_ETC_NOT_SELECT_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@ETC_NOT_ID", ViewState["ETC_NOT_ID"]);
        adp.SelectCommand.Parameters.AddWithValue("@RES_ID", int.Parse((string)Session["sRES_ID"]));

        DataSet ds = new DataSet();

        try
        {
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }

    /// <summary>
    /// 부문/부서 목록을 가져온다
    /// </summary>
    private DataSet Select_RES_RBS_List(string strGB, string strSECT_CD)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_RBS_LIST_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@GB", strGB);
        adp.SelectCommand.Parameters.AddWithValue("@SECT_CD", strSECT_CD);

        DataSet ds = new DataSet();

        try
        {
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }    

    // 덧글 첨부파일 저장 부분 수정 
    private void attachSaveComment()
    {
        string postFileCode = ViewState["RBS"].ToString();
        string orgFileName = string.Empty;
        string serverRoot = @"D:\EPM_Attatch\Notice\{0}\{1}\{2}"; //파일 저장 폴더 위치
        string saveFileName = string.Empty;
        string saveFilePath = string.Empty;
        // First we check to see if the user has selected a file
        //Response.Write("0");
        if (Request.Files != null)
        {
            //Response.Write(Request.Files.Count);
            for (int i = 0; i < Request.Files.Count; i++)
            {
                //Response.Write("2");
                HttpPostedFile postedFile = Request.Files[i] as HttpPostedFile;
                orgFileName = Path.GetFileName(postedFile.FileName);
                saveFileName = DateTime.Today.ToShortDateString() + "_" + orgFileName;
                saveFilePath = string.Format(serverRoot, DateTime.Now.Year, postFileCode, saveFileName);
                //Response.Write(saveFilePath);
                if (orgFileName != "")
                {
                    bool blReturn = false;
                    string tmpfileName = string.Empty;
                    string tmpSavefileName = string.Empty;

                    string folderPath = Path.GetDirectoryName(saveFilePath);
                    string tmpExt = Path.GetExtension(postedFile.FileName);

                    try
                    {
                        if (!Directory.Exists(folderPath)) // 디렉토리 없을 시 새로 만들기
                            Directory.CreateDirectory(folderPath);

                        tmpfileName = saveFileName;
                        tmpSavefileName = Path.Combine(folderPath, tmpfileName);

                        if (File.Exists(tmpSavefileName)) // 파일 중복 체크하여 저장 이름 변경
                        {
                            int j = 1;
                            string tmpName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                            do
                            {
                                tmpfileName = string.Format("{0}({1}){2}", tmpName, j, tmpExt);
                                tmpSavefileName = Path.Combine(folderPath, DateTime.Today.ToShortDateString() + "_" + tmpfileName);
                                j++;
                            } while (File.Exists(tmpSavefileName));
                            orgFileName = tmpfileName; //원본 파일 이름
                            saveFileName = DateTime.Today.ToShortDateString() + "_" + tmpfileName; // 파일 이름 변경
                            saveFilePath = string.Format(serverRoot, DateTime.Now.Year, postFileCode, saveFileName); // 저장 폴더 위치
                        }

                        postedFile.SaveAs(tmpSavefileName); // 실제 파일 저장
                        SaveFile(saveFileName, saveFilePath);
                        blReturn = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //postedFile.SaveAs(saveFilePath);                   
                }
            }
        }
    }

    // 덧글 파일 첨부 저장
    protected void SaveFile(string saveFile, string savePath)
    {
        bool bError = false;
        SqlConnection ConAtt = null;
        SqlTransaction trans = null;

        try
        {
            ConAtt = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            ConAtt.Open();

            SqlCommand CmdAtt = new SqlCommand("EPM_ATTATCH_SUBMIT", ConAtt);
            CmdAtt.CommandType = CommandType.StoredProcedure;
            trans = ConAtt.BeginTransaction();
            CmdAtt.Transaction = trans;

            CmdAtt.Parameters.AddWithValue("@ATT_GB", "COM");
            CmdAtt.Parameters.AddWithValue("@ATT_GB_ID", ViewState["NOT_COMMENT_ID"]);
            CmdAtt.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"]);
            CmdAtt.Parameters.AddWithValue("@FILE_NAME", saveFile);
            CmdAtt.Parameters.AddWithValue("@FILE_PATH", savePath);

            //Response.Write(Cmd.Parameters.Count);
            //return;
            CmdAtt.ExecuteNonQuery();

            trans.Commit();

        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (ConAtt != null)
            {
                ConAtt.Close();
                ConAtt = null;
            }
        }    
    }

    // 덧글 목록 조회
    private DataSet SelectComment_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ETC_NOTICE_COMMENT_SELECT_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@ETC_NOT_ID", Request.QueryString["ETC_NOT_ID"]);

        DataSet ds = new DataSet();

        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }

    // 덧글 바인딩
    private void SetList()
    {
        DataSet dsList = null;
        dsList = SelectComment_List();     

        if(dsList.Tables[1].Rows.Count !=0)
            lblNoticeTitle.Text = "[공지] " + dsList.Tables[1].Rows[0]["ETC_NOT_Title"].ToString();
            btnComment.Text = "본문 보기";

        if (dsList.Tables[0].Rows.Count != 0)
        {
            rowCnt = dsList.Tables[0].Rows.Count;
            lblCommentCnt.Text = rowCnt.ToString();          

            gvNoticeList.DataSource = dsList.Tables[0];
            gvNoticeList.DataBind();

        }        
          else
        {
            //  EmptyData 일때 Header 표시
            dsList.Tables[0].Rows.Add(dsList.Tables[0].NewRow());
            gvNoticeList.DataSource = dsList.Tables[0];
            gvNoticeList.DataBind();

            int columnCount = gvNoticeList.Rows[0].Cells.Count;
            gvNoticeList.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvNoticeList.Rows[0].Height = 30;
            gvNoticeList.Rows[0].HorizontalAlign = HorizontalAlign.Center;
            gvNoticeList.Rows[0].VerticalAlign = VerticalAlign.Middle;
            gvNoticeList.Rows[0].Cells[0].Text = "데이터가 없습니다.";

            for (int i = 1; i < columnCount; i++)
            {
                gvNoticeList.Rows[0].Cells[i].Visible = false;
            }
        }
    }

    //덧글 파일 첨부 삭제
    protected void attDeleteComment()
    {

        bool bError = false;
        SqlConnection ConAtt = null;
        SqlTransaction trans = null;

        try
        {
            ConAtt = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            ConAtt.Open();

            string[] delAtt = hdnAttDeleteComment.Value.ToString().TrimEnd(',').Split(',');

            for (int i = 0; i < delAtt.Length; i++)
            {
                //Response.Write(delAtt[i].ToString());
                SqlCommand CmdAtt = new SqlCommand("EPM_ATTATCH_SUBMIT", ConAtt);
                CmdAtt.CommandType = CommandType.StoredProcedure;
                trans = ConAtt.BeginTransaction();
                CmdAtt.Transaction = trans;

                CmdAtt.Parameters.AddWithValue("@ATT_GB", "D");
                CmdAtt.Parameters.AddWithValue("@ATT_ID", Convert.ToInt32(delAtt[i].ToString()));

                //Response.Write(Cmd.Parameters.Count);
                //return;
                CmdAtt.ExecuteNonQuery();

                trans.Commit();
            }
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (ConAtt != null)
            {
                ConAtt.Close();
                ConAtt = null;
            }
        }

    }

     // 덧글 저장 버튼
    protected void btnCommentSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand Cmd = new SqlCommand("EPM_ETC_NOTICE_COMMENT_SUBMIT", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@EXE_GB", "I");
            Cmd.Parameters.AddWithValue("@ETC_NOT_ID", Request.QueryString["ETC_NOT_ID"]);
            Cmd.Parameters.AddWithValue("@ETC_NOT_COMMENT_CONTENTS", txtETC_NOT_Comment.Text);
            Cmd.Parameters.AddWithValue("@RES_ID", int.Parse((string)Session["sRES_ID"]));
            Cmd.Parameters.AddWithValue("@RES_NAME", Session["sRES_Name"].ToString());

            if (ViewState["GB_COMMENT"] == "I")
            {
                ViewState["NOT_COMMENT_ID"] = Cmd.ExecuteScalar();
                //Response.Write(ViewState["NOT_COMMENT_ID"]);
            }
            else
                Cmd.ExecuteNonQuery();

            attachSaveComment();
            if (hdnAttDeleteComment.Value != "")
            {
                attDeleteComment();
            }

            trans.Commit();
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (Con != null)
            {
                Con.Close();
                Con = null;
            }
        }

        if (!bError)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type='text/javascript'>alert('저장되었습니다.');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());

            txtETC_NOT_Comment.Text = null;
            SetList();//댓글 바인딩 위치
        }
    }  

    // 덧글 삭제 버튼
    protected void btnCommentDelete_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        int CID = Convert.ToInt32(hdnCommentDeleteID.Value);

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand Cmd = new SqlCommand("EPM_ETC_NOTICE_COMMENT_SUBMIT", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@EXE_GB", "D");
            Cmd.Parameters.AddWithValue("@ETC_NOT_COMMENT_ID", CID);
      
            Cmd.ExecuteNonQuery();
            CommentDeleteFile();

            trans.Commit();
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (Con != null)
            {
                Con.Close();
                Con = null;
            }
        }

        if (!bError)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type='text/javascript'>alert('삭제되었습니다.');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());

            txtETC_NOT_Comment.Text = null;
            SetList();//댓글 바인딩 위치
        }
    }

    // 덧글 삭제시 파일 삭제
    protected void CommentDeleteFile()
    {

        bool bError = false;
        SqlConnection ConAtt = null;
        SqlTransaction trans = null;

        int CID = Convert.ToInt32(hdnCommentDeleteID.Value);

        try
        {
            ConAtt = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            ConAtt.Open();

            //Response.Write(delAtt[i].ToString());
            SqlCommand CmdAtt = new SqlCommand("EPM_ATTATCH_SUBMIT", ConAtt);
            CmdAtt.CommandType = CommandType.StoredProcedure;
            trans = ConAtt.BeginTransaction();
            CmdAtt.Transaction = trans;

            CmdAtt.Parameters.AddWithValue("@ATT_GB", "COM_DEL");
            CmdAtt.Parameters.AddWithValue("@ATT_GB_ID", CID);

            //Response.Write(Cmd.Parameters.Count);
            //return;
            CmdAtt.ExecuteNonQuery();

            trans.Commit();
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (ConAtt != null)
            {
                ConAtt.Close();
                ConAtt = null;
            }
        }

    }

    // 덧글 Row 바인딩 속성
    protected void gvNoticeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["sRES_RBS_CD"].ToString().Trim() == "1111" || ((DataRowView)e.Row.DataItem)["RES_ID"].ToString().Trim() == Session["sRES_ID"].ToString().Trim())  // 일반관리직(1111)이거나 작성자 본인일 경우 덧글 삭제 활성화
            {              
                if (((DataRowView)e.Row.DataItem)["ETC_NOT_FILE_CNT"].ToString() != "0")
                {
                    e.Row.Cells[0].Text = ((DataRowView)e.Row.DataItem)["RES_Name"].ToString()
                    + "<br /><span style=\"color:#666; font-size:.9em;\">" + ((DataRowView)e.Row.DataItem)["COM_CREATED_DATE"].ToString()
                    + "</span><br />" + "<span style=\"color:#880000; cursor:pointer;\" onclick=\"javascript:return fncCommentDelete(" + ((DataRowView)e.Row.DataItem)["ETC_NOT_COMMENT_ID"].ToString() + ");\">[삭제]</span>"
                    + "<br /><br />"
                    + "<ul><li onclick=\"toggle_display('div_com" + ((DataRowView)e.Row.DataItem)["ETC_NOT_COMMENT_ID"].ToString()
                    + "');\" class=\"btnList\"><input type=\"button\" style=\"width: 70px;\" value=\"파일(" + ((DataRowView)e.Row.DataItem)["ETC_NOT_FILE_CNT"].ToString()
                    + ")\" class=\"button gray mepm_asp_btn\" /></li><li id=\"div_com" + ((DataRowView)e.Row.DataItem)["ETC_NOT_COMMENT_ID"].ToString() + "\" style=\"display: none;\">"
                    + "<ul title=\"파일명을 클릭하시면 다운로드됩니다.\" class=\"fileList\">"
                    + "<li>" + ((DataRowView)e.Row.DataItem)["ETC_NOT_FILE"].ToString() + "</li>"
                    + "</ul></li></ul>";
                }
                else
                {
                    e.Row.Cells[0].Text = ((DataRowView)e.Row.DataItem)["RES_Name"].ToString()
                    + "<br /><span style=\"color:#666; font-size:.9em;\">" + ((DataRowView)e.Row.DataItem)["COM_CREATED_DATE"].ToString()
                    + "</span><br />" + "<span style=\"color:#880000; cursor:pointer;\" onclick=\"javascript:return fncCommentDelete(" + ((DataRowView)e.Row.DataItem)["ETC_NOT_COMMENT_ID"].ToString() + ");\">[삭제]</span>"
                    + "<br /><br />" +
                        "";
                }
            }
            else
            {
                if (((DataRowView)e.Row.DataItem)["ETC_NOT_FILE_CNT"].ToString() != "0")
                {
                    e.Row.Cells[0].Text = ((DataRowView)e.Row.DataItem)["RES_Name"].ToString()
                    + "<br /><span style=\"color:#666; font-size:.9em;\">" + ((DataRowView)e.Row.DataItem)["COM_CREATED_DATE"].ToString()
                    + "</span><br />"
                    + "<br /><br />"
                    + "<ul><li onclick=\"toggle_display('div_com" + ((DataRowView)e.Row.DataItem)["ETC_NOT_COMMENT_ID"].ToString()
                    + "');\" class=\"btnList\"><input type=\"button\" style=\"width: 70px;\" value=\"파일(" + ((DataRowView)e.Row.DataItem)["ETC_NOT_FILE_CNT"].ToString()
                    + ")\" class=\"button gray mepm_asp_btn\" /></li><li id=\"div_com" + ((DataRowView)e.Row.DataItem)["ETC_NOT_COMMENT_ID"].ToString() + "\" style=\"display: none; \">"
                    + "<ul title=\"파일명을 클릭하시면 다운로드됩니다.\" class=\"fileList\">"
                    + "<li>" + ((DataRowView)e.Row.DataItem)["ETC_NOT_FILE"].ToString() + "</li>"
                    + "</ul></li></ul>";

                }
                else
                {
                    e.Row.Cells[0].Text = ((DataRowView)e.Row.DataItem)["RES_Name"].ToString()
                    + "<br /><span style=\"color:#666; font-size:.9em;\">" + ((DataRowView)e.Row.DataItem)["COM_CREATED_DATE"].ToString()
                    + "</span><br />"
                    + "<br /><br />" +
                        "";
                }
            }
        }

    }
}