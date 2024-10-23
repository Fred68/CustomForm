namespace NcForms
{


	/// <summary>
	/// NcWindowsStyles flags
	/// </summary>
	[Flags]
	public enum NcWindowsStyles
	{
		None		= 0,
		Menu		= 1 << 0,
		MinMax		= 1 << 1,
		Help		= 1 << 2,
		Resizable	= 1 << 3,
		TopMost		= 1 << 4,
		All			= -1
	}


	/// <summary>
	/// NcWindowsState enumeration
	/// </summary>
	public enum NcFormWindowStates
	{
		/// <summary>
		///  A default sized window.
		/// </summary>
		Normal = FormWindowState.Normal,
		/// <summary>
		///  A minimized window.
		/// </summary>
		Minimized = FormWindowState.Minimized,
		/// <summary>
		///  A maximized window.
		/// </summary>
		Maximized = FormWindowState.Maximized,
		/// <summary>
		/// Title bar only window.
		/// </summary>
		BarOnly = 3
	}

	/// <summary>
	/// NcFormStyle class
	/// with static members
	/// </summary>
	public class NcFormStyle
	{
		public NcWindowsStyles		ncWindowsStyle;
		public NcFormWindowStates	ncFormWindowState;

		public NcFormStyle(	NcWindowsStyles ncwStyle, NcFormWindowStates ncfwState/*, Color bkgnd, Color title, Color status, float opac*/)
		{
			ncWindowsStyle		= ncwStyle;
			ncFormWindowState	= ncfwState;
		}

		/// <summary>
		/// Normal with everything
		/// </summary>
		public static NcFormStyle Normal = new NcFormStyle(NcWindowsStyles.All, NcFormWindowStates.Normal/*, Color.White, Color.White, Color.White,0.5f*/);
		public static NcFormStyle Simple = new NcFormStyle(NcWindowsStyles.Menu | NcWindowsStyles.MinMax | NcWindowsStyles.Resizable, NcFormWindowStates.Normal/*, Color.White, Color.White, Color.White,0.9f*/);
		public static NcFormStyle Fixed = new NcFormStyle(NcWindowsStyles.Help | NcWindowsStyles.TopMost, NcFormWindowStates.Normal/*, Color.White, Color.White, Color.White,0.7f*/);
		
	}


	public class NcFormColor
	{
		public Color				backgroundColor;
		public Color				titleBarColor;
		public Color				statusBarColor;
		public float				opacity;

		public NcFormColor(	Color bkgnd, Color title, Color status, float opac)
		{
			backgroundColor		= bkgnd;
			titleBarColor		= title;
			statusBarColor		= status;
			opacity				= opac;
		}

		/// <summary>
		/// Normal with everything
		/// </summary>
		public static NcFormColor Normal = new NcFormColor(Color.White, Color.White, Color.White, 0.9f);
		public static NcFormColor Simple = new NcFormColor(Color.White, Color.White, Color.White, 0.9f);
		public static NcFormColor Fixed = new NcFormColor(Color.White, Color.White, Color.White, 0.5f);
		
	}


}