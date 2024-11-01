using NcForms;

namespace CustomForm
{
    internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1(NcFormStyle.Normal,new NcFormColor(Color.GreenYellow, Color.Aquamarine, Color.Aquamarine,0.7f)));
		}
	}
}