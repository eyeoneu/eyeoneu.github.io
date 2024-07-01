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

public partial class Attendance_m_ATT_Parttime_Modify : System.Web.UI.Page
{
	public string conTime = string.Empty;
	public string conType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
            SetPage();
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

                // 하계휴가, 특근 항목을 비활성화 한다. (2014-06-03 : 김재영 추가)
                if (Code == "004" || Code == "006")		// 004:특근, 006:하계휴가
                    this.rdoATT_DAY_Code.Items[intindex].Enabled = false;

                this.rdoATT_DAY_Code.Items[intindex].Attributes.Add("onclick", "javascript:fncDDLControler('" + dr["COD_Remarks"].ToString() + "');");

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

            SqlDataReader rd = attendance.EPM_ATT_BY_DAY_ITEM_SELECT_MOBILE(this.Page.Request["DATE"].ToString(), "A", int.Parse(this.Page.Request["ATT_DAY_ID"].ToString()));

            if (rd.Read())
            {
                this.lblRES_Name.Text = rd["RES_Name"].ToString();
                this.lblRES_Number.Text = rd["RES_Number"].ToString();
                this.lblRES_Venter.Text = rd["RES_CON_VENDER"].ToString();
                this.lblRES_CP.Text = rd["RES_CP"].ToString();
                this.lblRES_STORE_NAME.Text = rd["RES_STORE_NAME"].ToString();

                this.lblRES_CON_Type.Text = rd["RES_CON_Type_NAME"].ToString();

                this.lblRES_CON_Time.Text = rd["RES_CON_Time"].ToString();
                this.lblRES_CON_Pay.Text = rd["RES_CON_Pay"].ToString();

                this.ddlRES_CON_TIME.SelectedValue = rd["ATT_DAY_Icon"].ToString();
				conTime = rd["RES_CON_Time"].ToString();
				conType = rd["RES_CON_Type"].ToString();

				this.rdoATT_DAY_Code.SelectedValue = rd["ATT_DAY_Code"].ToString();

                if (rd["RES_CON_Type"].ToString() == "D")
					this.dvATT_DAY_Code.Visible = false;
					
				if (rd["ATT_DAY_Code"].ToString() == "103" || rd["ATT_DAY_Code"].ToString() == "104")
                {
					this.rdoATT_DAY_Code.Enabled = false;
					this.btnSave.Enabled = false;
                } 	
			}

            rd.Close();
        }
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Attendance attendance = new Attendance();

            attendance.EPM_ATT_BY_DAY_ITEM_UPDATE_MOBILE("A", this.rdoATT_DAY_Code.SelectedValue.ToString(), this.ddlRES_CON_TIME.SelectedValue.ToString(), int.Parse(this.Page.Request["ATT_DAY_ID"].ToString()));

            Common.scriptAlert(this.Page, "저장되었습니다.", "/Attendance/m_ATT_Parttime_List.aspx?DATE="
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