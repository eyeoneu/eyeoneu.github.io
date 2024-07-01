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

public partial class m_BUS_Accident_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Page.Request["ACC_ID"].ToString()))
            ViewState["ACC_ID"] = this.Page.Request["ACC_ID"].ToString();

        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            SetddlCustomer();
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["ACC_ID"].ToString() != "0")
        {
            DataSet ds = Select_Acc_Detail();

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region ddl 바인딩
                ddlCustomer.SelectedValue = ds.Tables[0].Rows[0]["ACC_Customer"].ToString();

                ddlStore.Items.Clear();
                ddlStore.Enabled = true;

                DataSet ds1 = null;
                ds1 = Select_StoreList("CUS", ds.Tables[0].Rows[0]["ACC_Customer"].ToString());

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    string code_name = dr["COD_NM"].ToString();
                    string code = dr["COD_CD"].ToString();

                    ListItem tempItem = new ListItem(code_name, code);
                    ddlStore.Items.Add(tempItem);
                }

                ddlStore.SelectedValue = ds.Tables[0].Rows[0]["ACC_Store"].ToString();
                #endregion

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_OccurDate"].ToString()))
                    txtAccDate.Text = ds.Tables[0].Rows[0]["ACC_OccurDate"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_RES_NAME"].ToString()))
                    lblResName.Text = ds.Tables[0].Rows[0]["ACC_RES_NAME"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_RES_POSITION"].ToString()))
                    lblPosition.Text = ds.Tables[0].Rows[0]["ACC_RES_POSITION"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_RES_JOIN_DATE"].ToString()))
                    lblJoinDate.Text = ds.Tables[0].Rows[0]["ACC_RES_JOIN_DATE"].ToString();
           
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_When"].ToString()))
                    txtAccWhen.Text = ds.Tables[0].Rows[0]["ACC_When"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Where"].ToString()))
                    txtAccWhere.Text = ds.Tables[0].Rows[0]["ACC_Where"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Who"].ToString()))
                    txtAccWho.Text = ds.Tables[0].Rows[0]["ACC_Who"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_What"].ToString()))
                    txtAccWhat.Text = ds.Tables[0].Rows[0]["ACC_What"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_How"].ToString()))
                    txtAccHow.Text = ds.Tables[0].Rows[0]["ACC_How"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Why"].ToString()))
                    txtAccWhy.Text = ds.Tables[0].Rows[0]["ACC_Why"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Reason"].ToString()))
                    txtAccReason.Text = ds.Tables[0].Rows[0]["ACC_Reason"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Remark"].ToString()))
                    txtRemark.Text = ds.Tables[0].Rows[0]["ACC_Remark"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_RES_ID"].ToString()))
                    hdRES_ID.Value = ds.Tables[0].Rows[0]["ACC_RES_ID"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Place"].ToString()))
                    txtAccPlace.Text = ds.Tables[0].Rows[0]["ACC_Place"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ACC_Status"].ToString()))
                {
                    if (ds.Tables[0].Rows[0]["ACC_Status"].ToString().Trim().Equals("산재처리")
                        || ds.Tables[0].Rows[0]["ACC_Status"].ToString().Trim().Equals("공상처리"))
                    {
                        this.btnSave.Visible = false;
                        txtAccDate.Enabled = false;
                        ddlStore.Enabled = false;
                        ddlCustomer.Enabled = false;
                        txtAccWhen.Enabled = false;
                        txtAccWhere.Enabled = false;
                        txtAccWho.Enabled = false;
                        txtAccWhat.Enabled = false;
                        txtAccHow.Enabled = false;
                        txtAccWhy.Enabled = false;
                        txtAccReason.Enabled = false;
                        txtRemark.Enabled = false;
                        upFile.Visible = false;
                        txtResName.Enabled = false;
                        btnSearch.Enabled = false;
                        txtAccPlace.Enabled = false;
                        Acc_SubScript.Visible = true;

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

    private DataSet Select_Acc_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ACCIDENT_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@ACC_ID", this.Page.Request["ACC_ID"].ToString());

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

            SqlCommand Cmd = new SqlCommand("EPM_ACCIDENT_INSERT_MOBILE", Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            Cmd.Transaction = trans;

            Cmd.Parameters.AddWithValue("@ACC_ID", this.Page.Request["ACC_ID"].ToString());
            Cmd.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());
            //Cmd.Parameters.AddWithValue("@RES_ID", this.hdRES_SPT_ID.Value.ToString());
            Cmd.Parameters.AddWithValue("@ACC_CUSTOMER", ddlCustomer.SelectedValue);
            Cmd.Parameters.AddWithValue("@ACC_STORE", ddlStore.SelectedValue);
            Cmd.Parameters.AddWithValue("@ACC_Place", txtAccPlace.Text);
            Cmd.Parameters.AddWithValue("@ACC_OCCURDATE", txtAccDate.Text);
            Cmd.Parameters.AddWithValue("@ACC_RES_ID", this.hdRES_ID.Value);
            Cmd.Parameters.AddWithValue("@ACC_WHEN", txtAccWhen.Text);
            Cmd.Parameters.AddWithValue("@ACC_WHERE", txtAccWhere.Text);
            Cmd.Parameters.AddWithValue("@ACC_WHO", txtAccWho.Text);
            Cmd.Parameters.AddWithValue("@ACC_WHAT", txtAccWhat.Text);
            Cmd.Parameters.AddWithValue("@ACC_HOW", txtAccHow.Text);
            Cmd.Parameters.AddWithValue("@ACC_WHY", txtAccWhy.Text);
            Cmd.Parameters.AddWithValue("@ACC_REASON", txtAccReason.Text);
            Cmd.Parameters.AddWithValue("@ACC_REMARK", txtRemark.Text);

            if (this.Page.Request["ACC_ID"].ToString() == "0")
                ViewState["ACC_ID"] = Cmd.ExecuteScalar();
            else
                Cmd.ExecuteNonQuery();

            // viewstate["accid"] 예외 처리?
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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_BUS_Accident_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }


    #region 첨부 파일 폴더에 저장
    protected void btnUploadFile_Click()
    {
        string postFileCode = Session["sRES_RBS_AREA_NAME"].ToString(); // SetddlRES_RBS_Lv2(Session["sRES_RBS_CD"].ToString());
        string orgFileName = string.Empty;
        string serverRoot = @"D:\EPM_Attatch\Accident\{0}\{1}\{2}"; //파일 저장 폴더 위치
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

            CmdAtt.Parameters.AddWithValue("@ATT_GB", "ACC");
            CmdAtt.Parameters.AddWithValue("@ATT_GB_ID", ViewState["ACC_ID"].ToString());
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

    #region 거래처 DDL
    private void SetddlCustomer()
    {
        DataSet ds = null;
        ds = Select_StoreList("", "");

        //ListItem firstItem = new ListItem("-선택-", "");
        //ddlCustomer.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["COD_NM"].ToString();
            string Code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(CodeName, Code);
            ddlCustomer.Items.Add(tempItem);
        }
    }
    #endregion

    #region 매장 리스트 조회
    private DataSet Select_StoreList(string gb, string Customer)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CUSTOMER_STORE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@GB", gb);
        adp.SelectCommand.Parameters.AddWithValue("@CUSTOMER", Customer);

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

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStore.Items.Clear();

        ddlStore.Enabled = true;

        DataSet ds = null;
        ds = Select_StoreList("CUS", ddlCustomer.SelectedValue);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code_name = dr["COD_NM"].ToString();
            string code = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code);
            ddlStore.Items.Add(tempItem);
        }
    }

    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_SEARCH", Con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            adp.SelectCommand.Parameters.AddWithValue("@NAME", this.txtResName.Text.Trim());
            //adp.SelectCommand.Parameters.AddWithValue("@WORKGROUP1", "004");

            DataSet ds = new DataSet();

            try
            {
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            this.gvResList.DataSource = ds;
            this.gvResList.DataBind();

            //this.gvResList.DataSource = ds;
            //this.gvResList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncSelectRes('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["GB"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["ASSCON_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["CUSTOMER_ID"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["ASSCON_STOREID"].ToString() 
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetgvResList();
        this.dvResList.Visible = true;
    }

    protected void btnSelectRes_Click(object sender, EventArgs e)
    {
        this.lblResName.Text = "";
        this.lblPosition.Text = "";
        this.lblJoinDate.Text = "";


        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "ACC");

        if (rd.Read())
        {
            this.lblResName.Text = rd["RES_Name"].ToString() + "(" + rd["res_Number"].ToString() + ")";
            this.lblPosition.Text = rd["RES_WorkGroup2_NAME"].ToString();
            this.lblJoinDate.Text = DateTime.Parse(rd["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");
            this.hdResIdExtra.Value = rd["RES_ID"].ToString();
            //this.hdRES_SPT_ID.Value = rd["SUPPORTER_ID"].ToString();
            //this.lblSupporter.Text = rd["RES_Supporter"].ToString();

            #region ddl 바인딩
            //ddlCustomer.SelectedValue = rd["CUSTOMER_ID"].ToString();
            ddlCustomer.SelectedValue = this.hdCUSTOMER_ID.Value.ToString();

            ddlStore.Items.Clear();
            ddlStore.Enabled = true;

            DataSet ds1 = null;
            //ds1 = Select_StoreList("CUS", rd["CUSTOMER_ID"].ToString());
            ds1 = Select_StoreList("CUS", this.hdCUSTOMER_ID.Value.ToString());

            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                string code_name = dr["COD_NM"].ToString();
                string code = dr["COD_CD"].ToString();

                ListItem tempItem = new ListItem(code_name, code);
                ddlStore.Items.Add(tempItem);
            }

            //ddlStore.SelectedValue = rd["ASSCON_STOREID"].ToString();
            ddlStore.SelectedValue = this.hdASSCON_STOREID.Value.ToString();
            #endregion
        }

        this.dvResList.Visible = false;
    }

    protected void btnUploadFile_Click(Object s, EventArgs e)
    {
    }


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