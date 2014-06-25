<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanhSach.aspx.cs" Inherits="TDKT.Report.DanhSach" %>

<%@ Register assembly="Stimulsoft.Report.WebFx" namespace="Stimulsoft.Report.WebFx" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        * {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <cc1:StiWebViewerFx ID="StiWebViewerFx1" runat="server" ShowExportToBmp="False" ShowExportToCsv="False" ShowParametersButton="False" ShowBookmarksButton="False"
            ShowExportToDbf="False" ShowExportToDif="False" ShowExportToDocument="False"
            ShowExportToExcelXml="False" ShowExportToGif="False" ShowExportToHtml="False"
            ShowExportToJpeg="False" ShowExportToMetafile="False" ShowExportToMht="False"
            ShowExportToOds="False" ShowExportToOdt="False" ShowExportToPcx="False"
            ShowExportToPng="False" ShowExportToSvg="False" ShowExportToSvgz="False"
            ShowExportToSylk="False" ShowExportToText="False" ShowExportToTiff="False"
            ShowExportToXml="False" ToolbarAlignment="Center" ShowPrintButton="True" ZoomPercent="75" ViewMode="WholeReport" Width="100%" Height="100%"/>
        
    </form>
</body>
</html>
