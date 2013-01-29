using System;
using System.Web.UI;
using HansKindberg.DirectoryServices.TestApplication.Core;
using HansKindberg.DirectoryServices.TestApplication.Models;
using WebFormsMvp;

namespace HansKindberg.DirectoryServices.TestApplication.Views
{
	public interface IDefaultView : IView<DefaultModel>
	{
		#region Events

		event EventHandler<DirectoryArguments> DirectoryBind;
		event EventHandler PreRender;

		#endregion

		#region Properties

		Control ConfirmationControl { get; }
		Control ExceptionControl { get; }
		Control FormControl { get; }
		Control InformationControl { get; }
		bool IsPostBack { get; }

		#endregion
	}
}