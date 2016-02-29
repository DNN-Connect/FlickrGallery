<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Connect.DNN.Modules.FlickrGallery.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:Label ID="lblFlickrApiKey" runat="server" controlname="txtFlickrApiKey" suffix=":" />
  <asp:TextBox ID="txtFlickrApiKey" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:Label ID="lblFlickrSharedSecret" runat="server" controlname="txtFlickrSharedSecret" suffix=":" />
  <asp:TextBox ID="txtFlickrSharedSecret" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:label ID="lblFlickrGroupId" runat="server" controlname="txtFlickrGroupId" suffix=":" />
  <asp:TextBox ID="txtFlickrGroupId" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:label ID="lblFlickrUserId" runat="server" controlname="txtFlickrUserId" suffix=":" />
  <asp:TextBox ID="txtFlickrUserId" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:label ID="lblFlickrAlbumId" runat="server" controlname="txtFlickrAlbumId" suffix=":" />
  <asp:TextBox ID="txtFlickrAlbumId" runat="server" />
 </div>
 <div class="dnnFormItem">
  <dnn:label ID="lblIncludeInService" runat="server" controlname="chkIncludeInService" suffix=":" />
  <asp:CheckBox ID="chkIncludeInService" runat="server" />
 </div>
</fieldset>

