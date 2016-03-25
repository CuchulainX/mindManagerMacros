//------------------------------------------------------------------------------
//     Copyright 2005 Mindjet LLC, Larkspur, CA USA. All rights reserved.
//
//     This software is furnished under a license  and may be  used and  copied
//     only in  accordance of the  terms of such license and with the inclusion
//     of the above copyright note.   This software or any other copies thereof
//     may not be provided or otherwise made available to any other person.  No
//     title to and ownership of the software is hereby transferred.
//
//     The information in this software is subject to change without notice and
//     should not be construed as a commitment by Mindjet LLC.
//------------------------------------------------------------------------------

using System;

namespace Mindjet
{
	/// <summary>
	/// Utility Class
	/// </summary>
	public class Utility
	{
		#region Constant Data
		#endregion

		#region Member Data
		#endregion

		#region Constructors/Destructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Utility()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region Properties
		/// <summary>
		/// the Assembly Title
		/// </summary>
		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = typeof(Utility).Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), true);
	
				System.Reflection.AssemblyTitleAttribute attribute = (System.Reflection.AssemblyTitleAttribute) attributes[0];

				return attribute.Title;
			}
		}

		#endregion

		#region Methods
		/// <summary>
		/// Show a message box with the error details.
		/// </summary>
		/// <param name="se">an exception</param>
		public static void HandleError(System.Exception se)
		{
			string text = string.Format("{0}\n{1}", se.Message, se.StackTrace);

			System.Windows.Forms.MessageBox.Show(null, text, AssemblyTitle, 
				System.Windows.Forms.MessageBoxButtons.OK, 
				System.Windows.Forms.MessageBoxIcon.Error);
		}

		/// <summary>
		/// Release A COM object
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool Release(object o)
		{
			if (o != null)
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject(o);

				o = null;

				return true;
			}

			return false;
		}
		#endregion

		#region Implementation
		#endregion
	}
}
