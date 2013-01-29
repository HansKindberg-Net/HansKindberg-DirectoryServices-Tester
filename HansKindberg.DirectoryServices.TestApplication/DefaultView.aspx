<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultView.aspx.cs" Inherits="HansKindberg.DirectoryServices.TestApplication.DefaultView"
%><!DOCTYPE html>
<html>
	<head>
		<title>Default</title>
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<style>
			body {
				padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
			}
		</style>
		<link href="//netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css" rel="stylesheet">
		<style>
			label { font-weight: bold; }
			ul.authentication-types li { display: block; }
			ul.authentication-types label { display: inline; font-weight: normal; margin: 0 0 0 0.4em; padding: 0; }
			.button-area input { margin-top: 1em; }
			.checkbox-area input { margin: 0; padding: 0; }
			.inline { display: inline; }
			.structural { display: none; }
		</style>
		<script src="//code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
		<script src="//netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/js/bootstrap.min.js" type="text/javascript"></script>
		<!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
		<!--[if lt IE 9]>
			<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<![endif]-->
	</head>
	<body>
		<div class="navbar navbar-inverse navbar-fixed-top">
			<div class="navbar-inner">
				<div class="container">
					<a class="brand" href="#">HansKindberg-DirectoryServices-Tester</a>
				</div>
			</div>
		</div>
		<div class="container">
			<h1>DirectoryEntry.Bind() - test</h1>
			<asp:PlaceHolder id="exceptionPlaceHolder" Visible="false" runat="server">
				<div class="alert alert-error">
					<h3>Error</h3>
					<p><strong>Path: </strong><%# this.Model.DirectoryEntryPath %></p>
					<p><%# this.Model.Exception %></p>
				</div>	
			</asp:PlaceHolder>
			<asp:PlaceHolder id="confirmationPlaceHolder" Visible="false" runat="server">
				<div class="alert alert-success">
					<h3>Confirmation</h3>
					<p><strong>Path: </strong><%# this.Model.DirectoryEntryPath %></p>
					<p><strong>Name: </strong><%# this.Model.DirectoryEntryName %></p>
				</div>
			</asp:PlaceHolder>
			<div class="row-fluid">
				<div class="span8 well">
					<h2>Enter test values</h2>
					<form id="form" runat="server">
						<fieldset>
							<legend class="structural">Test values</legend>
							<div>
								<asp:Label AssociatedControlID="schemeDropDownList" runat="server">Scheme</asp:Label>
								<asp:DropDownList id="schemeDropDownList" DataSource="<%# this.Model.Schemes %>" DataTextField="Value" DataValueField="Key" runat="server" />
							</div>
							<div>
								<asp:Label AssociatedControlID="hostTextBox" runat="server">Host</asp:Label>
								<asp:TextBox id="hostTextBox" runat="server" />
							</div>
							<div>
								<asp:Label AssociatedControlID="portTextBox" runat="server">Port</asp:Label>
								<asp:TextBox id="portTextBox" runat="server" />
							</div>
							<div>
								<asp:Label AssociatedControlID="contextTextBox" runat="server">Context</asp:Label>
								<asp:TextBox id="contextTextBox" CssClass="input-xxlarge" runat="server" />
							</div>
							<div>
								<asp:Label AssociatedControlID="authenticationTypesCheckBoxList" runat="server">AuthenticationTypes</asp:Label>
								<asp:CheckBoxList id="authenticationTypesCheckBoxList"  RepeatLayout="UnorderedList" CssClass="authentication-types checkbox-area" DataSource="<%# this.Model.AuthenticationTypes %>" DataTextField="Value" DataValueField="Key" runat="server" />
							</div>
							<div>
								<asp:Label AssociatedControlID="userNameTextBox" runat="server">Username</asp:Label>
								<asp:TextBox id="userNameTextBox" runat="server" />
							</div>
							<div>
								<asp:Label AssociatedControlID="passwordTextBox" runat="server">Password</asp:Label>
								<asp:TextBox id="passwordTextBox" TextMode="Password" runat="server" />
							</div>
							<div class="checkbox-area">
								<asp:Label AssociatedControlID="impersonateCheckBox" CssClass="inline" runat="server">Impersonate</asp:Label>
								<asp:CheckBox id="impersonateCheckBox" runat="server" />
							</div>
							<div class="button-area">
								<asp:Button Text="Submit" OnClick="OnSubmit" runat="server" />
								<input type="reset" value="Reset" />
							</div>
						</fieldset>
					</form>
				</div>
				<div class="span4">
					<div class="alert alert-info">
						<h2>Information</h2>
						<asp:PlaceHolder id="informationPlaceHolder" runat="server">
							<h3>Context</h3>
							<h4>Thread-identity</h4>
							<ul>
								<li><strong>Name: </strong><%# this.Model.ThreadIdentity.Name %></li>
								<li><strong>Authentication-type: </strong><%# this.Model.ThreadIdentity.AuthenticationType %></li>
							</ul>
							<h4>Windows-identity</h4>
							<ul>
								<li><strong>Name: </strong><%# this.Model.WindowsIdentity.Name %></li>
								<li><strong>Authentication-type: </strong><%# this.Model.WindowsIdentity.AuthenticationType %></li>
							</ul>
							<h4>Http-identity</h4>
							<ul>
								<li><strong>Name: </strong><%# this.Model.HttpIdentity.Name %></li>
								<li><strong>Authentication-type: </strong><%# this.Model.HttpIdentity.AuthenticationType %></li>
							</ul>
							<h3>Configuration</h3>
							<h4>Authentication</h4>
							<ul>
								<li><strong>Mode: </strong><%# this.Model.AuthenticationSection.Mode %></li>
							</ul>
							<h4>Identity</h4>
							<ul>
								<li><strong>Impersonate: </strong><%# this.Model.IdentitySection.Impersonate %></li>
							</ul>
						</asp:PlaceHolder>
					</div>
				</div>
			</div>
		</div>
	</body>
</html>