using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Configuration;

namespace HansKindberg.DirectoryServices.TestApplication.Models
{
	public class DefaultModel
	{
		#region Properties

		public virtual AuthenticationSection AuthenticationSection { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual IDictionary<string, string> AuthenticationTypes { get; set; }

		public virtual AuthorizationSection AuthorizationSection { get; set; }
		public virtual string DirectoryEntryName { get; set; }
		public virtual string DirectoryEntryPath { get; set; }
		public virtual Exception Exception { get; set; }
		public virtual IIdentity HttpIdentity { get; set; }
		public virtual IdentitySection IdentitySection { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual IDictionary<string, string> Schemes { get; set; }

		public virtual IIdentity ThreadIdentity { get; set; }
		public virtual IIdentity WindowsIdentity { get; set; }

		#endregion
	}
}