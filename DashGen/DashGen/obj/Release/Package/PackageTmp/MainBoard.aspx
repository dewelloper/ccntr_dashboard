<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainBoard.aspx.cs" Inherits="DashGen.MainBoard" %>

<%@ Register Assembly="DevExpress.XtraCharts.v14.2.Web, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGauges.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.State" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" TagPrefix="dx" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/MainBoard.css" rel="stylesheet" />
    <style>
        .anim {
            text-align: center;
        }
    </style>
    <script src="Scripts/jquery-2.1.3.js"></script>
    <%--    <script src="Scripts/jquery.animations-tile.js"></script>
    <script src="Scripts/jquery.animations.min.js"></script>
    <script src="Scripts/app.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#width").val($(window).width());
            $("#height").val($(window).height());

            $("#go2")
               .animate({
                   width: "80%",
                   opacity: 0.4,
                   marginLeft: "0.1in",
                   fontSize: "3em",
                   borderWidth: "1px"
               }, 500)

            $("#go2")
               .animate({
                   width: "100%",
                   opacity: 1,
                   fontSize: "3em",
                   borderWidth: "1px"
               }, 500);

            $("#legendAnim").animate({
                width: "12%",
                opacity: 0.7,
                fontSize: "1em",
                borderWidth: "1px"
            }, 1000);

            //launchIntoFullscreen(document.documentElement);
        });

        function launchIntoFullscreen(element) {
            if (element.requestFullscreen) {
                element.requestFullscreen();
            } else if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();
            } else if (element.webkitRequestFullscreen) {
                element.webkitRequestFullscreen();
            } else if (element.msRequestFullscreen) {
                element.msRequestFullscreen();
            }
        }

        var isDirty;
        function PerformCallbackCore() {
            var gauge = window["gauge"];
            isDirty = gauge.InCallback();
            if (!isDirty)
                gauge.PerformCallback();
        }
        function OnChanged() {
            PerformCallbackCore();
        }
        function OnEndCallback() {
            if (isDirty)
                window.setTimeout(PerformCallbackCore, 0);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="width" runat="server" />
        <asp:HiddenField ID="height" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="timerRetriever" OnTick="timerRetriever_Tick" runat="server" Interval="20000">
        </asp:Timer>

        <%--        <div id="go2" class="anim">
            <dxchartsui:WebChartControl ID="wcc" runat="server">
            </dxchartsui:WebChartControl>
        </div>--%>

        <div id="NW">
            <dxchartsui:WebChartControl ID="wcc" runat="server" CrosshairEnabled="True" Height="100px" Theme="Metropolis" Width="150px">
            </dxchartsui:WebChartControl>
            <div id="Div0" class="legendAnimation">
                <ul>
                    <li id="lblEWT1" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                    <li id="lblWQC1" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                    <li id="lblLWT1" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                    <li id="lblAAC1" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                </ul>
            </div>
            <div id="gauger" style="z-index: 3; position: absolute; top: -200px; float: right;">
                <dx:ASPxGaugeControl runat="server" Width="120px" Height="100px" BackColor="Transparent" ID="gaugeControl" ClientInstanceName="gauge"
                    SaveStateOnCallbacks="false" >
                    <ClientSideEvents />
                    <%--<Gauges>
                        <dx:CircularGauge ID="cGauge1" Bounds="0, 0, 290, 290">
                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent Name="bg" ScaleCenterPos="0.5, 0.822" ScaleID="scale1"
                                    Size="250, 152" ZOrder="1000" ShapeType="CircularHalf_Style13"></dx:ArcScaleBackgroundLayerComponent>
                            </backgroundlayers>
                            <labels>
                                <dx:LabelComponent AppearanceText-Font="Tahoma, 10pt, style=Bold" AppearanceText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:WhiteSmoke&quot;/&gt;"
                                    Text="HP" Name="criteria" Position="125, 100" ZOrder="-25"></dx:LabelComponent>
                                <dx:LabelComponent Text="&lt;color=Red&gt;First Car &lt;color=Green&gt;Second Car"
                                    AllowHTMLString="True" Name="circularGauge1_Label2" Position="125, 180" Size="200, 25">
                                </dx:LabelComponent>
                            </labels>
                            <needles>
                                <dx:ArcScaleNeedleComponent EndOffset="-7.5" StartOffset="-5" ScaleID="scale1" Name="needleHP2"
                                    ZOrder="-50" ShapeType="CircularFull_Style13"></dx:ArcScaleNeedleComponent>
                                <dx:ArcScaleNeedleComponent EndOffset="-7.5" Shader="&lt;ShaderObject Type=&quot;Style&quot; Data=&quot;Colors[Style1:LightGreen;Style2:]&quot;/&gt;"
                                    StartOffset="-5" ScaleID="scale2" Name="needleHP1" ZOrder="-50" ShapeType="CircularFull_Style13">
                                </dx:ArcScaleNeedleComponent>
                            </needles>
                            <scales>
                                <dx:ArcScaleComponent Name="scale1" MaxValue="450" Value="250" MinorTickmark-ShapeType="Circular_Style13_4"
                                    MinorTickmark-ShapeOffset="4" Center="125, 130" EndAngle="0" MajorTickCount="7"
                                    MinorTickCount="4" MajorTickmark-TextOffset="10" MajorTickmark-TextOrientation="LeftToRight"
                                    MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeType="Circular_Style13_5"
                                    MajorTickmark-ShapeOffset="-7" StartAngle="-180" RadiusX="105" RadiusY="105"
                                    AppearanceTickmarkText-Font="Tahoma, 6pt, style=Bold" >
                                </dx:ArcScaleComponent>
                                <dx:ArcScaleComponent Name="scale2" MaxValue="450" Value="50" MinorTickmark-ShapeType="Circular_Style13_4"
                                    MinorTickmark-ShowTick="False" MinorTickmark-ShapeOffset="4" Center="125, 130"
                                    EndAngle="0" MajorTickCount="7" MinorTickCount="4" MajorTickmark-TextOffset="10"
                                    MajorTickmark-ShowText="False" MajorTickmark-TextOrientation="LeftToRight" MajorTickmark-FormatString="{0:F0}"
                                    MajorTickmark-ShapeType="Circular_Style13_5" MajorTickmark-ShowTick="False" MajorTickmark-ShapeOffset="-7"
                                    StartAngle="-180" RadiusX="105" RadiusY="105" AppearanceTickmarkText-Font="Tahoma, 6pt, style=Bold"
                                    >
                                </dx:ArcScaleComponent>
                            </scales>
                        </dx:CircularGauge>
                    </Gauges>--%>
                </dx:ASPxGaugeControl>
            </div>
        </div>
        <div id="NE">
            <dxchartsui:WebChartControl ID="wcc3" runat="server">
            </dxchartsui:WebChartControl>
            <div id="Div1" class="legendAnimation">
                <ul>
                    <li id="lblEWT2" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                    <li id="lblWQC2" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                    <li id="lblLWT2" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                    <li id="lblAAC2" runat="server" style="width: 70px; font-family: Tahoma; font-size: 8px; height: 18px;"></li>
                </ul>
            </div>
        </div>
        <div id="SW">
            <dxchartsui:WebChartControl ID="wcc2" runat="server">
            </dxchartsui:WebChartControl>
        </div>
        <div id="SE">
            <dxchartsui:WebChartControl ID="wcc4" runat="server">
            </dxchartsui:WebChartControl>

        </div>


    </form>
</body>
</html>

