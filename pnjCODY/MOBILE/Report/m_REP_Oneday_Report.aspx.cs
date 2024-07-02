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

public partial class MOBILE_Report_m_REP_Oneday_Report : System.Web.UI.Page
{
    // 로그인 사용자의 부문 코드
    string RES_RBS_CD = "";
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";
    // 입력 구분
    string exeGB = "I";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

        //로그인 사용자의 부문 코드
        RES_RBS_CD = Session["sRES_RBS_CD"].ToString();

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
        }

        if (!string.IsNullOrEmpty(Request.QueryString["REP_DLY_ID"]))
        {
            exeGB = "M";
        }

        if (!IsPostBack)
        {            
            SetControl();

            if (!string.IsNullOrEmpty(Request.QueryString["REP_DLY_ID"]))
            {
                this.hdREP_DLY_ID.Value = Request.QueryString["REP_DLY_ID"];
                ViewState["REP_DLY_ID"] = Request.QueryString["REP_DLY_ID"];
                exeGB = "M";
                //this.btnDelete.Visible = true;

                //일일업무보고 내용
                SetReportData();
            }

            

            /* 서버 장애로 업무일지 작성을 못했던 기간에 입력 가능하도록 한시적으로 변경 2020-02-14 박병진*/

            DateTime dt = DateTime.Parse(this.txtVISITDATE.Text);

            if(dt <= DateTime.Parse("2020-02-06")) 
            {
                Page.Response.Write(dt.ToString());

                this.btnSave.Visible = false;
            }
        }      
    }

    /// <summary>
    /// 기본 컨트롤 셋팅
    /// </summary>
    private void SetControl()
    {
        //방문일
        // this.txtVISITDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");

        /* 서버 장애로 업무일지 작성을 못했던 기간에 입력 가능하도록 한시적으로 변경 2020-02-14 박병진*/

        this.txtVISITDATE.Text = Request.QueryString["VISIT_DATE"].ToString();
        txtVISITDATE.Enabled = false;

        if (!string.IsNullOrEmpty(Request.QueryString["RES_ID"])) // 수정 모드시 작성자 RES_ID 기준으로..
        {
            // 거래처
            Code code = new Code();
            DataSet dsEPM_CUSTOMER_STORE = code.EPM_VISIT_STORE_DROPDOWN_LIST(int.Parse((string)Request.QueryString["RES_ID"]), this.txtVISITDATE.Text, exeGB);

            this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
            this.ddlEPM_CUSTOMER_STORE.DataBind();
        }
        else // 작성시 로그인한 세션의 RES_ID 기준으로..
        {
            // 거래처
            Code code = new Code();
            DataSet dsEPM_CUSTOMER_STORE = code.EPM_VISIT_STORE_DROPDOWN_LIST(int.Parse((string)Session["sRES_ID"]), this.txtVISITDATE.Text, exeGB);

            this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
            this.ddlEPM_CUSTOMER_STORE.DataBind();
        }
    }

    private void SetReportData()
    {
        DataSet dsData = null;

        dsData = Select_ReportData();

        if (dsData.Tables[0].Rows.Count != 0)
        {           
            ddlEPM_CUSTOMER_STORE.SelectedValue = dsData.Tables[0].Rows[0]["REP_DLY_CUST_CD"].ToString() + "," + dsData.Tables[0].Rows[0]["REP_DLY_AREA_CD"].ToString();       
            SetddlREP_DLY_Member();
            if (!string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["REP_DLY_Member_ID"].ToString()))
            {
                hdREP_DLY_Member_ID.Value = dsData.Tables[0].Rows[0]["REP_DLY_Member_ID"].ToString() + ",";
                txtREP_DLY_Member.Text = dsData.Tables[0].Rows[0]["REP_DLY_Member"].ToString() + ",";
            }
            txtVISITDATE.Text = dsData.Tables[0].Rows[0]["REAL_YYYYMMDD"].ToString();
            txtContents.Text = dsData.Tables[0].Rows[0]["Contents"].ToString();

            //거래처와 매장, 방문일은 수정 불가
            ddlEPM_CUSTOMER_STORE.Enabled = false;
            txtVISITDATE.Enabled = false;


            /* 서버 장애로 업무일지 작성을 못했던 기간에 입력 가능하도록 한시적으로 변경
                변경시 125~139 라인 삭제 후 142 ~ 169 주석 해제
             2020-02-14 박병진*/
            {
                //본인이 작성한 글이면 수정가능
                if (Session["sRES_ID"].ToString() == dsData.Tables[0].Rows[0]["RES_ID"].ToString())
                    this.btnSave.Enabled = true;
                else
                    this.btnSave.Enabled = false;

                //관리자는 수정가능
                if (RES_GB == "A")
                    this.btnSave.Enabled = true;

                btnSave.Text = "수정";
                btnSave.OnClientClick = "javascript:return fncChkUpdate();";
                btnCancel.OnClientClick = "javascript:return window.location='m_REP_Oneday_List.aspx';";

            }


    
            ////실적작성은 [당일]만 작성가능 -- 2013.09.23 김재영 수정
            //if (dsData.Tables[0].Rows[0]["REAL_YYYYMMDD"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
            // 수정가능 시간 변경 (현재: 당일, 변경: 당일 오전 7시 ~ 익일 오전 7시 까지, 서포터가 자정이 넘어갔을때 업무보고를 수정할 수 없는 현상 해결을 위함) 2016-08-24 정창화 수정
            if (dsData.Tables[0].Rows[0]["REAL_YYYYMMDD"].ToString() == DateTime.Now.AddHours(-7).ToString("yyyy-MM-dd"))
            {
                //본인이 작성한 글이면 수정가능
                if (Session["sRES_ID"].ToString() == dsData.Tables[0].Rows[0]["RES_ID"].ToString())
                    this.btnSave.Enabled = true;
                else
                    this.btnSave.Enabled = false;

                //관리자는 수정가능
                if (RES_GB == "A")
                    this.btnSave.Enabled = true;

                btnSave.Text = "수정";
                btnSave.OnClientClick = "javascript:return fncChkUpdate();";
                btnCancel.OnClientClick = "javascript:return window.location='m_REP_Oneday_List.aspx';";
            }
            else
            {
                this.btnSave.Visible = false;
                this.btnSaveFake.Visible = true;
                btnSaveFake.Text = "수정";
                btnCancel.OnClientClick = "javascript:return window.location='m_REP_Oneday_List.aspx';";
            }
    
        }

        if (dsData.Tables[1].Rows.Count != 0)
        {
            for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
            {
                lbAttFile.Text += "<a href='" + dsData.Tables[1].Rows[i]["FILE_PATH"].ToString().Remove(0, 3) + "'>"
                                    + dsData.Tables[1].Rows[i]["FILE_NAME"].ToString() + "</a><br />";
            }
        }
    }

    /// <summary>
    /// 일일업무보고 내용 가져오기
    /// </summary>
    /// <returns></returns>
    protected DataSet Select_ReportData()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_REP_DLY_SELECT_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@REP_DLY_ID", ViewState["REP_DLY_ID"]);

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
    /// 매장선택시 매장 근무자 변경
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEPM_CUSTOMER_STORE_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlREP_DLY_Member.Items.Clear();

        if (RES_RBS_CD != "" && this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString() != "")
            SetddlREP_DLY_Member();

        this.hdREP_DLY_Member_ID.Value = "";
        this.txtREP_DLY_Member.Text = "";
    }

    /// <summary>
    /// 근무자 셋팅
    /// </summary>
    private void SetddlREP_DLY_Member()
    {
        string[] Store = this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString().Split(',');

        DataSet ds = Select_REP_DLY_Member(Store[1]);

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.ddlREP_DLY_Member.DataSource = ds.Tables[0];
            this.ddlREP_DLY_Member.DataBind();
        }
    }

    /// <summary>
    /// 매장별 근무자 목록을 가져온다
    /// </summary>
    private DataSet Select_REP_DLY_Member(string strSECT_CD)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_LIST_STORE_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@RES_ASS_StoreID", strSECT_CD);
        adp.SelectCommand.Parameters.AddWithValue("@DATE", txtVISITDATE.Text);

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            //일일업무보고 
            SqlCommand Cmd = new SqlCommand("EPM_REP_DLY_SUBMIT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            string[] Store = this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString().Split(',');

            Cmd.Parameters.AddWithValue("@EXE_GB", exeGB);  // I:신규 , M:수정
            if (exeGB == "M")
                Cmd.Parameters.AddWithValue("@REP_DLY_ID", int.Parse((string)ViewState["REP_DLY_ID"]));

            Cmd.Parameters.AddWithValue("@REP_DLY_CUST_CD", Store[0]);
            Cmd.Parameters.AddWithValue("@REP_DLY_AREA_CD", Store[1]);
            Cmd.Parameters.AddWithValue("@REP_DLY_Member", this.txtREP_DLY_Member.Text.TrimEnd(','));
            Cmd.Parameters.AddWithValue("@REP_DLY_Member_ID", this.hdREP_DLY_Member_ID.Value.TrimEnd(','));
            Cmd.Parameters.AddWithValue("@REAL_YYYYMMDD", this.txtVISITDATE.Text);
            Cmd.Parameters.AddWithValue("@Contents", this.txtContents.Text);
            Cmd.Parameters.AddWithValue("@RES_ID", int.Parse((string)Session["sRES_ID"]));
            Cmd.Parameters.AddWithValue("@RES_Name", Session["sRES_Name"].ToString());

            if (exeGB == "I")
                ViewState["REP_DLY_ID"] = Cmd.ExecuteScalar();
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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_REP_Oneday_List.aspx");
        }
    }

    // 삭제 버튼 클릭 시
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            //일일업무보고 
            SqlCommand Cmd = new SqlCommand("EPM_REP_DLY_SUBMIT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@EXE_GB", "D");  
            Cmd.Parameters.AddWithValue("@REP_DLY_ID", int.Parse((string)ViewState["REP_DLY_ID"]));
            
            Cmd.ExecuteNonQuery();

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
            Common.scriptAlert(this.Page, "삭제되었습니다.", "m_REP_Oneday_List.aspx");
        }
    }

    #region 첨부 파일 폴더에 저장
    protected void btnUploadFile_Click()
    {
        string postFileCode = Session["sRES_RBS_NAME"].ToString();
        string orgFileName = string.Empty;
        string serverRoot = @"D:\EPM_Attatch\Report\{0}\{1}\{2}"; //파일 저장 폴더 위치
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
                            saveFileName = postFileCode + "_" + tmpfileName; // 파일 이름 변경
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

            CmdAtt.Parameters.AddWithValue("@ATT_GB", "REP");
            CmdAtt.Parameters.AddWithValue("@ATT_GB_ID", ViewState["REP_DLY_ID"]);
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
}