using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using HansKindberg.DirectoryServices.TestApplication.Core;
using HansKindberg.DirectoryServices.TestApplication.Models;
using HansKindberg.DirectoryServices.TestApplication.Presenters;
using HansKindberg.DirectoryServices.TestApplication.Views;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace HansKindberg.DirectoryServices.TestApplication
{
	[PresenterBinding(typeof(DefaultPresenter))]
	public partial class DefaultView : MvpPage<DefaultModel>, IDefaultView
	{
		#region Constructors

		public DefaultView()
		{
			this.AutoDataBind = false;
		}

		#endregion

		#region Events

		public event EventHandler<DirectoryArguments> DirectoryBind;

		#endregion

		#region Properties

		public virtual Control ConfirmationControl
		{
			get { return this.confirmationPlaceHolder; }
		}

		public virtual Control ExceptionControl
		{
			get { return this.exceptionPlaceHolder; }
		}

		public virtual Control FormControl
		{
			get { return this.form; }
		}

		public virtual Control InformationControl
		{
			get { return this.informationPlaceHolder; }
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnSubmit(object sender, EventArgs e)
		{
			if(this.DirectoryBind != null)
			{
				this.DirectoryBind(this, new DirectoryArguments
					{
						AuthenticationTypes = this.authenticationTypesCheckBoxList.Items.Cast<ListItem>(),
						Context = this.contextTextBox.Text,
						Host = this.hostTextBox.Text,
						Impersonate = this.impersonateCheckBox.Checked,
						Password = this.passwordTextBox.Text,
						Port = this.portTextBox.Text,
						Scheme = this.schemeDropDownList.SelectedValue,
						UserName = this.userNameTextBox.Text
					});
			}
		}

		#endregion
	}
}