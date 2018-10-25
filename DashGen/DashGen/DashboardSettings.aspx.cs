using DataAccessLayer;
using Microsoft.Web.Administration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DashGen
{
    public partial class DashboardSettings : System.Web.UI.Page
    {

        GenpaDashboardEntities _ctx = new GenpaDashboardEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                //DataAccessLayer.DashboardSettings ds = _ctx.DashboardSettings.ToList().FirstOrDefault();
                //if (ds != null)
                //{
                //    txtEditExpectedWaitingtime.Text = ds.MaxExpectedWaitingTime.ToString();
                //    txtEditQueueWaitingTime.Text = ds.MaxWaitedQueueCount.ToString();
                //    txtEditLongerWaitingTime.Text = ds.MaxLongerWaitTime.ToString();
                //    txtAcdNos.Text = ds.AcdNos.ToString();
                //    txtSkillNos.Text = ds.SkillNos.ToString();
                //    memoMailReceipts.Text = ds.MailReceipts.ToString();
                //    txtDataRangeInHours.Text = ds.BoardDataRangeInHours.ToString();
                //    txtMailSendingScaleInMinutes.Text = ds.MailSendingRepatInMinutes.ToString();
                //    //txtBoardDataLastIntegralTime.Text = ds.BoardDataLastIntegralTime.ToString();
                //    //txtBoardDataIntervalCount.Text = ds.BoardDataIntervalCount.ToString();
                //    memoMailFormat.Text = ds.MailFormat;
                //}
            }

            //Page.ClientScript.RegisterStartupScript(typeof(Page), "background", "ChangeBackground();");
        }

        protected override void OnInit(EventArgs e)
        {
            // Define an Literal control.
            HtmlGenericControl css = new HtmlGenericControl();
            css.TagName = "style";
            css.Attributes.Add("type", "text/css");

            string imageURL = string.Empty;

            List<GeneralConfig> gcs = _ctx.GeneralConfig.ToList();
            imageURL = "images/" + gcs[0].ImagesPath;
            //Update Tag
            css.InnerHtml = @"body{background-image: url(" + imageURL + ");}";


            // Add the Tag to the Head section of the page.
            Page.Header.Controls.Add(css);

            base.OnInit(e);
        } 

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (txtEditExpectedWaitingtime.Text == "" || txtEditLongerWaitingTime.Text == "" || txtEditQueueWaitingTime.Text == "" || txtMailSendingScaleInMinutes.Text == "" || txtDataRangeInHours.Text == "")
            //    return;

            //DataAccessLayer.DashboardSettings dset = _ctx.DashboardSettings.FirstOrDefault();
            //if (dset == null)
            //{
            //    int met = Convert.ToInt32(txtEditExpectedWaitingtime.Text);
            //    int met1 = Convert.ToInt32(txtEditLongerWaitingTime.Text);
            //    int met2 = Convert.ToInt32(txtEditQueueWaitingTime.Text);

            //    DataAccessLayer.DashboardSettings ds = new DataAccessLayer.DashboardSettings()
            //    {
            //        MaxExpectedWaitingTime = met,
            //        MaxLongerWaitTime = met1,
            //        MaxWaitedQueueCount = met2,
            //        ModifiedTime = DateTime.Now,
            //        SkillNos = txtSkillNos.Text,
            //        AcdNos = txtAcdNos.Text,
            //        MailReceipts = memoMailReceipts.Text,
            //        BoardDataRangeInHours = Convert.ToInt32(txtDataRangeInHours.Text),
            //        MailSendingRepatInMinutes = Convert.ToInt32(txtMailSendingScaleInMinutes.Text),
            //        BoardDataLastIntegralTime = 0, //Convert.ToInt32(txtBoardDataLastIntegralTime.Text),
            //        BoardDataIntervalCount = 0, //Convert.ToInt32(txtBoardDataIntervalCount.Text),
            //        MailFormat = memoMailFormat.Text
            //    };
            //    _ctx.DashboardSettings.Add(ds);
            //}
            //else
            //{ 
            //        dset.MaxExpectedWaitingTime = Convert.ToInt32(txtEditExpectedWaitingtime.Text);
            //        dset.MaxLongerWaitTime = Convert.ToInt32(txtEditLongerWaitingTime.Text);
            //        dset.MaxWaitedQueueCount = Convert.ToInt32(txtEditQueueWaitingTime.Text);
            //        dset.ModifiedTime = DateTime.Now;
            //        dset.SkillNos = txtSkillNos.Text;
            //        dset.AcdNos = txtAcdNos.Text;
            //        dset.MailReceipts = memoMailReceipts.Text;
            //        dset.BoardDataRangeInHours = Convert.ToInt32(txtDataRangeInHours.Text);
            //        dset.MailSendingRepatInMinutes = Convert.ToInt32(txtMailSendingScaleInMinutes.Text);
            //        dset.BoardDataLastIntegralTime = 0; //Convert.ToInt32(txtBoardDataLastIntegralTime.Text);
            //        dset.BoardDataIntervalCount = 0; //Convert.ToInt32(txtBoardDataIntervalCount.Text);
            //        dset.MailFormat = memoMailFormat.Text;
            //}

            //_ctx.SaveChanges();

        }

        protected void grdUsers_Load(object sender, EventArgs e)
        {
            grdUsers.SettingsText.PopupEditFormCaption = "Kullanıcı Düzenle";
            grdUsers.SettingsText.CommandUpdate = "Güncelle";
            grdUsers.SettingsText.CommandNew = "Yeni Kullanıcı";
            grdUsers.SettingsText.CommandCancel = "İptal";

            grdDashSettings.SettingsText.PopupEditFormCaption = "Düzenle";
            grdDashSettings.SettingsText.CommandUpdate = "Güncelle";
            grdDashSettings.SettingsText.CommandNew = "Yeni";
            grdDashSettings.SettingsText.CommandCancel = "İptal";

            grdMailSettings.SettingsText.PopupEditFormCaption = "Düzenle";
            grdMailSettings.SettingsText.CommandUpdate = "Güncelle";
            grdMailSettings.SettingsText.CommandNew = "Yeni";
            grdMailSettings.SettingsText.CommandCancel = "İptal";
        }

        protected void btnChangeImage_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/images/") + FileUpload1.FileName;
            if (FileUpload1.FileName != "")
                FileUpload1.SaveAs(path);
            List<GeneralConfig> confs = _ctx.GeneralConfig.ToList();
            if (confs.Count <= 0)
            {
                _ctx.GeneralConfig.Add(new GeneralConfig() { ImagesPath = FileUpload1.FileName });
                _ctx.SaveChanges();
                if (FileUpload1.FileName != null && FileUpload1.FileName != "")
                    confs[0].ImagesPath = FileUpload1.FileName;
                confs[0].ScreenPassAllow = chkPassAllowed.Checked;
                confs[0].ScreenPassTimeInterval = Convert.ToInt32(txtScreenPassTime.Text);
                confs[0].TimeStartOfTheDay = aspxtimeStart.DateTime.TimeOfDay;
                confs[0].TimeAfternoonOfTheDay = aspxtimeAfter.DateTime.TimeOfDay;
                confs[0].TimeEndOfTheDay = aspxtimeEnd.DateTime.TimeOfDay;
            }
            else
            {
                if (FileUpload1.FileName != null && FileUpload1.FileName != "")
                    confs[0].ImagesPath = FileUpload1.FileName;
                confs[0].ScreenPassAllow = chkPassAllowed.Checked;
                confs[0].ScreenPassTimeInterval = Convert.ToInt32(txtScreenPassTime.Text);
                confs[0].TimeStartOfTheDay = aspxtimeStart.DateTime.TimeOfDay;
                confs[0].TimeAfternoonOfTheDay = aspxtimeAfter.DateTime.TimeOfDay;
                confs[0].TimeEndOfTheDay = aspxtimeEnd.DateTime.TimeOfDay;
            }
            _ctx.SaveChanges();
        }

        protected void ASPxGridView2_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            UpdateSession();
        }

        protected void ASPxGridView2_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            UpdateSession();
        }

        protected void ASPxGridView1_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            UpdateSession();
        }

        protected void ASPxGridView1_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            UpdateSession();
        }

        protected void grdUsers_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            UpdateSession();
        }

        protected void grdUsers_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            UpdateSession();
        }

        private void UpdateSession()
        {
            //ArrayList arrayOfApplicationBags = new ArrayList();

            //ServerManager serverManager = new ServerManager();
            //ApplicationPoolCollection applicationPoolCollection = serverManager.ApplicationPools;
            //foreach (ApplicationPool applicationPool in applicationPoolCollection)
            //{
            //    if (applicationPool.Name == "")
            //    {
            //        applicationPool.Stop();
            //        applicationPool.Start();
            //        serverManager.CommitChanges();
            //        break;
            //    }
            //}
            
        }


    }
}