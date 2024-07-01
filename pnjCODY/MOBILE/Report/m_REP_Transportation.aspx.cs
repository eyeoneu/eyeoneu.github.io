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

public partial class m_REP_Transportation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["TRN_ID"].ToString() != "0")
        {
            DataTable dt = Select_Trn_Detail();

            if (dt.Rows.Count > 0)
            {

                txtCustomer.Text = dt.Rows[0]["TRN_CUSTOMER"].ToString();
                ddlType.SelectedValue = dt.Rows[0]["TRN_TYPE"].ToString();
                txtStore.Text = dt.Rows[0]["TRN_STORE"].ToString();
                txtPrice.Text = dt.Rows[0]["TRN_PRICE"].ToString();
                ddlEvidence.SelectedValue = dt.Rows[0]["TRN_EVIDENCE"].ToString().Trim();
                //if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_TYPE"].ToString()))
                //    ddlOilSelector.SelectedValue = dt.Rows[0]["DRV_TYPE"].ToString();

                //if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_LITER"].ToString()))
                //    txtLiter.Text = dt.Rows[0]["DRV_LITER"].ToString();

                //if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_PRICE"].ToString()))
                //    txtPrice.Text = dt.Rows[0]["DRV_PRICE"].ToString();

                //if (!string.IsNullOrEmpty(dt.Rows[0]["DRV_SUM"].ToString()))
                //    txtSum.Text = dt.Rows[0]["DRV_SUM"].ToString();
            }
        }
    }

    private DataTable Select_Trn_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_TRANSPORTATION_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TRN_ID", this.Page.Request["TRN_ID"].ToString());

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

        return ds.Tables[0];
    }

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand CmdRemark = new SqlCommand("EPM_TRANSPORTATION_INSERT_MOBILE", Con);
            CmdRemark.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            CmdRemark.Transaction = trans;

            CmdRemark.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@TRN_ID", this.Page.Request["TRN_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@TRN_CUSTOMER", txtCustomer.Text);
            CmdRemark.Parameters.AddWithValue("@TRN_TYPE", ddlType.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@TRN_STORE", txtStore.Text);
            CmdRemark.Parameters.AddWithValue("@TRN_PRICE", txtPrice.Text);
            CmdRemark.Parameters.AddWithValue("@TRN_EVIDENCE", ddlEvidence.SelectedValue);

            CmdRemark.ExecuteNonQuery();

            trans.Commit();
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (Con != null)
            {
                Con.Close();
                Con = null;
            }
        }

        if (!bError)
        {
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_REP_Transportation_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;

        if (ddl.SelectedValue.Equals("지하철")
            || ddl.SelectedValue.Equals("버스"))
        {
            txtStore.Text = "매장방문 교통비(대중교통)";
            txtStore.Enabled = false;
        }
        else
        {
            txtStore.Text = "";
            txtStore.Enabled = true;
        }
    }
}