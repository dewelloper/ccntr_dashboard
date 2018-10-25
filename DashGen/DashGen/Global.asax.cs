using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


namespace DashGen
{
    //This class has been writen by hamit Yıldırım at 18.03.2015
    //Aim: Calls a service who provides dashboard indicator knowledge and saves it to a database for a feature use 
    //This background threadd is also hosted in iis  as a second application pool it must be restarted when dasshboard settings changed in this reason the application side has an update method  which will be restart this pool by after any ui settings has changed

    public class Global : System.Web.HttpApplication
    {
        private static GenpaDashboardEntities _ctx = new GenpaDashboardEntities();
        private static DateTime _lastSendingMailTime = new DateTime();

        protected void Application_Start(object sender, EventArgs e)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += new DoWorkEventHandler(MailSender);
            //worker.WorkerReportsProgress = false;
            //worker.WorkerSupportsCancellation = true;
            //worker.RunWorkerCompleted +=
            //       new RunWorkerCompletedEventHandler(WorkerCompleted);


            //worker.RunWorkerAsync(); 
        }

        private static void MailSender(object sender, DoWorkEventArgs e)
        {
            GenpaDashboardService.CmsDataClient client = new GenpaDashboardService.CmsDataClient();
            GenpaDashboardService.CmsParameters param = new GenpaDashboardService.CmsParameters();

            List<DataAccessLayer.DashboardSettings> dss = _ctx.DashboardSettings.ToList();
            foreach (DataAccessLayer.DashboardSettings ds in dss)
            {
                param.AcdNo = 0;
                param.SkillNo = 0;

                List<string> skills = ds.SkillNos.Split(',').ToList();
                foreach (string skill in skills)
                {
                    if (ds != null)
                    {
                        param.AcdNo = Convert.ToInt32(ds.AcdNos.Split(',').ToList()[0]); 
                        param.SkillNo = Convert.ToInt32(skill);
                        if (param.AcdNo == 0 || param.SkillNo == 0)
                            return;
                    }
                    else return;

                    var report1 = client.GetRealTimeReportType1(param);

                    int ewt = 0;
                    try
                    {
                        ewt = Convert.ToInt32(report1.Ewt);
                    }
                    catch (Exception ex)
                    { }
                    int queue = 0;
                    try
                    {
                        queue = Convert.ToInt32(report1.Queue);
                    }
                    catch (Exception ex)
                    { }
                    int sl = 0; 
                    try
                    {
                        sl = Convert.ToInt32(report1.OldestCall);
                    }
                    catch (Exception ex)
                    { }
                    int avail = 0;
                    try
                    {
                        avail = Convert.ToInt32(report1.Avail);
                    }
                    catch (Exception ex)
                    { }

                    DashboardHistories dh = new DashboardHistories()
                    {
                        ExpectedWaitingTimeCount = ewt,
                        WaitedQueueCount = queue,
                        LongerWaitedTime = sl,
                        AvailAgenCount = avail,
                        SkillNo = Convert.ToInt32(skill),
                        ProcessingTime = DateTime.Now
                    };
                    _ctx.DashboardHistories.Add(dh);
                    _ctx.SaveChanges();

                    DateTime minutesBefore5 = DateTime.Now.AddMinutes(Convert.ToInt32(ds.MailSendingRepatInMinutes)*-1);
                    if ((ds.MaxExpectedWaitingTime < ewt || ds.MaxWaitedQueueCount < queue || ds.MaxLongerWaitTime < sl)
                        && _lastSendingMailTime < minutesBefore5)
                    {
                        MailSettings ms = _ctx.MailSettings.FirstOrDefault();
                        SendMails(ms, ds, ewt, queue, sl, skill);
                        _lastSendingMailTime = DateTime.Now;
                    }
                }
            }
        }

        private static void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker != null)
            {
                System.Threading.Thread.Sleep(20000);
                worker.RunWorkerAsync();
            }
        }

        private static void SendMails(MailSettings ms, DataAccessLayer.DashboardSettings ds, int ewt, int queue, int mlt, string skill)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                string[] mails = ds.MailReceipts.Split(';');
                for (int k = 0; k < mails.Length; k++)
                {
                    if (mails[k].ToString().Trim() != "")
                        mail.To.Add(mails[k]);
                }

                string mailContent = ds.MailFormat;

                mail.From = new MailAddress(ms.CredentalEmail, "Mail Reporter", System.Text.Encoding.UTF8);
                mail.Subject = skill + " EWT Queue Longer Time aşımı";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Dikkat! " + skill + " "+ mailContent  +"/n EWT:" + ewt + " \nQueue:" + queue + "\nOCW:" + mlt;

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                if (ms.Priority.Trim() == "3")
                    mail.Priority = MailPriority.High;
                else if (ms.Priority.Trim() == "2")
                    mail.Priority = MailPriority.Normal;
                else
                    mail.Priority = MailPriority.Low;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(ms.CredentalEmail, ms.CredentalPassword);
                client.Port = Convert.ToInt32(ms.Port);
                client.Host = ms.Host;
                client.EnableSsl = Convert.ToBoolean(ms.SSL);
                try
                {
                    client.Send(mail);
                    _ctx.Logs.Add(new Logs()
                    {
                        Result = "SkillNo: "+ skill + " EWT:" + ewt + " WQC:" + queue + " LWT:" + mlt,
                        WorkedDate = DateTime.Now
                    });
                    _ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    string errorMessage = string.Empty;
                    while (ex2 != null)
                    {
                        errorMessage += ex2.ToString();
                        ex2 = ex2.InnerException;

                        _ctx.Logs.Add(new Logs()
                        {
                            Result = errorMessage + " : " + ex2,
                            WorkedDate = DateTime.Now
                        });
                        _ctx.SaveChanges();
                    }
                }

                _ctx.SaveChanges();
            }
            catch (Exception exec)
            {
                Exception ex2 = exec;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;

                    _ctx.Logs.Add(new Logs()
                    {
                        Result = "OUTER ERROR:  " + errorMessage + " : " + ex2, // this case will be cached if raises any outer error. Will not work in normal conditions
                        WorkedDate = DateTime.Now
                    });
                    _ctx.SaveChanges();
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}