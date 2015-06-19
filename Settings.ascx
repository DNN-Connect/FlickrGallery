<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Connect.DNN.Modules.FlickrGallery.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:Label ID="lblFlickrApiKey" runat="server" controlname="txtFlickrApiKey" suffix=":" />
  <asp:TextBox ID="txtFlickrApiKey" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:label ID="lblFlickrGroupId" runat="server" controlname="txtFlickrGroupId" suffix=":" />
  <asp:TextBox ID="txtFlickrGroupId" runat="server" />
 </div>
</fieldset>

