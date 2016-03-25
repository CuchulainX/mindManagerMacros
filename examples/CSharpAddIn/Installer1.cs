using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;

//TODO Change this namespace to your company's name
namespace AddIn
{
	/// <summary>
	/// Installer object used in custom build steps during installation
	/// </summary>
	[RunInstaller(true)]
	public class Installer1 : System.Configuration.Install.Installer
	{
		#region Member Data
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructors/Destructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public Installer1()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		#endregion

		#region Overrides

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Register the assembly for COM interop
		/// </summary>
		/// <param name="stateSaver">An IDictionary used to save information 
		/// needed to perform a commit, rollback, or uninstall operation.
		/// </param>
		public override void Install(IDictionary stateSaver)
		{
			base.Install (stateSaver);

			RegistrationServices regSrv = new RegistrationServices();

			System.Reflection.Assembly assembly = base.GetType().Assembly;

			AssemblyRegistrationFlags flags = AssemblyRegistrationFlags.SetCodeBase;

			regSrv.RegisterAssembly(assembly, flags);
		}

		/// <summary>
		/// Unregister the assembly
		/// </summary>
		/// <param name="savedState">An IDictionary that contains the state of 
		/// the computer after the installation was complete.</param>
		/// <exception cref="ArgumentException">The stateSaver parameter is a 
		/// null reference (<b>Nothing</b> in Visual Basic).</exception>
		/// <exception cref="Exception">
		///		An exception occurred in the BeforeInstall event handler of one
		///		of the installers in the collection. 
		///		-or-
		///		An exception occurred in the AfterInstall event handler of one
		///		of the installers in the collection.</exception>
		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall (savedState);

			RegistrationServices regSrv = new RegistrationServices();

			System.Reflection.Assembly assembly = base.GetType().Assembly;

			regSrv.UnregisterAssembly(assembly);
		}

		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
