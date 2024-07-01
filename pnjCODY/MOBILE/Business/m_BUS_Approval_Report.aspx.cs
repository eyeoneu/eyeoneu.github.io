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
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;

public partial class m_BUS_Approval_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Page.Request["APP_ID"].ToString()))
            ViewState["APP_ID"] = this.Page.Request["APP_ID"].ToString();

        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["APP_ID"].ToString() != "0")
        {
            DataSet ds = Select_APP_Detail();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_REQUEST_YYYYMMDD"].ToString()))
                    txtAPP_REQUEST_YYYYMMDD.Text = ds.Tables[0].Rows[0]["APP_REQUEST_YYYYMMDD"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_NAME"].ToString()))
                    txtAPP_NAME.Text = ds.Tables[0].Rows[0]["APP_NAME"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_BIRTH_YYYYMMDD"].ToString()))
                    txtAPP_BIRTH_YYYYMMDD.Text = ds.Tables[0].Rows[0]["APP_BIRTH_YYYYMMDD"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_VENDER"].ToString()))
                    txtAPP_VENDER.Text = ds.Tables[0].Rows[0]["APP_VENDER"].ToString();
           
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_VISIT_STORE"].ToString()))
                    txtAPP_VISIT_STORE.Text = ds.Tables[0].Rows[0]["APP_VISIT_STORE"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_START_YYYYMMDD"].ToString()))
                    txtAPP_START_YYYYMMDD.Text = ds.Tables[0].Rows[0]["APP_START_YYYYMMDD"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_FINISH_YYYYMMDD"].ToString()))
                    txtAPP_FINISH_YYYYMMDD.Text = ds.Tables[0].Rows[0]["APP_FINISH_YYYYMMDD"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_WORK_DAY"].ToString()))
                    txtAPP_WORK_DAY.Text = ds.Tables[0].Rows[0]["APP_WORK_DAY"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_CAREER"].ToString()))
                    txtAPP_CAREER.Text = ds.Tables[0].Rows[0]["APP_CAREER"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_REASON"].ToString()))
                    txtAPP_REASON.Text = ds.Tables[0].Rows[0]["APP_REASON"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_REMARK"].ToString()))
                    txtAPP_REMARK.Text = ds.Tables[0].Rows[0]["APP_REMARK"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["APP_Status"].ToString()))
                {
                    txtAPP_REQUEST_YYYYMMDD.Attributes["class"] = "i_f_out";
                    txtAPP_BIRTH_YYYYMMDD.Attributes["class"] = "i_f_out";
                    txtAPP_START_YYYYMMDD.Attributes["class"] = "i_f_out";
                    txtAPP_FINISH_YYYYMMDD.Attributes["class"] = "i_f_out";
                    txtAPP_CAREER.Attributes["class"] = "i_f_out";

                    if (ds.Tables[0].Rows[0]["APP_STATUS"].ToString().Trim().Equals("A"))
                    {
                        this.btnSave.Visible = false;
                        txtAPP_REQUEST_YYYYMMDD.Enabled = false;
                        txtAPP_NAME.Enabled = false;
                        txtAPP_BIRTH_YYYYMMDD.Enabled = false;
                        txtAPP_VENDER.Enabled = false;
                        txtAPP_VISIT_STORE.Enabled = false;
                        txtAPP_START_YYYYMMDD.Enabled = false;
                        txtAPP_FINISH_YYYYMMDD.Enabled = false;
                        txtAPP_WORK_DAY.Enabled = false;
                        txtAPP_CAREER.Enabled = false;
                        txtAPP_REASON.Enabled = false;
                        txtAPP_REMARK.Enabled = false;
                        upFile.Visible = false;

                    }
                }
            }

            if (ds.Tables[1].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    lbAttFile.Text += "<a href='" + ds.Tables[1].Rows[i]["FILE_PATH"].ToString().Remove(0, 3) + "' target=\"_blank\">"
                                        + ds.Tables[1].Rows[i]["FILE_NAME"].ToString() + "</a><br />";
                }
            }
        }
    }

    private DataSet Select_APP_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_APPROVAL_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@APP_ID", this.Page.Request["APP_ID"].ToString());

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

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand Cmd = new SqlCommand("EPM_APPROVAL_INSERT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@APP_ID", this.Page.Request["APP_ID"].ToString());
            Cmd.Parameters.AddWithValue("@APP_SUPPORTER_ID", Session["sRES_ID"].ToString());
            Cmd.Parameters.AddWithValue("@APP_BIRTH_YYYYMMDD", txtAPP_BIRTH_YYYYMMDD.Text);
            Cmd.Parameters.AddWithValue("@APP_NAME", txtAPP_NAME.Text);
            Cmd.Parameters.AddWithValue("@APP_VENDER", txtAPP_VENDER.Text);
            Cmd.Parameters.AddWithValue("@APP_VISIT_STORE", txtAPP_VISIT_STORE.Text);
            Cmd.Parameters.AddWithValue("@APP_REQUEST_YYYYMMDD", txtAPP_REQUEST_YYYYMMDD.Text);
            Cmd.Parameters.AddWithValue("@APP_WORK_DAY", txtAPP_WORK_DAY.Text);
            Cmd.Parameters.AddWithValue("@APP_START_YYYYMMDD", txtAPP_START_YYYYMMDD.Text);
            Cmd.Parameters.AddWithValue("@APP_FINISH_YYYYMMDD", txtAPP_FINISH_YYYYMMDD.Text);
            Cmd.Parameters.AddWithValue("@APP_CAREER", txtAPP_CAREER.Text);
            Cmd.Parameters.AddWithValue("@APP_REASON", txtAPP_REASON.Text);
            Cmd.Parameters.AddWithValue("@APP_REMARK", txtAPP_REMARK.Text);

            if (this.Page.Request["APP_ID"].ToString() == "0")
                ViewState["APP_ID"] = Cmd.ExecuteScalar();
            else
                Cmd.ExecuteNonQuery();

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_BUS_Approval_List.aspx");
        }
    }


    #region 첨부 파일 폴더에 저장
    protected void btnUploadFile_Click()
    {
        string postFileCode = Session["sRES_RBS_AREA_NAME"].ToString(); // SetddlRES_RBS_Lv2(Session["sRES_RBS_CD"].ToString());
        string orgFileName = string.Empty;
        string serverRoot = @"D:\EPM_Attatch\Approval\{0}\{1}\{2}"; //파일 저장 폴더 위치
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
                        AtaaatchSubmit(saveFileName, saveFilePath);
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
    protected void AtaaatchSubmit(string saveFile, string savePath)
    {
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

            CmdAtt.Parameters.AddWithValue("@ATT_GB", "APP");
            CmdAtt.Parameters.AddWithValue("@ATT_GB_ID", ViewState["APP_ID"].ToString());
            CmdAtt.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
            CmdAtt.Parameters.AddWithValue("@FILE_NAME", saveFile);
            CmdAtt.Parameters.AddWithValue("@FILE_PATH", savePath);

            //Response.Write(Cmd.Parameters.Count);
            //return;
            CmdAtt.ExecuteNonQuery();

            trans.Commit();

        }
        catch (Exception ex)
        {
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


    // 파일명 중복 검사
    private string GetFilePath(string strBaseDirTemp, string strFileNameTemp)
    {
        string strName = //순수파일명
            Path.GetFileNameWithoutExtension(strFileNameTemp);
        string strExt =		//확장자
            Path.GetExtension(strFileNameTemp);
        bool blnExists = true;
        int i = 0;
        while (blnExists)
        {
            //Path.Combine(경로, 파일명) = 경로+파일명
            if (File.Exists(Path.Combine(strBaseDirTemp, strFileNameTemp)))
            {
                strFileNameTemp =
                  strName + "(" + ++i + ")" + strExt;//Test(3).txt
            }
            else
            {
                blnExists = false;
            }
        }
        return strFileNameTemp;
    }
}