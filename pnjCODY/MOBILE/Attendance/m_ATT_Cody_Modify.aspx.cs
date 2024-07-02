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

public partial class Attendance_m_ATT_Cody_Modify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["History"] = -1;   
            SetControl();
            SetPage();
        }
        else
        {
            ViewState["History"] = Convert.ToInt32(ViewState["History"]) - 1;
        }
    }

    // 컨트롤 세팅
    private void SetControl()
    {
        Code code = new Code();
        DataSet ds = code.EPM_CODE("7");

        int intindex = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["COD_Name"].ToString();
            string Code = dr["COD_CD"].ToString();

            if (Code != "")
            {
                ListItem tempItem = new ListItem(CodeName, Code);
                this.rdoATT_DAY_Code.Items.Add(tempItem);

                // 코드 값이 100 이상인 경우 해당 항목을 비활성화 한다.
                if (int.Parse(Code) > 100 || int.Parse(Code) == 4)
                    this.rdoATT_DAY_Code.Items[intindex].Enabled = false;

                intindex = intindex + 1;
            }
        }
    }


    // 페이지 세팅
    private void SetPage()
    {
        if (this.Page.Request["ATT_DAY_ID"] != null && this.Page.Request["DATE"] != null)
        {
            Attendance attendance = new Attendance();

            SqlDataReader rd = attendance.EPM_ATT_BY_DAY_ITEM_SELECT_MOBILE(this.Page.Request["DATE"].ToString(), "C", int.Parse(this.Page.Request["ATT_DAY_ID"].ToString()));

            if (rd.Read())
            {
                this.lblRES_Name.Text = rd["RES_Name"].ToString();
                this.lblRES_Number.Text = rd["RES_Number"].ToString();
                this.lblRES_CP.Text = rd["RES_CP"].ToString();
                this.lblRES_STORE_NAME.Text = rd["RES_STORE_NAME"].ToString();
                this.lblCNT_ALT_HOLIDAY.Text = rd["CNT_ALT_HOLIDAY"].ToString();
                this.rdoATT_DAY_Code.SelectedValue = rd["ATT_DAY_Code"].ToString();
                
                // 이전과 동일한 값일 경우 저장하지 못하도록 처리하기 위하여 히든필드에 이전 값 저장 처리 (2024-06-25 정창화 수정)
                this.hdATT_DAY_Code.Value = rd["ATT_DAY_Code"].ToString();
                
                if (rd["ATT_DAY_Code"].ToString() == "103" || rd["ATT_DAY_Code"].ToString() == "104")
                {
					this.rdoATT_DAY_Code.Enabled = false;
					this.btnSave.Enabled = false;
                } 
            }

            rd.Close();
        }
    }

    public void ViewMsgBack(string strMessage)
    {
        string strHTML;
        strHTML = "<script language=\"javascript\">\n" +
         "<!--\n" +
         "alert('" + strMessage + "');\n" +
         "history.go(-2);\n" +
         //"window.location.reload();\n" +
         "//-->\n" +
         "</script>\n";
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.End();
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
			// 이전과 동일한 값일 경우 저장하지 못하도록 처리 (2024-06-25 정창화 수정)
			if (this.rdoATT_DAY_Code.SelectedValue.ToString() != this.hdATT_DAY_Code.Value.ToString())
			{
				Attendance attendance = new Attendance();

				attendance.EPM_ATT_BY_DAY_ITEM_UPDATE_MOBILE("C", this.rdoATT_DAY_Code.SelectedValue.ToString(), "", int.Parse(this.Page.Request["ATT_DAY_ID"].ToString()));

				//ViewMsgBack("저장되었습니다.");
				Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Cody_List.aspx?DATE=" + this.Page.Request["DATE"].ToString()
								+ "&scroll=" + this.Page.Request["scroll"].ToString());

			}
			else
			{
				Common.scriptAlert(this.Page, "입력된 값이 이전과 동일할 경우 저장되지 않습니다.", "/Attendance/m_ATT_Cody_List.aspx?DATE=" + this.Page.Request["DATE"].ToString()
								+ "&scroll=" + this.Page.Request["scroll"].ToString());
			}
			SetPage();	
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}