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

public partial class Attendance_m_ATT_Cody_Request_Modify : System.Web.UI.Page
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
                if (int.Parse(Code) > 100)
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
            Attendance attendance = new Attendance();

            attendance.EPM_ATT_BY_DAY_ITEM_REQUEST_MOBILE("C", int.Parse(this.Page.Request["ATT_DAY_ID"].ToString()), "", this.rdoATT_DAY_Code.SelectedValue.ToString(), this.txtAttRequest.Text.ToString(), Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString(), int.Parse(Session["sRES_ID"].ToString()));

            //ViewMsgBack("저장되었습니다.");
            Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Cody_List.aspx?DATE=" 
                            + this.Page.Request["DATE"].ToString()
                            + "&scroll=" + this.Page.Request["scroll"].ToString());

            SetPage();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}