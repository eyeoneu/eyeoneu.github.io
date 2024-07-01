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

public partial class Resource_m_RES_Assignment_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            if (Session["sRES_ID"] != null)
                this.lblRES_ASS_SUPPORTER.Text = Session["sRES_Name"].ToString() + " (" + Session["sRES_Number"].ToString() + ")";

            // 신규등록 시
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_ASS_ID"] == null)
            {
                this.btnDel.Visible = false;
                this.txtRES_ASS_STARTDATE.CssClass = "i_f_out2";
                this.txtRES_ASS_FINISHDATE.CssClass = "i_f_out2";

                SethiddenFeild();
            }

            SetControl();
            
            // 수정 시
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_ASS_ID"] != null)
                SetPage();
        }
    }

    // 히든 필드 바인딩
    private void SethiddenFeild()
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));
        if (rd.Read())
        {
            this.hdRES_WorkType.Value = rd["Res_WorkType"].ToString();
            this.hdRES_WorkGroup1.Value = rd["RES_WorkGroup1"].ToString();
            this.hdRES_WorkGroup2.Value = rd["RES_WorkGroup2"].ToString();
            this.hdRES_WorkGroup3.Value = rd["RES_WorkGroup3"].ToString();
            this.hdRES_RBS_CD.Value = rd["RES_RBS_CD"].ToString();
            this.hdRES_RBS_AREA_CD.Value = rd["RES_RBS_AREA_CD"].ToString();
            this.lblRES_Name.Text = rd["RES_Name"].ToString();

            if(!rd["RES_WorkGroup1"].ToString().Equals("002"))
                this.ddlToList.Enabled = false;
        }
        rd.Close();
    }

    private bool getAssignmentStatus(string toID)
    {
        bool retValue = false;

        Resource resource = new Resource();
        DataSet ds = resource.EPM_RES_ASSIGNMENT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        DataRow[] dr = ds.Tables[0].Select(string.Format("TO_NUM={0}", toID));

        if (dr.Length > 0)
            retValue = true;

        return retValue;
    }

    private bool getAssignmentStatus(string assignDate, out DataRow drAssignment)
    {
        bool retValue = false;
        drAssignment = null;

        Resource resource = new Resource();
        DataSet ds = resource.EPM_RES_ASSIGNMENT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["RES_ASS_StartDate"].ToString().Equals(assignDate))
            {
                retValue = true;
                drAssignment = ds.Tables[0].Rows[i];
                break;
            }
        }

        return retValue;
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        // TO 리스트
        DataSet dsTo = Select_TOCodeList(Session["sRES_ID"].ToString(), Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());

        //#region TO 코드리스트 조회 현재 직원이 배정되어 있는 TO는 제외 한다
        //if (this.Page.Request["RES_ASS_ID"]== null)
        //{
        //    for (int i = 0; i < dsTo.Tables[0].Rows.Count; i++)
        //    {
        //        if (getAssignmentStatus(dsTo.Tables[0].Rows[i]["TO_ID"].ToString()))
        //            dsTo.Tables[0].Rows[i].Delete();
        //    }

        //    dsTo.Tables[0].AcceptChanges();
        //}
        //#endregion        
        
        this.ddlToList.DataSource = dsTo;
        this.ddlToList.DataBind();

        // 지원사 코드
        Code code = new Code();
        DataSet dsRES_ASS_VENDER_CD = code.EPM_CODE("5");

        this.ddlRES_ASS_VENDER_CD.DataSource = dsRES_ASS_VENDER_CD;
        this.ddlRES_ASS_VENDER_CD.DataBind();

        // 거래처
        DataSet dsEPM_CUSTOMER_STORE = code.EPM_CUSTOMER_STORE();

        this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
        this.ddlEPM_CUSTOMER_STORE.DataBind();
    }


    #region TO 코드리스트 조회
    private DataSet Select_TOCodeList(string resID, string rbs, string assarea)
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_LIST_FOR_DDL", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@TO_SPT_RES_ID", resID);
        adp.SelectCommand.Parameters.AddWithValue("@TO_RBS", rbs);
        adp.SelectCommand.Parameters.AddWithValue("@TO_AssArea", assarea);

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

    #region TO 코드리스트 조회
    private DataSet Select_TOCodeDetail(string toID)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_DETAIL_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@TO_NUM", toID);

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

    protected void ddlToList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        DataSet ds = Select_TOCodeDetail(ddl.SelectedValue.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_Vender"].ToString()))
            {
                Code code = new Code();
                DataSet dsRES_ASS_VENDER_CD = code.EPM_CODE("5");

                this.ddlRES_ASS_VENDER_CD.DataSource = dsRES_ASS_VENDER_CD;
                this.ddlRES_ASS_VENDER_CD.DataBind();

                this.ddlRES_ASS_VENDER_CD.SelectedValue = ds.Tables[0].Rows[0]["TO_Vender"].ToString().Trim();
                this.ddlRES_ASS_VENDER_CD.Enabled = false;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_VenArea"].ToString()))
            {
                SetddlRES_ASS_VEN_AREA_CD();
                this.ddlRES_ASS_VEN_AREA_CD.SelectedValue = ds.Tables[0].Rows[0]["TO_VenArea"].ToString().Trim();
                this.ddlRES_ASS_VEN_AREA_CD.Enabled = false;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_VenOffice"].ToString()))
            {
                SetddlRES_ASS_VEN_OFFICE_CD();
                this.ddlRES_ASS_VEN_OFFICE_CD.SelectedValue = ds.Tables[0].Rows[0]["TO_VenOffice"].ToString().Trim();
                this.ddlRES_ASS_VEN_OFFICE_CD.Enabled = false;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_Customer"].ToString()))
            {
                Code code = new Code();
                DataSet dsEPM_CUSTOMER_STORE = code.EPM_CUSTOMER_STORE();

                this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
                this.ddlEPM_CUSTOMER_STORE.DataBind();

                this.ddlEPM_CUSTOMER_STORE.SelectedValue = ds.Tables[0].Rows[0]["TO_Customer"].ToString().Trim();
                this.ddlEPM_CUSTOMER_STORE.Enabled = false;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_Store"].ToString()))
            {
                SetddlEPM_CUSTOMER_STORE_L2();
                this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = ds.Tables[0].Rows[0]["TO_Store"].ToString().Trim();
                this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;
            }

        }

        if (ddl.SelectedValue.ToString().Equals(""))
        {
            this.ddlRES_ASS_VENDER_CD.Enabled = true;
            this.ddlRES_ASS_VEN_AREA_CD.Enabled = true;
            this.ddlRES_ASS_VEN_OFFICE_CD.Enabled = true;
            this.ddlEPM_CUSTOMER_STORE.Enabled = true;
            this.ddlEPM_CUSTOMER_STORE_L2.Enabled = true;

            this.ddlRES_ASS_VENDER_CD.SelectedValue = "";
            this.ddlRES_ASS_VEN_AREA_CD.SelectedValue = "";
            this.ddlRES_ASS_VEN_OFFICE_CD.SelectedValue = "";
            this.ddlEPM_CUSTOMER_STORE.SelectedValue = "";
            this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = "";
        }
    }

    #region 지원사 관련 컨트롤 변경 시 세팅
    // 지원사 코드 변경 시
    protected void ddlRES_ASS_VENDER_CD_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetddlRES_ASS_VEN_AREA_CD();
    }

    // 소속 컨트롤 세팅
    private void SetddlRES_ASS_VEN_AREA_CD()
    {
        this.ddlRES_ASS_VEN_AREA_CD.Items.Clear();

        Code code = new Code();
        DataSet ds = code.EPM_VEN_AREA_LIST("L2", this.ddlRES_ASS_VENDER_CD.SelectedValue.ToString(), "");

        this.ddlRES_ASS_VEN_AREA_CD.DataSource = ds;
        this.ddlRES_ASS_VEN_AREA_CD.DataBind();
    }

    // 소속 코드 변경 시
    protected void ddlRES_ASS_VEN_AREA_CD_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetddlRES_ASS_VEN_OFFICE_CD();
    }

    // 근무부서 컨트롤 세팅
    private void SetddlRES_ASS_VEN_OFFICE_CD()
    {
        this.ddlRES_ASS_VEN_OFFICE_CD.Items.Clear();

        Code code = new Code();
        DataSet ds = code.EPM_VEN_AREA_LIST("L3", this.ddlRES_ASS_VENDER_CD.SelectedValue.ToString(), this.ddlRES_ASS_VEN_AREA_CD.SelectedValue.ToString());

        this.ddlRES_ASS_VEN_OFFICE_CD.DataSource = ds;
        this.ddlRES_ASS_VEN_OFFICE_CD.DataBind();
    }
    #endregion

    #region 거래처 관련 컨트롤 변경 시 세팅
    //거래처 변경 시
    protected void ddlEPM_CUSTOMER_STORE_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetddlEPM_CUSTOMER_STORE_L2();
    }

    // 매장 컨트롤 세팅 EPM_CUSTOMER_STORE("CUS", 거래처 선택 값)
    private void SetddlEPM_CUSTOMER_STORE_L2()
    {
        this.ddlEPM_CUSTOMER_STORE_L2.Items.Clear();

        Code code = new Code();
        DataSet ds = null;

        // 부문별 매장코드 가져오기: 2011-12-29 수정 (정창화)
        if (Session["sRES_RBS_CD"].ToString() == "2010" || Session["sRES_RBS_CD"].ToString() == "2011")
            ds = code.EPM_CUSTOMER_STORE("CUS", int.Parse(this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString()), Session["sRES_RBS_CD"].ToString());
        else
            ds = code.EPM_CUSTOMER_STORE("CUS", int.Parse(this.ddlEPM_CUSTOMER_STORE.SelectedValue.ToString()));

        this.ddlEPM_CUSTOMER_STORE_L2.DataSource = ds;
        this.ddlEPM_CUSTOMER_STORE_L2.DataBind();
    }
    #endregion

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_ASSIGNMENT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["RES_ASS_ID"].ToString()));

        if (rd.Read())
        {
            this.lblRES_Name.Text = rd["RES_Name"].ToString();

            if (!string.IsNullOrEmpty(rd["TO_NUM"].ToString()))
            {
                this.ddlToList.SelectedValue = rd["TO_NUM"].ToString();
                this.ddlRES_ASS_VENDER_CD.Enabled = false;
                this.ddlRES_ASS_VEN_AREA_CD.Enabled = false;
                this.ddlRES_ASS_VEN_OFFICE_CD.Enabled = false;
                this.ddlEPM_CUSTOMER_STORE.Enabled = false;
                this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;
            }
            else
            {
                this.ddlToList.Enabled = false;
                this.ddlRES_ASS_VENDER_CD.Enabled = true;
                this.ddlRES_ASS_VEN_AREA_CD.Enabled = true;
                this.ddlRES_ASS_VEN_OFFICE_CD.Enabled = true;
                this.ddlEPM_CUSTOMER_STORE.Enabled = true;
                this.ddlEPM_CUSTOMER_STORE_L2.Enabled = true;
            }

            // 지원사, 소속, 근무부서 바인딩
            this.ddlRES_ASS_VENDER_CD.SelectedValue = rd["RES_ASS_Vender_CD"].ToString();
            //this.ddlRES_ASS_VENDER_CD.Enabled = false;

            if (rd["RES_ASS_Vender_CD"].ToString() != "")
            {
                SetddlRES_ASS_VEN_AREA_CD();
                this.ddlRES_ASS_VEN_AREA_CD.SelectedValue = rd["RES_ASS_VEN_Area_CD"].ToString();
                //this.ddlRES_ASS_VEN_AREA_CD.Enabled = false;
            }
            if (rd["RES_ASS_VEN_Area_CD"].ToString() != "")
            {
                SetddlRES_ASS_VEN_OFFICE_CD();
                this.ddlRES_ASS_VEN_OFFICE_CD.SelectedValue = rd["RES_ASS_VEN_Office_CD"].ToString();
                //this.ddlRES_ASS_VEN_OFFICE_CD.Enabled = false;
            }

            // 거래처, 매장 바인딩
            this.ddlEPM_CUSTOMER_STORE.SelectedValue = rd["RES_ASS_CustomerID"].ToString();
            //this.ddlEPM_CUSTOMER_STORE.Enabled = false;
            if (rd["RES_ASS_CustomerID"].ToString() != "")
            {
                SetddlEPM_CUSTOMER_STORE_L2();
                this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = rd["RES_ASS_StoreID"].ToString();
                //this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;
            }

            this.txtRES_ASS_Sales.Text = rd["RES_ASS_Sales"].ToString();
            this.txtRES_ASS_STARTDATE.Text = rd["RES_ASS_STARTDATE"].ToString();
            this.txtRES_ASS_FINISHDATE.Text = rd["RES_ASS_FINISHDATE"].ToString();

            string ReferenceDay = ""; // 기준일

            // 현재일이 1일 일 경우
            if (DateTime.Now.ToString("dd") == "01" || DateTime.Now.ToString("dd") == "02")
            {
                ReferenceDay = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"); // 이전 달의 1일
            }
            else
            {
                ReferenceDay = DateTime.Now.ToString("yyyy-MM-01"); // 이번 달의 1일
            }

            if (this.txtRES_ASS_STARTDATE.Text.ToString() != "")
                if (DateTime.Parse(this.txtRES_ASS_STARTDATE.Text.ToString()) < DateTime.Parse(ReferenceDay))
                    this.txtRES_ASS_STARTDATE.Enabled = false;

            if (this.txtRES_ASS_FINISHDATE.Text.ToString() != "")
            {
                if (DateTime.Parse(this.txtRES_ASS_FINISHDATE.Text.ToString()) < DateTime.Parse(ReferenceDay))
                    this.txtRES_ASS_FINISHDATE.Enabled = false;
            }
            else
            {
                this.txtRES_ASS_FINISHDATE.CssClass = "i_f_out2";
            }

                ////  임시 허용  ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //if (this.txtRES_ASS_STARTDATE.Text.ToString() != "") 
                //    if (DateTime.Parse(this.txtRES_ASS_STARTDATE.Text.ToString()) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                //    this.txtRES_ASS_STARTDATE.Enabled = false;

                //if (this.txtRES_ASS_FINISHDATE.Text.ToString() != "") 
                //    if (DateTime.Parse(this.txtRES_ASS_FINISHDATE.Text.ToString()) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                //        this.txtRES_ASS_FINISHDATE.Enabled = false;

            // 배정확정 인 경우 삭제 버튼을 숨긴다.
            if (rd["RES_ASS_State"].ToString() == "002")
            {
                this.btnDel.Visible = false;
                this.btnSave.Visible = false;
            }
        }

        rd.Close();
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        try
        {
            if (this.txtRES_ASS_FINISHDATE.Text.ToString() == "YYYYMMDD")
                this.txtRES_ASS_FINISHDATE.Text = "";
            // 삽입
            if (this.Page.Request["RES_ASS_ID"] == null)
            {
                // 테스트용
                //Response.Write("'I','" + "','"
                //                                        + this.Page.Request["RES_ID"].ToString() + "','"
                //                                        + this.hdRES_WorkType.Value.ToString() + "','"
                //                                        + this.hdRES_WorkGroup1.Value.ToString() + "','"
                //                                        + this.hdRES_WorkGroup2.Value.ToString() + "','"
                //                                        + this.hdRES_WorkGroup3.Value.ToString() + "','"
                //                                        + this.ddlRES_ASS_VENDER_CD.SelectedValue.ToString() + "','"
                //                                        + this.ddlRES_ASS_VEN_AREA_CD.SelectedValue.ToString() + "','"
                //                                        + this.ddlRES_ASS_VEN_OFFICE_CD.SelectedValue.ToString() + "','"
                //                                        + this.Session["sRES_ID"].ToString() + "','"
                //                                        + this.hdRES_RBS_CD.Value.ToString() + "','"
                //                                        + this.hdRES_RBS_AREA_CD.Value.ToString() + "','"
                //                                        + "11111" + "','" //int.Parse(this.txtRES_ASS_STOREID_CD.Text.ToString()),
                //                                        + this.txtRES_ASS_Sales.Text.ToString() + "','"
                //                                        + this.txtRES_ASS_STARTDATE.Text.ToString() + "','"
                //                                        + this.txtRES_ASS_FINISHDATE.Text.ToString() + "','"
                //                                        + "001'");
                //DataRow drAssignment = null;
                
                //if (getAssignmentStatus(this.txtRES_ASS_STARTDATE.Text.ToString().Trim(), out drAssignment)) // 같은 날짜에 배정된 다른 정보가 있을경우, 2013-10-31 박병진
                //{
                //    Common.scriptAlert(this.Page, string.Format("같은 날짜에 배정된 다른 정보가 존재 합니다. 날짜를 변경해 주세요. ({0})", drAssignment["RES_ASS_StartDate"].ToString() + " " + drAssignment["RES_ASS_Store_NAME"].ToString()));
                //}
                //else
                //{
                    resource.EPM_RES_ASSIGNMENT_SUBMIT
                                                            ("I",
                                                            int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                            this.hdRES_WorkType.Value.ToString(),
                                                            this.hdRES_WorkGroup1.Value.ToString(),
                                                            this.hdRES_WorkGroup2.Value.ToString(),
                                                            this.hdRES_WorkGroup3.Value.ToString(),
                                                            this.ddlRES_ASS_VENDER_CD.SelectedValue.ToString(),
                                                            this.ddlRES_ASS_VEN_AREA_CD.SelectedValue.ToString(),
                                                            this.ddlRES_ASS_VEN_OFFICE_CD.SelectedValue.ToString(),
                                                            int.Parse(this.Session["sRES_ID"].ToString()),
                                                            this.hdRES_RBS_CD.Value.ToString(),
                                                            this.hdRES_RBS_AREA_CD.Value.ToString(),
                                                            int.Parse(this.ddlEPM_CUSTOMER_STORE_L2.Text.ToString()),
                                                            this.txtRES_ASS_Sales.Text.ToString(),
                                                            this.txtRES_ASS_STARTDATE.Text.ToString(),
                                                            this.txtRES_ASS_FINISHDATE.Text.ToString(),
                                                            "001",
                                                            this.ddlToList.SelectedValue.ToString(),
                                                            int.Parse(this.Session["sRES_ID"].ToString()));

                    Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Assignment.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
                //}
            }
             //수정
            else
            {
                //DataRow drAssignment = null;

                //if (getAssignmentStatus(this.txtRES_ASS_STARTDATE.Text.ToString().Trim(), out drAssignment)) // 같은 날짜에 배정된 다른 정보가 있을경우
                //{
                //    Common.scriptAlert(this.Page, string.Format("같은 날짜에 배정된 다른 정보가 존재 합니다. 날짜를 변경해 주세요. ({0})", drAssignment["RES_ASS_StartDate"].ToString() + " " + drAssignment["RES_ASS_Store_NAME"].ToString()));
                //}
                //else
                //{
                    resource.EPM_RES_ASSIGNMENT_SUBMIT
                                                            ("M",
                                                            int.Parse(this.Page.Request["RES_ASS_ID"].ToString()),
                                                            this.ddlRES_ASS_VENDER_CD.SelectedValue.ToString(),
                                                            this.ddlRES_ASS_VEN_AREA_CD.SelectedValue.ToString(),
                                                            this.ddlRES_ASS_VEN_OFFICE_CD.SelectedValue.ToString(),
                                                            int.Parse(this.ddlEPM_CUSTOMER_STORE_L2.Text.ToString()),
                                                            this.txtRES_ASS_Sales.Text.ToString(),
                                                            this.txtRES_ASS_STARTDATE.Text.ToString(),
                                                            this.txtRES_ASS_FINISHDATE.Text.ToString(),
                                                            this.ddlToList.SelectedValue.ToString(),
                                                            int.Parse(this.Session["sRES_ID"].ToString()));

                    Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Assignment.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
                }

            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    // 삭제 버튼 클릭 시
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_ASS_ID"] != null)
            {
                Resource resource = new Resource();
                resource.EPM_RES_ASSIGNMENT_SUBMIT
                                            ("D",
                                            int.Parse(this.Page.Request["RES_ASS_ID"].ToString()));
            }

            Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_Assignment.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}