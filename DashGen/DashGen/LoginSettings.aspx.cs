using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DashGen
{
    public partial class LoginSettings : System.Web.UI.Page
    {
        GenpaDashboardEntities _ctx = new GenpaDashboardEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void txtLogging_Click(object sender, EventArgs e)
        {
            //if (txtUsername1.Text.Trim() == "" || txtPassword1.Text.Trim() == "")
            //{
            //    lblError1.ForeColor = Color.Red;
            //    lblError1.Text = "Kullanıcıadı ve şifresini doğru girmeniz gerekmektedir";
            //    return;
            //}


            //Users usr = _ctx.Users.Where(k => k.Username == txtUsername1.Text && k.Password == txtPassword1.Text).FirstOrDefault();
            //if (usr != null)
            //{
            //    Session["CurrentUser"] = usr;
            //    Response.Redirect("MainBoard.Aspx");
            //}
            //else
            //{
            //    lblError1.ForeColor = Color.Red;
            //    lblError1.Text = "Dikkat! :Kullanıcıadı veya şifresi yanlış";
            //}
            if (txtUsername1.Text.Trim() == "" || txtPassword1.Text.Trim() == "")
            {
                lblError1.ForeColor = Color.Red;
                lblError1.Text = "Kullanıcıadı ve şifresini doğru girmeniz gerekmektedir";
                return;
            }


            Users usr = _ctx.Users.Where(k => k.Username == txtUsername1.Text && k.Password == txtPassword1.Text).FirstOrDefault();
            if (usr != null)
            {
                Session["CurrentUser"] = usr;
                Response.Redirect("DashboardSettings.Aspx");
            }
            else
            {
                lblError1.ForeColor = Color.Red;
                lblError1.Text = "Dikkat! :Kullanıcıadı veya şifresi yanlış";
            }
        }

        protected void imgButtonSettings_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUsername1.Text.Trim() == "" || txtPassword1.Text.Trim() == "")
            {
                lblError1.ForeColor = Color.Red;
                lblError1.Text = "Kullanıcıadı ve şifresini doğru girmeniz gerekmektedir";
                return;
            }


            Users usr = _ctx.Users.Where(k => k.Username == txtUsername1.Text && k.Password == txtPassword1.Text).FirstOrDefault();
            if (usr != null)
            {
                Session["CurrentUser"] = usr;
                Response.Redirect("DashboardSettings.Aspx");
            }
            else
            {
                lblError1.ForeColor = Color.Red;
                lblError1.Text = "Dikkat! :Kullanıcıadı veya şifresi yanlış";
            }
        }

    }
}