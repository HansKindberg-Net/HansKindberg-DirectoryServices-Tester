using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace HansKindberg.DirectoryServices.TestApplication.Core
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class DirectoryArguments : EventArgs
	{
		#region Properties

		public virtual IEnumerable<ListItem> AuthenticationTypes { get; set; }
		public virtual string Context { get; set; }
		public virtual string Host { get; set; }
		public virtual bool Impersonate { get; set; }
		public virtual string Password { get; set; }
		public virtual string Port { get; set; }
		public virtual string Scheme { get; set; }
		public virtual string UserName { get; set; }

		#endregion
	}
}