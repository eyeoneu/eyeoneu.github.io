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

public partial class Resource_m_RES_Serch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["sRES_RBS_CD"].ToString() == "2010" || Session["sRES_RBS_CD"].ToString() == "2011" || Session["sRES_RBS_CD"].ToString() == "3001")
                this.ddlRES_RBS_Lv1.SelectedValue = Session["sRES_RBS_CD"].ToString();

            SetControl();
            this.dvMassege.Visible = false;
        }
    }

    // 구분 선택 시
    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.dvMassege.Visible = false;

        if (this.rdoType.SelectedValue == "W")
        {
            this.dvRES_WORKGROUP1.Visible = true;
            this.dvName.Visible = true;

            this.dvRES_RBS_Lv1.Visible = false;
            this.dvEPM_CUSTOMER_STORE.Visible = false;
            this.dvEPM_CUSTOMER_STORE_L2.Visible = false;
        }
        else
        {
            this.dvRES_WORKGROUP1.Visible = false;
            this.dvName.Visible = false;

            this.dvRES_RBS_Lv1.Visible = true;
            this.dvEPM_CUSTOMER_STORE.Visible = true;
            this.dvEPM_CUSTOMER_STORE_L2.Visible = true;
        }
    }

    // 검색 버튼 클릭
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.rdoType.SelectedValue == "W")
        {
            if (this.txtName.Text == "")
            {
                this.dvMassege.Visible = true;
                this.lblMessage.Text = "정확한 이름을 입력해주세요.";
            }
            else
                Response.Redirect("/Resource/m_RES_List.aspx"
                    + "?RES_WORKGROUP1="
                    + this.rdoRES_WORKGROUP1.Text.ToString()
                    + "&NAME="
                    + this.txtName.Text.ToString());
        }
        else
        {
            if (this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue == "")
            {
                this.dvMassege.Visible = true;
                this.lblMessage.Text = "매장을 선택해주세요.";
            }
            else
                Response.Redirect("/Resource/m_RES_List_Store.aspx"
                    + "?RES_ASS_StoreID="
                    + this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue.ToString());
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        // 거래처
        Code code = new Code();
        DataSet dsEPM_CUSTOMER_STORE = code.EPM_CUSTOMER_STORE();

        this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
        this.ddlEPM_CUSTOMER_STORE.DataBind();
    }

    //부문 변경 시
    protected void ddlRES_RBS_Lv1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlEPM_CUSTOMER_STORE.SelectedValue != "")
            SetddlEPM_CUSTOMER_STORE_L2();
    }

    //거래처 변경 시
    protected void ddlEPM_CUSTOMER_STORE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlRES_RBS_Lv1.SelectedValue != "")
            SetddlEPM_CUSTOMER_STORE_L2();
    }

    // 매장 컨트롤 세팅 EPM_CUSTOMER_STORE("CUS", 거래처 선택 값)
    private void SetddlEPM_CUSTOMER_STORE_L2()
    {
        this.dvMassege.Visible = false;

        this.ddlEPM_CUSTOMER_STORE_L2.Items.Clear();

        Code code = new Code();
        DataSet ds = null;

        // 부문별 매장코드 가져오기: 2011-12-29 수정 (정창화)
        if (this.ddlRES_RBS_Lv1.SelectedValue.ToString() == "2010" || this.ddlRES_RBS_Lv1.SelectedValue.ToString() == "2011" || this.ddlRES_RBS_Lv1.SelectedValue.ToString() == "3001")
            ds = code.EPM_CUSTOMER_STORE("CUS", int.Parse(this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString()), this.ddlRES_RBS_Lv1.SelectedValue.ToString());
        else
            ds = code.EPM_CUSTOMER_STORE("CUS", int.Parse(this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString()));

        this.ddlEPM_CUSTOMER_STORE_L2.DataSource = ds;
        this.ddlEPM_CUSTOMER_STORE_L2.DataBind();
    }
}