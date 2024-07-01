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

public partial class m_EXP_Dependents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //// 신규 추가 등록 일 경우
        //if (this.Page.Request["RES_FAM_ID"] == null)
        //    this.btnDel.Visible = false;

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
        DataSet ds = code.DZICUBE_CODE("H1");

        this.ddlRES_FAM_Relation.DataSource = ds;
        this.ddlRES_FAM_Relation.DataBind();
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null && this.Page.Request["DEP_ID"] != "0")
        {
            DataTable dt = Select_Detail();

            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["DEP_AddDate"].ToString()))
                    txtDate.Text = Convert.ToDateTime(dt.Rows[0]["DEP_AddDate"].ToString()).ToShortDateString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["DEP_RELATION"].ToString()))
                    ddlRES_FAM_Relation.SelectedValue = dt.Rows[0]["DEP_RELATION"].ToString();

                if (string.IsNullOrEmpty(dt.Rows[0]["STATUS"].ToString()))
                {
                    this.rdoGB.Items[1].Selected = true;
                    this.rdoGB.Items[0].Enabled = false;
                    this.ddlRES_FAM_Relation.Enabled = false;
                    this.txtRES_FAM_Name.Enabled = false;
                    this.txtRES_FAM_Pnumber1.Enabled = false;
                    this.txtRES_FAM_Pnumber2.Enabled = false;
                    this.txtGetDate.Enabled = false;
                }
                else
                {
                    this.rdoGB.SelectedValue = dt.Rows[0]["DEP_GB"].ToString();

                    if (dt.Rows[0]["STATUS"].ToString().Equals("0"))
                    {
                        this.rdoGB.Items[1].Enabled = false;
                    }
                    else if (dt.Rows[0]["STATUS"].ToString().Equals("1"))
                    {
                        this.rdoGB.Items[0].Enabled = false;
                    }
                    else
                    {
                        this.btnSave.Visible = false;
                        this.rdoGB.Enabled = false;
                        this.ddlRES_FAM_Relation.Enabled = false;
                        this.txtRES_FAM_Name.Enabled = false;
                        this.txtRES_FAM_Pnumber1.Enabled = false;
                        this.txtRES_FAM_Pnumber2.Enabled = false;
                        this.txtGetDate.Enabled = false;
                    }

                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["DEP_Name"].ToString()))
                    txtRES_FAM_Name.Text = dt.Rows[0]["DEP_Name"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["DEP_PersonNumber"].ToString()))
                {
                    this.txtRES_FAM_Pnumber1.Text = dt.Rows[0]["DEP_PersonNumber"].ToString().Substring(0, 6);
                    this.txtRES_FAM_Pnumber2.Text = dt.Rows[0]["DEP_PersonNumber"].ToString().Substring(7, 7);
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["DEP_AcqDate"].ToString()))
                    txtGetDate.Text = Convert.ToDateTime(dt.Rows[0]["DEP_AcqDate"].ToString()).ToShortDateString();
            }
        }
        else if (this.Page.Request["RES_ID"] != null && this.Page.Request["DEP_ID"].ToString() == "0")
        {
            txtDate.Text = DateTime.Today.ToShortDateString();
            this.rdoGB.Items[1].Enabled = false; // 신규일 경우 삭제 요청 막음
        }
    }

    private DataTable Select_Detail()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_DEPENDENTS_DETAIL_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@DEP_ID", this.Page.Request["DEP_ID"].ToString());

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

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        try
        {
            Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            Con.Open();

            SqlCommand CmdRemark = new SqlCommand("EPM_DEPENDENTS_INSERT", Con);
            CmdRemark.CommandType = CommandType.StoredProcedure;
            trans = Con.BeginTransaction();
            CmdRemark.Transaction = trans;

            CmdRemark.Parameters.AddWithValue("@DEP_ID", this.Page.Request["DEP_ID"].ToString());
            CmdRemark.Parameters.AddWithValue("@RES_ID", int.Parse(Session["sRES_ID"].ToString()));
            CmdRemark.Parameters.AddWithValue("@RES_RBS", Session["sRES_RBS_CD"].ToString());
            CmdRemark.Parameters.AddWithValue("@RES_ASSAREA", Session["sRES_RBS_AREA_CD"].ToString());
            CmdRemark.Parameters.AddWithValue("@DEP_ADDDATE", txtDate.Text);
            CmdRemark.Parameters.AddWithValue("@DEP_GB", rdoGB.SelectedValue);
            if(rdoGB.SelectedValue.ToString().Equals("D"))
                CmdRemark.Parameters.AddWithValue("@DEP_DEL_DATE", DateTime.Today.ToShortDateString());
            CmdRemark.Parameters.AddWithValue("@DEP_RELATION", ddlRES_FAM_Relation.SelectedValue);
            CmdRemark.Parameters.AddWithValue("@DEP_NAME", txtRES_FAM_Name.Text);
            CmdRemark.Parameters.AddWithValue("@DEP_PERSONNUMBER", txtRES_FAM_Pnumber1.Text + "-" + txtRES_FAM_Pnumber2.Text);
            CmdRemark.Parameters.AddWithValue("@DEP_ACQDATE", string.IsNullOrEmpty(txtGetDate.Text) ? DBNull.Value : (object)txtGetDate.Text);

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
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_EXP_Dependents_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }

    // 삭제 버튼 클릭 시
    protected void btnDel_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (this.Page.Request["RES_ID"] != null && this.Page.Request["RES_FAM_ID"] != null)
        //    {
        //        Resource resource = new Resource();
        //        resource.EPM_RES_FAMILY_SUBMIT
        //                                    ("D",
        //                                    int.Parse(this.Page.Request["RES_ID"].ToString()),
        //                                    int.Parse(this.Page.Request["RES_FAM_ID"].ToString()),
        //                                    "",
        //                                    "",
        //                                    "",
        //                                    "",
        //                                    "",
        //                                    "",
        //                                    ""
        //                                );
        //    }

        //    Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_Family.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex);
        //}
    }
}