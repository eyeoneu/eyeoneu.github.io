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

public partial class MOBILE_Notice_m_NOT_WORKER_Write : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";
    // 입력 구분
    string exeGB = "I";
    // 댓글 개수
    public int rowCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

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
            this.btnEditFake.Visible = true;          
            this.btnSave.Visible = false;
        }

        if (!IsPostBack)
        {
          
            SetddlCategory();

            if (!string.IsNullOrEmpty(Request.QueryString["ETC_NOT_ID"]))
            {
                this.hdETC_NOT_ID.Value = Request.QueryString["ETC_NOT_ID"];
                ViewState["ETC_NOT_ID"] = Request.QueryString["ETC_NOT_ID"];
                exeGB = "M";

                //공지사항 내용
                SetNoticeData();

            }
            else
            {
                
                this.lblCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
             
                this.btnEdit.Visible = false;          
            }

          
        }

        if (!string.IsNullOrEmpty(Request.QueryString["ETC_NOT_ID"]))
        {
            exeGB = "M";
        }

    }
   /// <summary>
    /// 공지사항 내용 셋팅
    /// </summary>
    private void SetNoticeData()
    {
        DataSet dsData = null;

        dsData = Select_NoticeData();
        if (dsData.Tables[0].Rows.Count != 0)
        {
          
            lblCreateDate.Text = dsData.Tables[0].Rows[0]["CREATED_DATE"].ToString();
           
            lblETC_NOT_Title.Text = dsData.Tables[0].Rows[0]["ETC_NOT_Title"].ToString();
            txtETC_NOT_Title.Text = dsData.Tables[0].Rows[0]["ETC_NOT_Title"].ToString();
            lblETC_NOT_Contents.Text = dsData.Tables[0].Rows[0]["ETC_NOT_Contents"].ToString().Replace("\r\n", "</br>");
            txtETC_NOT_Contents.Text = dsData.Tables[0].Rows[0]["ETC_NOT_Contents"].ToString();
           
        
            
            lb_Category.Text = dsData.Tables[0].Rows[0]["ETC_CATEGORY"].ToString();
            ddl_Category.SelectedValue = dsData.Tables[0].Rows[0]["ETC_CATEGORY"].ToString();

          
            txtETC_NOT_Title.Visible = false;
            txtETC_NOT_Contents.Visible = false;
            
            btnSave.Visible = false;
            ddl_Category.Visible = false;

            if (Convert.ToString(Session["sRES_ID"]) == dsData.Tables[0].Rows[0]["RES_ID"].ToString())
            {                
                btnEdit.Visible = true;
                btnEditFake.Visible = false;
            }
            else
            {               
                btnEdit.Visible = false;
                btnEditFake.Visible = true;
            }

            //if (Session["sRES_ID"] == dsData.Tables[0].Rows[0]["RES_ID"].ToString())
            //    btnSave.OnClientClick = "javascript:return fncChkUpdate();";
            //else
            //    btnSave.Enabled = false;
            //btnSave.Text = "수정";
            btnCancel.OnClientClick = "javascript:return window.location='/Notice/m_NOT_WORKER_List.aspx'";
        }

        if (dsData.Tables[1].Rows.Count != 0)
        {
            for (int i = 0; i < dsData.Tables[1].Rows.Count ; i++)
            {
                lbAttFile.Text += "<a href='" + dsData.Tables[1].Rows[i]["FILE_PATH"].ToString().Remove(0, 3) + "' target=\"_blank\">"
                                    + dsData.Tables[1].Rows[i]["FILE_NAME"].ToString() + "</a><br />";
            }
        }
    }

    protected DataSet Select_NoticeData()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_ETC_NOT_SELECT_WORKER_MOBILE", Con);
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
    /// 저장시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 
    #region 저장 활성화
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        lblETC_NOT_Title.Visible = false;
        lblETC_NOT_Contents.Visible = false;
       
        lb_Category.Visible = false;
        btnEdit.Visible = false;
        txtETC_NOT_Title.Visible = true;
        txtETC_NOT_Contents.Visible = true;
     
        btnEditComplate.Visible = true;
        ddl_Category.Visible = true;
    } 
    #endregion

    #region 저장
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;


        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            //공지사항 
            SqlCommand Cmd = new SqlCommand("EPM_ETC_NOT_SUBMIT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@EXE_GB", exeGB);  // I:신규 , M:수정
            if (exeGB == "M")
                Cmd.Parameters.AddWithValue("@ETC_NOT_ID", int.Parse((string)ViewState["ETC_NOT_ID"]));

         
            Cmd.Parameters.AddWithValue("@ETC_NOT_Title", this.txtETC_NOT_Title.Text);
            Cmd.Parameters.AddWithValue("@ETC_NOT_Contents", this.txtETC_NOT_Contents.Text);
            Cmd.Parameters.AddWithValue("@RES_ID", int.Parse((string)Session["sRES_ID"]));
            Cmd.Parameters.AddWithValue("@RES_Name", Session["sRES_Name"].ToString());

            Cmd.Parameters.AddWithValue("@ETC_CATEGORY", this.ddl_Category.Text);

            if (exeGB == "I")
                ViewState["ETC_NOT_ID"] = Cmd.ExecuteScalar();
            else
                Cmd.ExecuteNonQuery();

            //최초 입력일 경우 공지대상 알림
            if (exeGB == "I")
            {
                string[] arrItem = null;
                string[] arrValue = null;
                string arrValue1, arrValue2 = null;

                arrItem = this.hdETC_NOT_Member.Value.ToString().Split(',');

                for (int i = 0; i < arrItem.Length - 1; i++)
                {
                    arrValue = arrItem[i].Split('|');

                    arrValue1 = arrValue[0].ToString();//부문
                    arrValue2 = arrValue[1].ToString();//지역
                    arrValue2 = arrValue2.Replace("A", "");//전체선택시 조건제거

                    //공지대상 입력
                    SqlCommand CmdRead = new SqlCommand("EPM_ETC_NOT_READ_SUBMIT_MOBILE", Con);
                    CmdRead.CommandType = CommandType.StoredProcedure;
                    CmdRead.Transaction = trans;

                    CmdRead.Parameters.AddWithValue("@EXE_GB", "I");  // I:신규
                    CmdRead.Parameters.AddWithValue("@ETC_NOT_ID", ViewState["ETC_NOT_ID"].ToString());
                    CmdRead.Parameters.AddWithValue("@RES_RBS_CD", arrValue1);
                    CmdRead.Parameters.AddWithValue("@RES_RBS_AREA_CD", arrValue2);

                    try
                    {
                        CmdRead.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        bError = true;
                        trans.Rollback();
                        Response.Write(ex.Message);
                        Response.End();
                    }
                }
            }

            btnUploadFile_Click();

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_NOT_List.aspx");
        }
    } 
    #endregion

    #region 첨부 파일 폴더에 저장
    protected void btnUploadFile_Click()
    {
        string postFileCode = "";
        string orgFileName = string.Empty;
        string serverRoot = @"D:\EPM_Attatch\Notice\{0}\{1}\{2}"; //파일 저장 폴더 위치
        string saveFileName = string.Empty;
        string saveFilePath = string.Empty;
        // First we check to see if the user has selected a file
        if (upFile.HasFile)
        {
            //Response.Write(Request.Files.Count);
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile postedFile = Request.Files[i] as HttpPostedFile;
                orgFileName = Path.GetFileName(postedFile.FileName);
                saveFileName = DateTime.Today.ToShortDateString() + "_" + orgFileName;
                saveFilePath = string.Format(serverRoot, DateTime.Now.Year, postFileCode, saveFileName);

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
                }
            }
        }

    } 
    #endregion

    #region 파일 첨부 DB 저장
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

            CmdAtt.Parameters.AddWithValue("@ATT_GB", "NOT");
            CmdAtt.Parameters.AddWithValue("@ATT_GB_ID", ViewState["ETC_NOT_ID"]);
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
    #endregion

 



    #region 카테고리 DDL 바인딩
    protected void SetddlCategory()
    {
        DataSet ds = null;
        ds = Select_CodeList("24");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_NAME"].ToString();

            ListItem tempItem = new ListItem(code_name, code_name);
            ddl_Category.Items.Add(tempItem);
        }
    }
    #endregion

    #region EPM 코드리스트 조회
    private DataSet Select_CodeList(string Code_Category)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CODE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@CODE_CATEGORY", Code_Category);

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
    #endregion

}