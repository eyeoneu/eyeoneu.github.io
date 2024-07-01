using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;


public partial class Resource_m_RES_Photo_Add_iPhone : System.Web.UI.Page
{
    public string RES_ID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Page.Request["RES_ID"]))
        {
            RES_ID = this.Page.Request["RES_ID"].ToString();

            SetPicture();
        }
    }

    // 사진 세팅
    private void SetPicture()
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "PHO");

        if (rd.Read())
        {
            // Display the image to the user
            this.imgRES_Picture.Visible = true;
            this.imgRES_Picture.ImageUrl = @"/Resource/RES_PIC/" + rd["RES_Picture"].ToString();
        }

        rd.Close();
    }

    // 사진 저장 버튼 클릭 시
    protected void btnUploadFile_Click(Object s, EventArgs e)
    {
        // First we check to see if the user has selected a file
        if (upPicture.HasFile)
        {
            // Find the fileUpload control
            string filename = upPicture.FileName;

            // Check if the directory we want the image uploaded to actually exists or not
            if (!Directory.Exists(MapPath(@"RES_PIC")))
            {
                // If it doesn't then we just create it before going any further
                Directory.CreateDirectory(MapPath(@"RES_PIC"));
            }
            // Specify the upload directory
            string directory = Server.MapPath(@"RES_PIC\");

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(upPicture.FileContent);

            // Calculate the new image dimensions
            //int origWidth = originalBMP.Width;
            //int origHeight = originalBMP.Height;
            //float sngRatio = origWidth / origHeight;

            int newWidth = 300;
            int newHeight = 400;
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

            Resource resource = new Resource();
            string strRES_Name = "";
            string strRES_PersonNumber = "";

            SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

            if (rd.Read())
            {
                strRES_Name = rd["RES_Name"].ToString();
                strRES_PersonNumber = rd["RES_PersonNumber"].ToString();
            }

            rd.Close();

            string chgFilename = "ID_" + strRES_PersonNumber.Substring(0, 6) + "_" + strRES_Name + Path.GetExtension(filename);
            string strFileName = string.Empty;

            strFileName = GetFilePath(directory, chgFilename); // 파일명 중복 확인
            File.Move(directory + filename, directory + strFileName);

            resource.EPM_RES_DETAIL_UPDATE_PICTURE_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), strFileName);

            //// Write a message to inform the user all is OK
            //lblRES_Picture.Text = "File Name: <b style='color: red;'>" + filename + "</b><br>";
            //lblRES_Picture.Text += "Content Type: <b style='color: red;'>" + upPicture.PostedFile.ContentType + "</b><br>";
            //lblRES_Picture.Text += "File Size: <b style='color: red;'>" + upPicture.PostedFile.ContentLength.ToString() + "</b>";

            // Display the image to the user
            imgRES_Picture.Visible = true;
            imgRES_Picture.ImageUrl = @"/Resource/RES_PIC/" + strFileName;  //이지지  URL



            Common.scriptAlert(this.Page, "저장되었습니다.");
        }
        else
        {
            lblRES_Picture.Text = "사진 업로드 오류!";
        }
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