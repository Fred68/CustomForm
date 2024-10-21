using NcForms;

namespace WinFormsApp1
{
	public partial class Form1: NcForms.NcForm
	{
		public Form1(NcFormStyle st) : base(st)
		{
			InitializeComponent();
			SetupControls(this);
		}
	}
}
