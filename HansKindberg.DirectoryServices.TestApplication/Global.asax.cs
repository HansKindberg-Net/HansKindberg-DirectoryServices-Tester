using System;

namespace HansKindberg.DirectoryServices.TestApplication
{
	public class Global : System.Web.HttpApplication
	{
		#region Methods

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {}
		protected void Application_BeginRequest(object sender, EventArgs e) {}
		protected void Application_End(object sender, EventArgs e) {}
		protected void Application_Error(object sender, EventArgs e) {}

		protected void Application_Start(object sender, EventArgs e)
		{
			Bootstrapper.Bootstrap();
		}

		protected void Session_End(object sender, EventArgs e) {}
		protected void Session_Start(object sender, EventArgs e) {}

		#endregion
	}
}