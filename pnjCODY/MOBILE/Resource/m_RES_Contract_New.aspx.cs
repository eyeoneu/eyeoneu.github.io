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

public partial class Resource_m_RES_Contract_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            if (Session["sRES_ID"] != null)
            {
                this.lblRES_CON_SUPPORTER.Text = Session["sRES_Name"].ToString() + " (" + Session["sRES_Number"].ToString() + ")";
                this.hdRES_RBS_CD.Value = Session["sRES_RBS_CD"].ToString();
                this.hdRES_RBS_AREA_CD.Value = Session["sRES_RBS_AREA_CD"].ToString();
            }

            // 신규등록 시
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_CON_ID"] == null)
            {
                this.btnContinue.Visible = false;
                this.btnDel.Visible = false;

                if (this.Page.Request["PRE_RES_CON_ID"] == null)
                {
                    this.txtRES_CON_STARTDATE.CssClass = "i_f_out2";
                    this.txtRES_CON_FINISHDATE.CssClass = "i_f_out2";
                }

                SethiddenFeild();
            }

            SetControl();

            // 수정 시
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_CON_ID"] != null)
                SetPage();

            // 계약연장 시
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["PRE_RES_CON_ID"] != null)
                SetPage_Continued();
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
            //this.hdRES_RBS_CD.Value = rd["RES_RBS_CD"].ToString();
            //this.hdRES_RBS_AREA_CD.Value = rd["RES_RBS_AREA_CD"].ToString();
            this.lblRES_Name.Text = rd["RES_Name"].ToString();
            this.hdRES_JoinDate.Value = rd["RES_JoinDate"].ToString();
        }
        rd.Close();
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {       
        // 지원사 코드
        Code code = new Code();
        DataSet dsRES_CON_VENDER_CD = code.EPM_CODE("5");

        this.ddlRES_CON_VENDER_CD.DataSource = dsRES_CON_VENDER_CD;
        this.ddlRES_CON_VENDER_CD.DataBind();

        // 거래처
        DataSet dsEPM_CUSTOMER_STORE = code.EPM_CUSTOMER_STORE();

        this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
        this.ddlEPM_CUSTOMER_STORE.DataBind();
    }

    //// 계약종류 선택 시
    //protected void ddlConType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    SetContractGB();

    //    if (ddlContractGB.SelectedValue.ToString().Equals("1") || ddlContractGB.SelectedValue.ToString().Equals("5") || ddlContractGB.SelectedValue.ToString().Equals("6"))
    //    {
    //        this.divForm.Visible = true;
    //        this.divContractType.Visible = true;
    //        this.divContractGB.Visible = false;

    //        this.ddlRES_CON_VENDER_CD.Enabled = false;
    //        this.ddlRES_CON_VEN_AREA_CD.Enabled = false;
    //        this.ddlRES_CON_VEN_OFFICE_CD.Enabled = false;
    //        this.ddlEPM_CUSTOMER_STORE.Enabled = false;
    //        this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;

    //        setDdlToList();
    //    }
    //    else if (ddlContractGB.SelectedValue.ToString().Equals(""))
    //    {
    //        this.divForm.Visible = false;
    //        this.divContractType.Visible = false;
    //        this.divContractGB.Visible = false;
    //    }
    //    else
    //    {
    //        this.divForm.Visible = true;
    //        this.divContractType.Visible = false;
    //        this.divContractGB.Visible = true;

    //        this.ddlRES_CON_VENDER_CD.Enabled = true;
    //        this.ddlRES_CON_VEN_AREA_CD.Enabled = true;
    //        this.ddlRES_CON_VEN_OFFICE_CD.Enabled = true;
    //        this.ddlEPM_CUSTOMER_STORE.Enabled = true;
    //        this.ddlEPM_CUSTOMER_STORE_L2.Enabled = true;
    //    }

    //    // 선택항목 초기화
    //    this.ddlWorkType.SelectedValue = "";
    //    this.ddlRES_CON_VENDER_CD.SelectedValue = "";
    //    this.ddlRES_CON_VEN_AREA_CD.SelectedValue = "";
    //    this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue = "";
    //    this.ddlEPM_CUSTOMER_STORE.SelectedValue = "";
    //    this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = "";
    //    this.ddlRES_CON_TIME.SelectedValue = "";
    //    this.txtRES_CON_PAY.Text = "";
    //    this.ddlRES_CON_TIME.Enabled = true;
    //    this.txtRES_CON_PAY.Enabled = true;
    //}

    //// 계약형태 DDL 바인딩
    //private void SetContractGB()
    //{
    //    ddlContractGB.Items.Clear();

    //    // 일계약 선택 시
    //    if (ddlRES_CON_Type.SelectedValue.ToString().Equals("D"))
    //    {
    //        ddlContractGB.Items.Add(new ListItem("-선택-", ""));
    //        ddlContractGB.Items.Add(new ListItem("고정일급직", "3"));
    //        ddlContractGB.Items.Add(new ListItem("단기일급직", "4"));
    //        ddlContractGB.Items.Add(new ListItem("대체지원직", "5"));
    //    }
    //    // 월계약 선택 시
    //    else
    //    {
    //        ddlContractGB.Items.Add(new ListItem("-선택-", ""));
    //        ddlContractGB.Items.Add(new ListItem("TO월급직", "6"));
    //        ddlContractGB.Items.Add(new ListItem("고정월급직", "1"));
    //        ddlContractGB.Items.Add(new ListItem("단기월급직(X)", "2"));
    //    }
    //}

    #region 지원사 관련 컨트롤 변경 시 세팅
    // 지원사 코드 변경 시
    protected void ddlRES_CON_VENDER_CD_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetddlRES_CON_VEN_AREA_CD();
    }

    // 소속 컨트롤 세팅
    private void SetddlRES_CON_VEN_AREA_CD()
    {
        this.ddlRES_CON_VEN_AREA_CD.Items.Clear();

        Code code = new Code();
        DataSet ds = code.EPM_VEN_AREA_LIST("L2", this.ddlRES_CON_VENDER_CD.SelectedValue.ToString(), "");

        this.ddlRES_CON_VEN_AREA_CD.DataSource = ds;
        this.ddlRES_CON_VEN_AREA_CD.DataBind();
    }
    
    // 계약구분 변경 시
    protected void ddlContractGB_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        if (ddl.SelectedValue.ToString().Equals("5") && (this.txtRES_CON_STARTDATE.Text == "" || this.txtRES_CON_STARTDATE.Text == "YYYYMMDD"))
        {
            divMsg.Visible = true;
            lblMsg.Text = "대체지원직을 입력할 경우 계약시작일을 먼저 입력해주세요.";

            ddl.SelectedValue = "";
        }
        else
        {
            divMsg.Visible = false;

            if (ddl.SelectedValue.ToString().Equals("1") || ddl.SelectedValue.ToString().Equals("5") || ddl.SelectedValue.ToString().Equals("6"))
            {
                this.divForm.Visible = true;
                this.divContractType.Visible = true;
                this.divContractGB.Visible = false;

                this.ddlRES_CON_VENDER_CD.Enabled = false;
                this.ddlRES_CON_VEN_AREA_CD.Enabled = false;
                this.ddlRES_CON_VEN_OFFICE_CD.Enabled = false;
                this.ddlEPM_CUSTOMER_STORE.Enabled = false;
                this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;
                this.ddlRES_CON_Type.Enabled = false;
                this.ddlRES_CON_TIME.Enabled = false;
                this.txtRES_CON_PAY.Enabled = false;

                setDdlToList();
            }
            else
            {
                this.divForm.Visible = true;
                this.divContractType.Visible = false;
                this.divContractGB.Visible = true;

                this.ddlRES_CON_VENDER_CD.Enabled = true;
                this.ddlRES_CON_VEN_AREA_CD.Enabled = true;
                this.ddlRES_CON_VEN_OFFICE_CD.Enabled = true;
                this.ddlEPM_CUSTOMER_STORE.Enabled = true;
                this.ddlEPM_CUSTOMER_STORE_L2.Enabled = true;
                this.ddlRES_CON_Type.Enabled = true;
                this.ddlRES_CON_TIME.Enabled = true;
                this.txtRES_CON_PAY.Enabled = true;
            }

            // 선택항목 초기화
            this.ddlWorkType.SelectedValue = "";
            this.ddlRES_CON_VENDER_CD.SelectedValue = "";
            this.ddlRES_CON_VEN_AREA_CD.SelectedValue = "";
            this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue = "";
            this.ddlEPM_CUSTOMER_STORE.SelectedValue = "";
            this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = "";
            this.ddlRES_CON_Type.SelectedValue = "";
            this.ddlRES_CON_TIME.SelectedValue = "";
            this.txtRES_CON_PAY.Text = "";
            
            // 무기일급직 또는 기간일급직을 선택했을경우 계약종류 항목을 일계약으로 고정 (2018-03-28 정창화 수정)
            if (ddl.SelectedValue.ToString().Equals("3") || ddl.SelectedValue.ToString().Equals("4"))
            {
                this.ddlRES_CON_Type.SelectedValue = "D";
                this.ddlRES_CON_Type.Enabled = false;
            }
            
            // 공조월급직 일 경우: 지원사=공조, 소속=(CH)유통영업부, 계약종류=월 계약, 일근무시간=0, 일/월급여=0 (2024-01-08 정창화 수정)
            if (ddl.SelectedValue.ToString().Equals("9"))
			{
			    this.ddlRES_CON_VENDER_CD.SelectedValue = "015";  
			    this.ddlRES_CON_VENDER_CD.Enabled = false;
			    SetddlRES_CON_VEN_AREA_CD();
			    
			    this.ddlRES_CON_VEN_AREA_CD.SelectedValue = "011";  
			    this.ddlRES_CON_VEN_AREA_CD.Enabled = false;
			    SetddlRES_CON_VEN_OFFICE_CD();
			    
			    this.ddlRES_CON_Type.SelectedValue = "M";
                this.ddlRES_CON_Type.Enabled = false;
                
                this.ddlRES_CON_TIME.SelectedValue = "0";
                this.ddlRES_CON_TIME.Enabled = false;
                
				this.txtRES_CON_PAY.Text = "0";
				this.txtRES_CON_PAY.Enabled = false;
			}
        }
    }

    private void setDdlToList()
    {
        // TO 리스트
        DataSet dsTo = Select_TOCodeList(Session["sRES_ID"].ToString(), Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());

        //#region TO 코드리스트 조회 현재 직원이 배정되어 있는 TO는 제외 한다
        //if (this.Page.Request["RES_CON_ID"] == null)
        //{
        //    for (int i = 0; i < dsTo.Tables[0].Rows.Count; i++)
        //    {
        //        if (getContractStatus(dsTo.Tables[0].Rows[i]["TO_ID"].ToString()))
        //            dsTo.Tables[0].Rows[i].Delete();
        //    }

        //    dsTo.Tables[0].AcceptChanges();
        //}
        //#endregion

        this.ddlToList.DataSource = dsTo;
        this.ddlToList.DataBind();
    }

    protected void ddlToList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        DataSet ds = Select_TOCodeDetail(ddl.SelectedValue.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_Vender"].ToString()))
            {
                Code code = new Code();
                DataSet dsRES_CON_VENDER_CD = code.EPM_CODE("5");

                this.ddlRES_CON_VENDER_CD.DataSource = dsRES_CON_VENDER_CD;
                this.ddlRES_CON_VENDER_CD.DataBind();

                this.ddlRES_CON_VENDER_CD.SelectedValue = ds.Tables[0].Rows[0]["TO_Vender"].ToString().Trim();
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_VenArea"].ToString()))
            {
                SetddlRES_CON_VEN_AREA_CD();
                this.ddlRES_CON_VEN_AREA_CD.SelectedValue = ds.Tables[0].Rows[0]["TO_VenArea"].ToString().Trim();
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_VenOffice"].ToString()))
            {
                SetddlRES_CON_VEN_OFFICE_CD();
                this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue = ds.Tables[0].Rows[0]["TO_VenOffice"].ToString().Trim();
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_Customer"].ToString()))
            {
                // 거래처
                Code code = new Code();
                DataSet dsEPM_CUSTOMER_STORE = code.EPM_CUSTOMER_STORE();

                this.ddlEPM_CUSTOMER_STORE.DataSource = dsEPM_CUSTOMER_STORE;
                this.ddlEPM_CUSTOMER_STORE.DataBind();

                this.ddlEPM_CUSTOMER_STORE.SelectedValue = ds.Tables[0].Rows[0]["TO_Customer"].ToString().Trim();
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_Store"].ToString()))
            {
                SetddlEPM_CUSTOMER_STORE_L2();
                this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = ds.Tables[0].Rows[0]["TO_Store"].ToString().Trim();
            }

            // 대체지원직을 선택할 경우 계약종류를 일계약으로 고정
            if (this.ddlContractGB.SelectedValue == "5")
            {
                this.ddlRES_CON_Type.SelectedValue = "D";
                this.ddlRES_CON_Type.Enabled = false;
            }
            else
            {
				if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_CON_TYPE"].ToString()))
				{

					this.ddlRES_CON_Type.SelectedValue = ds.Tables[0].Rows[0]["TO_CON_TYPE"].ToString().Trim();
					this.ddlRES_CON_Type.Enabled = false;
				}
				else
				{
					this.ddlRES_CON_TIME.SelectedValue = "";
					this.ddlRES_CON_TIME.Enabled = true;
				}
			}
			

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_TIME"].ToString()))
            {
                this.ddlRES_CON_TIME.SelectedValue = ds.Tables[0].Rows[0]["TO_TIME"].ToString().Trim();
                this.ddlRES_CON_TIME.Enabled = false;
            }
            else
            {
                this.ddlRES_CON_TIME.SelectedValue = "";
                this.ddlRES_CON_TIME.Enabled = true;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TO_PAY"].ToString()))
            {
                this.txtRES_CON_PAY.Text = ds.Tables[0].Rows[0]["TO_PAY"].ToString(); // 일/월 급여 셋팅
                this.txtRES_CON_PAY.Enabled = false;
            }
            else
            {
                this.txtRES_CON_PAY.Text = "";
                this.txtRES_CON_PAY.Enabled = true;
            }
        }
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
        adp.SelectCommand.Parameters.AddWithValue("@TO_TYPE", this.ddlContractGB.SelectedValue);

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

    #region TO 상세 조회
    private DataSet Select_TOCodeDetail(string toID)
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_TO_DETAIL_MOBILE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@TO_NUM", toID);
        adp.SelectCommand.Parameters.AddWithValue("@TO_TYPE", this.ddlContractGB.SelectedValue);
        adp.SelectCommand.Parameters.AddWithValue("@CON_STARTDATE_MOBILE", this.txtRES_CON_STARTDATE.Text.ToString());  

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


    // 소속 코드 변경 시
    protected void ddlRES_CON_VEN_AREA_CD_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetddlRES_CON_VEN_OFFICE_CD();
    }

    // 근무부서 컨트롤 세팅
    private void SetddlRES_CON_VEN_OFFICE_CD()
    {
        this.ddlRES_CON_VEN_OFFICE_CD.Items.Clear();

        Code code = new Code();
        DataSet ds = code.EPM_VEN_AREA_LIST("L3", this.ddlRES_CON_VENDER_CD.SelectedValue.ToString(), this.ddlRES_CON_VEN_AREA_CD.SelectedValue.ToString());

        this.ddlRES_CON_VEN_OFFICE_CD.DataSource = ds;
        this.ddlRES_CON_VEN_OFFICE_CD.DataBind();
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

        SqlDataReader rd = resource.EPM_RES_CONTRACT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["RES_CON_ID"].ToString()));

        if (rd.Read())
        {
            this.divForm.Visible = true;
            this.lblRES_Name.Text = rd["RES_Name"].ToString();

            

            //SetContractGB();

            if (!string.IsNullOrEmpty(rd["RES_CON_IS_TO_LINK"].ToString()))
            {
                this.ddlContractGB.SelectedValue = rd["RES_CON_IS_TO_LINK"].ToString();

                if (rd["RES_CON_IS_TO_LINK"].ToString().Equals("1") || rd["RES_CON_IS_TO_LINK"].ToString().Equals("5") || rd["RES_CON_IS_TO_LINK"].ToString().Equals("6")) // DDL변경이 TO지원직도 포함되도록 변경 (2016-01-06 정창화 수정)
                {
                    this.divContractType.Visible = true;
                    this.divContractGB.Visible = false;

                    setDdlToList();

                    if (!string.IsNullOrEmpty(rd["TO_NUM"].ToString()))
                    {
                        this.ddlToList.SelectedValue = rd["TO_NUM"].ToString();

                        this.ddlRES_CON_VENDER_CD.Enabled = false;
                        this.ddlRES_CON_VEN_AREA_CD.Enabled = false;
                        this.ddlRES_CON_VEN_OFFICE_CD.Enabled = false;
                        this.ddlRES_CON_VEN_OFFICE_CD.Enabled = false;
                        this.ddlEPM_CUSTOMER_STORE.Enabled = false;
                        this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;
                        this.ddlRES_CON_Type.Enabled = false;
                    }
                }
                else
                {
                    this.divContractType.Visible = false;
                    this.divContractGB.Visible = true;

                    if (!string.IsNullOrEmpty(rd["RES_CON_GB"].ToString()))
                        this.ddlWorkType.SelectedValue = rd["RES_CON_GB"].ToString();
                }

            }

            // 지원사, 소속, 근무부서 바인딩
            this.ddlRES_CON_VENDER_CD.SelectedValue = rd["RES_CON_Vender_CD"].ToString();
            if (rd["RES_CON_Vender_CD"].ToString() != "")
            {
                SetddlRES_CON_VEN_AREA_CD();
                this.ddlRES_CON_VEN_AREA_CD.SelectedValue = rd["RES_CON_VEN_Area_CD"].ToString();
            }
            if (rd["RES_CON_VEN_Area_CD"].ToString() != "")
            {
                SetddlRES_CON_VEN_OFFICE_CD();
                this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue = rd["RES_CON_VEN_Office_CD"].ToString();
            }

            // 거래처, 매장 바인딩
            this.ddlEPM_CUSTOMER_STORE.SelectedValue = rd["RES_CON_CustomerID"].ToString();
            if (rd["RES_CON_CustomerID"].ToString() != "")
            {
                SetddlEPM_CUSTOMER_STORE_L2();
                this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = rd["RES_CON_StoreID"].ToString();
            }

            this.ddlRES_CON_Type.SelectedValue = rd["RES_CON_Type"].ToString();

            this.txtRES_CON_Sales.Text = rd["RES_CON_Sales"].ToString();

            if (!string.IsNullOrEmpty(rd["RES_CON_TIME"].ToString()))
            {
                this.ddlRES_CON_TIME.SelectedValue = rd["RES_CON_TIME"].ToString().Trim();
                this.ddlRES_CON_TIME.Enabled = false;
            }
            else
            {
                this.ddlRES_CON_TIME.SelectedValue = "";
                this.ddlRES_CON_TIME.Enabled = true;
            }

            if (!string.IsNullOrEmpty(rd["RES_CON_PAY"].ToString()))
            {
                this.txtRES_CON_PAY.Text = rd["RES_CON_PAY"].ToString().Trim();
                this.txtRES_CON_PAY.Enabled = false;
            }
            else
            {
                this.txtRES_CON_PAY.Text = "";
                this.txtRES_CON_PAY.Enabled = true;
            }

            this.txtRES_CON_STARTDATE.Text = rd["RES_CON_STARTDATE"].ToString();
            this.txtRES_CON_FINISHDATE.Text = rd["RES_CON_FINISHDATE"].ToString();


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

            if (this.txtRES_CON_STARTDATE.Text.ToString() != "")
                if (DateTime.Parse(this.txtRES_CON_STARTDATE.Text.ToString()) < DateTime.Parse(ReferenceDay))
                    this.txtRES_CON_STARTDATE.Enabled = false;

            if (this.txtRES_CON_FINISHDATE.Text.ToString() != "")
                if (DateTime.Parse(this.txtRES_CON_FINISHDATE.Text.ToString()) < DateTime.Parse(ReferenceDay))
                    this.txtRES_CON_FINISHDATE.Enabled = false;


            //////  임시 허용  ///////////////////////////////////////////////////////////////////////////////////////////////////////
            //if (this.txtRES_CON_STARTDATE.Text.ToString() != "") 
            //    if (DateTime.Parse(this.txtRES_CON_STARTDATE.Text.ToString()) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            //        this.txtRES_CON_STARTDATE.Enabled = false;

            //if (this.txtRES_CON_FINISHDATE.Text.ToString() != "") 
            //    if (DateTime.Parse(this.txtRES_CON_FINISHDATE.Text.ToString()) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            //        this.txtRES_CON_FINISHDATE.Enabled = false;

            // 계약확정 인 경우 삭제 버튼을 숨긴다.
            if (rd["RES_CON_State"].ToString() == "002")
            {
                this.btnDel.Visible = false;
                this.btnSave.Visible = false;
            }
        }

        rd.Close();
    }

    // 계약연장 일 경우 페이지 데이터를 바인딩
    private void SetPage_Continued()
    {
        this.lblTitle.Text = "(계약연장)";

        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_CONTRACT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), int.Parse(this.Page.Request["PRE_RES_CON_ID"].ToString()));

        if (rd.Read())
        {
            this.lblRES_Name.Text = rd["RES_Name"].ToString();

            this.divForm.Visible = true;
            this.lblRES_Name.Text = rd["RES_Name"].ToString();

            if (!string.IsNullOrEmpty(rd["RES_CON_IS_TO_LINK"].ToString()))
            {
                this.ddlContractGB.SelectedValue = rd["RES_CON_IS_TO_LINK"].ToString();

                if (rd["RES_CON_IS_TO_LINK"].ToString().Equals("1") || rd["RES_CON_IS_TO_LINK"].ToString().Equals("5") || rd["RES_CON_IS_TO_LINK"].ToString().Equals("6")) // DDL변경이 TO지원직도 포함되도록 변경 (2016-01-06 정창화 수정)
                {
                    this.divContractType.Visible = true;
                    this.divContractGB.Visible = false;

                    setDdlToList();

                    if (!string.IsNullOrEmpty(rd["TO_NUM"].ToString()))
                    {
                        this.ddlToList.SelectedValue = rd["TO_NUM"].ToString();

                        this.ddlRES_CON_VENDER_CD.Enabled = false;
                        this.ddlRES_CON_VEN_AREA_CD.Enabled = false;
                        this.ddlRES_CON_VEN_OFFICE_CD.Enabled = false;
                        this.ddlRES_CON_VEN_OFFICE_CD.Enabled = false;
                        this.ddlEPM_CUSTOMER_STORE.Enabled = false;
                        this.ddlEPM_CUSTOMER_STORE_L2.Enabled = false;
                        this.ddlRES_CON_Type.Enabled = false;
                    }
                }
                else
                {
                    this.divContractType.Visible = false;
                    this.divContractGB.Visible = true;

                    if (!string.IsNullOrEmpty(rd["RES_CON_GB"].ToString()))
                        this.ddlWorkType.SelectedValue = rd["RES_CON_GB"].ToString();
                }

            }

            // 지원사, 소속, 근무부서 바인딩
            this.ddlRES_CON_VENDER_CD.SelectedValue = rd["RES_CON_Vender_CD"].ToString();
            if (rd["RES_CON_Vender_CD"].ToString() != "")
            {
                SetddlRES_CON_VEN_AREA_CD();
                this.ddlRES_CON_VEN_AREA_CD.SelectedValue = rd["RES_CON_VEN_Area_CD"].ToString();
            }
            if (rd["RES_CON_VEN_Area_CD"].ToString() != "")
            {
                SetddlRES_CON_VEN_OFFICE_CD();
                this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue = rd["RES_CON_VEN_Office_CD"].ToString();
            }

            // 거래처, 매장 바인딩
            this.ddlEPM_CUSTOMER_STORE.SelectedValue = rd["RES_CON_CustomerID"].ToString();
            if (rd["RES_CON_CustomerID"].ToString() != "")
            {
                SetddlEPM_CUSTOMER_STORE_L2();
                this.ddlEPM_CUSTOMER_STORE_L2.SelectedValue = rd["RES_CON_StoreID"].ToString();
            }

            this.ddlRES_CON_Type.SelectedValue = rd["RES_CON_Type"].ToString();

            this.txtRES_CON_Sales.Text = rd["RES_CON_Sales"].ToString();
            this.ddlRES_CON_TIME.SelectedValue = rd["RES_CON_TIME"].ToString();
            //this.txtRES_CON_PAY.Text = rd["RES_CON_PAY"].ToString();

            if (!string.IsNullOrEmpty(rd["RES_CON_PAY"].ToString()))
            {
                this.txtRES_CON_PAY.Text = rd["RES_CON_PAY"].ToString().Trim();
                this.txtRES_CON_PAY.Enabled = false;
            }
            else
            {
                this.txtRES_CON_PAY.Text = "";
                this.txtRES_CON_PAY.Enabled = true;
            }

            this.txtRES_CON_STARTDATE.Text = DateTime.Parse(rd["RES_CON_STARTDATE"].ToString()).AddMonths(1).ToString("yyyy-MM-dd");
            this.txtRES_CON_FINISHDATE.Text = DateTime.Parse(this.txtRES_CON_STARTDATE.Text).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            string ReferenceDay = ""; // 기준일

            // 현재일이 1일 일 경우
            if (DateTime.Now.ToString("dd") == "01")
            {
                ReferenceDay = DateTime.Now.AddMonths(1).ToString("yyyy-MM-01"); // 이전 달의 1일
            }
            else
            {
                ReferenceDay = DateTime.Now.ToString("yyyy-MM-01"); // 이번 달의 1일
            }

            if (this.txtRES_CON_STARTDATE.Text.ToString() != "")
                if (DateTime.Parse(this.txtRES_CON_STARTDATE.Text.ToString()) < DateTime.Parse(ReferenceDay))
                    this.txtRES_CON_STARTDATE.Enabled = false;

            if (this.txtRES_CON_FINISHDATE.Text.ToString() != "")
                if (DateTime.Parse(this.txtRES_CON_FINISHDATE.Text.ToString()) < DateTime.Parse(ReferenceDay))
                    this.txtRES_CON_FINISHDATE.Enabled = false;

        }

        rd.Close();
    }

    private bool getContractStatus(string toID)
    {
        bool retValue = false;

        Resource resource = new Resource();
        DataSet ds = resource.EPM_RES_CONTRACT_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        DataRow[] dr = ds.Tables[0].Select(string.Format("TO_NUM={0}", toID));

        if (dr.Length > 0)
            retValue = true;

        return retValue;
    }


    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String JoinDate =  string.IsNullOrEmpty(this.hdRES_JoinDate.Value) ? DateTime.Today.ToShortDateString() : this.hdRES_JoinDate.Value.ToString().Replace("-", "");
        String StartDate = this.txtRES_CON_STARTDATE.Text.ToString().Replace("-", "");

        int sYear = int.Parse(JoinDate.Substring(0, 4));
        int sMonth = int.Parse(JoinDate.Substring(4, 2));
        int eYear = int.Parse(StartDate.Substring(0, 4));
        int eMonth = int.Parse(StartDate.Substring(4, 2));

        int month_diff = (eYear - sYear) * 12 + (eMonth - sMonth);


        // 일계약을 저장할 경우 계약시작 종료일의 년,월 값이 같지 않으면 경고 문구를 출력한다.
        if (this.ddlRES_CON_Type.SelectedValue.ToString() == "D" && this.txtRES_CON_STARTDATE.Text.ToString().Replace("-", "").Substring(0, 6) != this.txtRES_CON_FINISHDATE.Text.ToString().Replace("-", "").Substring(0, 6))
        {
            divMsg.Visible = true;
            lblMsg.Text = "계약종료일은 계약시작일과 동일한 년도 및 월의 기간 안에서 입력해야 합니다.";
        }
        // 고용형태가 일용직 일 경우 입사일과 계약 시작일자의 년,월을 비교하여 3개월이 초과되지 않도록 제한 (2017-10-13 입사의 경우 2018-01-01 계약이 불가능)
        else if (this.hdRES_WorkType.Value.ToString() == "002" && month_diff >= 3)
        {
            divMsg.Visible = true;
            lblMsg.Text = "일용직 사원은 입사일의 월을 기준으로 3개월이 지나면 계약을 진행할 수 없습니다.";
        }
		else
		{
			Resource resource = new Resource();

			try
			{
				if (this.txtRES_CON_FINISHDATE.Text.ToString() == "YYYYMMDD")
					this.txtRES_CON_FINISHDATE.Text = "";
				// 삽입
				string strToID = string.Empty;
				string strConGB = string.Empty;

				if (this.ddlContractGB.SelectedValue.ToString().Equals("1") || this.ddlContractGB.SelectedValue.ToString().Equals("5") || this.ddlContractGB.SelectedValue.ToString().Equals("6"))
					strToID = this.ddlToList.SelectedValue.ToString();
				else
					strConGB = this.ddlWorkType.SelectedValue.ToString();

				if (this.Page.Request["RES_CON_ID"] == null)
				{
					resource.EPM_RES_CONTRACT_SUBMIT
															("I",
															int.Parse(this.Page.Request["RES_ID"].ToString()),
															this.hdRES_WorkType.Value.ToString(),
															this.hdRES_WorkGroup1.Value.ToString(),
															this.hdRES_WorkGroup2.Value.ToString(),
															this.hdRES_WorkGroup3.Value.ToString(),
															this.ddlRES_CON_VENDER_CD.SelectedValue.ToString(),
															this.ddlRES_CON_VEN_AREA_CD.SelectedValue.ToString(),
															this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue.ToString(),
															int.Parse(this.Session["sRES_ID"].ToString()),
															this.hdRES_RBS_CD.Value.ToString(),
															this.hdRES_RBS_AREA_CD.Value.ToString(),
															int.Parse(this.ddlEPM_CUSTOMER_STORE_L2.Text.ToString()),
															this.txtRES_CON_Sales.Text.ToString(),
															this.txtRES_CON_STARTDATE.Text.ToString(),
															this.txtRES_CON_FINISHDATE.Text.ToString(),
															"001",
															this.ddlRES_CON_Type.SelectedValue.ToString(),
															this.ddlRES_CON_TIME.SelectedValue.ToString(),
															this.txtRES_CON_PAY.Text.ToString(),
															this.ddlContractGB.SelectedValue.ToString(),
															strToID,
															strConGB,
															int.Parse(this.Session["sRES_ID"].ToString())
															);

					Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Contract.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
				}
				//수정 
				else
				{
					resource.EPM_RES_CONTRACT_SUBMIT
															("M",
															int.Parse(this.Page.Request["RES_CON_ID"].ToString()),
															int.Parse(this.Page.Request["RES_ID"].ToString()),
															this.ddlRES_CON_VENDER_CD.SelectedValue.ToString(),
															this.ddlRES_CON_VEN_AREA_CD.SelectedValue.ToString(),
															this.ddlRES_CON_VEN_OFFICE_CD.SelectedValue.ToString(),
															int.Parse(this.ddlEPM_CUSTOMER_STORE_L2.Text.ToString()),
															this.txtRES_CON_Sales.Text.ToString(),
															this.txtRES_CON_STARTDATE.Text.ToString(),
															this.txtRES_CON_FINISHDATE.Text.ToString(),
															this.ddlRES_CON_Type.SelectedValue.ToString(),
															this.ddlRES_CON_TIME.SelectedValue.ToString(),
															this.txtRES_CON_PAY.Text.ToString(),
															this.ddlContractGB.SelectedValue.ToString(),
															strToID,
															strConGB,
															 int.Parse(this.Session["sRES_ID"].ToString())
															);

					Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Contract.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());

				}
			}
			catch (Exception ex)
			{
				Response.Write(ex);
			}
		}
    }

    // 삭제 버튼 클릭 시
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_CON_ID"] != null)
            {
                Resource resource = new Resource();
                resource.EPM_RES_CONTRACT_SUBMIT
                                            ("D",
                                            int.Parse(this.Page.Request["RES_CON_ID"].ToString()));
            }

            Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_Contract.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }


    // 계약연장 버튼 클릭 시
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Common.scriptAlert(this.Page, "계약연장 페이지로 이동합니다.", "/Resource/m_RES_Contract_New.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString() + "&PRE_RES_CON_ID=" + this.Page.Request["RES_CON_ID"].ToString());
    }
}