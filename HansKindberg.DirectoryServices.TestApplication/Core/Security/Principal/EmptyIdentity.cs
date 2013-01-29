using System.Security.Principal;

namespace HansKindberg.DirectoryServices.TestApplication.Core.Security.Principal
{
	public class EmptyIdentity : IIdentity
	{
		#region Properties

		public virtual string AuthenticationType
		{
			get { return "None"; }
		}

		public virtual bool IsAuthenticated
		{
			get { return false; }
		}

		public virtual string Name
		{
			get { return string.Empty; }
		}

		#endregion
	}
}