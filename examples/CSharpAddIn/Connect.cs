//TODO Change this namespace to your company's name
//TODO In the AddIn project properties change the default namespace to your company's name
//TODO In the AddIn project properties change the Assembly name to the name of your addin.
namespace AddIn
{
	using System;
	using Extensibility;
	using System.Runtime.InteropServices;
	using System.Windows.Forms;
	using MindManager;

	#region Read me for Add-in installation and setup information.
	// When run, the Add-in wizard prepared the registry for the Add-in.
	// At a later time, if the Add-in becomes unavailable for reasons such as:
	//   1) You moved this project to a computer other than which is was originally created on.
	//   2) You chose 'Yes' when presented with a message asking if you wish to remove the Add-in.
	//   3) Registry corruption.
	// you will need to re-register the Add-in by building the MyAddin21Setup project 
	// by right clicking the project in the Solution Explorer, then choosing install.
	#endregion
	
	/// <summary>
	///   The object for implementing an Add-in.
	/// </summary>
	/// <seealso class='IDTExtensibility2' />
	/// TODO: Create a new GUID for this class using Tools...Create GUID
	/// TODO: Create a new ProgId for this class
	/// TODO: In the AddInSetup installer project, change the registry key for HKLM\Software\Mindjet\MindManager\5\AddIns\Mindjet.com.Sample.AddIn.1 to the new ProgID.
	/// TODO: In the AddInSetup installer project, change the Description in HKLM\Software\Mindjet\MindManager\5\AddIns\ProgID to a relevant value.
	/// TODO: In the AddInSetup installer project, change the FriendlyName in HKLM\Software\Mindjet\MindManager\5\AddIns\ProgID to a relevant value.
	/// TODO: In the AddInSetup installer project properties, change the Author property to the author of the add-in.
	/// TODO: In the AddInSetup installer project properties, change the Description property to the description of the add-in.
	/// TODO: In the AddInSetup installer project properties, change the Manufacturer property to the manufacturer of the add-in.
	/// TODO: In the AddInSetup installer project properties, change the Title property to the title of the add-in.
	/// TODO: In the AddInSetup installer project properties, create a new Upgrade Code by clicking on the ... in the UpgradeCode property.
 	/// TODO: In the AddInSetup installer project properties, change the version property to the version of the add-in.
	[GuidAttribute("8851764E-7D2C-48d2-9CC9-419A06E8F6D5"), ProgId("Mindjet.com.Sample.AddIn.1")]
	public class Connect : Object, Extensibility.IDTExtensibility2
	{
		#region Constructors/Destructor
		/// <summary>
		///		Implements the constructor for the Add-in object.
		///		Place your initialization code within this method.
		/// </summary>
		public Connect()
		{
			
		}
		#endregion

		#region Overrides

		/// <summary>
		///      Implements the OnConnection method of the IDTExtensibility2 interface.
		///      Receives notification that the Add-in is being loaded.
		/// </summary>
		/// <param term='application'>
		///      Root object of the host application.
		/// </param>
		/// <param term='connectMode'>
		///      Describes how the Add-in is being loaded.
		/// </param>
		/// <param term='addInInst'>
		///      Object representing this Add-in.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, Extensibility.ext_ConnectMode connectMode, object addInInst, ref System.Array custom)
		{
			try
			{
				MindManager.Application app = (MindManager.Application) application;

				//TODO: Connect the Add-in
			}
			catch(System.Exception e)
			{
				Mindjet.Utility.HandleError(e);
			}
		}


		/// <summary>
		///     Implements the OnDisconnection method of the IDTExtensibility2 interface.
		///     Receives notification that the Add-in is being unloaded.
		/// </summary>
		/// <param term='disconnectMode'>
		///      Describes how the Add-in is being unloaded.
		/// </param>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(Extensibility.ext_DisconnectMode disconnectMode, ref System.Array custom)
		{
			try
			{
				//TODO: Disconnect the add-in
			}
			catch(System.Exception e)
			{
				Mindjet.Utility.HandleError(e);
			}
		}


		/// <summary>
		///      Implements the OnAddInsUpdate method of the IDTExtensibility2 interface.
		///      Receives notification that the collection of Add-ins has changed.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnAddInsUpdate(ref System.Array custom)
		{
			try
			{
			}
			catch(System.Exception e)
			{
				Mindjet.Utility.HandleError(e);
			}
		}


		/// <summary>
		///      Implements the OnStartupComplete method of the IDTExtensibility2 interface.
		///      Receives notification that the host application has completed loading.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref System.Array custom)
		{
			try
			{
			}
			catch(System.Exception e)
			{
				Mindjet.Utility.HandleError(e);
			}
		}


		/// <summary>
		///      Implements the OnBeginShutdown method of the IDTExtensibility2 interface.
		///      Receives notification that the host application is being unloaded.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref System.Array custom)
		{
			try
			{
			}
			catch(System.Exception e)
			{
				Mindjet.Utility.HandleError(e);
			}
		}
		#endregion
	}
}