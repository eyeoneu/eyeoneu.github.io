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

public partial class Resource_m_RES_Basic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
            SetPage();
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        Code code = new Code();
        DataSet dsWorkState = code.EPM_CODE("6");

        this.ddlRES_WorkState.DataSource = dsWorkState;
        this.ddlRES_WorkState.DataBind();

        DataSet dsBANK = code.DZICUBE_EXC_CODE("B");

        this.ddlRES_Bank.DataSource = dsBANK;
        this.ddlRES_Bank.DataBind();
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "BAS");

            if (rd.Read())
            {
                this.txtRES_Name.Text = rd["RES_Name"].ToString();
                this.txtRES_PersonNumber1.Text = rd["RES_PersonNumber1"].ToString();
                this.txtRES_PersonNumber2.Text = rd["RES_PersonNumber2"].ToString();
                this.txtRES_CP.Text = rd["RES_CP"].ToString();
                this.txtRES_TEL.Text = rd["RES_TEL"].ToString();
                this.txtRES_Birthday.Text = rd["RES_Birthday"].ToString();
                this.rdoRES_Marry.SelectedValue = rd["RES_Marry"].ToString();
                if (rd["RES_WorkState"].ToString() != "")
                {
                    this.trRES_WorkState.Visible = true;
                    this.ddlRES_WorkState.SelectedValue = rd["RES_WorkState"].ToString();
                    this.ddlRES_WorkState.Enabled = false;
                }
                this.ddlRES_Bank.SelectedValue = rd["RES_Bank"].ToString();
                this.txtRES_BankNumber.Text = rd["RES_BankNumber"].ToString();
                this.txtRES_Disabled.Text = rd["RES_Disabled"].ToString();
            }

            rd.Close();
        }
    }

    // 상태 확인
    protected void btnCheck_Click(object sender, EventArgs e)
    { 
        Resource resource = new Resource();

        try
        {
            SqlDataReader rd = resource.EPM_RES_CHECK_JOIN_RESTRICTION_V2
                                                    (this.txtRES_PersonNumber1.Text.ToString() + "-" + this.txtRES_PersonNumber2.Text.ToString());
            if (rd.Read())
            {
                lblMSG.Text = rd["MSG"].ToString();
            }


            rd.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        try
        {
            // 삽입
            if (this.Page.Request["RES_ID"] == null)
            {
                string strRES_ID = "";
                string strPRE_RES_ID = "";
                string strRES_JOINDATE = "";

 		        SqlDataReader rd2 = resource.EPM_RES_CHECK_JOIN_RESTRICTION
                                                        (this.txtRES_PersonNumber1.Text.ToString() + "-" + this.txtRES_PersonNumber2.Text.ToString());
                if (rd2.Read())
                {
                    Common.scriptAlert(this.Page, "입사확인이 필요한 주민등록번호 입니다.", "/Resource/m_RES_Basic.aspx");
                }
            
                strRES_ID = resource.EPM_RES_DETAIL_INSERT_BASIC_MOBILE
                                                        (this.txtRES_Name.Text.ToString(),
                                                        this.txtRES_PersonNumber1.Text.ToString() + "-" + this.txtRES_PersonNumber2.Text.ToString(),
                                                        this.txtRES_TEL.Text.ToString(),
                                                        this.txtRES_CP.Text.ToString(),
                                                        this.txtRES_Birthday.Text.ToString(),
                                                        this.rdoRES_Marry.SelectedValue.ToString(),
                                                        this.txtRES_Disabled.Text.ToString(),
                                                        this.ddlRES_Bank.SelectedValue.ToString(),
                                                        this.txtRES_BankNumber.Text.ToString(),
                                                        Session["sRES_RBS_CD"].ToString(),
                                                        Session["sRES_RBS_AREA_CD"].ToString(),
                                                        FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtRES_PersonNumber2.Text.ToString(), "MD5")
                                                        );

                SqlDataReader rd = resource.EPM_RES_CHECK_SAVED_INFO_MOBILE
                                                        (int.Parse(strRES_ID), 
                                                        this.txtRES_PersonNumber1.Text.ToString() + "-" + this.txtRES_PersonNumber2.Text.ToString());

                if (rd.Read())
                {
                    strPRE_RES_ID = rd["RES_ID"].ToString();
                    strRES_JOINDATE = rd["RES_JOINDATE"].ToString();
                }

                rd.Close();


                if (strRES_JOINDATE == "")
                    Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Register.aspx?RES_ID=" + strRES_ID);
                else
                {
                    Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_PreInfoProcess.aspx?RES_ID=" + strRES_ID + "&PRE_RES_ID=" + strPRE_RES_ID + "&RES_JOINDATE=" + strRES_JOINDATE);
                }

            }
            // 수정
            else
            {
                resource.EPM_RES_DETAIL_SUBMIT_BASIC_MOBILE
                                                            ("M",
                                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                            this.txtRES_Name.Text.ToString(),
                                                            this.txtRES_PersonNumber1.Text.ToString() + "-" + this.txtRES_PersonNumber2.Text.ToString(),
                                                            this.txtRES_TEL.Text.ToString(),
                                                            this.txtRES_CP.Text.ToString(),
                                                            this.txtRES_Birthday.Text.ToString(),
                                                            this.rdoRES_Marry.SelectedValue.ToString(),
                                                            this.txtRES_Disabled.Text.ToString(),
                                                            this.ddlRES_Bank.SelectedValue.ToString(),
                                                            this.txtRES_BankNumber.Text.ToString()
                                                            );

                Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Register.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}