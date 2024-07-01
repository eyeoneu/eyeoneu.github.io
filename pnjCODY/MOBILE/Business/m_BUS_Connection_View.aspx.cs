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
using System.IO;

public partial class m_BUS_Connection_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetList();
        }
    }


    #region 목록 조회
    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_TO_HISTORY_LIST", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@HIS_ID", this.Page.Request["HIS_ID"].ToString());

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
        return ds;
    }
    #endregion

    #region 컨트롤 바인딩
    private void SetList()
    {
        DataSet dsList = null;
        dsList = Select_List();
        ViewState["ds"] = dsList;

        if (dsList.Tables[0].Rows.Count != 0)
        {        
            this.txtHIS_GB.Text = dsList.Tables[0].Rows[0]["HIS_GB_NAME"].ToString();
            this.txtHIS_TYPE.Text = dsList.Tables[0].Rows[0]["HIS_TYPE"].ToString();
            this.txtHIS_TEXT.Text = dsList.Tables[0].Rows[0]["HIS_TEXT"].ToString();
            if(!string.IsNullOrEmpty(dsList.Tables[0].Rows[0]["TO_TIME"].ToString()))
                this.txtTO_TimeNPay.Text ="일근무시간 (" + dsList.Tables[0].Rows[0]["TO_TIME"].ToString() + " 시간)  일/월급여(" + dsList.Tables[0].Rows[0]["TO_PAY"].ToString() + " 원)";
            this.txtHIS_Reason.Text = dsList.Tables[0].Rows[0]["HIS_Reason"].ToString();
            this.txtSPT_RES_NAME.Text = dsList.Tables[0].Rows[0]["TO_SPT_RES_NAME"].ToString();
            this.txtHIS_REQDATE.Text = dsList.Tables[0].Rows[0]["HIS_REQDATE"].ToString();
            this.txtHIS_DUEDATE.Text = dsList.Tables[0].Rows[0]["HIS_DUEDATE"].ToString();
            this.txtSTATUS.Text = dsList.Tables[0].Rows[0]["HIS_STATUS_NAME"].ToString();
            this.txtWorkType.Text = dsList.Tables[0].Rows[0]["TO_GB"].ToString();

            if (dsList.Tables[0].Rows[0]["HIS_TYPE"].ToString() == "계좌번호변경")
            {
                this.divFile.Visible = true;

                if (!String.IsNullOrEmpty(dsList.Tables[2].Rows[0]["FILE_NAME"].ToString()))
                {
                    byte[] image = (byte[])dsList.Tables[2].Rows[0]["FILE_INFO"];
                    MemoryStream ms = new MemoryStream(image, 0, image.Length);
                    string base64String = Convert.ToBase64String(image, 0, image.Length);
                    imgRES_Picture.ImageUrl = "data:image/jpg;base64," + base64String;
                }
            }

            if (dsList.Tables[0].Rows[0]["TO_GB"].ToString().Equals("격고") || dsList.Tables[0].Rows[0]["TO_GB"].ToString().Equals("순회"))
            {
                pnlStores.Visible = true;

                if(dsList.Tables[1].Rows.Count > 0)
                {
                    gvList.DataSource = dsList.Tables[1];
                    gvList.DataBind();
                }
            }
            else
                pnlStores.Visible = false;
        }

    }
    #endregion

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView row = (DataRowView)e.Row.DataItem;

        string morning = string.Empty;
        string afternoon = string.Empty;
        string wholeday = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                //e.Row.Attributes["onClick"] = string.Format("fncDelete('{0}', '{1}', '{2}');", row["TO_STORE"].ToString(), row["TO_CUSTOMER_NAME"].ToString(), row["TO_STORE_NAME"].ToString());

                e.Row.Cells[0].Text = row["TO_CUSTOMER_NAME"].ToString();
                e.Row.Cells[1].Text = row["TO_STORE_NAME"].ToString();

                if (row["TO_MONDAY"].ToString().Equals("오전"))
                    morning += "월";
                if (row["TO_MONDAY"].ToString().Equals("오후"))
                    afternoon += "월";
                if (row["TO_MONDAY"].ToString().Equals("종일"))
                    wholeday += "월";

                if (row["TO_TUESDAY"].ToString().Equals("오전"))
                    morning += "화";
                if (row["TO_TUESDAY"].ToString().Equals("오후"))
                    afternoon += "화";
                if (row["TO_TUESDAY"].ToString().Equals("종일"))
                    wholeday += "화";

                if (row["TO_WEDNESDAY"].ToString().Equals("오전"))
                    morning += "수";
                if (row["TO_WEDNESDAY"].ToString().Equals("오후"))
                    afternoon += "수";
                if (row["TO_WEDNESDAY"].ToString().Equals("종일"))
                    wholeday += "수";

                if (row["TO_THURSDAY"].ToString().Equals("오전"))
                    morning += "목";
                if (row["TO_THURSDAY"].ToString().Equals("오후"))
                    afternoon += "목";
                if (row["TO_THURSDAY"].ToString().Equals("종일"))
                    wholeday += "목";

                if (row["TO_FRIDAY"].ToString().Equals("오전"))
                    morning += "금";
                if (row["TO_FRIDAY"].ToString().Equals("오후"))
                    afternoon += "금";
                if (row["TO_FRIDAY"].ToString().Equals("종일"))
                    wholeday += "금";

                if (!string.IsNullOrEmpty(row["TO_SATURDAY"].ToString()))
                {
                    if (row["TO_SATURDAY"].ToString().Equals("오전"))
                        morning += "토";
                    if (row["TO_SATURDAY"].ToString().Equals("오후"))
                        afternoon += "토";
                    if (row["TO_SATURDAY"].ToString().Equals("종일"))
                        wholeday += "토";
                }

                if (!string.IsNullOrEmpty(row["TO_SUNDAY"].ToString()))
                {
                    if (row["TO_SUNDAY"].ToString().Equals("오전"))
                        morning += "일";
                    if (row["TO_SUNDAY"].ToString().Equals("오후"))
                        afternoon += "일";
                    if (row["TO_SUNDAY"].ToString().Equals("종일"))
                        wholeday += "일";
                }

                e.Row.Cells[2].Text = GetCommaString(morning);
                e.Row.Cells[3].Text = GetCommaString(afternoon);
                e.Row.Cells[4].Text = GetCommaString(wholeday);

                if (!string.IsNullOrEmpty(row["HIS_STATUS"].ToString()))
                {
                    //if (row["HIS_STATUS"].ToString().Equals("D") || row["HIS_STATUS"].ToString().Equals("R"))
                    //{
                        if (!string.IsNullOrEmpty(row["TO_Status"].ToString()))
                        {
                            e.Row.Cells[1].Text += row["TO_Status"].ToString().Replace("N", " (추가)").Replace("E", " (변경)").Replace("D", " (삭제)");
                            if (row["TO_Status"].ToString().Equals("D"))
                                e.Row.ForeColor = System.Drawing.Color.Red;
                            if (row["TO_Status"].ToString().Equals("N"))
                                e.Row.ForeColor = System.Drawing.Color.Blue;
                            if (row["TO_Status"].ToString().Equals("E"))
                                e.Row.ForeColor = System.Drawing.Color.Green;
                        }
                    //}
                }
                //e.Row.Attributes["style"] = "cursor: pointer;";
            }
        }
    }

    private string GetCommaString(string inputString)
    {
        for (int i = 0; i < inputString.Length; i++)
        {
            if (i % 2 != 0)
                inputString = inputString.Insert(i, ",");
        }

        return inputString;
    }

}