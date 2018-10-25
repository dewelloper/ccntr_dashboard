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
    public partial class Login : System.Web.UI.Page
    {
        GenpaDashboardEntities _ctx = new GenpaDashboardEntities();

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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtLogging_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Kullanıcıadı ve şifresini doğru girmeniz gerekmektedir";
                return;
            }


            Users usr = _ctx.Users.Where(k => k.Username == txtUsername.Text && k.Password == txtPassword.Text).FirstOrDefault();
            if (usr != null)
            {
                Session["CurrentUser"] = usr;
                Response.Redirect("MainBoard.Aspx");
            }
            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Dikkat! :Kullanıcıadı veya şifresi yanlış";
            }
        }

        protected void imgButtonSettings_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Kullanıcıadı ve şifresini doğru girmeniz gerekmektedir";
                return;
            }


            Users usr = _ctx.Users.Where(k => k.Username == txtUsername.Text && k.Password == txtPassword.Text).FirstOrDefault();
            if (usr != null)
            {
                Session["CurrentUser"] = usr;
                Response.Redirect("DashboardSettings.Aspx");
            }
            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Dikkat! :Kullanıcıadı veya şifresi yanlış";
            }
        }

    }
}