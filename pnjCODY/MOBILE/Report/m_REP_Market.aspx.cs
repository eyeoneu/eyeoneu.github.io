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

public partial class m_REP_Market : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.divEvent.Visible = false;
            this.divNew.Visible = false;
            this.divExtra.Visible = false;

            SetddlCustomer();
            SetddlClassify();
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        if (this.Page.Request["RES_ID"] != null && this.Page.Request["MKT_ID"].ToString() != "0")
        {
            DataTable dt = Select_Mkt_Detail();           

            if (dt.Rows.Count > 0)
            {
                #region ddl 바인딩
                ddlType.SelectedValue = dt.Rows[0]["MKT_GB"].ToString();
                ddlType.Enabled = false;
                ddlCustomer.SelectedValue = dt.Rows[0]["MKT_Customer"].ToString();

                ddlStore.Items.Clear();
                ddlStore.Enabled = true;

                DataSet ds = null;
                ds = Select_StoreList("CUS", dt.Rows[0]["MKT_Customer"].ToString());

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string code_name = dr["COD_NM"].ToString();
                    string code = dr["COD_CD"].ToString();

                    ListItem tempItem = new ListItem(code_name, code);
                    ddlStore.Items.Add(tempItem);
                }

                ddlStore.SelectedValue = dt.Rows[0]["MKT_Store"].ToString();
                #endregion

                if (dt.Rows[0]["MKT_GB"].ToString().Equals("S") || dt.Rows[0]["MKT_GB"].ToString().Equals("R"))
                {
                    this.divEvent.Visible = true;
                    this.divNew.Visible = false;
                    this.divExtra.Visible = false;

                    txtEventStart.Text = dt.Rows[0]["MKT_StartDate"].ToString();
                    txtEventFinsih.Text = dt.Rows[0]["MKT_FinishDate"].ToString();
                    txtEventProperty.Text = dt.Rows[0]["MKT_Remarks"].ToString();
                }
                else if (dt.Rows[0]["MKT_GB"].ToString().Equals("N"))
                {
                    this.divEvent.Visible = false;
                    this.divNew.Visible = true;
                    this.divExtra.Visible = false;

                    txtProduct.Text = dt.Rows[0]["MKT_Product"].ToString();
                    ddlCompany.SelectedValue = dt.Rows[0]["MKT_Company"].ToString();
                    ddlClassify.SelectedValue = dt.Rows[0]["MKT_Type"].ToString();
                    txtRetailPrice.Text = dt.Rows[0]["MKT_Consumers"].ToString();
                    txtPrice.Text = dt.Rows[0]["MKT_Price"].ToString();
                    txtWeight.Text = dt.Rows[0]["MKT_Weight"].ToString();
                    txtRemark.Text = dt.Rows[0]["MKT_Remarks"].ToString();
                }
                else if (dt.Rows[0]["MKT_GB"].ToString().Equals("E"))
                {
                    this.divEvent.Visible = false;
                    this.divNew.Visible = false;
                    this.divExtra.Visible = true;

                    this.txtResName.Text = "";
                    this.lblResName.Text = "";
                    this.lblPosition.Text = "";
                    this.lblJoinDate.Text = "";

                    this.lblResName.Text = dt.Rows[0]["MKT_RES_NAME"].ToString();
                    this.lblPosition.Text = dt.Rows[0]["MKT_RES_POSITION"].ToString();
                    this.lblJoinDate.Text = dt.Rows[0]["MKT_RES_JOIN"].ToString();
                    txtRemarks2.Text = dt.Rows[0]["MKT_Remarks"].ToString();
                    this.hdResIdExtra.Value = dt.Rows[0]["MKT_Res"].ToString();
                }
                else
                {
                    this.divEvent.Visible = false;
                    this.divNew.Visible = false;
                    this.divExtra.Visible = false;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["MKT_PhotoPath"].ToString()))
                {
                    imgRES_Picture.Visible = true;
                    imgRES_Picture.ImageUrl = @"/Report/Market_Photo/" + dt.Rows[0]["MKT_PhotoPath"].ToString();  //이지지  URL
                    hdPhotoPath.Value = dt.Rows[0]["MKT_PhotoPath"].ToString();
                }
            }
        }
    }

    #region 류별 DDL 바인딩
    private void SetddlClassify()
    {
        DataSet ds = null;
        ds = Select_CodeList("16");

        ListItem firstItem = new ListItem("-선택-", "");
        ddlClassify.Items.Add(firstItem);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["COD_CD"].ToString().Length > 0)
            {
                string CodeName = dr["COD_Name"].ToString();
                string Code = dr["COD_CD"].ToString();
                ListItem tempItem = new ListItem(CodeName, Code);
                ddlClassify.Items.Add(tempItem);
            }

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

    private DataTable Select_Mkt_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_MARKET_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@MKT_ID", this.Page.Request["MKT_ID"].ToString());

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

        return ds.Tables[0];
    }

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strFileName = string.Empty;

        // First we check to see if the user has selected a file
        if (upPicture.HasFile)
        {
            // Find the fileUpload control
            string filename = upPicture.FileName;

            // Check if the directory we want the image uploaded to actually exists or not
            if (!Directory.Exists(MapPath(@"Market_Photo")))
            {
                // If it doesn't then we just create it before going any further
                Directory.CreateDirectory(MapPath(@"Market_Photo"));
            }
            // Specify the upload directory
            string directory = Server.MapPath(@"Market_Photo\");

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(upPicture.FileContent);

            // Calculate the new image dimensions
            //int origWidth = originalBMP.Width;
            //int origHeight = originalBMP.Height;
            //float sngRatio = origWidth / origHeight;

            int newWidth = 480;
            int newHeight = 360;
            //int newHeight = newWidth / sngRatio;

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);
            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
            // Save the new graphic file to the server
            newBMP.Save(directory + filename);

            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();

            string chgFilename = "MK_" + this.ddlType.SelectedValue.ToString() + "_" + this.ddlCustomer.SelectedItem.Text + "_" + this.ddlStore.SelectedItem.Text + "_" + DateTime.Today.ToShortDateString() + Path.GetExtension(filename);

            strFileName = GetFilePath(directory, chgFilename); // 파일명 중복 확인
            File.Move(directory + filename, directory + strFileName);

            // Display the image to the user
            imgRES_Picture.Visible = true;
            imgRES_Picture.ImageUrl = @"/Report/Market_Photo/" + strFileName;  //이지지  URL
            hdPhotoPath.Value = strFileName;
            //Common.scriptAlert(this.Page, "저장되었습니다.");
        }
        //else
        //{
        //    lblRES_Picture.Text = "사진 업로드 오류!";
        //}

        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand CmdRemark = new SqlCommand("EPM_MARKET_INSERT_MOBILE", Con);
            CmdRemark.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            CmdRemark.Transaction = trans;

            CmdRemark.Parameters.AddWithValue("@MKT_ID", this.Page.Request["MKT_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@MKT_GB", ddlType.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@MKT_CUSTOMER", ddlCustomer.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@MKT_STORE", ddlStore.SelectedValue);
            
            if (ddlType.SelectedValue.Equals("S") || ddlType.SelectedValue.Equals("R"))
            {
                CmdRemark.Parameters.AddWithValue("@MKT_STARTDATE", txtEventStart.Text);
                CmdRemark.Parameters.AddWithValue("@MKT_FINISHDATE", txtEventFinsih.Text);
                CmdRemark.Parameters.AddWithValue("@MKT_REMARKS", txtEventProperty.Text);
            }
            else if (ddlType.SelectedValue.Equals("N"))
            {
                CmdRemark.Parameters.AddWithValue("@MKT_PRODUCT", txtProduct.Text);
                CmdRemark.Parameters.AddWithValue("@MKT_COMPANY", ddlCompany.SelectedValue);
                CmdRemark.Parameters.AddWithValue("@MKT_TYPE", ddlClassify.SelectedValue);
                CmdRemark.Parameters.AddWithValue("@MKT_CONSUMERS", txtRetailPrice.Text);
                CmdRemark.Parameters.AddWithValue("@MKT_PRICE", txtPrice.Text);
                CmdRemark.Parameters.AddWithValue("@MKT_WEIGHT", txtWeight.Text);
                CmdRemark.Parameters.AddWithValue("@MKT_REMARKS", txtRemark.Text);
            }
            else if (ddlType.SelectedValue.Equals("E"))
            {
                CmdRemark.Parameters.AddWithValue("@MKT_RES", this.hdResIdExtra.Value);
                CmdRemark.Parameters.AddWithValue("@MKT_REMARKS", txtRemarks2.Text);
            }

            if (!string.IsNullOrEmpty(hdPhotoPath.Value))
                CmdRemark.Parameters.AddWithValue("@MKT_PHOTOPATH", hdPhotoPath.Value);
 
            CmdRemark.ExecuteNonQuery();

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_REP_Market_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }

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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue.Equals("S") || ddlType.SelectedValue.Equals("R"))
        {
            this.divEvent.Visible = true;
            this.divNew.Visible = false;
            this.divExtra.Visible = false;
        }
        else if (ddlType.SelectedValue.Equals("N"))
        {
            this.divEvent.Visible = false;
            this.divNew.Visible = true;
            this.divExtra.Visible = false;
        }
        else if (ddlType.SelectedValue.Equals("E"))
        {
            this.divEvent.Visible = false;
            this.divNew.Visible = false;
            this.divExtra.Visible = true;

            this.txtResName.Text = "";
            this.lblResName.Text = "";
            this.lblPosition.Text = "";
            this.lblJoinDate.Text = "";
        }
        else
        {
            this.divEvent.Visible = false;
            this.divNew.Visible = false;
            this.divExtra.Visible = false;
        }
    }

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
            SqlDataAdapter adp = new SqlDataAdapter("EPM_RES_SEARCH_MOBILE", Con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            adp.SelectCommand.Parameters.AddWithValue("@NAME", this.txtResName.Text.Trim());
            adp.SelectCommand.Parameters.AddWithValue("@WORKGROUP1", "999");
            adp.SelectCommand.Parameters.AddWithValue("@RES_RBS_CD", Session["sRES_RBS_CD"].ToString());
            adp.SelectCommand.Parameters.AddWithValue("@RES_RBS_AREA_CD", Session["sRES_RBS_AREA_CD"].ToString());
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
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncSelectRes('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()  + "');";
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

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ");

        if (rd.Read())
        {
            this.lblResName.Text = rd["RES_Name"].ToString() + "(" + rd["res_id"].ToString() + ")";
            this.lblPosition.Text = rd["RES_WorkGroup2_NAME"].ToString();
            this.lblJoinDate.Text = DateTime.Parse(rd["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");
            this.hdResIdExtra.Value = rd["res_id"].ToString();
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