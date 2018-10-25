using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Base;
using DevExpress.Web.ASPxGauges.Gauges.Circular;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraGauges.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace DashGen
{
    public partial class MainBoard : System.Web.UI.Page
    {
        GenpaDashboardEntities _ctx = new GenpaDashboardEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GeneralConfig gc = _ctx.GeneralConfig.FirstOrDefault();
                timerRetriever.Interval = Convert.ToInt32(gc.ScreenPassTimeInterval);
                //cGauge1.Labels["criteria"].Text = "17";
                //gaugeControl.DataBind();

                if (Session["CurrentUser"] == null)
                    Response.Redirect("Login.aspx");

                List<DataAccessLayer.DashboardSettings> settings = _ctx.DashboardSettings.ToList();
                List<SkillChain> schain = new List<SkillChain>();
                for (int k = 0; k < settings.Count-1; k=k + 2)
                {
                    schain.Add(
                    new SkillChain() { 
                        IsSkillActive = false,
                        SkillA = Convert.ToInt32(settings[k].SkillNos),
                        SkillB = Convert.ToInt32(settings[k+1].SkillNos),
                    });
                }
                if (settings.Count > (schain.Count * 2))
                {
                    schain.Add(
                                        new SkillChain()
                                        {
                                            IsSkillActive = false,
                                            SkillA = Convert.ToInt32(settings[settings.Count-1].SkillNos),
                                            SkillB = 0,
                                        });
                }

                if (schain.Count > 0)
                    schain[0].IsSkillActive = true;

                Session["SkillChain"] = schain;
                timerRetriever_Tick(null, null);
            }


            //if (!IsCallback)
            //{
            //    cGauge1.Labels["criteria"].Text = "5";
            //    gaugeControl.DataBind();
            //}
        }

        protected override void OnInit(EventArgs e)
        {
            //HtmlGenericControl css = new HtmlGenericControl();
            //css.TagName = "style";
            //css.Attributes.Add("type", "text/css");

            //string imageURL = string.Empty;

            //List<GeneralConfig> gcs = _ctx.GeneralConfig.ToList();
            //imageURL = "images/" + gcs[0].ImagesPath;
            //css.InnerHtml = @"body{background-image: url(" + imageURL + ");}";

            //Page.Header.Controls.Add(css);

            base.OnInit(e);
        }

        private static int _currSkillNo = 0;
        private static DateTime timeEndOfTheDay;
        protected void timerRetriever_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            GeneralConfig gc = _ctx.GeneralConfig.FirstOrDefault();
            TimeSpan tss = gc.TimeStartOfTheDay.Value;
            DateTime timeStartOfTheDay = new DateTime(now.Year, now.Month, now.Day, tss.Hours, tss.Minutes, tss.Seconds);
            TimeSpan tas = gc.TimeAfternoonOfTheDay.Value;
            DateTime timeAfternoonOfTheDay = new DateTime(now.Year, now.Month, now.Day, tas.Hours, tas.Minutes, tas.Seconds);
            TimeSpan tes = gc.TimeEndOfTheDay.Value;
            timeEndOfTheDay = new DateTime(now.Year, now.Month, now.Day, tes.Hours, tes.Minutes, tes.Seconds);

            List<DataAccessLayer.DashboardSettings> settings = _ctx.DashboardSettings.ToList();
            int skillPanelLeft = 0;
            int skillPanelRight = 0;

            List<SkillChain> sc = Session["SkillChain"] as List<SkillChain>;
            if (Convert.ToBoolean(gc.ScreenPassAllow))
            {
                for (int k = 0; k < sc.Count(); k++)
                    if (sc[k].IsSkillActive)
                    {
                        skillPanelLeft = sc[k].SkillA;
                        skillPanelRight = sc[k].SkillB;
                        sc[k].IsSkillActive = false;
                        try
                        {
                            sc[k + 1].IsSkillActive = true;
                        }
                        catch (Exception ex)
                        {
                            sc[0].IsSkillActive = true;
                        }
                        break;
                    }
            }
            else
            {
                skillPanelLeft = sc[0].SkillA;
                if (sc.Count > 1)
                    skillPanelRight = sc[1].SkillB;
            }

            double firstPartCounter = (timeAfternoonOfTheDay.Hour - timeStartOfTheDay.Hour) / 0.5;
            double secondPartCounter = (timeEndOfTheDay.Hour - timeAfternoonOfTheDay.Hour) / 0.5;

            FillChartBySkill(timeStartOfTheDay, skillPanelLeft, wcc, firstPartCounter, true);
            FillChartBySkill(timeAfternoonOfTheDay, skillPanelLeft, wcc2, secondPartCounter, true);
            
            FillChartBySkill(timeStartOfTheDay, skillPanelRight, wcc3, firstPartCounter, false);
            FillChartBySkill(timeAfternoonOfTheDay, skillPanelRight, wcc4, secondPartCounter, false);
        }

        private void FillChartBySkill(DateTime timeStartOfTheDay, int skillId, WebChartControl wcc, double timePartCounter, bool isLeft)
        {
            List<DashboardHistories> dhistories = _ctx.DashboardHistories.Where(p => p.ProcessingTime > timeStartOfTheDay && p.ProcessingTime < timeEndOfTheDay && p.SkillNo == skillId).OrderByDescending(k => k.Id).ToList();

            int v1 = 0;
            int v2 = 0;
            int v3 = 0;
            int v4 = 0;
            if (dhistories.Count > 0)
            {
                v1 = dhistories[0].ExpectedWaitingTimeCount;
                v2 = dhistories[0].WaitedQueueCount;
                v3 = dhistories[0].LongerWaitedTime;
                v4 = dhistories[0].AvailAgenCount;
            }
            if (isLeft)
            {
                lblEWT1.InnerText = "EWT " + v1.ToString();
                lblWQC1.InnerText = "Q " + v2.ToString();
                lblLWT1.InnerText = "LWT " + v3.ToString();
                lblAAC1.InnerText = "AAC " + v4.ToString();
            }
            else
            {
                lblEWT2.InnerText = "EWT " + v1.ToString();
                lblWQC2.InnerText = "Q " + v2.ToString();
                lblLWT2.InnerText = "LWT " + v3.ToString();
                lblAAC2.InnerText = "AAC " + v4.ToString();
            }

            wcc.DataBind();
            wcc.EnableCallBacks = false;
            wcc.EnableViewState = false;
            wcc.SaveStateOnCallbacks = false;

            wcc.Width = width.Value == "" ? 400 : (Convert.ToInt32(width.Value) / 2);
            wcc.Height = height.Value == "" ? 300 : (Convert.ToInt32(height.Value) / 2);
            wcc.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            wcc.ClearSelection();
            wcc.Series.Clear();

            Series series1 = new Series("Expected Waiting Time", ViewType.Bar);
            Series series2 = new Series("Queue", ViewType.Bar);
            Series series3 = new Series("Longer Waited Time", ViewType.Bar);
            Series series4 = new Series("Avail Agent Count", ViewType.Bar);

            series1.ArgumentScaleType = ScaleType.DateTime;
            series1.ValueScaleType = ScaleType.Numerical;
            series2.ArgumentScaleType = ScaleType.DateTime;
            series2.ValueScaleType = ScaleType.Numerical;
            series3.ArgumentScaleType = ScaleType.DateTime;
            series3.ValueScaleType = ScaleType.Numerical;
            series4.ArgumentScaleType = ScaleType.DateTime;
            series4.ValueScaleType = ScaleType.Numerical;

            wcc.Titles.Clear();
            ChartTitle chartTitle1 = new ChartTitle() { Text = "SkillNo: " + skillId.ToString() };
            wcc.Titles.Add(chartTitle1);

            DateTime halfHourTimer = timeStartOfTheDay;
            for (int i = 0; i < timePartCounter; i++)
            {
                halfHourTimer = halfHourTimer.AddMinutes(30);
                DateTime hourTimer = halfHourTimer.AddMinutes(-30);
                List<DashboardHistories> halfHistories = _ctx.DashboardHistories.Where(p => p.ProcessingTime > hourTimer && p.ProcessingTime < halfHourTimer && p.SkillNo == skillId).OrderByDescending(k => k.Id).ToList();

                int ewc = 0;
                if (halfHistories.Count > 0)
                    ewc = halfHistories.Max(k => k.ExpectedWaitingTimeCount);
                int wqc = 0;
                if (halfHistories.Count > 0)
                    wqc = halfHistories.Max(k => k.WaitedQueueCount);
                int lwt = 0;
                if (halfHistories.Count > 0)
                    lwt = halfHistories.Max(k => k.LongerWaitedTime);
                int aac = 0;
                if (halfHistories.Count > 0)
                    aac = halfHistories.Max(k => k.AvailAgenCount);

                series1.Points.Add(new SeriesPoint(halfHourTimer, ewc));
                series2.Points.Add(new SeriesPoint(halfHourTimer, wqc));
                series3.Points.Add(new SeriesPoint(halfHourTimer, lwt));
                series4.Points.Add(new SeriesPoint(halfHourTimer, aac));
            }

            wcc.Series.AddRange(new Series[] { series1, series2, series3, series4 });

            ((XYDiagram)wcc.Diagram).AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Minute;
            ((XYDiagram)wcc.Diagram).AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Minute;
            ((XYDiagram)wcc.Diagram).AxisX.DateTimeOptions.Format = DateTimeFormat.ShortTime;
            ((XYDiagram)wcc.Diagram).AxisX.GridSpacingAuto = true;
            ((XYDiagram)wcc.Diagram).AxisX.GridSpacing = 30;
            ((XYDiagram)wcc.Diagram).AxisY.Thickness = 7;

            series1.SeriesPointsSorting = SortingMode.None;
            series2.SeriesPointsSorting = SortingMode.None;
            series3.SeriesPointsSorting = SortingMode.None;
            series4.SeriesPointsSorting = SortingMode.None;
 
        }



    }
    public class Car
    {
        string nameCore;
        int idCore;
        int cylCore;
        float hpCore;
        float literCore;
        float mpgCityCore;
        float mpgHighwayCore;

        public Car(int id, string name, int cyl, float hp, float liter, float mpgCity, float mpgHighWay)
        {
            idCore = id;
            nameCore = name;
            cylCore = cyl;
            hpCore = hp;
            literCore = liter;
            mpgCityCore = mpgCity;
            mpgHighwayCore = mpgHighWay;
        }
        public int ID { get { return idCore; } }
        public string Name { get { return nameCore; } }
        public float Cyl { get { return cylCore; } }
        public float HP { get { return hpCore; } }
        public float MPG_City { get { return mpgCityCore; } }
        public float MPG_Highway { get { return mpgHighwayCore; } }
        public float Liter { get { return literCore; } }
    }
    public static class CarsExtension
    {
        public static Car GetCarByID(IEnumerable<Car> list, int id)
        {
            Car result = null;
            foreach (Car c in list)
            {
                if (c.ID == id) return c;
            }
            return result;
        }
        public static object GetCarProperty(Car car, string propName)
        {
            switch (propName)
            {
                case "Cylinders": return car.Cyl;
                case "HP": return car.HP;
                case "Liter": return car.Liter;
                case "MPG City": return car.MPG_City;
                case "MPG Highway": return car.MPG_Highway;
                default: return car.Name;
            }
        }
    }
    
    public class SkillChain
    {
        private int _skillA = 0;

        public int SkillA
        {
            get { return _skillA; }
            set { _skillA = value; }
        }

        private int _skillB = 0;

        public int SkillB
        {
            get { return _skillB; }
            set { _skillB = value; }
        }

        private bool _isSkillActive = false;

        public bool IsSkillActive
        {
            get { return _isSkillActive; }
            set { _isSkillActive = value; }
        }
    }

    
}