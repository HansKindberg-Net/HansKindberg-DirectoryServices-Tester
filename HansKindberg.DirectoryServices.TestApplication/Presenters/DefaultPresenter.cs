using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using HansKindberg.DirectoryServices.TestApplication.Core;
using HansKindberg.DirectoryServices.TestApplication.Core.Security.Principal;
using HansKindberg.DirectoryServices.TestApplication.Views;
using WebFormsMvp;

namespace HansKindberg.DirectoryServices.TestApplication.Presenters
{
	public class DefaultPresenter : Presenter<IDefaultView>
	{
		#region Fields

		private readonly AuthenticationSection _authenticationSection;
		private readonly AuthorizationSection _authorizationSection;
		private readonly IdentitySection _identitySection;

		#endregion

		#region Constructors

		public DefaultPresenter(IDefaultView view, AuthenticationSection authenticationSection, AuthorizationSection authorizationSection, IdentitySection identitySection) : base(view)
		{
			if(authenticationSection == null)
				throw new ArgumentNullException("authenticationSection");

			if(authorizationSection == null)
				throw new ArgumentNullException("authorizationSection");

			if(identitySection == null)
				throw new ArgumentNullException("identitySection");

			this._authenticationSection = authenticationSection;
			this._authorizationSection = authorizationSection;
			this._identitySection = identitySection;

			this.View.DirectoryBind += this.OnDirectoryBind;
			this.View.Load += this.OnViewLoad;
			this.View.PreRender += this.OnViewPreRender;
		}

		#endregion

		#region Methods

		protected internal virtual string BuildPath(DirectoryArguments e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			string path = string.Empty;

			if(!string.IsNullOrEmpty(e.Scheme))
				path += e.Scheme + "://";

			if(!string.IsNullOrEmpty(e.Host))
				path += e.Host;

			if(!string.IsNullOrEmpty(e.Port))
			{
				if(!string.IsNullOrEmpty(e.Host))
					path += ":";

				path += e.Port;
			}

			if(!string.IsNullOrEmpty(e.Context))
			{
				if(!string.IsNullOrEmpty(e.Host) || !string.IsNullOrEmpty(e.Port))
					path += "/";

				path += e.Context;
			}

			return path;
		}

		protected internal virtual void PopulateModel()
		{
			this.PopulateModelIdentities();
			this.PopulateModelConfigurations();

			this.View.Model.AuthenticationTypes = new Dictionary<string, string>();
			foreach(string name in Enum.GetNames(typeof(AuthenticationTypes)))
			{
				this.View.Model.AuthenticationTypes.Add(name, name);
			}

			this.View.Model.Schemes = new Dictionary<string, string>();
			foreach(string name in Enum.GetNames(typeof(Scheme)))
			{
				this.View.Model.Schemes.Add(name, name);
			}
		}

		protected internal virtual void PopulateModelConfigurations()
		{
			this.View.Model.AuthenticationSection = this._authenticationSection;
			this.View.Model.AuthorizationSection = this._authorizationSection;
			this.View.Model.IdentitySection = this._identitySection;
		}

		protected internal virtual void PopulateModelIdentities()
		{
			// ReSharper disable ConditionIsAlwaysTrueOrFalse
			if(this.HttpContext.User != null && this.HttpContext.User.Identity != null)
				this.View.Model.HttpIdentity = this.HttpContext.User.Identity;
			else
				this.View.Model.HttpIdentity = new EmptyIdentity();

			if(Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null)
				this.View.Model.ThreadIdentity = Thread.CurrentPrincipal.Identity;
			else
				this.View.Model.ThreadIdentity = new EmptyIdentity();

			WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
			if(windowsIdentity != null)
				this.View.Model.WindowsIdentity = windowsIdentity;
			else
				this.View.Model.WindowsIdentity = new EmptyIdentity();
			// ReSharper restore ConditionIsAlwaysTrueOrFalse
		}

		protected internal virtual AuthenticationTypes? ResolveAuthenticationTypes(IEnumerable<ListItem> listItems)
		{
			string authenticationTypesString = string.Empty;

			foreach(ListItem listItem in (listItems ?? new ListItem[0]).Where(listItem => listItem.Selected))
			{
				if(!string.IsNullOrEmpty(authenticationTypesString))
					authenticationTypesString += ", ";

				authenticationTypesString += listItem.Value;
			}

			AuthenticationTypes? authenticationTypes = null;

			if(!string.IsNullOrEmpty(authenticationTypesString))
			{
				AuthenticationTypes parsedAuthenticationTypes;
				if(!Enum.TryParse(authenticationTypesString, true, out parsedAuthenticationTypes))
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The value \"{0}\" is not parseable to \"{1}\".", authenticationTypesString, typeof(AuthenticationTypes)));

				authenticationTypes = parsedAuthenticationTypes;
			}

			return authenticationTypes;
		}

		protected internal virtual void ResolveControls()
		{
			this.View.InformationControl.DataBind();

			if(this.View.IsPostBack)
			{
				if(this.View.Model.Exception == null)
				{
					this.View.ConfirmationControl.Visible = true;
					this.View.ExceptionControl.Visible = false;
					this.View.ConfirmationControl.DataBind();
				}
				else
				{
					this.View.ConfirmationControl.Visible = false;
					this.View.ExceptionControl.Visible = true;
					this.View.ExceptionControl.DataBind();
				}
			}
			else
			{
				this.View.FormControl.DataBind();
			}
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected internal virtual void OnDirectoryBind(object sender, DirectoryArguments e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			string path = this.BuildPath(e);
			this.View.Model.DirectoryEntryPath = path;

			try
			{
				AuthenticationTypes? authenticationTypes = this.ResolveAuthenticationTypes(e.AuthenticationTypes);

				if(e.Impersonate)
				{
					using(WindowsImpersonationContext windowsImpersonationContext = ((WindowsIdentity) this.HttpContext.User.Identity).Impersonate())
					{
						this.View.Model.DirectoryEntryName = this.TryGetDirectoryEntryName(path, authenticationTypes, e.UserName, e.Password);
					}
				}
				else
				{
					this.View.Model.DirectoryEntryName = this.TryGetDirectoryEntryName(path, authenticationTypes, e.UserName, e.Password);
				}
			}
			catch(Exception exception)
			{
				this.View.Model.Exception = exception;
			}
		}

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.PopulateModel();
		}

		protected internal virtual void OnViewPreRender(object sender, EventArgs e)
		{
			this.ResolveControls();
		}

		protected internal virtual string TryGetDirectoryEntryName(string path, AuthenticationTypes? authenticationTypes, string userName, string password)
		{
			string directoryEntryName;

			using(DirectoryEntry directoryEntry = new DirectoryEntry(path))
			{
				if(authenticationTypes.HasValue)
					directoryEntry.AuthenticationType = authenticationTypes.Value;

				if(!string.IsNullOrEmpty(userName))
					directoryEntry.Username = userName;

				if(!string.IsNullOrEmpty(password))
					directoryEntry.Password = password;

				directoryEntryName = directoryEntry.Name; // This will invoke DirectoryEntry.Bind()
			}

			return directoryEntryName;
		}

		#endregion
	}
}