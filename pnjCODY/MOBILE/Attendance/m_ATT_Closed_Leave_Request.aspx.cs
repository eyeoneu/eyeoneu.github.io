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

public partial class Attendance_m_ATT_Closed_Leave_Request : System.Web.UI.Page
{
    string exeGB = "I";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
		if (Session["sRES_WorkGroup1"].ToString() != "008")
		{
			this.dvResSearch.Visible = false;
		}
    
        if (this.Page.Request["ATT_REQ_ID"] == null)
        {
    		if (Session["sRES_WorkGroup1"].ToString() != "008")
			{
				Resource resource = new Resource();

				DataSet ds = new DataSet();

				ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(Session["sRES_ID"].ToString()), "REQ", "Table");

				if (ds.Tables[1].Rows.Count > 1)
				{
					this.dvResSubList.Visible = true;

					gvResSubList.DataSource = ds.Tables[1];
					gvResSubList.DataBind();
				}
				else
				{
					this.hdRES_ID.Value = ds.Tables[0].Rows[0]["RES_ID"].ToString();
					
					this.lblREQ_RES_ID.Text = ds.Tables[0].Rows[0]["RES_NUMBER"].ToString();
					this.lblRES_Name.Text = ds.Tables[0].Rows[0]["RES_Name"].ToString();
					this.lblRES_RBS_NAME.Text = ds.Tables[0].Rows[0]["RES_RBS_NAME"].ToString();
					this.lblRES_WorkGroup1_NAME.Text = ds.Tables[0].Rows[0]["RES_WorkGroup1_NAME"].ToString();
					this.lblRES_RBS_AREA_NAMEE.Text = ds.Tables[0].Rows[0]["RES_RBS_AREA_NAME"].ToString();
					this.lblRES_JoinDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");

					this.txtATT_REQ_Tel.Text = ds.Tables[0].Rows[0]["RES_CP"].ToString();

					/* 기본적으로 배정정를 넣을 수 있도록 배정으로 정렬된 데이터의 첫뻔체 값을 넣는다 2016-10-28 박병진*/
					this.lblRES_Vender.Text = ds.Tables[1].Rows[0]["RES_Vender"].ToString();
					this.lblRES_Store.Text = ds.Tables[1].Rows[0]["RES_Store"].ToString();
					this.hdnGB.Value = ds.Tables[1].Rows[0]["GB"].ToString();
					this.hdnASSCONID.Value = ds.Tables[1].Rows[0]["ASSCON_ID"].ToString();
				}
			}
        }
        else
        {
            this.btnSave.Text = "수정";

            Attendance attendance = new Attendance();

            SqlDataReader rd = attendance.EPM_ATT_REQ_SELECT(int.Parse(this.Page.Request["ATT_REQ_ID"].ToString()));

            if (rd.Read())
            {
                this.ddlREQ_TYPE.SelectedValue = rd["ATT_REQ_Type"].ToString();

                this.hdRES_ID.Value = rd["REQ_RES_ID"].ToString();
                this.lblREQ_RES_ID.Text = rd["REQ_RES_NUMBER"].ToString();
                this.lblRES_Name.Text = rd["REQ_RES_NAME"].ToString();

                this.lblRES_RBS_NAME.Text = rd["REQ_RES_RBS"].ToString();
                this.lblRES_WorkGroup1_NAME.Text = rd["REQ_RES_WORKGROUP1"].ToString();
                this.lblRES_RBS_AREA_NAMEE.Text = rd["REQ_RES_AREA"].ToString();
                this.lblRES_JoinDate.Text = DateTime.Parse(rd["REQ_RES_JOIN"].ToString()).ToString("yyyy-MM-dd");

                this.lblRES_Vender.Text = rd["REQ_RES_VENDER"].ToString();
                this.lblRES_Store.Text = rd["REQ_RES_STORE"].ToString();

                this.txtATT_REQ_StartDate.CssClass = "i_f_out";
                this.txtATT_REQ_FinishDate.CssClass = "i_f_out";
                this.txtATT_REQ_StartDate.Text = rd["ATT_REQ_StartDate"].ToString();
                if(rd["ATT_REQ_FinishDate"].ToString() != "")
                    this.txtATT_REQ_FinishDate.Text = rd["ATT_REQ_FinishDate"].ToString();
                else
                    this.txtATT_REQ_FinishDate.CssClass = "i_f_out2";

                this.lblATT_REQ_Duration.Text = rd["ATT_REQ_Duration"].ToString();
                this.hdATT_REQ_Duration.Value = rd["ATT_REQ_Duration"].ToString();

                this.txtATT_REQ_Tel.Text = rd["ATT_REQ_Tel"].ToString();
                this.txtATT_REQ_Reason.Text = rd["ATT_REQ_Reason"].ToString();
                this.txtATT_REQ_Attatch.Text = rd["ATT_REQ_Attatch"].ToString();
                this.txtATT_REQ_Delay.Text = rd["ATT_REQ_Delay"].ToString();
				
				if (!string.IsNullOrEmpty(rd["ATT_REQ_PhotoPath"].ToString()))
                {
                    imgRES_Picture.Visible = true;
                    imgRES_Picture.ImageUrl = @"/Attendance/Request_Photo/" + rd["ATT_REQ_PhotoPath"].ToString();  //이미지  URL
                    hdPhotoPath.Value = rd["ATT_REQ_PhotoPath"].ToString();
                }
                
                this.ddlConfirm.SelectedValue = rd["ATT_REQ_ApproveStatus"].ToString();
                this.txtRejectReason.Text = rd["ATT_REQ_RejectReason"].ToString();
				
				DateTime t1 = DateTime.Today; // 오늘 날짜
                DateTime t2 = DateTime.Parse(this.txtATT_REQ_StartDate.Text); //  신청 시작일자
                TimeSpan t3 = t2.Subtract(t1);
                int t4 = t3.Days; // 두 날짜의 차이
				
				// 신청 시작일자가 오늘이거나 오늘보다 이전일자일 경우에만 승인결과를 변경할 수 있도록 수정
				if (t4 > 0)
				{
					this.ddlConfirm.Enabled = false;
				}				
				
				// 서포터가 자신의 부서에 소속된 사원의 연창 신청서인 경우 서포터가 직접 승인/반려 처리한다.
				// (서포터 본인의 연차신청서는 기존 프로세스와 마찬가지로 팀장 및 관리자 승인을 받는다)
				if (rd["ATT_REQ_Writer_ID"].ToString() == Session["sRES_ID"].ToString())
				{
					ConfirmReq.Visible = true;
					btnConfirm.Visible = true;
					btnSave.Visible = false;
				}

                // 팀장 승인 이후 단계 일 경우
                if (rd["ATT_REQ_Approve1"].ToString() != "")
                {
                    this.ddlREQ_TYPE.Enabled = false;
                    this.dvResSearch.Visible = false;
                    //this.ConfirmReq.Visible = false;
                    this.upPicture.Visible = false;
                    this.ddlConfirm.Enabled = false;
                    this.txtRejectReason.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnConfirm.Enabled = false;
                    this.txtATT_REQ_StartDate.Enabled = false;
                    this.txtATT_REQ_FinishDate.Enabled = false;
                    this.txtATT_REQ_Tel.Enabled = false;
                    this.txtATT_REQ_Reason.Enabled = false;
                    this.txtATT_REQ_Attatch.Enabled = false;
                    this.txtATT_REQ_Delay.Enabled = false;
                }
                

            }

            rd.Close();
        }
    }

    // 검색 버튼 클릭
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetgvResList();
        this.dvResList.Visible = true;
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            Resource resource = new Resource();
            DataSet ds = resource.EPM_RES_LIST_MOBILE(this.txtRES_Name.Text.ToString(), "005", "", "");

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
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    /// <summary>
    /// 휴무 신청서 대상 선택 시
    /// </summary>
    protected void btnSelectRes_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        DataSet ds = new DataSet();

        ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ", "Table");

        if (ds.Tables[1].Rows.Count > 1)
        {
            this.dvResSubList.Visible = true;

            gvResSubList.DataSource = ds.Tables[1];
            gvResSubList.DataBind();
        }
        else
        {
            this.lblREQ_RES_ID.Text = ds.Tables[0].Rows[0]["RES_NUMBER"].ToString();
            this.lblRES_Name.Text = ds.Tables[0].Rows[0]["RES_Name"].ToString();
            this.lblRES_RBS_NAME.Text = ds.Tables[0].Rows[0]["RES_RBS_NAME"].ToString();
            this.lblRES_WorkGroup1_NAME.Text = ds.Tables[0].Rows[0]["RES_WorkGroup1_NAME"].ToString();
            this.lblRES_RBS_AREA_NAMEE.Text = ds.Tables[0].Rows[0]["RES_RBS_AREA_NAME"].ToString();
            this.lblRES_JoinDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");

            this.txtATT_REQ_Tel.Text = ds.Tables[0].Rows[0]["RES_CP"].ToString();

            /* 기본적으로 배정정를 넣을 수 있도록 배정으로 정렬된 데이터의 첫뻔체 값을 넣는다 2016-10-28 박병진*/
            this.lblRES_Vender.Text = ds.Tables[1].Rows[0]["RES_Vender"].ToString();
            this.lblRES_Store.Text = ds.Tables[1].Rows[0]["RES_Store"].ToString();
            this.hdnGB.Value = ds.Tables[1].Rows[0]["GB"].ToString();
            this.hdnASSCONID.Value = ds.Tables[1].Rows[0]["ASSCON_ID"].ToString();


            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    this.lblRES_Vender.Text = dr["RES_Vender"].ToString();
            //    this.lblRES_Store.Text = dr["RES_Store"].ToString();
            //    this.hdnGB.Value = dr["GB"].ToString();
            //    this.hdnASSCONID.Value = dr["ASSCON_ID"].ToString();
            //}
        }


        //SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ");

        //if (rd.Read())
        //{
        //    this.lblREQ_RES_ID.Text = rd["RES_ID"].ToString();
        //    this.lblRES_Name.Text = rd["RES_Name"].ToString();
        //    this.lblRES_RBS_NAME.Text = rd["RES_RBS_NAME"].ToString();
        //    this.lblRES_WorkGroup1_NAME.Text = rd["RES_WorkGroup1_NAME"].ToString();
        //    this.lblRES_RBS_AREA_NAMEE.Text = rd["RES_RBS_AREA_NAME"].ToString();
        //    this.lblRES_JoinDate.Text = DateTime.Parse(rd["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");
        //}

        this.dvResList.Visible = false;
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
		DateTime ATT_REQ_StartDate = Convert.ToDateTime(this.txtATT_REQ_StartDate.Text.ToString());
    
		DateTime t1 = DateTime.Today; // 오늘 날짜
		DateTime t2 = ATT_REQ_StartDate.AddDays(3 - ATT_REQ_StartDate.Day).AddMonths(1); // 기준일자: 
		TimeSpan t3 = t2.Subtract(t1); // 날짜 차이값 구하기
		
		//Response.Write(t1 + " : " + t2 + " : " + t3);
		
		
		// 신청기간의 시작을 기준으로 익월 3일까지만 저장이 가능하도록 처리한다.
		// 익월 3일이 지났을 경우에는 저장하지 않고 사용자에게 알림 메시지를 보여준다.
		if (t3.Days < 0)
		{
			divMessage.Visible = true;
		}
		else
		{
			Attendance attendance = new Attendance();
			
			
		
			SqlDataReader rd = attendance.EPM_ATT_REQ_CHECK		(
																int.Parse(this.hdRES_ID.Value.ToString()),
																this.txtATT_REQ_StartDate.Text.ToString(),
																this.txtATT_REQ_FinishDate.Text.ToString()
																);
		
			// 중복된 기간에 등록된 연차 신청서가 존재할 경우: 중복된 기간에 등록된 연차 신청서가 존재합니다 메시지 표시
			if (rd.Read())
			{
				divMessage2.Visible = true;
			}
			else
			{
				

				try
				{
					if (this.txtATT_REQ_FinishDate.Text.ToString() == "YYYYMMDD")
						this.txtATT_REQ_FinishDate.Text = "";

					// 삽입
					if (this.Page.Request["ATT_REQ_ID"] == null)
					{
						attendance.EPM_ATT_REQ_SUBMIT
																("",
																int.Parse(Session["sRES_ID"].ToString()),
																this.ddlREQ_TYPE.SelectedValue.ToString(),
																int.Parse(this.hdRES_ID.Value.ToString()),
																this.txtATT_REQ_StartDate.Text.ToString(),
																this.txtATT_REQ_FinishDate.Text.ToString(),
																this.hdATT_REQ_Duration.Value.ToString(),
																this.txtATT_REQ_Tel.Text.ToString(),
																this.txtATT_REQ_Reason.Text.ToString(),
																this.txtATT_REQ_Attatch.Text.ToString(),
																this.txtATT_REQ_Delay.Text.ToString(),
																this.hdnGB.Value.ToString(),
																this.hdnASSCONID.Value.ToString()
																);
					}
					// 수정
					else
					{
						attendance.EPM_ATT_REQ_SUBMIT
																("",
																int.Parse(this.Page.Request["ATT_REQ_ID"].ToString()),
																int.Parse(Session["sRES_ID"].ToString()),
																this.ddlREQ_TYPE.SelectedValue.ToString(),
																int.Parse(this.hdRES_ID.Value.ToString()),
																this.txtATT_REQ_StartDate.Text.ToString(),
																this.txtATT_REQ_FinishDate.Text.ToString(),
																this.hdATT_REQ_Duration.Value.ToString(),
																this.txtATT_REQ_Tel.Text.ToString(),
																this.txtATT_REQ_Reason.Text.ToString(),
																this.txtATT_REQ_Attatch.Text.ToString(),
																this.txtATT_REQ_Delay.Text.ToString(),
																this.hdnGB.Value.ToString(),
																this.hdnASSCONID.Value.ToString()
																);
					}

					Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Closed_Leave_Mng.aspx");
				}
				catch (Exception ex)
				{
					Response.Write(ex);

				}
			}
		}
    }
    
    
    // 승인확인 버튼 클릭 시
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
		string strFileName = string.Empty;

        // First we check to see if the user has selected a file
        if (upPicture.HasFile)
        {
            // Find the fileUpload control
            string filename = upPicture.FileName;

            // Check if the directory we want the image uploaded to actually exists or not
            if (!Directory.Exists(MapPath(@"Request_Photo")))
            {
                // If it doesn't then we just create it before going any further
                Directory.CreateDirectory(MapPath(@"Request_Photo"));
            }
            // Specify the upload directory
            string directory = Server.MapPath(@"Request_Photo\");

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(upPicture.FileContent);

            // Calculate the new image dimensions
            //int origWidth = originalBMP.Width;
            //int origHeight = originalBMP.Height;
            //float sngRatio = origWidth / origHeight;

            int newWidth = originalBMP.Width; //360;
            int newHeight = originalBMP.Height; //480;
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

            string chgFilename = "RQ_" + this.lblREQ_RES_ID.Text.ToString() + "_" + DateTime.Today.ToShortDateString() + Path.GetExtension(filename);

            strFileName = GetFilePath(directory, chgFilename); // 파일명 중복 확인
            File.Move(directory + filename, directory + strFileName);

            // Display the image to the user
            imgRES_Picture.Visible = true;
            imgRES_Picture.ImageUrl = @"/Attendance/Request_Photo/" + strFileName;  //이미지  URL
            hdPhotoPath.Value = strFileName;
            //Common.scriptAlert(this.Page, "저장되었습니다.");
        }
    
        Attendance attendance = new Attendance();

        try
        {
            if (this.txtATT_REQ_FinishDate.Text.ToString() == "YYYYMMDD")
                this.txtATT_REQ_FinishDate.Text = "";

 
            attendance.EPM_ATT_REQ_SUBMIT
                                                    ("S3",
                                                    int.Parse(this.Page.Request["ATT_REQ_ID"].ToString()),
                                                    int.Parse(Session["sRES_ID"].ToString()),
                                                    this.ddlREQ_TYPE.SelectedValue.ToString(),
                                                    int.Parse(this.hdRES_ID.Value.ToString()),
                                                    this.txtATT_REQ_StartDate.Text.ToString(),
                                                    this.txtATT_REQ_FinishDate.Text.ToString(),
                                                    this.hdATT_REQ_Duration.Value.ToString(),
                                                    this.txtATT_REQ_Tel.Text.ToString(),
                                                    this.txtATT_REQ_Reason.Text.ToString(),
                                                    this.txtATT_REQ_Attatch.Text.ToString(),
                                                    this.txtATT_REQ_Delay.Text.ToString(),
                                                    this.hdnGB.Value.ToString(),
                                                    this.hdnASSCONID.Value.ToString(),
                                                    this.ddlConfirm.SelectedValue.ToString(),
                                                    this.txtRejectReason.Text.ToString(),
                                                    this.hdPhotoPath.Value.ToString()
                                                    );


            Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Closed_Leave_Mng.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    protected void gvResSubList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncSelectResSub('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString() + "', '"
                                        + ((DataRowView)e.Row.DataItem)["GB"].ToString() + "', '"
                                        + ((DataRowView)e.Row.DataItem)["ASSCON_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    protected void btnSelectResSub_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        DataSet ds = new DataSet();

        ds = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ", "Table");


        this.lblREQ_RES_ID.Text = ds.Tables[0].Rows[0]["RES_NUMBER"].ToString();
        this.lblRES_Name.Text = ds.Tables[0].Rows[0]["RES_Name"].ToString();
        this.lblRES_RBS_NAME.Text = ds.Tables[0].Rows[0]["RES_RBS_NAME"].ToString();
        this.lblRES_WorkGroup1_NAME.Text = ds.Tables[0].Rows[0]["RES_WorkGroup1_NAME"].ToString();
        this.lblRES_RBS_AREA_NAMEE.Text = ds.Tables[0].Rows[0]["RES_RBS_AREA_NAME"].ToString();
        this.lblRES_JoinDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");

        this.txtATT_REQ_Tel.Text = ds.Tables[0].Rows[0]["RES_CP"].ToString();

        foreach (DataRow dr in ds.Tables[1].Select("GB = '" + hdnGB.Value + "' AND ASSCON_ID = '" + hdnASSCONID.Value + "'"))
        {
            this.lblRES_Vender.Text = dr["RES_Vender"].ToString();
            this.lblRES_Store.Text = dr["RES_Store"].ToString();
        }

        this.dvResSubList.Visible = false;
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