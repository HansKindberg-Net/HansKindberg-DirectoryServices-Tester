using System.Configuration;
using System.Web.Configuration;
using Autofac;
using WebFormsMvp.Autofac;
using WebFormsMvp.Binder;

namespace HansKindberg.DirectoryServices.TestApplication
{
	public static class Bootstrapper
	{
		#region Methods

		public static void Bootstrap()
		{
			ContainerBuilder containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterInstance((AuthenticationSection) ConfigurationManager.GetSection("system.web/authentication")).As<AuthenticationSection>().SingleInstance();
			containerBuilder.RegisterInstance((AuthorizationSection) ConfigurationManager.GetSection("system.web/authorization")).As<AuthorizationSection>().SingleInstance();
			containerBuilder.RegisterInstance((IdentitySection) ConfigurationManager.GetSection("system.web/identity")).As<IdentitySection>().SingleInstance();

			PresenterBinder.Factory = new AutofacPresenterFactory(containerBuilder.Build());
		}

		#endregion
	}
}